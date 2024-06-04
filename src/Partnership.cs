namespace KeyGenerator
{
    public class Partnership
    {
        public int indexA;
        public char weekA;
        public int indexB;
        public char weekB;

        public Partnership(Club clubA, string weekA, Club clubB, string weekB)
        {
            this.indexA = clubA.index;
            this.indexB = clubB.index;
            this.weekA = weekA.ToCharArray()[0];
            this.weekB = weekB.ToCharArray()[0];
        }

        public Partnership(Club clubA, char weekA, Club clubB, char weekB)
        {
            this.indexA = clubA.index;
            this.weekA = weekA;
            this.indexB = clubB.index;
            this.weekB = weekB;
        }

        public Partnership(int indexA, char weekA, int indexB, char weekB) 
        {
            this.indexA = indexA;
            this.weekA = weekA;
            this.indexB = indexB;
            this.weekB = weekB;
        }
    }
}
