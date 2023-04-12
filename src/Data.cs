using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public class Data
    {
        public static int TEAM_MIN = 6;
        public static int TEAM_MAX = 14;
        public static string path;
        public static League[] league;
        public static Club[] club;
        public static List<Partnership> partnership = new List<Partnership>();
        public static Schluesselzahlen caller;
        public static TextFile schedule;
        public static List<String> notification = new List<String>();
        public static char[, ,] schedule_1;
        public static int[] field = new int[2];
        public static int[] prio;
        public static int runtime;
        public static Hashtable ht = new Hashtable();
        public static int[] rand = { 0, 0 };
        public static KeyMapper km;

        public static string[] ageGroups = {
            "Herren", "Damen", "Jungen 19", "Jungen 15", "Jungen 13", "Mädchen 19", "Mädchen 15", "Mädchen 13"
        };

        public static string[] weekdays = {
            "Mo", "Di","Mi","Do","Fr","Sa","So"
        };

        public static int resetKeys(int l, int t, ComboBox c, bool isNew)
        {
            bool[] selectable = new bool[TEAM_MAX];
            int n = league[l].nrOfTeams;
            int r = -1;

            if (isNew)
                n++;
            for (int j = 0; j < TEAM_MAX; j++)
                selectable[j] = true;
            for (int j = 0; j < league[l].nrOfTeams; j++)
                if ((league[l].team[j].key + 1) / 2 > (league[l].field) / 2)
                    league[l].team[j].key = 0;
                else if (league[l].team[j].key > 0 && j != t)
                    selectable[league[l].team[j].key - 1] = false;
            c.Items.Clear();

            for (int j = 0; n > j; j += 2)
            {
                if (selectable[j])
                    c.Items.Add(j + 1);
                if (t != league[l].nrOfTeams)
                    if (j == league[l].team[t].key - 1)
                        r = c.Items.Count - 1;
                if (selectable[j + 1])
                    c.Items.Add(j + 2);
                if (t != league[l].nrOfTeams)
                    if (j == league[l].team[t].key - 2)
                        r = c.Items.Count - 1;
            }
            return r;
        }

        public static void copy(League[] oldLeague, League[] newLeague, Club[] oldClub, Club[] newClub, List<Partnership> oldPartnership, List<Partnership> newPartnership)
        {
            for (int i = 0; i < oldClub.Length && oldClub[i] != null; i++)
                newClub[i] = oldClub[i].clone();
                
            newPartnership = new List<Partnership>();
            foreach (Partnership p in oldPartnership)
                newPartnership.Add(new Partnership(newClub[p.clubA.index], p.weekA, newClub[p.clubB.index], p.weekB));

            for (int i = 0; i < oldLeague.Length && oldLeague[i] != null; i++)
            {
                newLeague[i] = oldLeague[i].clone();
                for (int j = 0; j < oldLeague[i].team.Length && oldLeague[i].team[j] != null; j++)
                {
                    newLeague[i].team[j] = oldLeague[i].team[j].clone();
                    if (oldLeague[i].team[j].club.index == -1)
                        newLeague[i].team[j].club = oldLeague[i].team[j].club.clone();
                    else
                    {
                        newLeague[i].team[j].club = newClub[oldLeague[i].team[j].club.index];

                        /*for (int k = 0; k < newClub[oldLeague[i].team[j].club.index].team.Count; k++)
                            if (newClub[oldLeague[i].team[j].club.index].team[k] == null)
                            {
                                newClub[oldLeague[i].team[j].club.index].team[k] = newLeague[i].team[j];
                                break;
                            }*/
                    }
                }
            }
            allocateTeams(newClub, newLeague);
        }

        public static void setOptions()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nrOfTeams; j++)
                    if (league[i].team[j].key != 0)
                    {
                        for (int k = 0; k < TEAM_MAX; k++)
                            league[i].team[j].option[k] = false;
                        league[i].team[j].option[league[i].team[j].key - 1] = true;
                    }
                    else
                    {
                        for (int k = 0; k < league[i].field; k++)
                        {
                            if (league[i].team[j].keyOK(k + 1))
                                league[i].team[j].option[k] = true;
                            else
                                league[i].team[j].option[k] = false;
                            for (int l = 0; l < league[i].nrOfTeams; l++)
                                if (league[i].team[l].key == k + 1)
                                    league[i].team[j].option[k] = false;
                        }
                        for (int k = league[i].field; k < TEAM_MAX; k++)
                            league[i].team[j].option[k] = false;
                    }
        }

        public static void setWeeks()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nrOfTeams; j++)
                    if (league[i].team[j].key != 0)
                        league[i].team[j].week = '-';
        }

        public static Club[] getClubs(TextFile c, List<Partnership> p)
        {
            int nrOfClubs;
            String content = c.ReadLine(1, false, notification).Replace("\r", "");
            String[] help;
            for (nrOfClubs=0;!content.Equals(""); nrOfClubs++)
                content = c.ReadLine(2 + nrOfClubs, false, notification).Replace("\r", "");
            Club[] v = new Club[nrOfClubs];
            p.Clear();
            for (int i=0;i<nrOfClubs;i++)
            {
                content = c.ReadLine(1 + i, false, notification).Replace("\r", "");
                content = Util.clear(content);
                help = content.Split(';');
                v[i] = new Club {
                    name = help[0],
                    index = i,
                    team = new List<Team>()
                };
                try
                {
                    v[i].a = Util.toInt(help[1]);
                    v[i].b = Util.toInt(help[2]);
                    v[i].x = Util.toInt(help[3]);
                    v[i].y = Util.toInt(help[4]);
                    v[i].capacity = help[5] == "X";
                    for (int j = 6; j < help.Length - 2; j += 3)
                        p.Add(new Partnership(v[i], help[j].ToCharArray()[0], v[Util.toInt(help[j + 1])], help[j + 2].ToCharArray()[0]));
                }
                catch (Exception e) {
                    notification.Append(e.ToString());
                }
            }
            return v;
        }

        public static int nrOfKeys(League[] l)
        {
            int keys = 0;
            int teams = 0;

            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nrOfTeams; j++)
                {
                    if (l[i].team[j].key != 0)
                        keys++;
                    teams++;
                }
            return keys;
        }

        public static int run()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nrOfTeams; j++)
                    {
                        if (league[i].team[j].key == 0)
                            league[i].team[j].getKey();
                        if (league[i].team[j].key != 0)
                            league[i].removeOptions(league[i].team[j].key);
                        league[i].checkOneOption();
                    }
            return nrOfKeys(league);
        }

        public static bool isComplete()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nrOfTeams; j++)
                    if (league[i].team[j].key == 0)
                        return false;
            return true;
        }

        public static bool hasError()
        {
            bool help;
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nrOfTeams; j++)
                {
                    help = false;
                    for (int k = 0; k < TEAM_MAX; k++)
                        help = help || league[i].team[j].option[k];
                    if (!help)
                    {
                        notification.Add("Keine moegliche Schluesselzahl fuer " + league[i].team[j].name + " (" + league[i].name + ") gefunden!");
                        return true;
                    }
                }
            return false;
        }

        public static void save(League[] l, Club[] c, List<Partnership> p, TextFile clubs, TextFile groups, TextFile relations)
        {
            String help;
            int line = 0;
            relations.WriteFile("", notification);
            for (int i=0; i<l.Length; i++)
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
                help = c[i].name + ";" + c[i].a + ";" + c[i].b + ";" + c[i].x + ";" + c[i].y + ";";
                if (c[i].capacity)
                    help += "X;";
                else
                    help += ";";
                foreach (Partnership pt in p)
                    if (pt.clubA == c[i])
                        help += pt.weekA + ";" + pt.clubB.index + ";" + pt.weekB + ";";
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
                    if (l[j].team[i] != null && l[j].team[i].key != 0)
                        content += l[j].team[i].name + " [" + l[j].team[i].key + "];";
                    else if (l[j].team[i] != null && l[j].team[i].key == 0)
                        content += l[j].team[i].name + ";";
                    else
                        content += ";";
                content += "\n";
                groups.Append(content, notification);
            }
        }

        public static void getRelations(League[] l, TextFile relations)
        {
            String content;
            String[] help;
            String[] row;
            content = relations.ReadFile(false, notification);

            if (content.Equals(""))
                return;
            row = content.Split('\n');
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = row[i].Replace("\r","");
                help = row[i].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    Team t = l[Util.toInt(help[0])].team[Util.toInt(help[1])];
                    t.week = help[2].ToCharArray()[0];
                    for (int j = 3; j < help.Length && j < 16; j++)
                        t.day[j - 3] = help[j].ToCharArray()[0];
                }
                catch (Exception e) { 
                    notification.Append(e.ToString());
                }
            }
        }

        public static void getSchedule(String path)
        {
            String content;
            String[] help;
            String[] row;
            TextFile schedule = new TextFile(path + @"\Spielplan.csv");
            content = schedule.ReadFile(false, notification);

            if (content.Equals(""))
                return;
            row = content.Split('\n');
            schedule_1 = new char[TEAM_MAX, TEAM_MAX, TEAM_MAX];
            for (int i = 0; i < row.Length; i++)
            {
                help = row[i].Split(';');
                for (int j=2; j<help.Length; j++)
                    try
                    {
                        schedule_1[Util.toInt(help[0]) - 1, Util.toInt(help[1]) - 1, j - 2] = help[j].ToCharArray()[0];
                    }
                    catch (Exception e)
                    {
                        notification.Append(e.ToString());
                    }
            }
        }

        public static void solve()
        {
            int oldConflicts = 0;
            int newConflicts = run();
            while (oldConflicts != newConflicts)
            {
                oldConflicts = newConflicts;
                newConflicts = run();
                if (isComplete())
                    return;
            }
        }

        public static void generateKeys()
        {
            setOptions();
            solve();
            bool fertig = Data.isComplete();
            while (!fertig)
                for (int i = 0; i < league.Length; i++)
                    for (int j = 0; j < league[i].nrOfTeams; j++)
                        for (int k = 0; k < league[i].field; k++)
                            if (league[i].team[j].option[k] && !fertig)
                            {
                                league[i].team[j].key = k + 1;
                                for (int l = 0; l < league[i].field; l++)
                                    if (l != k)
                                        league[i].team[j].option[l] = false;
                                for (int l = 0; l < league[i].nrOfTeams; l++)
                                    if (league[i].team[l] != league[i].team[j])
                                        league[i].team[l].option[k] = false;
                                solve();
                                fertig = Data.isComplete();
                            }
            save(Data.league, Data.club, Data.partnership, Club.file, League.file, Team.file);
            caller.initUI();
            if (hasError())
                notification.Add("Die Generierung ist aufgrund eines logischen Fehlers nicht moeglich!");
            else
                notification.Add("Die Schluesselzahlen wurden erfolgreich generiert!");
        }

        public static bool checkFatal(League[] l, int[] conflicts)
        {
            int[] allocation;
            conflicts[0] = 0;
            foreach (League league in l)
            {
                allocation = new int[league.field];
                
                // Widerspricht die Zahl eines Teams den vorgegebenen Spieltagen?
                for (int j = 0; j < league.nrOfTeams; j++)
                    if (league.team[j].key > 0)
                    {
                        allocation[league.team[j].key - 1]++;
                        if (!league.team[j].keyOK(league.team[j].key))
                            return true;
                    }
                
                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < league.field; )
                    if (allocation[j] > 3)
                        return true;
                    else if (allocation[j] > 1)
                    {
                        conflicts[0]++;
                        if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                            return true;
                        for (int m = 0; m < league.nrOfTeams; m++)
                            for (int k = m + 1; k < league.nrOfTeams; k++)
                                if (league.team[m].key == j+1 && league.team[k].key == j+1 && league.team[m].club != league.team[k].club
                                 && league.team[m].club.capacity && league.team[k].club.capacity)
                                    return true;
                        if (km.getSimilar(league.field, j+1)[0] != 0 && allocation[km.getSimilar(league.field, j+1)[0] - 1] == 0)
                            allocation[km.getSimilar(league.field, j+1)[0] - 1]++;
                        else if (km.getSimilar(league.field, j + 1)[1] != 0 && allocation[km.getSimilar(league.field, j + 1)[1] - 1] == 0)
                            allocation[km.getSimilar(league.field, j + 1)[1] - 1]++;
                        else
                            return true;
                        allocation[j]--;
                    }
                    else
                        j++;
                    if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                        return true;
            }    
            return false;
        }

        public static void checkFatal(League[] l, List<string> notification)
        {
            int[] allocation;
            foreach (League league in l)
            {
                allocation = new int[league.field];
                // Belegung berechnen
                for (int j = 0; j < league.nrOfTeams; j++)
                    if (league.team[j].key > 0)
                        allocation[league.team[j].key - 1]++;

                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < league.field; )
                    if (allocation[j] > 3)
                        notification.Add("Die Schlüsselzahl " + (j++ + 1) + " wurde in der " + league + " mehr als dreimal vergeben!");
                    else if (allocation[j] > 1)
                    {
                        for (int m = 0; m < league.nrOfTeams; m++)
                            for (int k = m + 1; k < league.nrOfTeams; k++)
                                if (league.team[m].key == j + 1 && league.team[k].key == j + 1 && league.team[m].club != league.team[k].club
                                 && league.team[m].club.capacity && league.team[k].club.capacity)
                                    notification.Add("Konflikt zwischen " + league.team[m].name + " und " + league.team[k].name + " in der " + league.name + "!");
                        if (km.getSimilar(league.field, j + 1)[0] != 0 && allocation[km.getSimilar(league.field, j + 1)[0] - 1] == 0)
                            allocation[km.getSimilar(league.field, j + 1)[0] - 1]++;
                        else if (km.getSimilar(league.field, j + 1)[1] != 0 && allocation[km.getSimilar(league.field, j + 1)[1] - 1] == 0)
                            allocation[km.getSimilar(league.field, j + 1)[1] - 1]++;
                        else
                            notification.Add("Unlösbarer Konflikt in der " + league.name + "(Schlüsselzahl " + (j + 1) + ")!");
                        allocation[j]--;
                    }
                    else
                        j++;
            }
        }

        public static void checkConflicts(League[] leagues, List<Conflict> conflicts)
        {
            int[] allocation;
            Conflict conflict;
            int team;
            int number;

            foreach (League league in leagues)
            {
                allocation = new int[league.field];
                for (int j = 0; j < league.nrOfTeams; j++)
                    if (league.team[j].key > 0)
                        allocation[league.team[j].key - 1]++;
                for (int j = 0; j < league.field; j++)
                    if (allocation[j] > 1)
                    {
                        team = 0;
                        number = 0;
                        conflict = new Conflict
                        {
                            wish = j + 1,
                            t = new Team[allocation[j]]
                        };
                        for (int x = 0; x < league.nrOfTeams; x++)
                            if (league.team[x].key == j + 1)
                                conflict.t[team++] = league.team[x];
                        conflict.key[number++] = j + 1;
                        if (allocation[km.getSimilar(league.field, j+1)[0] - 1] == 0)
                            conflict.key[number++] = km.getSimilar(league.field, j+1)[0];
                        if (allocation[km.getSimilar(league.field, j+1)[1] - 1] == 0)
                            conflict.key[number++] = km.getSimilar(league.field, j+1)[1];
                        if (conflict.key[allocation[j] - 1] == 0)
                        {
                            conflict.key[1] = km.getSimilar(league.field, j+1)[0];
                            conflict.key[2] = km.getSimilar(league.field, j+1)[1];
                        }
                        conflicts.Add(conflict);
                    }
            } 
        }

        public static bool partnerOK(Club[] club, int c, int p, int x, int[] key)
        {
            int keyA = 0;
            int keyB = 0;
            int fieldA = 0;
            int fieldB = 0;
            bool okay = true;

            if (x == 0)
            {
                club[c].a = key[p * 2];
                club[c].b = km.getOpposed(field[0], field[0], key[p * 2]);
            }
            else if (x == 1)
            {
                club[c].x = key[p * 2 + 1];
                club[c].y = km.getOpposed(field[1], field[1], key[p * 2 + 1]);
            }
            for (int i = 0; i < partnership.Count; i++)
            {
                if (partnership[i].clubA.index != club[c].index && partnership[i].clubB.index != club[c].index)
                    continue;
                switch (partnership[i].weekA)
                {
                    case 'A': keyA = partnership[i].clubA.a; fieldA = field[0]; break;
                    case 'B': keyA = partnership[i].clubA.b; fieldA = field[0]; break;
                    case 'X': keyA = partnership[i].clubA.x; fieldA = field[1]; break;
                    case 'Y': keyA = partnership[i].clubA.y; fieldA = field[1]; break;
                }
                switch (partnership[i].weekB)
                {
                    case 'A': keyB = partnership[i].clubB.a; fieldB = field[0]; break;
                    case 'B': keyB = partnership[i].clubB.b; fieldB = field[0]; break;
                    case 'X': keyB = partnership[i].clubB.x; fieldB = field[1]; break;
                    case 'Y': keyB = partnership[i].clubB.y; fieldB = field[1]; break;
                }

                if (keyA == 0 || keyB == 0)
                    continue;
                if (km.getParallel(fieldA, fieldB, keyA) == keyB)
                    continue;
                okay = false;
                break;
            }
            if (x == 0)
                club[c].a = club[c].b = 0;
            else if (x == 1)
                club[c].x = club[c].y = 0;
            return okay;
        }

        public static void setAdditional(League[] l, League[] best_l, Club[] club, Club[] best_club, int[] conflicts, int[] key)
        {
            string value;

            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nrOfTeams; j++)
                    if (l[i].team[j].key == 0 && l[i].team[j].week == '-' && l[i].team[j].hasAdditional())
                    {
                        for (int k = 0; k < l[i].field; k++)
                            if (l[i].team[j].keyOK(k + 1))
                            {
                                l[i].team[j].key = k + 1;
                                if (checkFatal(l, conflicts))
                                    conflicts[0] = -1;
                                else
                                    setAdditional(l, best_l, club, best_club, conflicts, key);
                            }
                        l[i].team[j].key = 0;
                        conflicts[0] = -1;
                        return;
                    }
            if (conflicts[1] == conflicts[0])
                return;
            conflicts[1] = conflicts[0];
            copy(l, best_l, club, best_club, Data.partnership, Data.partnership);
            value = getValue(key);
            if (!ht.Contains(value))
                ht.Add(value, value);
        }

        public static void findSolution(int p, League[] l, League[] best_l, Club[] c, Club[] best_c, int[] conflicts, int[] key)
        {
            int[] newAllocations = { 0, 0 };
            bool[] determined = new bool[2];
            string value;

            if (p < c.Length)
            {
                Club club = c[prio[p]];

                if (club.prio == 0)
                    setAdditional(l, best_l, c, best_c, conflicts, key);
                else
                {
                    for (int j = 0; j < club.team.Count; j++)
                        if (club.team[j].club.index == club.index)
                            switch (club.team[j].week)
                            {
                                case 'A': newAllocations[0]++; break;
                                case 'B': newAllocations[0]++; break;
                                case 'X': newAllocations[1]++; break;
                                case 'Y': newAllocations[1]++; break;
                            }
                    determined[0] = club.a != 0 || newAllocations[0] == 0;
                    determined[1] = club.x != 0 || newAllocations[1] == 0;
                    for (int x = 0; x < 3; x++)
                    {
                        if (x == 2)
                        {
                            findSolution(p + 1, l, best_l, c, best_c, conflicts, key);
                            break;
                        }
                        if (determined[x])
                            continue;
                        rand[x] %= field[x];
                        key[p * 2 + x] = ++rand[x];
                        value = getValue(key);
                        if (!partnerOK(c, prio[p], p, x, key))
                            if (!ht.Contains(value))
                                ht.Add(value, value);
                        for (int j = 0; ht.Contains(value); j++)
                        {
                            if (j == field[x])
                            {
                                conflicts[0] = -1;
                                fillHashTable(key, p * 2 + x, field[x]);
                                x = 3;
                                break;
                            }
                            rand[x] %= field[x];
                            key[p * 2 + x] = ++rand[x];
                            value = getValue(key);
                            if (!partnerOK(c, prio[p], p, x, key))
                                if (!ht.Contains(value))
                                    ht.Add(value, value);
                        }

                        if (x == 0)
                        {
                            club.a = key[p * 2];
                            club.b = km.getOpposed(field[0] , field[0] , key[p * 2]);
                            foreach (Team team in club.team)
                                if (team.week == 'A')
                                {
                                    if (team.league.field == field[0])
                                        team.key = club.a;
                                    else
                                        team.key = km.getParallel(field[0], team.league.field, club.a);
                                }
                                else if (team.week == 'B')
                                {
                                    if (team.league.field == field[0])
                                        team.key = club.b;
                                    else
                                        team.key = km.getParallel(field[0] , team.league.field , club.b);
                                }
                        }
                        else if (x == 1)
                        {
                            club.x = key[p * 2 + 1];
                            club.y = km.getOpposed(field[1], field[1], key[p * 2 + 1]);
                            foreach (Team team in club.team)
                                if (team.week == 'X')
                                {
                                    if (team.league.field == field[1])
                                        team.key = club.x;
                                    else
                                        team.key = km.getParallel(field[1], team.league.field, club.x);
                                }
                                else if (team.week == 'Y')
                                {
                                    if (team.league.field == field[1])
                                        team.key = club.y;
                                    else
                                        team.key = km.getParallel(field[1] , team.league.field , club.y );
                                }
                        }
                        else if (x == 3)
                            break;

                        if (checkFatal(l, conflicts))
                        {
                            conflicts[0] = -1;
                            value = getValue(key);
                            ht.Add(value, value);
                            break;
                        }
                    }

                    if (!determined[0])
                    {
                        club.a = 0;
                        club.b = 0;
                        foreach (Team team in club.team)
                            if (team.week == 'A')
                                team.key = 0;
                            else if (team.week == 'B')
                                team.key = 0;
                    }
                    if (!determined[1])
                    {
                        club.x = 0;
                        club.y = 0;
                        foreach (Team team in club.team)
                            if (team.week == 'X')
                                team.key = 0;
                            else if (team.week == 'Y')
                                team.key = 0;
                    }
                    conflicts[0] = -1;
                    key[p * 2] = 0;
                    key[p * 2 + 1] = 0;
                }
            }
            else
                setAdditional(l, best_l, c, best_c, conflicts, key);
        }

        public static void fillHashTable(int[] key, int pos, int field)
        {
            string value;
            for (int i = 1; i <= field; i++)
            {
                key[pos] = i;
                value = getValue(key);
                ht.Remove(value);
            }
            key[pos] = 0;
            value = getValue(key);
            ht.Add(value, value);
        }

        public static void copyKeys()
        {
            foreach (Club club in club) {
                foreach (Team team in club.team)
                    if (team.key == 0)
                        switch (team.week)
                        {
                            case 'A':
                                if (team.league.field == field[0])
                                    team.key = club.a;
                                else if (club.a > 0)
                                    team.key = km.getParallel(field[0], team.league.field, club.a);
                                break;
                            case 'B':
                                if (team.league.field == field[0])
                                    team.key = club.b;
                                else if (club.b > 0)
                                    team.key = km.getParallel(field[0], team.league.field, club.b);
                                break;
                            case 'X':
                                if (team.league.field == field[1])
                                    team.key = club.x;
                                else if (club.x > 0)
                                    team.key = km.getParallel(field[1], team.league.field, club.x);
                                break;
                            case 'Y':
                                if (team.league.field == field[1])
                                    team.key = club.y;
                                else if (club.y > 0)
                                    team.key = km.getParallel(field[1], team.league.field, club.y);
                                break;
                            case '-':
                                team.key = 0;
                                break;
                        }
                //setPartner(verein, i);
            }
        }

        public static void allocateTeams(Club[] v, League[] l)
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

        public static void createPriority()
        {
            prio = new int[club.Length];
            for (int i = 0; i < club.Length; i++)
                club[i].setPrio(partnership);
            int counter = 0;
            int max_prio;

            while (counter < club.Length)
            {
                max_prio = 0;
                for (int i = 0; i < club.Length; i++)
                    if (club[i].prio > max_prio && (counter == 0 || club[i].prio < club[prio[counter - 1]].prio))
                        max_prio = club[i].prio;
                for (int i = 0; i < club.Length; i++)
                    if (club[i].prio == max_prio)
                        prio[counter++] = club[i].index;
            }
        }

        public static bool checkClub(Team t, Club c)
        {
            string team = t.name;
            string club = c.name;
            t.team = "";
            try
            {
                string[] token = team.Split(' ');
                string suffix = token[token.Length - 1];

                if (Util.isRomanNumber(suffix))
                {
                    t.team = suffix;
                    team = team.Remove(team.Length - suffix.Length - 1);
                }

                char[] chars = team.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                    if (!club.Contains(chars[i]))
                        return false;
                    else
                    {
                        if (club.IndexOf(chars[i]) != 0)
                            if (Char.IsLetter(chars[i]) && Char.IsLetter(club.ToCharArray()[club.IndexOf(chars[i]) - 1]))
                                return false;
                        club = club.Substring(club.IndexOf(chars[i]) + 1);
                    }
                return true;
            }
            catch (Exception e)
            {
                notification.Append(e.ToString());
                return false;
            }
        }

        public static void checkPlausibility(League[] leagues, List<string> m)
        {
            bool plausible;
            foreach (League league in leagues)
                foreach (Team team in league.team)
                {
                    if (team == null)
                        continue;
                    plausible = false;
                    if (team.key == 0)
                        for (int k = 0; k < league.field; k++)
                            plausible |= team.keyOK(k + 1);
                    else
                        plausible = team.keyOK(team.key);
                    if (!plausible)
                        m.Add("Der Spielplan für " + team.name + " in der " + league.name + " ist inkonsistent!");
                }
            if (m.Count > 0)
                return;
            foreach (Club club in club)
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

        public static string getValue(int[] key)
        {
            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == 0)
                    continue;
                if (key[i] < 10)
                    result += " ";
                result += key[i];
            }
            return result;
        }
    }
}