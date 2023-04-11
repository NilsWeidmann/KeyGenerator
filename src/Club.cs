using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class Club
    {
        public String name;
        public int index;
        public int a;
        public int b;
        public int x;
        public int y;
        public Team[] team;
        public int prio;
        public bool capacity;

        public void setPrio(List<Partnership> partnership)
        {
            prio = 0;
            for (int i = 0; i < team.Length; i++)
                if (team[i].week != '-' && team[i].number == 0)
                    prio++;
            foreach (Partnership p in partnership)
                if (p.a.index == index)
                {
                    for (int i = 0; i < p.b.team.Length; i++)
                        if (p.b.team[i].week != '-' && p.b.team[i].number == 0)
                            prio++;
                }
                else if (p.b.index == index)
                {
                    for (int j = 0; j < p.a.team.Length; j++)
                        if (p.a.team[j].week != '-' && p.a.team[j].number == 0)
                            prio++;
                }
        }

        public Club clone()
        {
            Club v = new Club();
            v.name = name;
            v.index = index;
            v.a = a;
            v.b = b;
            v.x = x;
            v.y = y;
            v.team = new Team[team.Length];
            v.capacity = capacity;
            return v;
        }
    }
}
