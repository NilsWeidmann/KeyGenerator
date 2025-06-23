using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyGenerator
{
    public class Group : IComparable
    {
        public static TextFile file;
        public static TextFile backup;

        public String name;
        public Team[] team;
        public int index;
        public int nrOfTeams;
        public int field;
        public int nrOfConflicts;

        public Group()
        {
            team = new Team[Data.TEAM_MAX];
            nrOfTeams = 0;
            field = 0;
            nrOfConflicts = 0;
        }

        public Group(String name, int index) : this()
        {
            this.name = name;
            this.index = index;
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

        public Group clone()
        {
            Group l = new Group();
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
                return index - ((Group)obj).index;
        }

        public static Group[] getGroups(Club[] clubs, TextFile inputFile, List<String> notification)
        {
            String content = inputFile.ReadLine(1, false, notification);
            if (notification.Count > 0)
                return null;
            char[] split = { ';' };
            String[] help = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            List<Group> groupList = new List<Group>();
            String[] help2;

            for (int i = 0; i < help.Length; i++)
            {
                Group group = new Group();
                help2 = help[i].Split('[');
                group.name = help2[0].TrimEnd(' ');
                group.index = i;
                if (help2.Length > 1)
                    group.field = Util.toInt(help2[1]);
                groupList.Add(group);
            }
            for (int i = 0; i < Data.TEAM_MAX; i++)
            {
                content = inputFile.ReadLine(2 + i, false, notification);
                if (notification.Count > 0)
                    return groupList.ToArray();
                help = content.Split(';');
                for (int j = 0; j < help.Length; j++)
                    if (!help[j].Equals(""))
                    {
                        if (help[j].Contains("Dummy"))
                            continue;
                        Group group = groupList.ElementAt(j);
                        Team team = new Team();
                        group.team[i] = team;
                        group.nrOfTeams++;
                        help2 = help[j].Split('[');
                        team.name = help2[0].TrimEnd(' ');
                        if (help2.Length > 1)
                            team.key = Util.toInt(help2[1]);
                        team.group = group;
                        team.index = i;
                        for (int k = 0; k < clubs.Length; k++)
                            if (Data.isTeamOfClub(team, clubs[k]))
                                team.club = clubs[k];
                        if (team.club == null)
                        {
                            team.club = new Club
                            {
                                name = team.name,
                                index = -1
                            };
                            team.week = '-';
                        }
                    }
            }
            return groupList.ToArray();
        }

        public static void getRelations(Group[] l, TextFile relations, List<string> notification)
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

        public int[] getAllocation()
        {
            nrOfConflicts = 0;
            int[] allocation = new int[field];

            foreach (Team t in team)
                if (t != null && t.key > 0)
                {
                    allocation[t.key - 1]++;
                    if (allocation[t.key - 1] > 1)
                        nrOfConflicts++;
                }

            return allocation;
        }
    }
}
