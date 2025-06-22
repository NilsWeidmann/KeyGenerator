using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace KeyGenerator
{
    public class KeyMapper
    {
        private Tuple<int,int>[,] similar;
        private int /*List<int>*/[,] opposed;
        private List<int>[,,] parallel;
        private char[,,] schedule;

        public KeyMapper(Tuple<int, int>[,] similar, int[,] opposed, List<int>[,,] parallel, char[,,] schedule)
        {
            this.similar = similar;
            this.opposed = opposed;
            this.parallel = parallel;
            this.schedule = schedule;
        }

        public KeyMapper(String path)
        {

            // Gegenläufig
            TextFile fileOpposed = new TextFile(path + @"\Gegenlaeufig.csv");
            int[,] fieldOpposed = getField(fileOpposed, 4);
            opposed = new int /*List<int>*/[Data.TEAM_MAX, Data.TEAM_MAX];
            /*for (int i = 0; i < Data.TEAM_MAX; i++)
                for (int j = 0; j < Data.TEAM_MAX; j++)
                    opposed[i, j] = new List<int>();*/
            for (int i = 0; i < fieldOpposed.GetLength(0); i++)
                if (fieldOpposed[i, 0] != 0 && fieldOpposed[i, 1] != 0)
                    opposed[fieldOpposed[i, 0] - 1, fieldOpposed[i, 1] - 1] = fieldOpposed[i, 2];

            // Parallel
            TextFile fileParallel = new TextFile(path + @"\Parallel.csv");
            int[,] fieldParallel = getField(fileParallel, 4);
            parallel = new List<int>[Data.TEAM_MAX, Data.TEAM_MAX, Data.TEAM_MAX];
            for (int i = 0; i < Data.TEAM_MAX; i++)
                for (int j = 0; j < Data.TEAM_MAX; j++)
                    for (int k = 0; k < Data.TEAM_MAX; k++)
                        parallel[i, j, k] = new List<int>();
            for (int i = 0; i < fieldParallel.GetLength(0); i++)
                if (fieldParallel[i, 0] != 0 && fieldParallel[i, 1] != 0 && fieldParallel[i, 2] != 0)
                    parallel[fieldParallel[i, 0] - 1, fieldParallel[i, 1] - 1, fieldParallel[i, 2] - 1].Add(fieldParallel[i, 3]);
            for (int i = Data.TEAM_MIN; i <= Data.TEAM_MAX; i++)
                for (int j = 0; j < i; j++)
                    parallel[i - 1, i - 1, j].Add(j + 1);

            // Ähnliche Zahlen
            TextFile fileSimilar = new TextFile(path + @"\Aehnlich.csv");
            int[,] fieldSimilar = getField(fileSimilar, 4);
            similar = new Tuple<int,int>[Data.TEAM_MAX, Data.TEAM_MAX];
            for (int i = 0; i < fieldSimilar.GetLength(0); i++)
                similar[fieldSimilar[i, 0] - 1, fieldSimilar[i, 1] - 1] = Tuple.Create(fieldSimilar[i, 2], fieldSimilar[i, 3]);

            // Spielplan
            TextFile schedule = new TextFile(path + @"\Spielplan.csv");
            this.schedule = getSchedule(schedule);
            
        }

        private char[,,] getSchedule(TextFile file)
        {
            String content = file.ReadFile(false, Data.notification);
            String[] help;
            char[] split = { '\n' };

            char[,,] schedule = new char[Data.TEAM_MAX, Data.TEAM_MAX, Data.TEAM_MAX];
            String[] row = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < row.Length; i++)
            {
                help = row[i].Split(';');
                for (int j = 2; j < help.Length; j++)
                    schedule[Util.toInt(help[0]) - 1, Util.toInt(help[1]) - 1, j - 2] = help[j].ToCharArray()[0];
            }
            return schedule;
        }

        private int[,] getField(TextFile file, int cols)
        {
            String content = file.ReadFile(true, Data.notification);
            String[] help;
            char[] split = { '\n' };

            String[] row = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            int[,] field = new int[row.Length, cols];
            for (int i = 0; i < row.Length; i++)
            {
                help = row[i].Split(';');
                for (int j = 0; j < help.Length && j < cols; j++)
                    field[i, j] = Util.toInt(help[j]);
            }
            return field;
        }

        public char getDay(int field, int key, int weekNumber)
        {
            return schedule[field - 1, key - 1, weekNumber];
        }

        public List<int> getParallel(int fieldFrom, int fieldTo, int keyFrom)
        {
            //if (parallel[fieldFrom - 1, fieldTo - 1, keyFrom - 1].Count == 1)
                return parallel[fieldFrom - 1, fieldTo - 1, keyFrom - 1];//.First();
            //else
                //return parallel[fieldFrom - 1, fieldTo - 1, keyFrom - 1].Last();
        }

        public int getOpposed(int field, int key)
        {
            //if (opposed[field - 1, key - 1].Count == 1)
                return opposed[field - 1, key - 1];//.First();
            //else
                //return opposed[field - 1, key - 1].Last();
        }

        public Tuple<int,int> getSimilar(int field, int key)
        {
            return similar[field - 1, key - 1];
        }
    }
}
