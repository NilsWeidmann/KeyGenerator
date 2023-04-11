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
        public static int team_min = 6;
        public static int team_max = 14;
        public static string path;
        public static League[] league;
        public static Club[] club;
        public static List<Partnership> partnership = new List<Partnership>();
        public static Schluesselzahlen caller;
        public static TextFile group;
        public static TextFile group_b;
        public static TextFile clubs;
        public static TextFile clubs_b;
        public static TextFile relations;
        public static TextFile relations_b;
        public static TextFile schedule;
        public static List<String> notification = new List<String>();
        public static char[, ,] schedule_1;
        public static int[] field = new int[2];
        public static int[] prio;
        public static int runtime;
        public static Hashtable ht = new Hashtable();
        public static int[] rand = { 0, 0 };
        public static NumberMapper nm;

        public static int resetKeys(int l, int t, ComboBox c, bool isNew)
        {
            bool[] selectable = new bool[team_max];
            int n = league[l].nr_of_teams;
            int r = -1;

            if (isNew)
                n++;
            for (int j = 0; j < team_max; j++)
                selectable[j] = true;
            for (int j = 0; j < league[l].nr_of_teams; j++)
                if ((league[l].team[j].number + 1) / 2 > (league[l].field) / 2)
                    league[l].team[j].number = 0;
                else if (league[l].team[j].number > 0 && j != t)
                    selectable[league[l].team[j].number - 1] = false;
            c.Items.Clear();

            for (int j = 0; n > j; j += 2)
            {
                if (selectable[j])
                    c.Items.Add(j + 1);
                if (t != league[l].nr_of_teams)
                    if (j == league[l].team[t].number - 1)
                        r = c.Items.Count - 1;
                if (selectable[j + 1])
                    c.Items.Add(j + 2);
                if (t != league[l].nr_of_teams)
                    if (j == league[l].team[t].number - 2)
                        r = c.Items.Count - 1;
            }
            return r;
        }

        public static void copy(League[] l1, League[] l2, Club[] c1, Club[] c2, List<Partnership> p1, List<Partnership> p2)
        {
            for (int i = 0; i < c1.Length && c1[i] != null; i++)
                c2[i] = c1[i].clone();
                
            p2 = new List<Partnership>();
            foreach (Partnership p in p1)
                p2.Add(new Partnership(c2[p.a.index], p.week_a, c2[p.b.index], p.week_b));

            for (int i = 0; i < l1.Length && l1[i] != null; i++)
            {
                l2[i] = l1[i].clone();
                for (int j = 0; j < l1[i].team.Length && l1[i].team[j] != null; j++)
                {
                    l2[i].team[j] = l1[i].team[j].clone();
                    if (l1[i].team[j].club.index == -1)
                        l2[i].team[j].club = l1[i].team[j].club.clone();
                    else
                    {
                        l2[i].team[j].club = c2[l1[i].team[j].club.index];
                        for (int k = 0; k < c2[l1[i].team[j].club.index].team.Length; k++)
                            if (c2[l1[i].team[j].club.index].team[k] == null)
                            {
                                c2[l1[i].team[j].club.index].team[k] = l2[i].team[j];
                                break;
                            }
                    }
                }
            }
        }

        public static void setOptions()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nr_of_teams; j++)
                    if (league[i].team[j].number != 0)
                    {
                        for (int k = 0; k < team_max; k++)
                            league[i].team[j].option[k] = false;
                        league[i].team[j].option[league[i].team[j].number - 1] = true;
                    }
                    else
                    {
                        for (int k = 0; k < league[i].field; k++)
                        {
                            if (league[i].team[j].numberOK(k + 1))
                                league[i].team[j].option[k] = true;
                            else
                                league[i].team[j].option[k] = false;
                            for (int l = 0; l < league[i].nr_of_teams; l++)
                                if (league[i].team[l].number == k + 1)
                                    league[i].team[j].option[k] = false;
                        }
                        for (int k = league[i].field; k < team_max; k++)
                            league[i].team[j].option[k] = false;
                    }
        }

        public static void setWeeks()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nr_of_teams; j++)
                    if (league[i].team[j].number != 0)
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
                content = clear(content);
                help = content.Split(';');
                v[i] = new Club();
                v[i].name = help[0];
                v[i].index = i;
                v[i].team = new Team[team_max];
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

        public static League[] getGroups(Club[] v, TextFile sta)
        {
            League[] l;
            String content = sta.ReadLine(1, false, notification);
            if (notification.Count > 0)
                return null;
            char[] split = { ';' };
            String[] help = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            l = new League[help.Length];
            String[] help2;
            for (int i = 0; i < l.Length; i++)
            {
                l[i] = new League();
                help2 = help[i].Split('[');
                l[i].name = help2[0].TrimEnd(' ');
                l[i].index = i;
                if (help2.Length > 1)
                    l[i].field = Util.toInt(help2[1]);
            }
            for (int i = 0; i < team_max; i++)
            {
                content = sta.ReadLine(2 + i, false, notification);
                if (notification.Count > 0)
                    return l;
                help = content.Split(';');
                for (int j = 0; j < help.Length; j++)
                    if (!help[j].Equals("")) 
                    {
                        help[j] = clear(help[j]);
                        if (help[j].Contains("Dummy"))
                            continue;
                        l[j].team[i] = new Team();
                        l[j].nr_of_teams++;
                        help2 = help[j].Split('[');
                        l[j].team[i].name = help2[0].TrimEnd(' ');
                        if (help2.Length > 1)
                            l[j].team[i].number = Util.toInt(help2[1]);
                        l[j].team[i].league = l[j];
                        l[j].team[i].index = i;
                        for (int k = 0; k < v.Length; k++) 
                            if (checkVerein(l[j].team[i], v[k])) 
                                l[j].team[i].club = v[k];
                        if (l[j].team[i].club == null)
                        {
                            l[j].team[i].club = new Club();
                            l[j].team[i].club.name = l[j].team[i].name;
                            l[j].team[i].club.index = -1;
                            l[j].team[i].club.team = new Team[team_max];
                            l[j].team[i].week = '-';
                        }
                    }
            }
            return l;
        }

        public static int nrOfNumbers(League[] l)
        {
            int numbers = 0;
            int teams = 0;

            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nr_of_teams; j++)
                {
                    if (l[i].team[j].number != 0)
                        numbers++;
                    teams++;
                }
            return numbers;
        }

        public static int run()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nr_of_teams; j++)
                    {
                        if (league[i].team[j].number == 0)
                            league[i].team[j].getNumber();
                        if (league[i].team[j].number != 0)
                            league[i].removeOptions(league[i].team[j].number);
                        league[i].checkOneOption();
                    }
            return nrOfNumbers(league);
        }

        public static bool isComplete()
        {
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nr_of_teams; j++)
                    if (league[i].team[j].number == 0)
                        return false;
            return true;
        }

        public static bool hasError()
        {
            bool help;
            for (int i = 0; i < league.Length; i++)
                for (int j = 0; j < league[i].nr_of_teams; j++)
                {
                    help = false;
                    for (int k = 0; k < team_max; k++)
                        help = help || league[i].team[j].option[k];
                    if (!help)
                    {
                        notification.Add("Keine moegliche Schluesselzahl fuer " + league[i].team[j].name + " (" + league[i].name + ") gefunden!");
                        return true;
                    }
                }
            return false;
        }

        public static String clear(String s)
        {
            s = s.Replace("Ä", "Ae");
            s = s.Replace("Ö", "Oe");
            s = s.Replace("Ü", "Ue");
            s = s.Replace("ä", "ae");
            s = s.Replace("ö", "oe");
            s = s.Replace("ü", "ue");
            s = s.Replace("ß", "ss");
            return s;
        }

        public static void save(League[] l, Club[] c, List<Partnership> p, TextFile clubs, TextFile groups, TextFile relations)
        {
            String help;
            int line = 0;
            relations.WriteFile("", notification);
            for (int i=0; i<l.Length; i++)
                for (int j = 0; j < l[i].nr_of_teams; j++)
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
                    if (pt.a == c[i])
                        help += pt.week_a + ";" + pt.b.index + ";" + pt.week_b + ";";
                help += "\n";
                clubs.Append(help, notification);
            }
            groups.WriteFile("", notification);
            String content = "";
            for (int i = 0; i < l.Length; i++)
                content += l[i].name + " [" + l[i].field + "];";
            content += "\n";
            groups.Append(content, notification);
            for (int i = 0; i < team_max; i++)
            {
                content = "";
                for (int j = 0; j < l.Length; j++)
                    if (l[j].team[i] != null && l[j].team[i].number != 0)
                        content += l[j].team[i].name + " [" + l[j].team[i].number + "];";
                    else if (l[j].team[i] != null && l[j].team[i].number == 0)
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
                catch (Exception e) { }
            }
        }

        public static void getSpielplan(String path)
        {
            String inhalt;
            String[] hilf;
            String[] zeile;
            TextFile spielplan = new TextFile(path + @"\Spielplan.csv");
            inhalt = spielplan.ReadFile(false, notification);

            if (inhalt.Equals(""))
                return;
            zeile = inhalt.Split('\n');
            schedule_1 = new char[team_max, team_max, team_max];
            for (int i = 0; i < zeile.Length; i++)
            {
                hilf = zeile[i].Split(';');
                for (int j=2; j<hilf.Length; j++)
                    try
                    {
                        schedule_1[Util.toInt(hilf[0]) - 1, Util.toInt(hilf[1]) - 1, j - 2] = hilf[j].ToCharArray()[0];
                    }
                    catch (Exception e)
                    {
                    }
            }
        }

        public static void loese()
        {
            int alt = 0;
            int neu = run();
            while (alt != neu)
            {
                alt = neu;
                neu = run();
                if (isComplete())
                    return;
            }
        }

        public static void generiereSchluesselzahlen()
        {
            setOptions();
            loese();
            bool fertig = Data.isComplete();
            while (!fertig)
                for (int i = 0; i < league.Length; i++)
                    for (int j = 0; j < league[i].nr_of_teams; j++)
                        for (int k = 0; k < league[i].field; k++)
                            if (league[i].team[j].option[k] && !fertig)
                            {
                                league[i].team[j].number = k + 1;
                                for (int l = 0; l < league[i].field; l++)
                                    if (l != k)
                                        league[i].team[j].option[l] = false;
                                for (int l = 0; l < league[i].nr_of_teams; l++)
                                    if (league[i].team[l] != league[i].team[j])
                                        league[i].team[l].option[k] = false;
                                loese();
                                fertig = Data.isComplete();
                            }
            save(Data.league, Data.club, Data.partnership, Data.clubs, Data.group, Data.relations);
            caller.initUI();
            if (hasError())
                notification.Add("Die Generierung ist aufgrund eines logischen Fehlers nicht moeglich!");
            else
                notification.Add("Die Schluesselzahlen wurden erfolgreich generiert!");
        }

        public static bool checkFatal(League[] l, int[] conflicts)
        {
            int[] belegung;
            conflicts[0] = 0;
            foreach (League league in l)
            {
                belegung = new int[league.field];
                
                // Widerspricht die Zahl eines Teams den vorgegebenen Spieltagen?
                for (int j = 0; j < league.nr_of_teams; j++)
                    if (league.team[j].number > 0)
                    {
                        belegung[league.team[j].number - 1]++;
                        if (!league.team[j].numberOK(league.team[j].number))
                            return true;
                    }
                
                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < league.field; )
                    if (belegung[j] > 3)
                        return true;
                    else if (belegung[j] > 1)
                    {
                        conflicts[0]++;
                        if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                            return true;
                        for (int m = 0; m < league.nr_of_teams; m++)
                            for (int k = m + 1; k < league.nr_of_teams; k++)
                                if (league.team[m].number == j+1 && league.team[k].number == j+1 && league.team[m].club != league.team[k].club
                                 && league.team[m].club.capacity && league.team[k].club.capacity)
                                    return true;
                        if (nm.getAehnlich(league.field, j+1)[0] != 0 && belegung[nm.getAehnlich(league.field, j+1)[0] - 1] == 0)
                            belegung[nm.getAehnlich(league.field, j+1)[0] - 1]++;
                        else if (nm.getAehnlich(league.field, j + 1)[1] != 0 && belegung[nm.getAehnlich(league.field, j + 1)[1] - 1] == 0)
                            belegung[nm.getAehnlich(league.field, j + 1)[1] - 1]++;
                        else
                            return true;
                        belegung[j]--;
                    }
                    else
                        j++;
                    if (conflicts[1] != -1 && conflicts[0] >= conflicts[1])
                        return true;
            }    
            return false;
        }

        public static void checkFatal(League[] l, List<string> meldung)
        {
            int[] belegung;
            foreach (League league in l)
            {
                belegung = new int[league.field];
                // Belegung berechnen
                for (int j = 0; j < league.nr_of_teams; j++)
                    if (league.team[j].number > 0)
                        belegung[league.team[j].number - 1]++;

                // Treten unlösbare Konflikte auf?
                for (int j = 0; j < league.field; )
                    if (belegung[j] > 3)
                        meldung.Add("Die Schlüsselzahl " + (j++ + 1) + " wurde in der " + league + " mehr als dreimal vergeben!");
                    else if (belegung[j] > 1)
                    {
                        for (int m = 0; m < league.nr_of_teams; m++)
                            for (int k = m + 1; k < league.nr_of_teams; k++)
                                if (league.team[m].number == j + 1 && league.team[k].number == j + 1 && league.team[m].club != league.team[k].club
                                 && league.team[m].club.capacity && league.team[k].club.capacity)
                                    meldung.Add("Konflikt zwischen " + league.team[m].name + " und " + league.team[k].name + " in der " + league.name + "!");
                        if (nm.getAehnlich(league.field, j + 1)[0] != 0 && belegung[nm.getAehnlich(league.field, j + 1)[0] - 1] == 0)
                            belegung[nm.getAehnlich(league.field, j + 1)[0] - 1]++;
                        else if (nm.getAehnlich(league.field, j + 1)[1] != 0 && belegung[nm.getAehnlich(league.field, j + 1)[1] - 1] == 0)
                            belegung[nm.getAehnlich(league.field, j + 1)[1] - 1]++;
                        else
                            meldung.Add("Unlösbarer Konflikt in der " + league.name + "(Schlüsselzahl " + (j + 1) + ")!");
                        belegung[j]--;
                    }
                    else
                        j++;
            }
        }

        public static void checkConflicts(League[] l, List<Conflict> k)
        {
            int[] belegung;
            Conflict konflikt;
            int team = 0;
            int zahl = 0;
            foreach (League league in l)
            {
                belegung = new int[league.field];
                for (int j = 0; j < league.nr_of_teams; j++)
                    if (league.team[j].number > 0)
                        belegung[league.team[j].number - 1]++;
                for (int j = 0; j < league.field; j++)
                    if (belegung[j] > 1)
                    {
                        team = 0;
                        zahl = 0;
                        konflikt = new Conflict();
                        konflikt.wish = j + 1;
                        konflikt.t = new Team[belegung[j]];
                        for (int x = 0; x < league.nr_of_teams; x++)
                            if (league.team[x].number == j + 1)
                                konflikt.t[team++] = league.team[x];
                        konflikt.number[zahl++] = j + 1;
                        if (belegung[nm.getAehnlich(league.field, j+1)[0] - 1] == 0)
                            konflikt.number[zahl++] = nm.getAehnlich(league.field, j+1)[0];
                        if (belegung[nm.getAehnlich(league.field, j+1)[1] - 1] == 0)
                            konflikt.number[zahl++] = nm.getAehnlich(league.field, j+1)[1];
                        if (konflikt.number[belegung[j] - 1] == 0)
                        {
                            konflikt.number[1] = nm.getAehnlich(league.field, j+1)[0];
                            konflikt.number[2] = nm.getAehnlich(league.field, j+1)[1];
                        }
                        k.Add(konflikt);
                    }
            } 
        }

        public static bool partnerOK(Club[] ver, int v, int p, int x, int[] schluessel)
        {
            int zahl_a = 0;
            int zahl_b = 0;
            int feld_a = 0;
            int feld_b = 0;
            bool okay = true;

            if (x == 0)
            {
                ver[v].a = schluessel[p * 2];
                ver[v].b = nm.getGegenlaeufig(field[0], field[0], schluessel[p * 2]);
            }
            else if (x == 1)
            {
                ver[v].x = schluessel[p * 2 + 1];
                ver[v].y = nm.getGegenlaeufig(field[1], field[1], schluessel[p * 2 + 1]);
            }
            for (int i = 0; i < partnership.Count; i++)
            {
                if (partnership[i].a.index != ver[v].index && partnership[i].b.index != ver[v].index)
                    continue;
                switch (partnership[i].week_a)
                {
                    case 'A': zahl_a = partnership[i].a.a; feld_a = field[0]; break;
                    case 'B': zahl_a = partnership[i].a.b; feld_a = field[0]; break;
                    case 'X': zahl_a = partnership[i].a.x; feld_a = field[1]; break;
                    case 'Y': zahl_a = partnership[i].a.y; feld_a = field[1]; break;
                }
                switch (partnership[i].week_b)
                {
                    case 'A': zahl_b = partnership[i].b.a; feld_b = field[0]; break;
                    case 'B': zahl_b = partnership[i].b.b; feld_b = field[0]; break;
                    case 'X': zahl_b = partnership[i].b.x; feld_b = field[1]; break;
                    case 'Y': zahl_b = partnership[i].b.y; feld_b = field[1]; break;
                }

                if (zahl_a == 0 || zahl_b == 0)
                    continue;
                if (nm.getParallel(feld_a, feld_b, zahl_a) == zahl_b)
                    continue;
                okay = false;
                break;
            }
            if (x == 0)
                ver[v].a = ver[v].b = 0;
            else if (x == 1)
                ver[v].x = ver[v].y = 0;
            return okay;
        }

        public static void setZusatz(League[] l, League[] best_l, Club[] ver, Club[] best_ver, int[] kon, int[] schluessel)
        {
            string value;

            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nr_of_teams; j++)
                    if (l[i].team[j].number == 0 && l[i].team[j].week == '-' && l[i].team[j].hasAdditional())
                    {
                        for (int k = 0; k < l[i].field; k++)
                            if (l[i].team[j].numberOK(k + 1))
                            {
                                l[i].team[j].number = k + 1;
                                if (checkFatal(l, kon))
                                    kon[0] = -1;
                                else
                                    setZusatz(l, best_l, ver, best_ver, kon, schluessel);
                            }
                        l[i].team[j].number = 0;
                        kon[0] = -1;
                        return;
                    }
            if (kon[1] == kon[0])
                return;
            kon[1] = kon[0];
            copy(l, best_l, ver, best_ver, Data.partnership, Data.partnership);
            value = getValue(schluessel);
            if (!ht.Contains(value))
                ht.Add(value, value);
        }

        public static void findSolution(int p, League[] l, League[] best_l, Club[] ver, Club[] best_ver, int[] kon, int[] schluessel)
        {
            int[] neu = { 0, 0 };
            bool[] gesetzt = new bool[2];
            string value;

            if (p < ver.Length)
            {
                Club club = ver[prio[p]];

                if (club.prio == 0)
                    setZusatz(l, best_l, ver, best_ver, kon, schluessel);
                else
                {
                    for (int j = 0; j < club.team.Length; j++)
                        if (club.team[j].club.index == club.index)
                            switch (club.team[j].week)
                            {
                                case 'A': neu[0]++; break;
                                case 'B': neu[0]++; break;
                                case 'X': neu[1]++; break;
                                case 'Y': neu[1]++; break;
                            }
                    gesetzt[0] = club.a != 0 || neu[0] == 0;
                    gesetzt[1] = club.x != 0 || neu[1] == 0;
                    for (int x = 0; x < 3; x++)
                    {
                        if (x == 2)
                        {
                            findSolution(p + 1, l, best_l, ver, best_ver, kon, schluessel);
                            break;
                        }
                        if (gesetzt[x])
                            continue;
                        rand[x] %= field[x];
                        schluessel[p * 2 + x] = ++rand[x];
                        value = getValue(schluessel);
                        if (!partnerOK(ver, prio[p], p, x, schluessel))
                            if (!ht.Contains(value))
                                ht.Add(value, value);
                        for (int j = 0; ht.Contains(value); j++)
                        {
                            if (j == field[x])
                            {
                                kon[0] = -1;
                                fillHashTable(schluessel, p * 2 + x, field[x]);
                                x = 3;
                                break;
                            }
                            rand[x] %= field[x];
                            schluessel[p * 2 + x] = ++rand[x];
                            value = getValue(schluessel);
                            if (!partnerOK(ver, prio[p], p, x, schluessel))
                                if (!ht.Contains(value))
                                    ht.Add(value, value);
                        }

                        if (x == 0)
                        {
                            club.a = schluessel[p * 2];
                            club.b = nm.getGegenlaeufig(field[0] , field[0] , schluessel[p * 2]);
                            foreach (Team team in club.team)
                                if (team.week == 'A')
                                {
                                    if (team.league.field == field[0])
                                        team.number = club.a;
                                    else
                                        team.number = nm.getParallel(field[0], team.league.field, club.a);
                                }
                                else if (team.week == 'B')
                                {
                                    if (team.league.field == field[0])
                                        team.number = club.b;
                                    else
                                        team.number = nm.getParallel(field[0] , team.league.field , club.b);
                                }
                        }
                        else if (x == 1)
                        {
                            club.x = schluessel[p * 2 + 1];
                            club.y = nm.getGegenlaeufig(field[1], field[1], schluessel[p * 2 + 1]);
                            foreach (Team team in club.team)
                                if (team.week == 'X')
                                {
                                    if (team.league.field == field[1])
                                        team.number = club.x;
                                    else
                                        team.number = nm.getParallel(field[1], team.league.field, club.x);
                                }
                                else if (team.week == 'Y')
                                {
                                    if (team.league.field == field[1])
                                        team.number = club.y;
                                    else
                                        team.number = nm.getParallel(field[1] , team.league.field , club.y );
                                }
                        }
                        else if (x == 3)
                            break;

                        if (checkFatal(l, kon))
                        {
                            kon[0] = -1;
                            value = getValue(schluessel);
                            ht.Add(value, value);
                            break;
                        }
                    }

                    if (!gesetzt[0])
                    {
                        club.a = 0;
                        club.b = 0;
                        foreach (Team team in club.team)
                            if (team.week == 'A')
                                team.number = 0;
                            else if (team.week == 'B')
                                team.number = 0;
                    }
                    if (!gesetzt[1])
                    {
                        club.x = 0;
                        club.y = 0;
                        foreach (Team team in club.team)
                            if (team.week == 'X')
                                team.number = 0;
                            else if (team.week == 'Y')
                                team.number = 0;
                    }
                    kon[0] = -1;
                    schluessel[p * 2] = 0;
                    schluessel[p * 2 + 1] = 0;
                }
            }
            else
                setZusatz(l, best_l, ver, best_ver, kon, schluessel);
        }

        public static void fillHashTable(int[] schluessel, int pos, int feld)
        {
            string value;
            for (int i = 1; i <= feld; i++)
            {
                schluessel[pos] = i;
                value = getValue(schluessel);
                ht.Remove(value);
            }
            schluessel[pos] = 0;
            value = getValue(schluessel);
            ht.Add(value, value);
        }

        public static void copyKeys()
        {
            foreach (Club club in club) {
                foreach (Team team in club.team)
                    if (team.number == 0)
                        switch (team.week)
                        {
                            case 'A':
                                if (team.league.field == field[0])
                                    team.number = club.a;
                                else if (club.a > 0)
                                    team.number = nm.getParallel(field[0], team.league.field, club.a);
                                break;
                            case 'B':
                                if (team.league.field == field[0])
                                    team.number = club.b;
                                else if (club.b > 0)
                                    team.number = nm.getParallel(field[0], team.league.field, club.b);
                                break;
                            case 'X':
                                if (team.league.field == field[1])
                                    team.number = club.x;
                                else if (club.x > 0)
                                    team.number = nm.getParallel(field[1], team.league.field, club.x);
                                break;
                            case 'Y':
                                if (team.league.field == field[1])
                                    team.number = club.y;
                                else if (club.y > 0)
                                    team.number = nm.getParallel(field[1], team.league.field, club.y);
                                break;
                            case '-':
                                team.number = 0;
                                break;
                        }
                //setPartner(verein, i);
            }
        }

        public static void allocateTeams(Club[] v, League[] l)
        {
            if (v == null || l == null)
                return;
            List<Team> al;
            for (int i = 0; i < v.Length; i++)
            {
                al = new List<Team>();
                for (int j = 0; j < l.Length; j++)
                    for (int k = 0; k < l[j].nr_of_teams; k++)
                        if (l[j].team[k].club.index == v[i].index)
                            al.Add(l[j].team[k]);
                v[i].team = al.ToArray();
            }
        }

        public static void createPriority()
        {
            prio = new int[club.Length];
            for (int i = 0; i < club.Length; i++)
                club[i].setPrio(partnership);
            int counter = 0;
            int max_prio = 0;
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

        public static bool checkVerein(Team t, Club v)
        {
            string team = t.name;
            string verein = v.name;
            t.team = "";
            try
            {
                string[] wort = team.Split(' ');
                string endung = wort[wort.Length - 1];
                string pattern = "IVX";
                bool team_endung = true;
                foreach (char c in endung)
                    if (pattern.IndexOf(c) < 0)
                        team_endung = false;
                if (team_endung)
                {
                    t.team = endung;
                    team = team.Remove(team.Length - endung.Length - 1);
                }

                char[] zeichen = team.ToCharArray();
                for (int i = 0; i < zeichen.Length; i++)
                    if (!verein.Contains(zeichen[i]))
                        return false;
                    else
                    {
                        if (verein.IndexOf(zeichen[i]) != 0)
                            if (Char.IsLetter(zeichen[i]) && Char.IsLetter(verein.ToCharArray()[verein.IndexOf(zeichen[i]) - 1]))
                                return false;
                        verein = verein.Substring(verein.IndexOf(zeichen[i]) + 1);
                    }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void checkPlausibility(League[] liga, Form caller, List<string> m)
        {
            bool plausible;
            foreach (League league in liga)
                foreach (Team team in league.team)
                {
                    if (team == null)
                        continue;
                    plausible = false;
                    if (team.number == 0)
                        for (int k = 0; k < league.field; k++)
                            plausible |= team.numberOK(k + 1);
                    else
                        plausible = team.numberOK(team.number);
                    if (!plausible)
                        m.Add("Der Spielplan für " + team.name + " in der " + league.name + " ist inkonsistent!");
                }
            if (m.Count > 0)
                return;
            foreach (Club club in club)
                for (int j = 0; j < club.team.Length; j++)
                    if (club.team[j].week != '-' && club.team[j].hasAdditional())
                        for (int k = j + 1; k < club.team.Length; k++)
                            if (club.team[k].week != '-' && club.team[k].hasAdditional())
                                if (club.team[j].week == club.team[k].week)
                                {
                                    for (int l = 0; l < team_max; l++)
                                        if (club.team[j].day[l] != club.team[k].day[l])
                                            m.Add("Spielplan und Spielwochen für den Verein " + club.name + " sind inkonsitstent!");
                                }
                                else if (club.team[j].week == 'A' && club.team[k].week == 'B'
                                      || club.team[j].week == 'B' && club.team[k].week == 'A'
                                      || club.team[j].week == 'X' && club.team[k].week == 'Y'
                                      || club.team[j].week == 'Y' && club.team[k].week == 'X')
                                    for (int l = 0; l < team_max; l++)
                                        if (club.team[j].day[l] == club.team[k].day[l] && club.team[j].day[l] != '-')
                                            m.Add("Spielplan und Spielwochen für den Verein " + club.name + " sind inkonsitstent!");
        }

        public static string getValue(int[] schluessel)
        {
            string result = "";
            for (int i = 0; i < schluessel.Length; i++)
            {
                if (schluessel[i] == 0)
                    continue;
                if (schluessel[i] < 10)
                    result += " ";
                result += schluessel[i];
            }
            return result;
        }
    }
}