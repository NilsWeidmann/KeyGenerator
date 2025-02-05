using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.Graph;
using Google.OrTools.Init;
using Google.OrTools.Sat;
using Google.Protobuf.WellKnownTypes;

namespace KeyGenerator
{
    public class OptimizationModel
    {
        // Constants
        public const int NR_OF_WEEKS = 4;

        // Binary Variables
        private IntVar[,,] t;
        private IntVar[,,] c;
        private IntVar[,] x;

        // Constraints

        // Solver
        private CpModel model;
        private CpSolver solver;
        private BackgroundWorker bw;

        public OptimizationModel(Group[] group, Club[] club, BackgroundWorker bw, int runtime)
        {
            this.bw = bw;
            model = new CpModel();
            solver = new CpSolver();

            if (model is null || solver is null)
            {
                Data.notification.Append("Optimierung konnte nicht gestartet werden: Solver nicht gefunden.");
                return;
            }
            createVariables(group, club);

            clubHasOneKeyPerWeekScheme(club);
            teamHasOneKey(group);
            uniqueKeysPerDivision(group);
            oppositeKeysForOppositeWeekSchemes(club);
            fixAssignments(club);
            keySimilarity(group, club);
            confllictDetection(group, club);

            setObjective(group);

            //TODO Callback
            //solver.SetTimeLimit(runtime * 1000);
        }

        private void createVariables(Group[] group, Club[] club)
        {
            // Variables for teams
            t = new IntVar[group.Length, Data.TEAM_MAX, Data.TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    for (int k = 0; k < group[i].field; k++)
                        t[i, j, k] = model.NewIntVar(0, 1, group[i].team[j].name + " in group " + group[i].name + " has key " + (k + 1));

            // Variables for clubs
            c = new IntVar[club.Length, NR_OF_WEEKS, Data.TEAM_MAX];

            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++) // Woche A
                    c[i, 0, j] = model.NewIntVar(0, 1, club[i].name + " has key " + (j + 1) + " in week A");
                for (int j = 0; j < Data.field[0]; j++) // Woche B
                    c[i, 1, j] = model.NewIntVar(0, 1, club[i].name + " has key " + (j + 1) + " in week B");
                for (int j = 0; j < Data.field[1]; j++) // Woche X
                    c[i, 2, j] = model.NewIntVar(0, 1, club[i].name + " has key " + (j + 1) + " in week X");
                for (int j = 0; j < Data.field[1]; j++) // Woche Y
                    c[i, 3, j] = model.NewIntVar(0, 1, club[i].name + " has key " + (j + 1) + " in week Y");
            }

            // Variables for conflicts
            x = new IntVar[group.Length, Data.TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    x[i, j] = model.NewIntVar(0, 1, "Conflict for team " + group[i].team[j].name + " in group " + group[i].name);
        }


        public void findSolution(Group[] group, Club[] club, int[] conflicts)
        {

            CpSolverStatus status = solver.Solve(model);

            if (status != CpSolverStatus.Optimal && status != CpSolverStatus.Feasible)
            {
                conflicts[1] = -1;
            }
            else
            {
                convertSolution(group, club);
                conflicts[1] = (int)solver.ObjectiveValue; 
            }
        }

        private void convertSolution(Group[] group, Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++) // Woche A
                    if (solver.Value(c[i, 0, j]) == 1)
                        club[i].keys['A'] = j + 1;
                for (int j = 0; j < Data.field[0]; j++) // Woche B
                    if (solver.Value(c[i, 1, j]) == 1)
                        club[i].keys['B'] = j + 1;
                for (int j = 0; j < Data.field[1]; j++) // Woche X
                    if (solver.Value(c[i, 2, j]) == 1)
                        club[i].keys['X'] = j + 1;
                for (int j = 0; j < Data.field[1]; j++) // Woche Y
                    if (solver.Value(c[i, 3, j]) == 1)
                        club[i].keys['Y'] = j + 1;

