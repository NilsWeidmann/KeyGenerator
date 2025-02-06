using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGenerator
{
    public class InstanceGenerator
    {
        // Configuration
        static KeyMapper km;
        static int runtime = 120;
        const int TEAM_MIN = 6;
        const int TEAM_MAX = 14;
        
        public InstanceGenerator()
        {
            km = createKeyMapper();

            Tuple<Group[], Club[]> data = generate(50, 10, 100, ['A','B','X','Y'], 0.7, 0.15);
            OptimizationModel om = new OptimizationModel(data.Item1, data.Item2, ['A', 'B', 'X', 'Y'], [10, 10], runtime, km, 10);
            int[] conflicts = [-1, -1];
            om.findSolution(data.Item1, data.Item2, conflicts);
        }

        private static KeyMapper createKeyMapper()
        {
            Tuple<int, int>[,] similar = new Tuple<int,int>[TEAM_MAX, TEAM_MAX];

            for (int i = TEAM_MIN; i <= TEAM_MAX; i += 2)
                for (int j = 1; j <= i; j++)
                    similar[i-1, j-1] = new Tuple<int, int>(j % i + 1, (j + i - 2) % i + 1);

            int[,] opposed = new int[TEAM_MAX, TEAM_MAX];

            for (int i = TEAM_MIN; i <= TEAM_MAX; i += 2)
                for (int j = 1; j <= i; j++)
                    opposed[i-1, j-1] = (j-1 + i / 2) % i + 1;

            int[,,] parallel = new int[TEAM_MAX, TEAM_MAX, TEAM_MAX];

            for (int i = TEAM_MIN; i <= TEAM_MAX; i += 2)
                for (int j = i; j <= TEAM_MAX; j += 2)
                    for (int k=0, l=1; l<=j/2; l++)
                    {
                        if (k < i / 2)
                        {
                            k++;
                            parallel[i - 1, j - 1, k - 1] = l;
                            parallel[i - 1, j - 1, opposed[i - 1, k - 1] - 1] = opposed[j - 1, l - 1];
                        }

                        parallel[j - 1, i - 1, l - 1] = k;
                        parallel[j - 1, i - 1, opposed[j - 1, l - 1] - 1] = opposed[i - 1, k - 1];
                    }
                        

            return new KeyMapper(similar, opposed, parallel, null);
        }

        private static Tuple<Group[],Club[]> generate(int nrOfGroups, int nrOfTeamsPerGroup, int nrOfClubs, char[] week, double portionOfDependentTeams, double portionOfFixedKeys)
        {
            Random r = new Random();

            Group[] group = new Group[nrOfGroups];
            Club[] club = new Club[nrOfClubs];

            for (int i=0; i<nrOfClubs; i++)
            {
                club[i] = new Club();
                club[i].index = i;

                if (r.NextDouble() < portionOfFixedKeys)
                {
                    for (int j=0; j<week.Length; j+=2)
                    {
                        club[i].keys[week[j]] = (int)(r.NextDouble() * nrOfTeamsPerGroup) + 1;
                        club[i].keys[week[j + 1]] = km.getOpposed(nrOfTeamsPerGroup, club[i].keys[week[j]]);
                    }
                }
            }
            for (int i=0; i<nrOfGroups; i++)
            {
                group[i] = new Group();
                group[i].index = i;
                group[i].field = nrOfTeamsPerGroup;
                group[i].nrOfTeams = nrOfTeamsPerGroup;
                group[i].team = new Team[nrOfTeamsPerGroup];

                for (int j=0; j<nrOfTeamsPerGroup; j++)
                {
                    group[i].team[j] = new Team();
                    group[i].team[j].index = j;
                    group[i].team[j].group = group[i];
                    group[i].team[j].club = club[(int)(r.NextDouble() * nrOfClubs)];
                    group[i].team[j].club.team.Add(group[i].team[j]);

                    if (r.NextDouble() < portionOfDependentTeams)
                        group[i].team[j].week = week[(int)(r.NextDouble() * week.Length)];
                    else
                        group[i].team[j].week = '-';
                }
            }

            return new Tuple<Group[], Club[]>(group,club);
        }
    }
}
