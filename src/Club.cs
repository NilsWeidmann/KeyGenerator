using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class Club : IComparable
    {
        public String name;
        public int id;
        public int index;
        public int a;
        public int b;
        public int x;
        public int y;
        public List<Team> team;
        public int prio;
        public bool capacity;

        public Club() { 
        }
        
        public Club(String name, int id, int index)
        {
            this.name = name;
            this.id = id;
            this.team = new List<Team>();
        }

        public void setPrio(List<Partnership> partnership)
        {
            prio = 0;
            for (int i = 0; i < team.Count; i++)
                if (team[i].week != '-' && team[i].number == 0)
                    prio++;
            foreach (Partnership p in partnership)
                if (p.a.index == index)
                {
                    for (int i = 0; i < p.b.team.Count; i++)
                        if (p.b.team[i].week != '-' && p.b.team[i].number == 0)
                            prio++;
                }
                else if (p.b.index == index)
                {
                    for (int j = 0; j < p.a.team.Count; j++)
                        if (p.a.team[j].week != '-' && p.a.team[j].number == 0)
                            prio++;
                }
        }

        public Club clone()
        {
            Club v = new Club();
            v.name = name;
            v.id = id;
            v.index = index;
            v.a = a;
            v.b = b;
            v.x = x;
            v.y = y;
            //Referenzen werden später gesetzt!
            v.team = new List<Team>();
            v.capacity = capacity;
            return v;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !obj.GetType().Equals(this.GetType()))
                return false;
            else 
                return ((Club)obj).name.Equals(name);
        }

        public override int GetHashCode()
        {
            return id;
        }

        public int CompareTo(object obj)
        {
            if ((obj == null) || !obj.GetType().Equals(this.GetType()))
                return 0;
            else
                return id - ((Club)obj).id;
        }
    }
}
