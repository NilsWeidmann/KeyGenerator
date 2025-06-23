using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGenerator
{
    public class KeyAssignmentFinalizer
    {
        public static void generateKeys(Group[] group, Club[] club, KeyGenerator caller, List<String> notification)
        {
            solve(group, club);
            bool done = isComplete(group);
            while (!done)
                foreach (Group l in group)
                    foreach (Team t in l.team)
                    {
                        if (t == null)
                            continue;
                        for (int k = 0; k < l.field; k++)
                            if (t.option[k] && !done)
                            {
                                t.key = k + 1;
                                for (int i = 0; i < l.field; i++)
                                    if (i != k)
                                        t.option[i] = false;
                                foreach (Team t2 in l.team)
                                    if (t2 == null)
                                        continue;
                                    else if (!t2.Equals(t))
                                        t2.option[k] = false;
                                solve(group, club);
                                done = isComplete(group);
                            }
                    }
            Data.save(group, club, Club.file, Group.file, Team.file);
            caller.initUI();
            if (hasError(group, notification))
                notification.Add("Die Generierung ist aufgrund eines logischen Fehlers nicht moeglich!");
            else
                notification.Add("Die Schluesselzahlen wurden erfolgreich generiert!");
        }
        public static void solve(Group[] group, Club[] club)
        {
            int oldConflicts = 0;
            int newConflicts = run(group);
            while (oldConflicts != newConflicts)
            {
                oldConflicts = newConflicts;
                newConflicts = run(group);
                if (isComplete(group))
                    return;
            }
        }

        public static int run(Group[] group)
        {
            foreach (Group l in group)
                foreach (Team t in l.team)
                {
                    if (t == null)
                        continue;
                    if (t.key == 0)
                        t.getKey();
                    if (t.key != 0)
                        l.removeOptions(t.key);
                    l.checkOneOption();
                }
            return nrOfKeys(group);
        }

        public static bool isComplete(Group[] group)
        {
            foreach (Group l in group)
                foreach (Team t in l.team)
                    if (t == null)
                        continue;
                    else if (t.key == 0)
                        return false;
            return true;
        }

        public static int nrOfKeys(Group[] group)
        {
            int keys = 0;
            int teams = 0;

            for (int i = 0; i < group.Length; i++)
                for (int j = 0; j < group[i].nrOfTeams; j++)
                {
                    if (group[i].team[j].key != 0)
                        keys++;
                    teams++;
                }
            return keys;
        }

        public static bool hasError(Group[] group, List<String> notification)
        {
            bool help;
            foreach (Group l in group)
                foreach (Team t in l.team)
                {
                    if (t == null)
                        continue;
                    help = false;
                    for (int k = 0; k < Data.TEAM_MAX; k++)
                        help = help || t.option[k];
                    if (!help)
                    {
                        notification.Add("Keine moegliche Schluesselzahl fuer " + t.name + " (" + l.name + ") gefunden!");
                        return true;
                    }
                }
            return false;
        }
    }
}
