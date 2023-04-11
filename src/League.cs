using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class League
    {
        public String name;
        public Team[] team = new Team[Data.team_max];
        public int index;
        public int nr_of_teams = 0;
        public int field = 0;

        public void removeOptions(int number)
        {
            for (int i = 0; i < nr_of_teams; i++)
                if (team[i].number != number)
                    team[i].option[number - 1] = false;
                else
                    for (int j = 0; j < Data.team_max; j++)
                        if (j != number - 1)
                            team[i].option[j] = false;
        }

        public void checkOneOption()
        {
            int t = -1;
            for (int i = 0; i < Data.team_max; i++)
            {
                t = -1;
                for (int j = 0; j < nr_of_teams; j++)
                    if (team[j].option[i])
                        if (t == -1)
                            t = j;
                        else
                        {
                            t = -1;
                            break;
                        }
                if (t >= 0 && team[t].number == 0)
                {
                    team[t].number = i + 1;
                    removeOptions(i + 1);
                    checkOneOption();
                }
            }
        }

        public League clone()
        {
            League l = new League();
            l.nr_of_teams = nr_of_teams;
            l.index = index;
            l.name = name;
            l.team = new Team[Data.team_max];
            l.field = field;
            return l;
        }
    }  
}
