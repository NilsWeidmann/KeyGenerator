using System;

namespace KeyGenerator
{
    public class Team
    {
        public static TextFile file;
        public static TextFile backup;

        public String name;
        public Group group;
        public Club club;
        public String team;
        public String ageGroup;
        public int key;
        public int index;
        public bool[] option;
        public char week;
        public char[] day;
        public String weekday;
        public String weekday2;

        public Team(String name, Club club, String team, String ageGroup) : this()
        {
            this.name = name;
            this.club = club;
            this.team = team;
            this.ageGroup = ageGroup;
            this.weekday = "";
            this.weekday2 = "";
        }

        public Team()
        {
            key = 0;
            week = '-';
            option = new bool[Data.TEAM_MAX];
            day = new char[Data.TEAM_MAX];
            for (int i = 0; i < Data.TEAM_MAX; i++)
                day[i] = '-';
            this.weekday = "";
            this.weekday2 = "";
        }

        public void getKey()
        {
            int key = 0;
            for (int i = 0; i < Data.TEAM_MAX; i++)
                if (option[i])
                    if (key == 0)
                        key = i + 1;
                    else
                        return;
            this.key = key;
        }

        public bool hasAdditional()
        {
            for (int i = 0; i < day.Length; i++)
                if (day[i] == 'H' || day[i] == 'A')
                    return true;
            return false;
        }

        public bool keyOK(int key)
        {
            if (key == 0)
                return true;
            for (int i = 0; i < day.Length; i++)
                if (Data.km.getDay(group.field, key, i) == 'H' && day[i] == 'A'
                || Data.km.getDay(group.field, key, i) == 'A' && day[i] == 'H')
                    return false;
            return true;
        }

        public Team clone()
        {
            Team t = new Team();
            t.index = index;
            t.group = group;
            t.name = name;
            t.option = new bool[Data.TEAM_MAX];
            for (int k = 0; k < Data.TEAM_MAX; k++)
                t.option[k] = option[k];
            for (int k = 0; k < day.Length; k++)
                t.day[k] = day[k];
            t.week = week;
            t.team = team;
            t.key = key;
            t.weekday = weekday;
            t.weekday2 = weekday2;
            return t;
        }
    }
}
