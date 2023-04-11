using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    class UnitTests
    {
        public bool allTeamsHaveDifferentNumbers(League[] l)
        {
            foreach (League league in l)
                for (int i = 0; i < league.team.Length; i++)
                    for (int j = i + 1; j < league.team.Length; j++)
                        if (league.team[i].number == league.team[j].number)
                            return false;
            return true;
        }

        public bool allTeamsHaveValidNumbers(League[] l)
        {
            foreach (League league in l)
                for (int i = 0; i < league.team.Length; i++)
                    if (league.team[i].number < 1 || league.team[i].number > league.field)
                        return false;
            return true;
        }

        public bool checkValidity(Club[] v)
        {
            return true;
        }
    }
}
