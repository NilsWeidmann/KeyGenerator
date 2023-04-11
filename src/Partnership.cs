using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class Partnership
    {
        public Club a;
        public char week_a;
        public Club b;
        public char week_b;

        public Partnership(Club a, string week_a, string name_b, string week_b, Club[] v_list)
        {
            Club b = null;
            for (int i = 0; i < v_list.Length; i++)
                if (v_list[i].name == name_b)
                    b = v_list[i];
            if (a.index > b.index)
            {
                this.a = a;
                this.b = b;
                this.week_a = week_a.ToCharArray()[0];
                this.week_b = week_b.ToCharArray()[0];
            }
            else
            {
                this.a = b;
                this.b = a;
                this.week_a = week_b.ToCharArray()[0];
                this.week_b = week_a.ToCharArray()[0];
            }
        }

        public Partnership(Club a, char woche_a, Club b, char woche_b)
        {
            if (a.index > b.index)
            {
                this.a = a;
                this.week_a = woche_a;
                this.b = b;
                this.week_b = woche_b;
            }
            else
            {
                this.a = b;
                this.week_a = woche_b;
                this.b = a;
                this.week_b = woche_a;
            }
        }
    }
}