                foreach (Team team in club[i].team)
                {
                    if (team.week != '-')
                    {
                        int field = team.week == 'A' || team.week == 'B' ? Data.field[0] : Data.field[1];
                        int key = Data.km.getParallel(field, team.group.field, club[i].keys[team.week]);
                        team.key = key;
                    }
                }
            }
        }

        private void fixAssignments(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                if (club[i].keys['A'] != 0)
                {
                    model.Add(-c[i, 0, club[i].keys['A'] - 1] <= -1);
                }
                if (club[i].keys['B'] != 0)
                {
                    model.Add(-c[i, 1, club[i].keys['B'] - 1] <= -1);
                }
                if (club[i].keys['X'] != 0)
                {
                    model.Add(-c[i, 2, club[i].keys['X'] - 1] <= -1);
                }
                if (club[i].keys['Y'] != 0)
                {
                    model.Add(-c[i, 3, club[i].keys['Y'] - 1] <= -1);
                }
            }
        }
        private void setObjective(Group[] group)
        {
            List<LinearExpr> summands = new List<LinearExpr>();

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    summands.Add(x[i,j]);

            LinearExpr objective = LinearExpr.Sum(summands);
            model.Minimize(objective);
        }

        private void clubHasOneKeyPerWeekScheme(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                List<LinearExpr> sumA = new List<LinearExpr>();

                for (int j = 0; j < Data.field[0]; j++)
                {
                    sumA.Add(c[i, 0, j]);
                }
                model.Add(LinearExpr.Sum(sumA) == 1);

                List<LinearExpr> sumB = new List<LinearExpr>();
                for (int j = 0; j < Data.field[0]; j++)
                {
                    sumB.Add(c[i, 1, j]);
                }
                model.Add(LinearExpr.Sum(sumB) == 1);

                List<LinearExpr> sumX = new List<LinearExpr>();
                for (int j = 0; j < Data.field[1]; j++)
                {
                    sumX.Add(c[i, 2, j]);
                }
                model.Add(LinearExpr.Sum(sumX) == 1);

                List<LinearExpr> sumY = new List<LinearExpr>();
                for (int j = 0; j < Data.field[1]; j++)
                {
                    sumY.Add(c[i, 3, j]);
                }
                model.Add(LinearExpr.Sum(sumY) == 1);
            }
        }
        private void teamHasOneKey(Group[] group)
        {
            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                {
                    List<LinearExpr> sum = new List<LinearExpr>();
                    for (int k = 0; k < group[i].field; k++)
                    {
                        sum.Add(t[i, j, k]);
                    }
                    model.Add(LinearExpr.Sum(sum) == 1);
                }
        }

        private void uniqueKeysPerDivision(Group[] group)
        {
            for (int i = 0; i < group.Length; i++) 
                for (int k = 0; k < group[i].field; k++)
                {
                    List<LinearExpr> sum = new List<LinearExpr>();
                    for (int j = 0; j < group[i].nrOfTeams; j++)
                    {
                        sum.Add(t[i, j, k]);
                    }
                    model.Add(LinearExpr.Sum(sum) <= 1);
                }
        }

        private void oppositeKeysForOppositeWeekSchemes(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++)
                {
                    model.Add(c[i, 0, j] - c[i, 1, Data.km.getOpposed(Data.field[0], j + 1) - 1] == 0);
                }
                for (int j = 0; j < Data.field[1]; j++)
                {
                    model.Add(c[i, 2, j] - c[i, 3, Data.km.getOpposed(Data.field[1], j + 1) - 1] == 0);
                }
            }
        }

        private void keySimilarity(Group[] group, Club[] club)
        { 
            for (int i = 0; i < club.Length; i++)
            {
                foreach (Team team in club[i].team)
                {
                    if (team.week == '-')
                        continue;

                    int field = team.week == 'A' || team.week == 'B' ? Data.field[0] : Data.field[1];
                    int j = team.week == 'A' ? 0 : team.week == 'B' ? 1 : team.week == 'X' ? 2 : 3;

                    for (int k=0; k<field; k++) {

                        int p = Data.km.getParallel(field, team.group.field, k + 1);
                        Tuple<int, int> s = Data.km.getSimilar(team.group.field, p);

                        model.Add(c[i, j, k] - t[team.group.index, team.index, p - 1] 
                                             - t[team.group.index, team.index, s.Item1 - 1] 
                                             - t[team.group.index, team.index, s.Item2 - 1] <= 0);
                    }
                }
            }
        }
        private void confllictDetection(Group[] group, Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                foreach (Team team in club[i].team)
                {
                    if (team.week == '-')
                        continue;

                    int field = team.week == 'A' || team.week == 'B' ? Data.field[0] : Data.field[1];
                    int j = team.week == 'A' ? 0 : team.week == 'B' ? 1 : team.week == 'X' ? 2 : 3;

                    for (int k = 0; k < field; k++)
                    {
                        int p = Data.km.getParallel(field, team.group.field, k + 1);
                        model.Add(t[team.group.index, team.index, p - 1] - x[team.group.index, team.index] - c[i, j, k] <= 0);
                    }
                }
            }
        }
    }
}