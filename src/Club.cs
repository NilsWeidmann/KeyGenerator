using System;
using System.Collections.Generic;

namespace KeyGenerator
{
    public class Club : IComparable
    {
        public static TextFile file;
        public static TextFile backup;

        public String name;
        public int id;
        public int index;
        public Dictionary<char, int> keys;
        public List<Team> team;
        public int[] prio;
        public bool capacity;
        public List<Partnership> partnerships;

        public Club()
        {
            this.name = "";
            this.team = new List<Team>();
            this.keys = new Dictionary<char, int>();
            keys['A'] = keys['B'] = keys['X'] = keys['Y'] = 0;
            prio = new int[2];
            partnerships = new List<Partnership>();
        }

        public Club(String name, int id, int index) : this()
        {
            this.name = name;
            this.id = id;
            this.index = index;
        }

        public void setPrio()
        {
            prio[0] = prio[1] = 0;

            if (keys['A'] == 0 && keys['B'] == 0)
                for (int i = 0; i < team.Count; i++)
                    if ((team[i].week == 'A' || team[i].week == 'B') && team[i].key == 0)
                        prio[0]++;
            if (keys['X'] == 0 && keys['Y'] == 0)
                for (int i = 0; i < team.Count; i++)
                    if ((team[i].week == 'X' || team[i].week == 'Y') && team[i].key == 0)
                        prio[1]++;
        }

        public Club clone()
        {
            Club v = new Club();
            v.name = name;
            v.id = id;
            v.index = index;
            v.keys = new Dictionary<char, int>();
            foreach (char c in keys.Keys)
                v.keys[c] = keys[c];
            //Referenzen werden später gesetzt!
            v.team = new List<Team>();
            v.capacity = capacity;
            v.partnerships = new List<Partnership>();
            foreach (Partnership p in partnerships)
                v.partnerships.Add(new Partnership(p.indexA, p.weekA, p.indexB, p.weekB));
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
