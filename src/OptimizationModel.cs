using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Google.OrTools.Graph;
using Google.OrTools.Init;
using Google.OrTools.Sat;
using Google.Protobuf.WellKnownTypes;

namespace KeyGenerator
{
    public class OptimizationModel
    {
        // Configuration
        private KeyMapper km;
        private int TEAM_MAX;
        private char[] week;
        private int[] field;

        // Binary Variables
        private IntVar[,,] t;
        private IntVar[,,] c;
        private IntVar[,] x;

        // Solver
        private CpModel model;
        private CpSolver solver;
        private int runtime;

        public OptimizationModel(Group[] group, Club[] club, char[] week, int[] field, int runtime, KeyMapper km, int TEAM_MAX)
        {
            this.week = week;
            this.field = field;
            this.km = km;
            this.TEAM_MAX = TEAM_MAX;

            model = new CpModel();
            solver = new CpSolver();
            solver.StringParameters = "max_time_in_seconds:" + runtime + ".0";
            this.runtime = runtime;

            if (model is null || solver is null)
            {
                Console.WriteLine("Optimierung konnte nicht gestartet werden: Solver nicht gefunden.");
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
            t = new IntVar[group.Length, TEAM_MAX, TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    for (int k = 0; k < group[i].field; k++)
                        t[i, j, k] = model.NewIntVar(0, 1, group[i].team[j].name + " in group " + group[i].name + " has key " + (k + 1));

            // Variables for clubs
            c = new IntVar[club.Length, week.Length, TEAM_MAX];

            for (int i = 0; i < club.Length; i++)
                for (int j=0; j< week.Length; j++)
                    for (int k = 0; k < field[j / 2]; k++) 
                        c[i, j, k] = model.NewIntVar(0, 1, club[i].name + " has key " + (k + 1) + " in week " + week[j]);
                

            // Variables for conflicts
            x = new IntVar[group.Length, TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    x[i, j] = model.NewIntVar(0, 1, "Conflict for team " + group[i].team[j].name + " in group " + group[i].name);
        }

        private void convertSolution(Group[] group, Club[] club)
        {
            for (int i = 0; i < club.Length; i++) { 
                for (int j = 0; j < week.Length; j++)
                    for (int k = 0; k < field[j / 2]; k++) 
                        if (solver.Value(c[i, j, k]) == 1)
                            club[i].keys[week[j]] = k + 1;

                foreach (Team team in club[i].team)
                {
                    if (team.week != '-')
                    {
                        int f = -1;
                        for (int w = 0; w < week.Length; w++)
                            if (week[w] == team.week)
                                f = field[w / 2];
                        int key = km.getParallel(f, team.group.field, club[i].keys[team.week]);
                        team.key = key;
                    }
                }
            }
        }

        private void fixAssignments(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
                for (int j=0; j<week.Length; j++)
                    if (club[i].keys[week[j]] != 0)
                        model.Add(c[i, j, club[i].keys[week[j]] - 1] == 1);
        }

        private void clubHasOneKeyPerWeekScheme(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
                for (int j = 0; j < week.Length; j++)
                {
                    List<LinearExpr> sum = new List<LinearExpr>();

                    for (int k = 0; k < field[j / 2]; k++)
                        sum.Add(c[i, j, k]);
                    model.Add(LinearExpr.Sum(sum) == 1);
                }
            
        }

        private void teamHasOneKey(Group[] group)
        {
            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                {
                    List<LinearExpr> sum = new List<LinearExpr>();
                    for (int k = 0; k < group[i].field; k++)
                        sum.Add(t[i, j, k]);
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
                        sum.Add(t[i, j, k]);
                    model.Add(LinearExpr.Sum(sum) <= 1);
                }
        }

        private void oppositeKeysForOppositeWeekSchemes(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
                for (int j=0; j<field.Length; j++)
                    for (int k = 0; k < field[j]; k++)
                        model.Add(c[i, 2*j, k] - c[i, 2*j+1, km.getOpposed(field[j], k + 1) - 1] == 0);
        }

        private void keySimilarity(Group[] group, Club[] club)
        { 
            for (int i = 0; i < club.Length; i++)
            {
                foreach (Team team in club[i].team)
                {
                    if (team.week == '-')
                        continue;

                    int f = -1;
                    int w = -1;
                    for (int j = 0; j < week.Length; j++)
                        if (week[j] == team.week)
                        {
                            f = field[j / 2];
                            w = j;
                        }

                    for (int k=0; k<f; k++) {

                        int p = km.getParallel(f, team.group.field, k + 1);
                        Tuple<int, int> s = km.getSimilar(team.group.field, p);

                        model.Add(c[i, w, k] - t[team.group.index, team.index, p - 1] 
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

                    int f = -1;
                    int w = -1;
                    for (int j = 0; j < week.Length; j++)
                        if (week[j] == team.week)
                        {
                            f = field[j / 2];
                            w = j;
                        }

                    for (int k = 0; k < f; k++)
                    {
                        int p = km.getParallel(f, team.group.field, k + 1);
                        model.Add(t[team.group.index, team.index, p - 1] - x[team.group.index, team.index] - c[i, w, k] <= 0);
                    }
                }
            }
        }
        private void setObjective(Group[] group)
        {
            List<LinearExpr> summands = new List<LinearExpr>();

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    summands.Add(x[i, j]);

            LinearExpr objective = LinearExpr.Sum(summands);
            model.Minimize(objective);
        }

        public void findSolution(Group[] group, Club[] club, int[] conflicts, BackgroundWorker bw)
        {
            KeyGeneratorSolutionCallback callback = new KeyGeneratorSolutionCallback(bw, runtime);
            CpSolverStatus status = solver.Solve(model, callback);

            if (status != CpSolverStatus.Optimal && status != CpSolverStatus.Feasible)
                conflicts[1] = -1;
            else
            {
                convertSolution(group, club);
                conflicts[1] = (int)solver.ObjectiveValue;
            }
        }

        class KeyGeneratorSolutionCallback : CpSolverSolutionCallback
        {
            private BackgroundWorker bw;
            private DateTime start;
            private int runtime;
            private int progress;

            public KeyGeneratorSolutionCallback(BackgroundWorker bw, int runtime)
            {
                this.bw = bw;
                this.runtime = runtime;
                this.start = DateTime.Now;
                this.progress = 0;
            }

            public override void OnSolutionCallback()
            {
                double d = ObjectiveValue();
                Data.currentConflicts = (int)d;
                reportProgress(bw, runtime, start, progress);
            }
            private static int reportProgress(BackgroundWorker bw, int runtime, DateTime start, int progress)
            {
                if (progress < 100 * (int)(DateTime.Now - start).TotalSeconds / runtime)
                {
                    progress = 100 * (int)(DateTime.Now - start).TotalSeconds / runtime;
                    bw.ReportProgress(progress > 100 ? 100 : progress);
                    if (progress >= 100)
                        bw.CancelAsync();
                }
                return progress;
            }
        }
    }
}