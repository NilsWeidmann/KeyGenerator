using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schluesselzahlen
{
    public class Partnership
    {
        public Club clubA;
        public char weekA;
        public Club clubB;
        public char weekB;

        public Partnership(Club clubA, string weekA, string nameB, string weekB, Club[] clubList)
        {
            Club clubB = null;
            for (int i = 0; i < clubList.Length; i++)
                if (clubList[i].name == nameB)
                    clubB = clubList[i];
            if (clubA.index > clubB.index)
            {
                this.clubA = clubA;
                this.clubB = clubB;
                this.weekA = weekA.ToCharArray()[0];
                this.weekB = weekB.ToCharArray()[0];
            }
            else
            {
                this.clubA = clubB;
                this.clubB = clubA;
                this.weekA = weekB.ToCharArray()[0];
                this.weekB = weekA.ToCharArray()[0];
            }
        }

        public Partnership(Club clubA, char weekA, Club clubB, char weekB)
        {
            if (clubA.index > clubB.index)
            {
                this.clubA = clubA;
                this.weekA = weekA;
                this.clubB = clubB;
                this.weekB = weekB;
            }
            else
            {
                this.clubA = clubB;
                this.weekA = weekB;
                this.clubB = clubA;
                this.weekB = weekA;
            }
        }
    }
}
