using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class Team
    {
        public String name;
        public League league;
        public Club club;
        public String team;
        public int number = 0;
        public int index;
        public bool[] option;
        public int nrOfOptions;
        public char week = '-';
        public char[] day;

        public Team()
        {
            option = new bool[Data.team_max];
            day = new char[Data.team_max];
            for (int i = 0; i < Data.team_max; i++)
                day[i] = '-';
        }

        public void setNrOfOptions()
        {
            nrOfOptions = 0;
            for (int k = 0; k < Data.team_max; k++)
                if (option[k])
                    nrOfOptions++;
        }

        public void getNumber()
        {
            int number = 0;
            for (int i = 0; i < Data.team_max; i++)
                if (option[i])
                    if (number == 0)
                        number = i + 1;
                    else
                        return;
            this.number = number;
        }

        public bool hasAdditional()
        {
            for (int i = 0; i < day.Length; i++)
                if (day[i] == 'H' || day[i] == 'A')
                    return true;
            return false;
        }

        public bool numberOK(int number)
        {
            if (number == 0)
                return true;
            for (int i = 0; i < day.Length; i++)
                if (Data.schedule_1[league.field - 1, number - 1, i] == 'H' && day[i] == 'A'
                ||  Data.schedule_1[league.field - 1, number - 1, i] == 'A' && day[i] == 'H')
                    return false;
            return true;
        }

        public Team clone()
        {
            Team t = new Team();
            t.index = index;
            t.league = league;
            t.name = name;
            t.option = new bool[Data.team_max];
            for (int k = 0; k < Data.team_max; k++)
                t.option[k] = option[k];
            for (int k = 0; k < day.Length; k++)
                t.day[k] = day[k];
            t.week = week;
            t.team = team;
            t.number = number;
            return t;
        }
    }
}
