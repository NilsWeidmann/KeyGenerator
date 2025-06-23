using System;
using System.Collections.Generic;
using System.Linq;

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

        public static Club[] getClubs(TextFile c, List<string> notification)
        {
            int nrOfClubs;
            String content = c.ReadLine(1, false, notification).Replace("\r", "");
            String[] help;
            for (nrOfClubs = 0; !content.Equals(""); nrOfClubs++)
                content = c.ReadLine(2 + nrOfClubs, false, notification).Replace("\r", "");
            Club[] club = new Club[nrOfClubs];

            for (int i = 0; i < nrOfClubs; i++)
            {
                content = c.ReadLine(1 + i, false, notification).Replace("\r", "");
                help = content.Split(';');
                club[i] = new Club
                {
                    name = help[0],
                    index = i,
                    team = new List<Team>()
                };
                try
                {
                    club[i].keys['A'] = Util.toInt(help[1]);
                    club[i].keys['B'] = Util.toInt(help[2]);
                    club[i].keys['X'] = Util.toInt(help[3]);
                    club[i].keys['Y'] = Util.toInt(help[4]);
                    club[i].capacity = help[5] == "X";
                    for (int j = 6; j < help.Length - 2; j += 3)
                        club[i].partnerships.Add(new Partnership(club[i], help[j], club[Util.toInt(help[j + 1])], help[j + 2]));
                }
                catch (Exception e)
                {
                    notification.Append(e.ToString());
                }
            }
            return club;
        }

        public static bool partnerOK(Club[] club, int clubIndex, int partnerIndex, bool ab, int[] key, int[] field, KeyMapper km)
        {
            int keyA = 0, keyB = 0, fieldA = 0, fieldB = 0;
            bool okay = true;

            if (ab)
            {
                club[clubIndex].keys['A'] = key[partnerIndex];
                club[clubIndex].keys['B'] = km.getOpposed(field[0], key[partnerIndex]);
            }
            else
            {
                club[clubIndex].keys['X'] = key[partnerIndex];
                club[clubIndex].keys['Y'] = km.getOpposed(field[1], key[partnerIndex]);
            }
            foreach (Partnership p in club[clubIndex].partnerships)
            {
                if (p.indexA != club[clubIndex].index && p.indexB != club[clubIndex].index)
                    continue;
                if (p.weekA != '-')
                {
                    keyA = club[p.indexA].keys[p.weekA];
                    fieldA = p.weekA == 'A' || p.weekA == 'B' ? field[0] : field[1];
                }
                if (p.weekB != '-')
                {
                    keyB = club[p.indexB].keys[p.weekB];
                    fieldB = p.weekB == 'A' || p.weekB == 'B' ? field[0] : field[1];
                }

                if (keyA == 0 || keyB == 0)
                    continue;
                if (km.getParallel(fieldA, fieldB, keyA).Contains(keyB))
                    continue;
                okay = false;
                break;
            }
            if (ab)
                club[clubIndex].keys['A'] = club[clubIndex].keys['B'] = 0;
            else
                club[clubIndex].keys['X'] = club[clubIndex].keys['Y'] = 0;
            return okay;
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
