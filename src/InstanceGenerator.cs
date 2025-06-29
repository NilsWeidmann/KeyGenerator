﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.Sat;

namespace KeyGenerator
{
    public class InstanceGenerator
    {
        // Basic Configuration
        BackgroundWorker bw;
        TextFile log;
        string path;
        List<string> messages;
        KeyMapper km;
        int timeout;
        const int TEAM_MIN = 4;
        const int TEAM_MAX = 20;
        char[] WEEK_SCHEMES = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        int NR_OF_REPS = 10;

        // Test Configuration
        int[] nrOfGroups = [20, 50, 100, 200, 500];
        int[] nrOfTeamsPerDivision = [6, 8, 10, 12, 14];
        int[] nrOfClubs = [20, 50, 100, 200, 500];
        int[] nrOfWeekSchemes = [1, 2, 3, 4, 5];

        double[] portionOfTeamsWithDependencies = [0.75, 0.80, 0.85, 0.9, 0.95];
        double[] portionOfFixedAssignments = [0.10, 0.20, 0.30, 0.40, 0.50];

        // Base Scenario
        int[] bS = [1, 2, 1, 1, 0, 0];

        // Status
        int testNo;
        int totalNoOfTests;

        public InstanceGenerator(BackgroundWorker bw, int timeout, TextFile log, string path)
        {
            km = createKeyMapper();
            this.bw = bw;
            this.log = log;
            this.path = path;
            messages = new List<string>();
            this.timeout = timeout;
            testNo = 0;
            totalNoOfTests = nrOfGroups.Length + nrOfTeamsPerDivision.Length + nrOfClubs.Length + nrOfWeekSchemes.Length + portionOfTeamsWithDependencies.Length + portionOfFixedAssignments.Length;
        }

        public void runTests()
        {
            for (int rep = 0; rep < NR_OF_REPS; rep++)
            {

                foreach (int nOG in nrOfGroups)
                {
                    runTest(nOG, nrOfTeamsPerDivision[bS[1]], nrOfClubs[bS[2]], SubArray(WEEK_SCHEMES, nrOfWeekSchemes[bS[3]] * 2), portionOfTeamsWithDependencies[bS[4]], portionOfFixedAssignments[bS[5]], rep);
                }

                foreach (int nOTPD in nrOfTeamsPerDivision)
                {
                    runTest(nrOfGroups[bS[0]], nOTPD, nrOfClubs[bS[2]], SubArray(WEEK_SCHEMES, nrOfWeekSchemes[bS[3]] * 2), portionOfTeamsWithDependencies[bS[4]], portionOfFixedAssignments[bS[5]], rep);
                }

                foreach (int nOC in nrOfClubs)
                {
                    runTest(nrOfGroups[bS[0]], nrOfTeamsPerDivision[bS[1]], nOC, SubArray(WEEK_SCHEMES, nrOfWeekSchemes[bS[3]] * 2), portionOfTeamsWithDependencies[bS[4]], portionOfFixedAssignments[bS[5]], rep);
                }

                foreach (int nOWS in nrOfWeekSchemes)
                {
                    runTest(nrOfGroups[bS[0]], nrOfTeamsPerDivision[bS[1]], nrOfClubs[bS[2]], SubArray(WEEK_SCHEMES, nOWS * 2), portionOfTeamsWithDependencies[bS[4]], portionOfFixedAssignments[bS[5]], rep);
                }

                foreach (double pOTWD in portionOfTeamsWithDependencies)
                {
                    runTest(nrOfGroups[bS[0]], nrOfTeamsPerDivision[bS[1]], nrOfClubs[bS[2]], SubArray(WEEK_SCHEMES, nrOfWeekSchemes[bS[3]] * 2), pOTWD, portionOfFixedAssignments[bS[5]], rep);
                }

                foreach (double pOFA in portionOfFixedAssignments)
                {
                    runTest(nrOfGroups[bS[0]], nrOfTeamsPerDivision[bS[1]], nrOfClubs[bS[2]], SubArray(WEEK_SCHEMES, nrOfWeekSchemes[bS[3]] * 2), portionOfTeamsWithDependencies[bS[4]], pOFA, rep);
                }

                // Cool down
                //Thread.Sleep(120000);
            }
        }

        public void runTests(string path)
        {
            MLIParser parser = new MLIParser(path);
            List<Tuple<Group[], Club[]>> parseResult = parser.parse();
            totalNoOfTests = parseResult.Count;

            foreach (Tuple<Group[],Club[]> data in parseResult)
            {
                int[] conflicts = [-1, -1];
                int[] field = getField(data);
                char[] wS = SubArray(WEEK_SCHEMES, field.Length * 2);
                int nOTPD = field[0];

                OptimizationModel om = new OptimizationModel(data.Item1, data.Item2, wS, field, timeout, km, nOTPD, log, bw, messages);
                testNo++;

                CpSolverStatus status = om.findSolution(data.Item1, data.Item2, conflicts);
            } 
        }

