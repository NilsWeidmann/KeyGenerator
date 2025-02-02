using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.Init;
using Google.OrTools.LinearSolver;

namespace KeyGenerator
{
    public class Optimizer
    {
        public static void optimize()
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
            info +=  "Problem solved in " + solver.Iterations() + " iterations" + "\n";
            MessageBox.Show(info);
        }
    }
}
