using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyGenerator
{
    public class Util
    {
        public struct Index
        {
            public int region;
            public int group;
            public int clubId;
            public int clubName;
            public int ageGroup;
            public int teamNo;

            public Index(int init)
            {
                region = group = clubId = clubName = ageGroup = teamNo = init;
            }
        };

        public static int toInt(String s)
        {
            char[] zeichen = s.ToCharArray();
            int r = 0;

            for (int i = 0; i < zeichen.Length; i++)
            {
                r *= 10;
                switch (zeichen[i])
                {
                    case '0': r += 0; break;
                    case '1': r += 1; break;
                    case '2': r += 2; break;
                    case '3': r += 3; break;
                    case '4': r += 4; break;
                    case '5': r += 5; break;
                    case '6': r += 6; break;
                    case '7': r += 7; break;
                    case '8': r += 8; break;
                    case '9': r += 9; break;
                    default: r /= 10; break;
                }
            }
            return r;
        }

        public static bool isRomanNumber(String s)
        {
            string pattern = "IVX";

            foreach (char c in s)
                if (pattern.IndexOf(c) < 0)
                    return false;

            return true;
        }

        public static string toRoman(string arabic)
        {
            int value = toInt(arabic);

            if ((value < 1) || (value >= Int32.MaxValue)) { return ""; }
            string res = "";

            while (value >= 1000) { value -= 1000; res += "M"; }
            if (value >= 900) { value -= 900; res += "CM"; }

            while (value >= 500) { value -= 500; res += "D"; }
            if (value >= 400) { value -= 400; res += "CD"; }

            while (value >= 100) { value -= 100; res += "C"; }
            if (value >= 90) { value -= 90; res += "XC"; }

            while (value >= 50) { value -= 50; res += "L"; }
            if (value >= 40) { value -= 40; res += "XL"; }

            while (value >= 10) { value -= 10; res += "X"; }
            if (value >= 9) { value -= 9; res += "IX"; }

            while (value >= 5) { value -= 5; res += "V"; }
            if (value >= 4) { value -= 4; res += "IV"; }

            while (value >= 1) { value -= 1; res += "I"; }

            return res;
        }

        public static String clear(String s)
        {
            s = s.Replace("Ä", "Ae");
            s = s.Replace("Ö", "Oe");
            s = s.Replace("Ü", "Ue");
            s = s.Replace("ä", "ae");
            s = s.Replace("ö", "oe");
            s = s.Replace("ü", "ue");
            s = s.Replace("ß", "ss");
            return s;
        }

        public static bool confirm(KeyGenerator caller, Group[] groups, Club[] clubs)
        {
            if (!(clubs == null || groups == null))
                switch (MessageBox.Show("Wollen Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        caller.prepare();
                        return true;
                    case DialogResult.Yes:
                        Data.save(groups,clubs, Club.file, Group.file, Team.file);
                        caller.loadFromFile(Club.file, Group.file, Team.file);
                        caller.prepare();
                        return true;
                    case DialogResult.Cancel:
                        return false;
                    default:
                        return false;
                }
            return true;
        }
    }
}
