using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KeyGenerator
{
    public class Data
    {
        public const int TEAM_MIN = 6;
        public const int TEAM_MAX = 14;
        //public static List<Partnership> partnership = new List<Partnership>();
        public static KeyGenerator caller;
        public static List<String> notification = new List<String>();
        public static int[] field = new int[2];
        public static Tuple<int, bool>[] prio;
        public static int runtime;
        public static KeyMapper km;
        public static int currentConflicts;
        public static TextFile log;

        /* Outdated from Season 2025/26 on
        public static string[] ageGroups = {
            "Herren", "Damen",
            "Jungen 19", "Jungen 15", "Jungen 13", "Maedchen 19", "Maedchen 15", "Maedchen 13",
            "Senioren 40", "Senioren 50", "Senioren 60", "Senioren 70",
            "Seniorinnen 40", "Seniorinnen 50", "Seniorinnen 60", "Seniorinnen 70"
        };
        */

        public static string[] ageGroups = {
            "Erwachsene", "Damen",
            "Jugend 19", "Jugend 15", "Jugend 13", "Maedchen 19", "Maedchen 15", "Maedchen 13",
            "Senioren 40", "Senioren 50", "Senioren 60", "Senioren 70",
            "Seniorinnen 40", "Seniorinnen 50", "Seniorinnen 60", "Seniorinnen 70"
        };

        public static string[] weekdays = {
            "Mo", "Di","Mi","Do","Fr","Sa","So"
        };

        public static void copy(Group[] oldGroup, Group[] newGroup, Club[] oldClub, Club[] newClub)
        {
            for (int i = 0; i < oldClub.Length && oldClub[i] != null; i++)
                newClub[i] = oldClub[i].clone();

            for (int i = 0; i < oldGroup.Length && oldGroup[i] != null; i++)
            {
                newGroup[i] = oldGroup[i].clone();
                for (int j = 0; j < oldGroup[i].team.Length && oldGroup[i].team[j] != null; j++)
                {
                    newGroup[i].team[j] = oldGroup[i].team[j].clone();
                    if (oldGroup[i].team[j].club.index == -1)
                        newGroup[i].team[j].club = oldGroup[i].team[j].club.clone();
                    else
                    {
                        newGroup[i].team[j].club = newClub[oldGroup[i].team[j].club.index];
                    }
                }
            }
            allocateTeams(newClub, newGroup);
        }

        public static void setOptions(Group[] group)
        {
            foreach (Group l in group)
                foreach (Team t in l.team)
                    if (t == null)
                        continue;
                    else if (t.key != 0)
                    {
                        for (int k = 0; k < TEAM_MAX; k++)
                            t.option[k] = false;
                        t.option[t.key - 1] = true;
                    }
                    else
                    {
                        for (int k = 0; k < l.field; k++)
                        {
                            if (t.keyOK(k + 1))
                                t.option[k] = true;
                            else
                                t.option[k] = false;
                            foreach (Team t2 in l.team)
                                if (t2 == null)
                                    continue;
                                else if (t2.key == k + 1)
                                    t.option[k] = false;
                        }
                        for (int k = l.field; k < TEAM_MAX; k++)
                            t.option[k] = false;
                    }
        }

        public static void setWeeks(Group[] group)
        {
            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    if (group[i].team[j].key != 0)
                        group[i].team[j].week = '-';
        }

        public static void save(Group[] l, Club[] c, TextFile clubs, TextFile groups, TextFile relations, string weeks = "ABXY")
        {
            String help;
            int line = 0;
            relations.WriteFile("", notification);
            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nrOfTeams; j++)
                {
                    line++;
                    help = "";
                    help += i + ";" + j + ";" + l[i].team[j].week + ";";
                    for (int k = 0; k < l[i].team[j].day.Length; k++)
                        help += l[i].team[j].day[k] + ";";
                    help += "\n";
                    relations.Append(help, notification);
                }
            clubs.WriteFile("", notification);
            for (int i = 0; i < c.Length; i++)
            {
                help = c[i].name + ";";
                foreach (char week in weeks.ToCharArray())
                    help += c[i].keys[week] + ";";
                if (c[i].capacity)
                    help += "X;";
                else
                    help += ";";
                foreach (Partnership pt in c[i].partnerships)
                    if (pt.indexA == c[i].index) // Sollte immer der Fall sein!
                        help += pt.weekA + ";" + pt.indexB + ";" + pt.weekB + ";";
                help += "\n";
                clubs.Append(help, notification);
            }
            groups.WriteFile("", notification);
            String content = "";
            for (int i = 0; i < l.Length; i++)
                content += l[i].name + " [" + l[i].field + "];";
            content += "\n";
            groups.Append(content, notification);
            for (int i = 0; i < TEAM_MAX; i++)
            {
                content = "";
                for (int j = 0; j < l.Length; j++)
                    if (l[j].field <= i)
                        content += ";";
                    else if (l[j].team[i] != null && l[j].team[i].key != 0)
                        content += l[j].team[i].name + " [" + l[j].team[i].key + "];";
                    else if (l[j].team[i] != null && l[j].team[i].key == 0)
                        content += l[j].team[i].name + ";";
                    else
                        content += ";";
                content += "\n";
                groups.Append(content, notification);
            }
        }

        public static void prepare(Group[] group, Club[] club, List<String> notification)
        {
            save(group, club, Club.backup, Group.backup, Team.backup);
            setOptions(group);
            setWeeks(group);
            copyKeys(group, club);
            createPriority(club);
            checkPlausibility(group, club, notification);
            checkFatal(group, notification);
        }

        public static void copyKeys(Group[] group, Club[] club)
        {
            foreach (Club c in club)
            {
                foreach (Team team in c.team)
                    if (team == null)
                        continue;
                    else if (team.key == 0)
                        if (team.week == 'A' || team.week == 'B')
                        {
                            if (c.keys[team.week] > 0)
                                team.key = km.getParallel(field[0], team.group.field, c.keys[team.week]).First();
                        }
                        else if (team.week == 'X' || team.week == 'Y')
                        {
                            if (c.keys[team.week] > 0)
                                team.key = km.getParallel(field[1], team.group.field, c.keys[team.week]).First();
                        }
                        else
                            team.key = 0;
            }
        }

        public static void createPriority(Club[] club)
        {
            prio = new Tuple<int, bool>[club.Length * 2];
            for (int i = 0; i < club.Length; i++)
                club[i].setPrio();
            int counter = 0;
            int max_prio;

            max_prio = 0;
            for (int i = 0; i < club.Length; i++)
                for (int p = 0; p < 2; p++)
                    if (club[i].prio[p] > max_prio)
                        max_prio = club[i].prio[p];
            for (int j = max_prio; j >= 0; j--)
                for (int i = 0; i < club.Length; i++)
                    for (int p = 0; p < 2; p++)
                        if (club[i].prio[p] == j)
                            prio[counter++] = Tuple.Create(club[i].index, p == 0);
        }

        public static void allocateTeams(Club[] v, Group[] l)
        {
            if (v == null || l == null)
                return;

            for (int i = 0; i < v.Length; i++)
            {
                v[i].team = new List<Team>();
                for (int j = 0; j < l.Length; j++)
                    for (int k = 0; k < l[j].nrOfTeams; k++)
                        if (l[j].team[k].club.index == v[i].index)
                            v[i].team.Add(l[j].team[k]);
            }
        }

        

        public static bool isTeamOfClub(Team t, Club c)
        {
            string teamName = t.name;
            string clubName = c.name;
            t.team = "";
            try
            {
                string[] token = teamName.Split(' ');
                string suffix = token[token.Length - 1];

                if (Util.isRomanNumber(suffix))
                {
                    t.team = suffix;
                    teamName = teamName.Remove(teamName.Length - suffix.Length - 1);
                }

                return teamName.Equals(clubName);
            }
            catch (Exception e)
            {
                notification.Append(e.ToString());
                return false;
            }
        }

        public static void checkPlausibility(Group[] groups, Club[] clubs, List<string> m)
        {
            bool plausible;
            foreach (Group group in groups)
                foreach (Team team in group.team)
                {
                    if (team == null)
                        continue;
                    plausible = false;
                    if (team.key == 0)
                        for (int k = 0; k < group.field; k++)
                            plausible |= team.keyOK(k + 1);
                    else
                        plausible = team.keyOK(team.key);
                    if (!plausible)
                        m.Add("Der Spielplan für " + team.name + " in der " + group.name + " ist inkonsistent!");
                }
            if (m.Count > 0)
                return;
            foreach (Club club in clubs)
                for (int j = 0; j < club.team.Count; j++)
                    if (club.team[j].week != '-' && club.team[j].hasAdditional())
                        for (int k = j + 1; k < club.team.Count; k++)
                            if (club.team[k].week != '-' && club.team[k].hasAdditional())
                                if (club.team[j].week == club.team[k].week)
                                {
                                    for (int l = 0; l < TEAM_MAX; l++)
                                        if (club.team[j].day[l] != club.team[k].day[l])
                                            m.Add("Spielplan und Spielwochen für den Verein " + club.name + " sind inkonsitstent!");
                                }
                                else if (club.team[j].week == 'A' && club.team[k].week == 'B'
                                      || club.team[j].week == 'B' && club.team[k].week == 'A'
                                      || club.team[j].week == 'X' && club.team[k].week == 'Y'
                                      || club.team[j].week == 'Y' && club.team[k].week == 'X')
                                    for (int l = 0; l < TEAM_MAX; l++)
                                        if (club.team[j].day[l] == club.team[k].day[l] && club.team[j].day[l] != '-')
                                            m.Add("Spielplan und Spielwochen für den Verein " + club.name + " sind inkonsitstent!");
        }

        public static void checkFatal(Group[] l, List<string> notification)
        {
            Tuple<Group, Team, int> fatal = checkFatal(l, new int[] { 0, -1 }, null);

            if (fatal == null)
                return;
            if (fatal.Item1 == null)
                notification.Add("Unzulässiger Rückgabewert (bitte überprüfen)!");
            else if (fatal.Item3 == 0)
                notification.Add("Es gab insgesamt zu viele Konflikte!");
            else if (fatal.Item2 == null)
                notification.Add("Unlösbarer Konflikt in der " + fatal.Item1.name + " für die Schlüsselzahl " + fatal.Item3 + "!");
            else
                notification.Add("In der " + fatal.Item1.name + " widerspricht die für " + fatal.Item2.name + " vergebene Zahl "
                    + fatal.Item3 + " den Vorgaben für Heim- und Auswärtsspieltage!");
        }

        public static Tuple<Group, Team, int> checkFatal(Group[] group, int[] conflicts, Club currentClub)
        {
            int[] allocation;
            List<Group> relevantGroups = new List<Group>();

            if (currentClub == null)
                relevantGroups = group.ToList();
            else
                foreach (Team tc in currentClub.team)
                    relevantGroups.Add(tc.group);

            foreach (Group g in relevantGroups)
            {
                allocation = g.getAllocation();

                foreach (Team t in g.team)
                {
                    if (t != null && !t.keyOK(t.key))
                        return Tuple.Create(g, t, t.key); //Zahl widerspricht den vorgegebenen Spieltagen
                    if (t != null && t.key != 0 && t.club.capacity)
                        foreach (Team t2 in g.team)
                            if (t2 != null && t2.club.capacity)
                                if (t.key == t2.key && t.club != t2.club)
                                    return Tuple.Create(g, (Team)null, t.key); // Zahl zu oft vergeben
                }


                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < g.field; j++)
                    if (allocation[j] > 1)
                    {
                        if (km.getSimilar(g.field, j + 1).Item1 != 0 && allocation[km.getSimilar(g.field, j + 1).Item1 - 1] == 0)
                        {
                            allocation[km.getSimilar(g.field, j + 1).Item1 - 1]++;
                            allocation[j]--;
                        }
                        if (allocation[j] > 1 && km.getSimilar(g.field, j + 1).Item2 != 0 && allocation[km.getSimilar(g.field, j + 1).Item2 - 1] == 0)
                        {
                            allocation[km.getSimilar(g.field, j + 1).Item2 - 1]++;
                            allocation[j]--;
                        }
                        if (allocation[j] > 1)
                            return Tuple.Create(g, (Team)null, j + 1); // Zahl j+1 zu oft vergeben
                    }
            }

            conflicts[0] = 0;
            foreach (Group g in group)
                conflicts[0] += g.nrOfConflicts;

            if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                return Tuple.Create((Group)null, (Team)null, 0); // Zu viele Konflikte
            return null;
        }

        public static string concatenate(List<int> wishes)
        {
            if (wishes.Count == 0)
                return "";
            string retString = wishes.First().ToString();
            for (int i = 1; i < wishes.Count; i++)
                retString += "," + wishes.ElementAt(i).ToString();
            return retString;
        }
    }
}