using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGenerator
{
    public class MLIParser
    {
        private string path;
        private List<Tuple<Group[], Club[]>> instances;
        private List<string> errors;
        private char[] week;

        public MLIParser(string path)
        {
            this.path = path;
            this.instances = new List<Tuple<Group[], Club[]>>();
            this.errors = new List<string>();
            this.week = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        }

        public List<Tuple<Group[], Club[]>> parse()
        {
            DirectoryInfo dir = Directory.CreateDirectory(path);
            List<Tuple<Group[], Club[]>> result = new List<Tuple<Group[], Club[]>>();

            foreach (FileInfo fi in dir.GetFiles()) {
                if (fi.FullName.EndsWith("txt")) {
                    TextFile file = new TextFile(fi.FullName);

                    string[] values = file.ReadLine(1,true,errors).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    Team[] team = new Team[Util.toInt(values[0])];
                    Group[] group = new Group[Util.toInt(values[1])];
                    Club[] club = new Club[Util.toInt(values[2])];

                    for (int i=0; i<team.Length; i++)
                    {
                        team[i] = new Team();
                        team[i].name = "Team_" + i;
                    }

                    for (int i=0; i<club.Length; i++)
                    {
                        club[i] = new Club();
                        club[i].name = "Club_" + i;
                        club[i].index = i;
                        club[i].keys = new Dictionary<char, int>();

                        values = file.ReadLine(i + 2, true, errors).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        int nrOfTeamsWithWeeks = 2 * (Util.toInt(values[0]) - Util.toInt(values[1])) <= Util.toInt(values[0]) ? 2 * (Util.toInt(values[0]) - Util.toInt(values[1])) : (Util.toInt(values[0]) / 2) * 2;

                        for (int j=0; j < nrOfTeamsWithWeeks; j++)
                        {
                            club[i].team.Add(team[Util.toInt(values[j+2])]);
                            team[Util.toInt(values[j + 2])].week = week[j];
                            club[i].keys[week[j]] = 0;
                            team[Util.toInt(values[j + 2])].club = club[i];
                        }

                        for (int j = nrOfTeamsWithWeeks; j < Util.toInt(values[0]); j++)
                        {
                            club[i].team.Add(team[Util.toInt(values[j + 2])]);
                            team[Util.toInt(values[j + 2])].week = '-';
                        }
                    }

                    for (int i = 0; i < group.Length; i++)
                    {
                        group[i] = new Group();
                        group[i].name = "Group_" + i;
                        group[i].index = i;

                        values = file.ReadLine(i + club.Length + 2, true, errors).Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        group[i].field = Util.toInt(values[0]);
                        group[i].nrOfTeams = Util.toInt(values[0]);
                        group[i].team = new Team[Util.toInt(values[0])];

                        for (int j = 0; j < Util.toInt(values[0]); j++)
                        {
                            group[i].team[j] = team[Util.toInt(values[j+1])];
                            group[i].team[j].index = j;
                            group[i].team[j].group = group[i];
                        }
                    }

                    result.Add(new Tuple<Group[],Club[]>(group, club));
                }
            }

            return result;
        }
    }
}
