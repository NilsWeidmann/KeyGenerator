using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Schluesselzahlen
{
    public class Data
    {
        public const int TEAM_MIN = 6;
        public const int TEAM_MAX = 14;
        public static string path;
        public static League[] league;
        public static Club[] club;
        public static List<Partnership> partnership = new List<Partnership>();
        public static Schluesselzahlen caller;
        public static List<String> notification = new List<String>();
        public static int[] field = new int[2];
        public static Tuple<int, bool>[] prio;
        public static int runtime;
        public static Hashtable ht = new Hashtable();
        public static int[] rand = { 0, 0 };
        public static KeyMapper km;
        public static int currentConflicts;

        public static string[] ageGroups = {
            "Herren", "Damen",
            "Jungen 19", "Jungen 15", "Jungen 13", "Maedchen 19", "Maedchen 15", "Maedchen 13",
            "Senioren 40", "Senioren 50", "Senioren 60", "Senioren 70",
            "Seniorinnen 40", "Seniorinnen 50", "Seniorinnen 60", "Seniorinnen 70"
        };

        public static string[] weekdays = {
            "Mo", "Di","Mi","Do","Fr","Sa","So"
        };

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
                    }
                }
            }
            allocateTeams(newClub, newLeague);
        }

        public static void setOptions()
        {
            foreach (League l in league)
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
            for (nrOfClubs = 0; !content.Equals(""); nrOfClubs++)
                content = c.ReadLine(2 + nrOfClubs, false, notification).Replace("\r", "");
            Club[] v = new Club[nrOfClubs];
            p.Clear();
            for (int i = 0; i < nrOfClubs; i++)
            {
                content = c.ReadLine(1 + i, false, notification).Replace("\r", "");
                content = Util.clear(content);
                help = content.Split(';');
                v[i] = new Club
                {
                    name = help[0],
                    index = i,
                    team = new List<Team>()
                };
                try
                {
                    v[i].keys['A'] = Util.toInt(help[1]);
                    v[i].keys['B'] = Util.toInt(help[2]);
                    v[i].keys['X'] = Util.toInt(help[3]);
                    v[i].keys['Y'] = Util.toInt(help[4]);
                    v[i].capacity = help[5] == "X";
                    for (int j = 6; j < help.Length - 2; j += 3)
                        p.Add(new Partnership(v[i], help[j].ToCharArray()[0], v[Util.toInt(help[j + 1])], help[j + 2].ToCharArray()[0]));
                }
                catch (Exception e)
                {
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
            foreach (League l in league)
                foreach (Team t in l.team)
                {
                    if (t == null)
                        continue;
                    if (t.key == 0)
                        t.getKey();
                    if (t.key != 0)
                        l.removeOptions(t.key);
                    l.checkOneOption();
                }
            return nrOfKeys(league);
        }

        public static bool isComplete()
        {
            foreach (League l in league)
                foreach (Team t in l.team)
                    if (t == null)
                        continue;
                    else if (t.key == 0)
                        return false;
            return true;
        }

        public static bool hasError()
        {
            bool help;
            foreach (League l in league)
                foreach (Team t in l.team)
                {
                    if (t == null)
                        continue;
                    help = false;
                    for (int k = 0; k < TEAM_MAX; k++)
                        help = help || t.option[k];
                    if (!help)
                    {
                        notification.Add("Keine moegliche Schluesselzahl fuer " + t.name + " (" + l.name + ") gefunden!");
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
                help = c[i].name + ";" + c[i].keys['A'] + ";" + c[i].keys['B'] + ";" 
                    + c[i].keys['X'] + ";" + c[i].keys['Y'] + ";";
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
                row[i] = row[i].Replace("\r", "");
                help = row[i].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    Team t = l[Util.toInt(help[0])].team[Util.toInt(help[1])];
                    t.week = help[2].ToCharArray()[0];
                    for (int j = 3; j < help.Length && j < 16; j++)
                        t.day[j - 3] = help[j].ToCharArray()[0];
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
            bool done = Data.isComplete();
            while (!done)
                foreach (League l in league)
                    foreach (Team t in l.team)
                    {
                        if (t == null)
                            continue;
                        for (int k = 0; k < l.field; k++)
                            if (t.option[k] && !done)
                            {
                                t.key = k + 1;
                                for (int i = 0; i < l.field; i++)
                                    if (i != k)
                                        t.option[i] = false;
                                foreach (Team t2 in l.team)
                                    if (t2 == null)
                                        continue;
                                    else if (!t2.Equals(t))
                                        t2.option[k] = false;
                                solve();
                                done = Data.isComplete();
                            }
                    }
            save(Data.league, Data.club, Data.partnership, Club.file, League.file, Team.file);
            caller.initUI();
            if (hasError())
                notification.Add("Die Generierung ist aufgrund eines logischen Fehlers nicht moeglich!");
            else
                notification.Add("Die Schluesselzahlen wurden erfolgreich generiert!");
        }

        public static Tuple<League,Team,int> checkFatal(League[] league, int[] conflicts)
        {
            int[] allocation;
            conflicts[0] = 0;
            foreach (League l in league)
            {
                allocation = new int[l.field];

                foreach (Team t in l.team)
                    if (t == null)
                        continue;
                    else if (t.key > 0)
                    {
                        allocation[t.key - 1]++;
                        if (!t.keyOK(t.key))
                            return Tuple.Create(l,t,t.key); //Zahl widerspricht den vorgegebenen Spieltagen
                    }

                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < l.field;)
                    if (allocation[j] > 3)
                        return Tuple.Create(l,(Team)null,j+1); // Zahl j+1 zu oft vergeben
                    else if (allocation[j] > 1)
                    {
                        conflicts[0]++;
                        if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                            return Tuple.Create((League)null, (Team)null, 0); // Zu viele Konflikte
                        foreach (Team t in l.team)
                            foreach (Team t2 in l.team)
                                if (t == null || t2 == null)
                                    continue;
                                else if (t.key == j + 1 && t2.key == j + 1 && t.club != t2.club
                                 && t.club.capacity && t2.club.capacity)
                                    return Tuple.Create(l, (Team)null, j+1); // Zahl j+1 zu oft vergeben
                        if (km.getSimilar(l.field, j + 1).Item1 != 0 && allocation[km.getSimilar(l.field, j + 1).Item1 - 1] == 0)
                            allocation[km.getSimilar(l.field, j + 1).Item1 - 1]++;
                        else if (km.getSimilar(l.field, j + 1).Item2 != 0 && allocation[km.getSimilar(l.field, j + 1).Item2 - 1] == 0)
                            allocation[km.getSimilar(l.field, j + 1).Item2 - 1]++;
                        else
                            return Tuple.Create(l, (Team)null, j+1); // Zahl j+1 zu oft vergeben
                        allocation[j]--;
                    }
                    else
                        j++;
            }
            return null;
        }

        public static void checkFatal(League[] l, List<string> notification)
        {
            Tuple<League, Team, int> fatal = checkFatal(l, new int[] { 0, -1 });

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

        public static List<Conflict> getConflicts(League[] leagues)
        {
            int[] allocation;
            List<Conflict> conflicts = new List<Conflict>();
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
                        Conflict conflict = new Conflict
                        {
                            wish = j + 1,
                            t = new Team[allocation[j]]
                        };
                        for (int x = 0; x < league.nrOfTeams; x++)
                            if (league.team[x].key == j + 1)
                                conflict.t[team++] = league.team[x];

                        int alt1 = km.getSimilar(league.field, j + 1).Item1;
                        int alt2 = km.getSimilar(league.field, j + 1).Item2;
                        conflict.key[number++] = j + 1;
                        if (alt1 > 0 && allocation[alt1 - 1] == 0)
                            conflict.key[number++] = alt1;
                        if (alt2 > 0 && allocation[alt2 - 1] == 0)
                            conflict.key[number++] = alt2;
                        if (conflict.key[allocation[j] - 1] == 0)
                        {
                            conflict.key[1] = alt1;
                            conflict.key[2] = alt2;
                        }
                        conflicts.Add(conflict);
                    }
            }
            return conflicts;
        }

        public static bool partnerOK(Club[] club, int c, int p, bool ab, int[] key)
        {
            int keyA = 0, keyB = 0, fieldA = 0, fieldB = 0;
            bool okay = true;

            if (ab)
            {
                club[c].keys['A'] = key[p];
                club[c].keys['B'] = km.getOpposed(field[0], field[0], key[p]);
            }
            else
            {
                club[c].keys['X'] = key[p];
                club[c].keys['Y'] = km.getOpposed(field[1], field[1], key[p]);
            }
            for (int i = 0; i < partnership.Count; i++)
            {
                if (partnership[i].clubA.index != club[c].index && partnership[i].clubB.index != club[c].index)
                    continue;
                if (partnership[i].weekA != '-')
                {
                    keyA = partnership[i].clubA.keys[partnership[i].weekA];
                    fieldA = partnership[i].weekA == 'A' || partnership[i].weekA == 'B' ? field[0] : field[1];
                }
                if (partnership[i].weekB != '-')
                {
                    keyB = partnership[i].clubB.keys[partnership[i].weekB];
                    fieldB = partnership[i].weekB == 'A' || partnership[i].weekB == 'B' ? field[0] : field[1];
                }

                if (keyA == 0 || keyB == 0)
                    continue;
                if (km.getParallel(fieldA, fieldB, keyA) == keyB)
                    continue;
                okay = false;
                break;
            }
            if (ab)
                club[c].keys['A'] = club[c].keys['B'] = 0;
            else
                club[c].keys['X'] = club[c].keys['Y'] = 0;
            return okay;
        }

        public static void setAdditional(League[] l, League[] best_l, Club[] club, Club[] best_club, int[] conflicts, int[] key)
        {
            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nrOfTeams; j++)
                    if (l[i].team[j].key == 0 && l[i].team[j].week == '-' && l[i].team[j].hasAdditional())
                    {
                        for (int k = 0; k < l[i].field; k++)
                            if (l[i].team[j].keyOK(k + 1))
                            {
                                l[i].team[j].key = k + 1;
                                if (checkFatal(l, conflicts) != null)
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
            // Neue beste Lösung wurde gefunden
            conflicts[1] = conflicts[0];
            currentConflicts = conflicts[0];
            copy(l, best_l, club, best_club, Data.partnership, Data.partnership);
        }

        public static void findSolution(League[] l, League[] best_l, Club[] c, Club[] best_c, int[] conflicts, int[] key, BackgroundWorker bw)
        {
            string value;
            int p = 0;
            Club club;
            int idx;
            char week1, week2;
            DateTime start = DateTime.Now;
            int progress = 0;

            while (!ht.Contains(""))
            {
                if (p == c.Length * 2)
                {
                    setAdditional(l, best_l, c, best_c, conflicts, key);
                    safeAdd(p-1, key, prio);
                }
                else
                {
                    club = c[prio[p].Item1];
                    idx = prio[p].Item2 ? 0 : 1;
                    week1 = prio[p].Item2 ? 'A' : 'X';
                    week2 = prio[p].Item2 ? 'B' : 'Y';

                    if (club.prio[idx] == 0)
                    {
                        setAdditional(l, best_l, c, best_c, conflicts, key);
                        safeAdd(p-1, key, prio);
                    }
                    else { 
                        do
                        {
                            rand[idx] %= field[idx];
                            key[p] = ++rand[idx];
                            value = getValue(key);
                        } while (ht.Contains(value));

                        club.keys[week1] = key[p];
                        club.keys[week2] = km.getOpposed(field[idx], field[idx], key[p]);
                        foreach (Team team in club.team)
                            if (team == null)
                                continue;
                            else if (team.week == week1)
                                team.key = km.getParallel(field[idx], team.league.field, club.keys[week1]);
                            else if (team.week == week2)
                                team.key = km.getParallel(field[idx], team.league.field, club.keys[week2]);

                        if (checkFatal(l, conflicts) != null) // || !partnerOK(c, prio[p].Item1, p, prio[p].Item2, key))
                            safeAdd(p, key, prio);
                        else
                        {
                            p++;
                            continue;
                        }
                    }
                }
                
                // Zurücksetzen
                conflicts[0] = -1;
                for (; p >= 0; p--)
                {
                    club = c[prio[p].Item1];
                    idx = prio[p].Item2 ? 0 : 1;
                    week1 = prio[p].Item2 ? 'A' : 'X';
                    week2 = prio[p].Item2 ? 'B' : 'Y';

                    club.keys[week1] = club.keys[week2] = 0;
                    foreach (Team team in club.team)
                        if (team == null)
                            continue;
                        else if (team.week == week1 || team.week == week2)
                            team.key = 0;
                    key[p] = 0;

                    if (bw.CancellationPending)
                        return;
                    else
                        progress = reportProgress(progress, start, bw);
                }
                p++;
            }
        }
            
        private static int reportProgress(int progress, DateTime start, BackgroundWorker bw)
        {
            if (progress < 100 * (int)(DateTime.Now - start).TotalSeconds / runtime)
            {
                progress = 100 * (int)(DateTime.Now - start).TotalSeconds / runtime;
                bw.ReportProgress(progress > 100 ? 100: progress);
                if (progress >= 100)
                    bw.CancelAsync();
            }
            return progress;
        }

        public static void safeAdd(int pos, int[] keys, Tuple<int, bool>[] prio)
        {
            string value = getValue(keys);
            ht.Add(value, 1);

            if (pos < 0)
                return;

            int currentField = prio[pos].Item2 ? field[0] : field[1];

            for (int i = 1; i <= currentField; i++)
            {
                keys[pos] = i;
                value = getValue(keys);
                if (!ht.Contains(value))
                    return;
            }

            for (int i = 1; i <= currentField; i++)
            {
                keys[pos] = i;
                value = getValue(keys);
                ht.Remove(value);
            }
            keys[pos] = 0;

            safeAdd(pos - 1, keys, prio);
        }

        public static void copyKeys()
        {
            foreach (Club club in club)
            {
                foreach (Team team in club.team)
                    if (team == null)
                        continue;
                    else if (team.key == 0)
                        if (team.week == 'A' || team.week == 'B') { 
                            if (club.keys[team.week] > 0)
                                team.key = km.getParallel(field[0], team.league.field, club.keys[team.week]);
                        }
                        else if (team.week == 'X' || team.week == 'Y')
                        {
                            if (club.keys[team.week] > 0)
                                team.key = km.getParallel(field[1], team.league.field, club.keys[team.week]);
                        }
                        else
                            team.key = 0;
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
            prio = new Tuple<int, bool>[club.Length * 2];
            for (int i = 0; i < club.Length; i++)
                club[i].setPrio();
            int counter = 0;
            int max_prio;

            max_prio = 0;
            for (int i = 0; i < club.Length; i++)
                for (int p = 0; p<2; p++)
                    if (club[i].prio[p] > max_prio)
                        max_prio = club[i].prio[p];
            for (int j = max_prio; j >= 0; j--)
                for (int i = 0; i < club.Length; i++)
                    for (int p = 0; p < 2; p++)
                        if (club[i].prio[p] == j)
                            prio[counter++] = Tuple.Create(club[i].index, p==0);
        }

        public static bool checkClub(Team t, Club c)
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
                    break;
                if (key[i] < 10)
                    result += key[i];
                else
                    switch(key[i]) {
                        case 10: result += 'A'; break;
                        case 11: result += 'B'; break;
                        case 12: result += "C"; break;
                        case 13: result += "D"; break;
                        case 14: result += "E"; break;
                    }
            }
            return result;
        }
    }
}