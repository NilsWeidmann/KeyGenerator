using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.Init;
using Google.OrTools.LinearSolver;

namespace KeyGenerator
{
    public class OptimizationModel
    {
        // Constants
        public const int NR_OF_WEEKS = 4;

        // Binary Variables
        private Variable[,,] t;
        private Variable[,,] c;
        private Variable[,] x;

        // Constraints

        // Solver
        private Solver solver;
        private BackgroundWorker bw;

        public OptimizationModel(Group[] group, Club[] club, BackgroundWorker bw, int runtime)
        {
            this.bw = bw;
            solver = Solver.CreateSolver("GLOP");
            if (solver is null)
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

            setObjective(group);

            solver.SetTimeLimit(runtime * 1000);
        }

        private void createVariables(Group[] group, Club[] club)
        {
            // Variables for teams
            t = new Variable[group.Length, Data.TEAM_MAX, Data.TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    for (int k = 0; k < group[i].field; k++)
                        t[i, j, k] = solver.MakeIntVar(0.0, 1.0, group[i].team[j].name + " in group " + group[i].name + " has key " + k + 1);

            // Variables for clubs
            c = new Variable[club.Length, NR_OF_WEEKS, Data.TEAM_MAX];

            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++) // Woche A
                    c[i, 0, j] = solver.MakeIntVar(0.0, 1.0, club[i].name + " has key " + (j + 1) + " in week A");
                for (int j = 0; j < Data.field[0]; j++) // Woche B
                    c[i, 1, j] = solver.MakeIntVar(0.0, 1.0, club[i].name + " has key " + (j + 1) + " in week B");
                for (int j = 0; j < Data.field[1]; j++) // Woche X
                    c[i, 2, j] = solver.MakeIntVar(0.0, 1.0, club[i].name + " has key " + (j + 1) + " in week X");
                for (int j = 0; j < Data.field[1]; j++) // Woche Y
                    c[i, 3, j] = solver.MakeIntVar(0.0, 1.0, club[i].name + " has key " + (j + 1) + " in week Y");
            }

            // Variables for conflicts
            x = new Variable[group.Length, Data.TEAM_MAX];

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    x[i, j] = solver.MakeIntVar(0.0, 1.0, "Conflict for team " + group[i].team[j].name + " in group " + group[i].name);
        }


