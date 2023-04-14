using System;

namespace Schluesselzahlen
{
    public class KeyMapper
    {
        private int[,] similar_1;
        private int[,] similar_2;
        private int[,,] opposed_1;
        private int[,,] parallel_1;


        public KeyMapper(String path)
        {

            // Gegenläufig
            TextFile opposed = new TextFile(path + @"\Gegenlaeufig.csv");
            int[,] fieldOpposed = getField(opposed, 5);
            opposed_1 = new int[Data.TEAM_MAX, Data.TEAM_MAX, Data.TEAM_MAX];
            for (int i = 0; i < fieldOpposed.GetLength(0); i++)
                if (fieldOpposed[i, 0] != 0 && fieldOpposed[i, 1] != 0 && fieldOpposed[i, 2] != 0)
                    opposed_1[fieldOpposed[i, 0] - 1, fieldOpposed[i, 1] - 1, fieldOpposed[i, 2] - 1] = fieldOpposed[i, 3];

            // Parallel
            TextFile parallel = new TextFile(path + @"\Parallel.csv");
            int[,] fieldParallel = getField(parallel, 4);
            parallel_1 = new int[Data.TEAM_MAX, Data.TEAM_MAX, Data.TEAM_MAX];
            for (int i = 0; i < fieldParallel.GetLength(0); i++)
                if (fieldParallel[i, 0] != 0 && fieldParallel[i, 1] != 0 && fieldParallel[i, 2] != 0)
                    parallel_1[fieldParallel[i, 0] - 1, fieldParallel[i, 1] - 1, fieldParallel[i, 2] - 1] = fieldParallel[i, 3];
            for (int i = Data.TEAM_MIN; i <= Data.TEAM_MAX; i++)
                for (int j = 0; j < i; j++)
                    parallel_1[i - 1, i - 1, j] = j + 1;

            // Vereinsintern - multipel
            TextFile clubInternal = new TextFile(path + @"\Vereinsintern.csv");
            //int[,] feld_v = getField(vereinsintern, 5);

            // Ähnliche Zahlen
            TextFile similar = new TextFile(path + @"\Aehnlich.csv");
            int[,] fieldSimilar = getField(similar, 4);
            similar_1 = new int[Data.TEAM_MAX, Data.TEAM_MAX];
            similar_2 = new int[Data.TEAM_MAX, Data.TEAM_MAX];
            for (int i = 0; i < fieldSimilar.GetLength(0); i++)
            {
                similar_1[fieldSimilar[i, 0] - 1, fieldSimilar[i, 1] - 1] = fieldSimilar[i, 2];
                similar_2[fieldSimilar[i, 0] - 1, fieldSimilar[i, 1] - 1] = fieldSimilar[i, 3];
            }
        }

        private int[,] getField(TextFile file, int cols)
        {
            String content = file.ReadFile(true, Data.notification);
            String[] help;
            String[] row;
            char[] split = { '\n' };
            row = content.Split(split, StringSplitOptions.RemoveEmptyEntries);
            int[,] field = new int[row.Length, cols];
            for (int i = 0; i < row.Length; i++)
            {
                help = row[i].Split(';');
                for (int j = 0; j < help.Length && j < cols; j++)
                    field[i, j] = Util.toInt(help[j]);
            }
            return field;
        }

        public int getParallel(int fieldFrom, int fieldTo, int keyFrom)
        {
            return parallel_1[fieldFrom - 1, fieldTo - 1, keyFrom - 1];
        }

        public int getOpposed(int fieldFrom, int fieldTo, int keyTo)
        {
            return opposed_1[fieldFrom - 1, fieldTo - 1, keyTo - 1];
        }

        public int[] getSimilar(int field, int key)
        {
            return new int[] { similar_1[field - 1, key - 1], similar_2[field - 1, key - 1] };
        }
    }
}