        private int[] getField(Tuple<Group[], Club[]> data)
        {
            int maxFieldSize = 0;
            int nrOfWeekSchemes = 0;

            foreach (Group g in data.Item1)
            {
                if (g.field > maxFieldSize)
                    maxFieldSize = g.field;
                foreach (Team t in g.team)
                    if (t.week - 'A' + 1 > nrOfWeekSchemes)
                        nrOfWeekSchemes = t.week - 'A' + 1;
            }

            int[] result = new int[nrOfWeekSchemes / 2];
            for (int i = 0; i < result.Length; i++)
                result[i] = maxFieldSize;

            return result;
        }

        private void runTest(int nOG, int nOTPD, int nOC, char[] wS, double pOTWD, double pOFA, int rep)
        {
            int[] conflicts = [-1, -1];
            int[] field = new int[wS.Length / 2];
            for (int i = 0; i < wS.Length / 2; i++)
                field[i] = nOTPD;

            TextFile clubs = new TextFile(path + @"\clubs_" + nOG + "_" + nOTPD + "_" + nOC + "_" + (wS.Length / 2) + "_" + pOTWD + "_" + pOFA + "_" + rep + ".csv");
            TextFile groups = new TextFile(path + @"\groups_" + nOG + "_" + nOTPD + "_" + nOC + "_" + (wS.Length / 2) + "_" + pOTWD + "_" + pOFA + "_" + rep + ".csv");
            TextFile relations = new TextFile(path + @"\relations_" + nOG + "_" + nOTPD + "_" + nOC + "_" + (wS.Length / 2) + "_" + pOTWD + "_" + pOFA + "_" + rep + ".csv");

            Tuple<Group[], Club[]> data = generate(nOG, nOTPD, nOC, wS, pOTWD, pOFA);
            Data.save(data.Item1, data.Item2, clubs, groups, relations, new string(wS));

            OptimizationModel om = new OptimizationModel(data.Item1, data.Item2, wS, field, timeout, km, nOTPD, log, bw, messages);
            testNo++;

            DateTime start = DateTime.Now;
            CpSolverStatus status = om.findSolution(data.Item1, data.Item2, conflicts);
            DateTime stop = DateTime.Now;
        }

        private static char[] SubArray(char[] data, int length)
        {
            char[] result = new char[length];
            Array.Copy(data, 0, result, 0, length);
            return result;
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

            List<int>[,,] parallel = new List<int>[TEAM_MAX, TEAM_MAX, TEAM_MAX];

            for (int i = TEAM_MIN; i <= TEAM_MAX; i += 2)
                for (int j = i; j <= TEAM_MAX; j += 2)
                    for (int k=0, l=1; l<=j/2; l++)
                    {
                        if (k < i / 2)
                        {
                            k++;
                            parallel[i - 1, j - 1, k - 1].Add(l);
                            parallel[i - 1, j - 1, opposed[i - 1, k - 1] - 1].Add(opposed[j - 1, l - 1]);
                        }

                        parallel[j - 1, i - 1, l - 1].Add(k);
                        parallel[j - 1, i - 1, opposed[j - 1, l - 1] - 1].Add(opposed[i - 1, k - 1]);
                    }
                        

            return new KeyMapper(similar, opposed, parallel, null);
        }

        private Tuple<Group[],Club[]> generate(int nrOfGroups, int nrOfTeamsPerGroup, int nrOfClubs, char[] week, double portionOfDependentTeams, double portionOfFixedKeys)
        {
            Random r = new Random();

            Group[] group = new Group[nrOfGroups];
            Club[] club = new Club[nrOfClubs];

            for (int i=0; i<nrOfClubs; i++)
            {
                club[i] = new Club();
                club[i].index = i;
                club[i].name = "Club_" + i;
                club[i].keys = new Dictionary<char, int>();

                for (int j = 0; j < week.Length; j++)
                    club[i].keys[week[j]] = 0;

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
                group[i].name = "Group_" + i;
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
                    group[i].team[j].name = group[i].team[j].club.name + " " + group[i].team[j].club.team.Count;
                    if (r.NextDouble() < portionOfDependentTeams)
                        group[i].team[j].week = week[(int)(r.NextDouble() * week.Length)];
                    else
                        group[i].team[j].week = '-';
                }
            }

            return new Tuple<Group[], Club[]>(group,club);
        }

        public void refreshStatusText(Label status)
        {
            status.Text = String.Format("{0,2}/{1,2}", testNo, totalNoOfTests);
        }
    }
}