        public void findSolution(Group[] group, Club[] club, int[] conflicts)
        {
            
            Solver.ResultStatus resultStatus = solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL && resultStatus != Solver.ResultStatus.FEASIBLE)
            {
                conflicts[1] = -1;
            }
            else
            {
                convertSolution(group, club);
                conflicts[1] = (int)solver.Objective().Value();
            }
        }

        private void convertSolution(Group[] group, Club[] club)
        {
            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    for (int k = 0; k < group[i].field; k++)
                        if ((int)t[i, j, k].SolutionValue() == 1)
                            group[i].team[j].key = k + 1;

            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++) // Woche A
                    if ((int)c[i, 0, j].SolutionValue() == 1)
                        club[i].keys['A'] = j + 1;
                for (int j = 0; j < Data.field[0]; j++) // Woche B
                    if ((int)c[i, 1, j].SolutionValue() == 1)
                        club[i].keys['B'] = j + 1;
                for (int j = 0; j < Data.field[1]; j++) // Woche X
                    if ((int)c[i, 2, j].SolutionValue() == 1)
                        club[i].keys['X'] = j + 1;
                for (int j = 0; j < Data.field[1]; j++) // Woche Y
                    if ((int)c[i, 3, j].SolutionValue() == 1)
                        club[i].keys['Y'] = j + 1;
            }
        }

        private void fixAssignments(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                if (club[i].keys['A'] != 0)
                {
                    Constraint constraint = solver.MakeConstraint(int.MinValue, -1.0, club[i].name + " has fixed key " + club[i].keys['A'] + " in week A");
                    constraint.SetCoefficient(c[i, 0, club[i].keys['A'] - 1], -1);
                }
                if (club[i].keys['B'] != 0)
                {
                    Constraint constraint = solver.MakeConstraint(int.MinValue, -1.0, club[i].name + " has fixed key " + club[i].keys['B'] + " in week B");
                    constraint.SetCoefficient(c[i, 1, club[i].keys['B'] - 1], -1);
                }
                if (club[i].keys['X'] != 0)
                {
                    Constraint constraint = solver.MakeConstraint(int.MinValue, -1.0, club[i].name + " has fixed key " + club[i].keys['X'] + " in week X");
                    constraint.SetCoefficient(c[i, 2, club[i].keys['X'] - 1], -1);
                }
                if (club[i].keys['Y'] != 0)
                {
                    Constraint constraint = solver.MakeConstraint(int.MinValue, -1.0, club[i].name + " has fixed key " + club[i].keys['Y'] + " in week Y");
                    constraint.SetCoefficient(c[i, 3, club[i].keys['Y'] - 1], -1);
                }
            }
        }
        private void setObjective(Group[] group)
        {
            Objective objective = solver.Objective();

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                    objective.SetCoefficient(x[i,j], 1);
            
            objective.SetMinimization();
        }

        private void clubHasOneKeyPerWeekScheme(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                Constraint constraintA = solver.MakeConstraint(1.0, 1.0, club[i].name + " has exactly one key for week A");
                for (int j = 0; j < Data.field[0]; j++)
                {
                    constraintA.SetCoefficient(c[i, 0, j], 1);
                }

                Constraint constraintB = solver.MakeConstraint(1.0, 1.0, club[i].name + " has exactly one key for week B");
                for (int j = 0; j < Data.field[0]; j++)
                {
                    constraintB.SetCoefficient(c[i, 1, j], 1);
                }

                Constraint constraintX = solver.MakeConstraint(1.0, 1.0, club[i].name + " has exactly one key for week X");
                for (int j = 0; j < Data.field[1]; j++)
                {
                    constraintX.SetCoefficient(c[i, 2, j], 1);
                }

                Constraint constraintY = solver.MakeConstraint(1.0, 1.0, club[i].name + " has exactly one key for week Y");
                for (int j = 0; j < Data.field[1]; j++)
                {
                    constraintY.SetCoefficient(c[i, 3, j], 1);
                }
            }
        }
        private void teamHasOneKey(Group[] group)
        {
            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                {
                    Constraint constraint = solver.MakeConstraint(1.0, 1.0, group[i].team[j].name + " has exactly one key");
                    for (int k = 0; k < group[i].field; k++)
                    {
                        constraint.SetCoefficient(t[i, j, k], 1);
                    }
                }
        }

        private void uniqueKeysPerDivision(Group[] group)
        {
            for (int i = 0; i < group.Length; i++) 
                for (int k = 0; k < group[i].field; k++)
                {
                    Constraint constraint = solver.MakeConstraint(0.0, 1.0, group[i].name + " has at most one team with key " + (k+1));
                    for (int j = 0; j < group[i].nrOfTeams; j++)
                    {
                        constraint.SetCoefficient(t[i, j, k], 1);
                    }
                }
        }

        private void oppositeKeysForOppositeWeekSchemes(Club[] club)
        {
            for (int i = 0; i < club.Length; i++)
            {
                for (int j = 0; j < Data.field[0]; j++)
                {
                    Constraint constraint = solver.MakeConstraint(0.0, 0.0, club[i].name + " has opposite key for " +(j+1)+ " in A/B week schemes");
                    constraint.SetCoefficient(c[i, 0, j], 1);
                    constraint.SetCoefficient(c[i, 1, Data.km.getOpposed(Data.field[0], j + 1) - 1], -1);
                }
                for (int j = 0; j < Data.field[1]; j++)
                {
                    Constraint constraint = solver.MakeConstraint(0.0, 0.0, club[i].name + " has opposite key for " + (j + 1) + " in X/Y week schemes");
                    constraint.SetCoefficient(c[i, 2, j], 1);
                    constraint.SetCoefficient(c[i, 3, Data.km.getOpposed(Data.field[1], j + 1) - 1], -1);
                }
            }
        }
    }
}
    
        

    /*public static void optimize()
{
    string info = "";
    info += "Google.OrTools version: " + OrToolsVersion.VersionString() + "\n";

    // Create the linear solver with the GLOP backend.
    Solver solver = Solver.CreateSolver("GLOP");
    if (solver is null)
    {
        info += "Could not create solver GLOP" + "\n";
        return;
    }

    // Create the variables x and y.
    Variable x = solver.MakeNumVar(0.0, 1.0, "x");
    Variable y = solver.MakeNumVar(0.0, 2.0, "y");

    info += "Number of variables = " + solver.NumVariables() + "\n";

    // Create a linear constraint, x + y <= 2.
    Constraint constraint = solver.MakeConstraint(double.NegativeInfinity, 2.0, "constraint");
    constraint.SetCoefficient(x, 1);
    constraint.SetCoefficient(y, 1);

    info += "Number of constraints = " + solver.NumConstraints() + "\n";

    // Create the objective function, 3 * x + y.
    Objective objective = solver.Objective();
    objective.SetCoefficient(x, 3);
    objective.SetCoefficient(y, 1);
    objective.SetMaximization();

    info += "Solving with " + solver.SolverVersion() + "\n";
    Solver.ResultStatus resultStatus = solver.Solve();

    info += "Status: " + resultStatus + "\n";
    if (resultStatus != Solver.ResultStatus.OPTIMAL)
    {
        info += "The problem does not have an optimal solution!" + "\n";
        if (resultStatus == Solver.ResultStatus.FEASIBLE)
        {
            info += "A potentially suboptimal solution was found" + "\n";
        }
        else
        {
            info += "The solver could not solve the problem." + "\n";
            return;
        }
    }

    info += "Solution:" + "\n";
    info += "Objective value = " + solver.Objective().Value() + "\n";
    info += "x = " + x.SolutionValue() + "\n";
    info += "y = " + y.SolutionValue() + "\n";

    info += "Advanced usage:" + "\n";
    info += "Problem solved in " + solver.WallTime() + " milliseconds" + "\n";
    info += "Problem solved in " + solver.Iterations() + " iterations" + "\n";
    MessageBox.Show(info);
}*/
