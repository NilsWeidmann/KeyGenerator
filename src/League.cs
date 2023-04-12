using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class League : IComparable
    {
        public static TextFile file;
        public static TextFile backup;

        public String name;
        public Team[] team;
        public int index;
        public int nrOfTeams;
        public int field;


        public League() {
            team = new Team[Data.TEAM_MAX];
            nrOfTeams = 0;
            field = 0;
        }

        public League(String name, int index) : this() 
        {
            this.name = name;
            this.index = index;
            nrOfTeams = Data.TEAM_MAX;
            field = Data.TEAM_MAX;
        }

        public void removeOptions(int number)
        {
            for (int i = 0; i < nrOfTeams; i++)
                if (team[i].key != number)
                    team[i].option[number - 1] = false;
                else
                    for (int j = 0; j < Data.TEAM_MAX; j++)
                        if (j != number - 1)
                            team[i].option[j] = false;
        }

        public void checkOneOption()
        {
            int t = -1;
            for (int i = 0; i < Data.TEAM_MAX; i++)
            {
                t = -1;
                for (int j = 0; j < nrOfTeams; j++)
                    if (team[j].option[i])
                        if (t == -1)
                            t = j;
                        else
                        {
                            t = -1;
                            break;
                        }
                if (t >= 0 && team[t].key == 0)
                {
                    team[t].key = i + 1;
                    removeOptions(i + 1);
                    checkOneOption();
                }
            }
        }

        public League clone()
        {
            League l = new League();
            l.nrOfTeams = nrOfTeams;
            l.index = index;
            l.name = name;
            l.team = new Team[Data.TEAM_MAX];
            l.field = field;
            return l;
        }

        public int CompareTo(object obj)
        {
            if ((obj == null) || !obj.GetType().Equals(this.GetType()))
                return 0;
            else
                return index - ((League)obj).index;
        }

        public static League[] getGroups(Club[] clubs, TextFile inputFile, List<String> notification)
        {
            String content = inputFile.ReadLine(1, false, notification);
            if (notification.Count > 0)
                return null;
            char[] split = { ';' };
            String[] help = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            League[] l = new League[help.Length];
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
            for (int i = 0; i < Data.TEAM_MAX; i++)
            {
                content = inputFile.ReadLine(2 + i, false, notification);
                if (notification.Count > 0)
                    return l;
                help = content.Split(';');
                for (int j = 0; j < help.Length; j++)
                    if (!help[j].Equals(""))
                    {
                        help[j] = Util.clear(help[j]);
                        if (help[j].Contains("Dummy"))
                            continue;
                        l[j].team[i] = new Team();
                        l[j].nrOfTeams++;
                        help2 = help[j].Split('[');
                        l[j].team[i].name = help2[0].TrimEnd(' ');
                        if (help2.Length > 1)
                            l[j].team[i].key = Util.toInt(help2[1]);
                        l[j].team[i].league = l[j];
                        l[j].team[i].index = i;
                        for (int k = 0; k < clubs.Length; k++)
                            if (Data.checkClub(l[j].team[i], clubs[k]))
                                l[j].team[i].club = clubs[k];
                        if (l[j].team[i].club == null)
                        {
                            l[j].team[i].club = new Club
                            {
                                name = l[j].team[i].name,
                                index = -1
                            };
                            l[j].team[i].week = '-';
                        }
                    }
            }
            return l;
        }
    }  
}
