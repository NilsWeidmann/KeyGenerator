using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows.Forms;
using System.Collections;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace KeyGenerator
{
    public class Parser
    {
        private static string[] weeks = { "-", "A", "B", "X", "Y" };

        public static bool parseGroupsAndClubs(Hashtable groups, Hashtable clubs, OpenFileDialog openFileDialog)
        {
            Club currentClub = null;
            Team currentTeam = null;
            Group currentGroup = null;

            TextFile groupFile = new TextFile(openFileDialog.FileName);
            List<String> notification = new List<string>();
            String content = groupFile.ReadFile(false, notification);
            String[] row = content.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (row.Length == 0)
                return false;

            String[] colNames = row[0].Split(new char[] { ';' }, StringSplitOptions.None);
            Util.Index idx = new Util.Index(-1);

            // Indizes ermitteln
            for (int i = 0; i < colNames.Length; i++)
            {
                switch (colNames[i])
                {
                    case "Region":
                        idx.region = i; break;
                    case "Gruppe":
                        idx.group = i; break;
                    case "VereinNr":
                        idx.clubId = i; break;
                    case "VereinName":
                        idx.clubName = i; break;
                    case "MannschaftAltersklasse":
                        idx.ageGroup = i; break;
                    case "MannschaftNr":
                        idx.teamNo = i; break;
                }
            }

            // Sichergehen, dass alle Indizes gefunden wurden
            if (idx.region == -1 || idx.group == -1 || idx.clubId == -1 || idx.clubName == -1 || idx.ageGroup == -1 || idx.teamNo == -1)
                return false;

            // Daten auslesen
            for (int i = 1; i < row.Length; i++)
            {
                String[] data = row[i].Split(new char[] { ';' }, StringSplitOptions.None);

                // Verein ermitteln oder anlegen
                if (clubs.ContainsKey(data[idx.clubId]))
                    currentClub = (Club)clubs[data[idx.clubId]];
                else
                {
                    currentClub = new Club(data[idx.clubName], Util.toInt(data[idx.clubId]), clubs.Count);
                    clubs.Add(data[idx.clubId], currentClub);
                }

                // Gruppe ermitteln oder anlegen
                String fullName = data[idx.group] + " (" + data[idx.region] + ")";
                if (groups.ContainsKey(fullName))
                    currentGroup = (Group)groups[fullName];
                else
                {
                    currentGroup = new Group(fullName, groups.Count);
                    groups.Add(fullName, currentGroup);
                }

                // Team anlegen und der Gruppe hinzufügen
                currentTeam = new Team(currentClub.name + " " + Util.toRoman(data[idx.teamNo]), currentClub,
                    Util.toRoman(data[idx.teamNo]), data[idx.ageGroup]);
                currentTeam.group = currentGroup;
                currentGroup.team[currentGroup.nrOfTeams++] = currentTeam;
                currentClub.team.Add(currentTeam);
            }

            return true;
        }

        public static bool addWishes(Hashtable groups, Hashtable clubs, OpenFileDialog openFileDialog)
        {

            Stream file = openFileDialog.OpenFile();
            HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument wishes = new HtmlAgilityPack.HtmlDocument();

            bool isHeader = true;
            wishes.Load(file);
            // Regex clubNameAndID = new Regex("(\\w+\\s)+\\([0-9]{6}\\)");
            Regex clubIDPattern = new Regex(@"^[0-9]{5}$");
            Regex timePattern = new Regex(@"[A-Z][a-z] [0-9]{2}:[0-9]{2}");
            HtmlNodeCollection divs = wishes.DocumentNode.SelectNodes("//div");

            if (divs == null)
                return false;

            Club currentClub = null;
            Club dummyClub = new Club();
            Team currentTeam = null;
            Team dummyTeam = new Team();

            foreach (HtmlNode div in divs)
            {
                IEnumerable<HtmlNode> ps = div.Descendants("p");
                isHeader = true;

                if (ps == null)
                    return false;

                foreach (HtmlNode p in ps)
                {
                    string[] lines = p.InnerHtml.Split(new string[] { "<br>", "<br/>", "<b>", "</b>" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] data;

                    foreach (string line in lines)
                    {
                        string parsedLine = Util.clear(line);

                        // Neuer Verein
                        string[] clubNameAndID = parsedLine.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        if (clubNameAndID.Length == 2)
                        {
                            if (clubIDPattern.IsMatch(clubNameAndID[1]))
                            {

                                string clubID = clubNameAndID[1];
                                string clubName = clubNameAndID[0].TrimEnd(' ');

                                if (clubs.ContainsKey(clubID))
                                    currentClub = (Club)clubs[clubID];
                                else
                                    currentClub = dummyClub;
                                currentTeam = dummyTeam;
                            }
                        }

                        // Header vorbei?
                        if (parsedLine.Equals("Terminwuensche"))
                            isHeader = false;

                        if (isHeader)
                        {
                            continue;
                        }

                        // Neue Mannschaft
                        foreach (string ageGroup in Data.ageGroups)
                        {
                            if (parsedLine.StartsWith(ageGroup))
                            {
                                parsedLine = parsedLine.Replace(ageGroup, "").TrimStart();
                                data = parsedLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (data.Length == 0)
                                    break;

                                string team = data[0];

                                if (!Util.isRomanNumber(team))
                                    team = "I";
                                else
                                    parsedLine = parsedLine.Remove(0, team.Length).TrimStart();

                                bool found = false;
                                foreach (Team t in currentClub.team)
                                    if (t.club.name.Equals(currentClub.name)
                                        && t.ageGroup.Equals(ageGroup)
                                        && t.team.Equals(team))
                                    {
                                        currentTeam = t;
                                        found = true;
                                        break;
                                    }

                                if (!found)
                                    currentTeam = dummyTeam;
                            }
                        }
                        // Festspieltag
                        data = parsedLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (data.Length == 0)
                            continue;

                        string weekday = data[0];
                        if (timePattern.IsMatch(weekday))
                        {
                            if (currentTeam.weekday.Equals(""))
                                currentTeam.weekday = weekday;
                            else if (currentTeam.weekday2.Equals("") && !currentTeam.weekday.Equals(weekday))
                                currentTeam.weekday2 = weekday;
                        }
                        // Spielwoche
                        foreach (string week in weeks)
                            if (parsedLine.StartsWith("Spielwoche " + week))
                            {
                                currentTeam.week = week.ToCharArray()[0];
                            }
                    }
                }
            }
            return true;
        }

        public static void saveInData(Hashtable groups, Hashtable clubs, List<Group> group, List<Club> club)
        {
            foreach (Club c in clubs.Values)
                club.Add(c);
            club.Sort();

            foreach (Group g in groups.Values)
            {
                // Feld ermitteln. Default: so klein wie möglich
                if (g.field == 0)
                    g.field = g.nrOfTeams < Data.TEAM_MIN ? Data.TEAM_MIN : g.nrOfTeams + (g.nrOfTeams % 2);
                group.Add(g);
            }
            group.Sort();
        }
    }
}
