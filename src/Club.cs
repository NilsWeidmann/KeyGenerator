using System;
using System.Collections.Generic;

namespace Schluesselzahlen
{
    public class Club : IComparable
    {
        public static TextFile file;
        public static TextFile backup;

        public String name;
        public int id;
        public int index;
        public int a;
        public int b;
        public int x;
        public int y;
        public List<Team> team;
        public int prioAB;
        public int prioXY;
        public bool capacity;

        public Club()
        {
            this.team = new List<Team>();
        }

        public Club(String name, int id, int index) : this()
        {
            this.name = name;
            this.id = id;
            this.index = index;

        }

        public void setPrio(List<Partnership> partnership)
        {
            prioAB = prioXY = 0;

            if (a == 0 && b == 0)
                for (int i = 0; i < team.Count; i++)
                    if ((team[i].week == 'A' || team[i].week == 'B') && team[i].key == 0)
                        prioAB++;
            if (x == 0 && y == 0)
                for (int i = 0; i < team.Count; i++)
                    if ((team[i].week == 'X' || team[i].week == 'Y') && team[i].key == 0)
                        prioXY++;

            // Partnerschaften der Einfachheit halber weglassen
            /*foreach (Partnership p in partnership)
                if (p.clubA.index == index)
                {
                    for (int i = 0; i < p.clubB.team.Count; i++)
                        if (p.clubB.team[i].week != '-' && p.clubB.team[i].key == 0)
                            prio++;
                }
                else if (p.clubB.index == index)
                {
                    for (int j = 0; j < p.clubA.team.Count; j++)
                        if (p.clubA.team[j].week != '-' && p.clubA.team[j].key == 0)
                            prio++;
                }*/
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
