using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGenerator
{
    public class Visualization
    {
        private static string[] weeks = { "-", "A", "B", "X", "Y" };

        public static void visualizeGroupData(Group group, DataGridView dataGridView)
        {
            for (int j = 0; j < group.nrOfTeams; j++)
            {
                String[] content = new String[5];
                Team team = group.team[j];
                int field = group.field;

                if (team.week != '-')
                    content[0] = team.week.ToString();
                else
                    content[0] = "";
                content[1] = group.name;
                content[2] = group.team[j].name;

                if (team.key != 0)
                    content[3] = team.key.ToString();
                else
                    content[3] = "";

                if (team.week == '-' || team.club.keys[team.week] == 0)
                    content[4] = "";
                else if (team.week == 'A' || team.week == 'B')
                    content[4] = Data.concatenate(Data.km.getParallel(Data.field[0], field, team.club.keys[team.week]));
                else if (team.week == 'X' || team.week == 'Y')
                    content[4] = Data.concatenate(Data.km.getParallel(Data.field[1], field, team.club.keys[team.week]));

                dataGridView.Rows.Add(content);
                Color backgroundColor;

                if (team.week == '-')
                    if (team.day.Contains('H') || team.day.Contains('A'))
                        backgroundColor = Color.LightBlue;
                    else
                        backgroundColor = Color.White;
                else if (content[3].Equals("") && content[4].Equals(""))
                    backgroundColor = Color.Yellow;
                else if (!content[4].Split(",").Contains(content[3]))
                    backgroundColor = Color.Orange;  // Conflict
                else
                    backgroundColor = Color.LightGreen;

                for (int i = 0; i < 5; i++)
                    dataGridView.Rows[j].Cells[i].Style.BackColor = backgroundColor;
            }
        }

        public static void visualizeClubData(Club club, DataGridView dataGridView)
        {
            for (int j = 0; j < club.team.Count; j++)
            {
                String[] content = new String[5];
                if (club.team[j].week != '-')
                    content[0] = club.team[j].week.ToString();
                else
                    content[0] = "";
                content[1] = club.team[j].group.name;
                content[2] = club.team[j].name;
                if (club.team[j].key != 0)
                    content[3] = club.team[j].key.ToString();
                if (club.team[j].week == '-' || club.team[j].club.keys[club.team[j].week] == 0)
                    content[4] = "";
                else if (club.team[j].week == 'A' || club.team[j].week == 'B')
                    content[4] = Data.concatenate(Data.km.getParallel(Data.field[0], club.team[j].group.field, club.keys[club.team[j].week]));
                else if (club.team[j].week == 'X' || club.team[j].week == 'Y')
                    content[4] = Data.concatenate(Data.km.getParallel(Data.field[1], club.team[j].group.field, club.keys[club.team[j].week]));

                dataGridView.Rows.Add(content);

                for (int l = 0; l < 5; l++)
                {
                    Color color;
                    switch (club.team[j].week)
                    {
                        case 'A': color = Color.Yellow; break;
                        case 'B': color = Color.Orange; break;
                        case 'X': color = Color.LightBlue; break;
                        case 'Y': color = Color.LightGreen; break;
                        default: color = Color.White; break;
                    }
                    dataGridView.Rows[j].Cells[l].Style.BackColor = color;
                }
            }
        }

        public static void initGroupGrid(DataGridView dataGridView, bool readOnly)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            string[] values = { "Staffel", "Feld"};
            foreach (string s in values)
            {
                dataGridView.Columns.Add(s, s);
            }
            dataGridView.Columns[0].ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            foreach (DataGridViewColumn col in dataGridView.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public static void fillGroupGrid(DataGridView dataGridView, Group[] groupArray)
        {
            String[] values = new String[2];

            for (int i = 0; i < groupArray.Length; i++)
            {
                Group g = groupArray[i];
                values[0] = g.name;
                values[1] = g.field.ToString();
                dataGridView.Rows.Add(values);
            }
        }

        public static void initClubGrid(DataGridView dataGridView, bool readOnly)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            string[] values = { "Verein", "A", "B", "X", "Y", "Kap." };
            foreach (string s in values)
            {
                dataGridView.Columns.Add(s, s);
            }
            dataGridView.Columns[0].ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            foreach (DataGridViewColumn col in dataGridView.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public static void fillClubGrid(DataGridView dataGridView, Club[] clubArray)
        {
            String[] values = new String[6];

            for (int i = 0; i < clubArray.Length; i++)
            {
                Club v = clubArray[i];
                values[0] = v.name;

                int[] intValues = new int[] { v.keys['A'], v.keys['B'], v.keys['X'], v.keys['Y'] };
                for (int j = 1; j < 5; j++)
                    values[j] = intValues[j - 1].ToString() == "0" ? "" : intValues[j - 1].ToString();

                values[5] = v.capacity ? "X" : "";
                dataGridView.Rows.Add(values);
            }
        }

        public static void changeClubData(DataGridViewCellEventArgs e, DataGridView dataGridView, List<Club> club)
        {
            if (dataGridView.Rows[e.RowIndex].IsNewRow && e.ColumnIndex != 0)
                return;
            String value = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
            int key = Util.toInt(value);
            switch (e.ColumnIndex)
            {
                case 0:
                    if (value.Equals(""))
                        break;
                    club.ElementAt(e.RowIndex).name = Util.clear(value);
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = club.ElementAt(e.RowIndex).name;
                    break;
                case 1:
                    assignValue(dataGridView, club, 'A', 'B', key, Data.field[0], e.RowIndex, 1, 2);
                    break;
                case 2:
                    assignValue(dataGridView, club, 'B', 'A', key, Data.field[0], e.RowIndex, 2, 1);
                    break;
                case 3:
                    assignValue(dataGridView, club, 'X', 'Y', key, Data.field[1], e.RowIndex, 3, 4);
                    break;
                case 4:
                    assignValue(dataGridView, club, 'Y', 'X', key, Data.field[1], e.RowIndex, 4, 3);
                    break;
                case 5:
                    if (value.Equals(""))
                        club.ElementAt(e.RowIndex).capacity = false;
                    else
                        club.ElementAt(e.RowIndex).capacity = true;
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = club.ElementAt(e.RowIndex).capacity ? "X" : "";
                    break;
            }
        }

        public static void changeField(DataGridView dataGridView, DataGridViewCellEventArgs e, Group currentGroup)
        {
            if (dataGridView.Rows[e.RowIndex].IsNewRow && e.ColumnIndex != 0)
                return;

            if (e.ColumnIndex == 1)
            {
                String value = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
                int field = Util.toInt(value);
                if (field % 2 != 0 || field < currentGroup.nrOfTeams || field > Data.TEAM_MAX)
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentGroup.field.ToString();
                else
                    currentGroup.field = field;
            }
        }

        private static void assignValue(DataGridView dataGridView, List<Club> club, char week1, char week2, int key, int field, int rowIdx, int colIdx1, int colIdx2)
        {
            if (key > 0 && key <= field)
            {
                club.ElementAt(rowIdx).keys[week1] = key;
                club.ElementAt(rowIdx).keys[week2] = Data.km.getOpposed(field, key);
            }
            else
            {
                club.ElementAt(rowIdx).keys[week1] = 0;
                club.ElementAt(rowIdx).keys[week2] = 0;
            }
            if (club.ElementAt(rowIdx).keys[week1] == 0)
                dataGridView.Rows[rowIdx].Cells[colIdx1].Value = "";
            else
                dataGridView.Rows[rowIdx].Cells[colIdx1].Value = club.ElementAt(rowIdx).keys[week1].ToString();
            if (club.ElementAt(rowIdx).keys[week2] == 0)
                dataGridView.Rows[rowIdx].Cells[colIdx2].Value = "";
            else
                dataGridView.Rows[rowIdx].Cells[colIdx2].Value = club.ElementAt(rowIdx).keys[week2].ToString();
        }

        public static void addClub(DataGridViewRowEventArgs e, List<Club> clubs)
        {
            Club v = new Club();
            v.index = e.Row.Index - 1;
            for (int i = e.Row.Index - 1; i < clubs.Count; i++)
                clubs.ElementAt(i).index++;
            clubs.Insert(e.Row.Index - 1, v);
        }

        public static void deleteClub(DataGridViewRowCancelEventArgs e, List<Club> clubs, List<Partnership> partnership)
        {
            foreach (Partnership p in partnership)
                if (p.indexA == e.Row.Index - 1 || p.indexB == e.Row.Index - 1)
                    partnership.Remove(p);
            clubs.RemoveAt(e.Row.Index - 1);
            for (int i = e.Row.Index - 1; i < clubs.Count; i++)
                clubs.ElementAt(i).index--;

        }

        public static void initTeamGrid(DataGridView dataGridView, bool forClubs)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Woche", "Woche");
            dataGridView.Columns.Add("Liga", "Liga");
            dataGridView.Columns.Add("Team", "Team");
            dataGridView.Columns.Add("Schlüssel", "Schlüssel");
            dataGridView.Columns.Add("Wunsch", "Wunsch");
            dataGridView.Columns[0].ReadOnly = false;
            dataGridView.Columns[1].ReadOnly = true;
            dataGridView.Columns[2].ReadOnly = true;
            dataGridView.Columns[3].ReadOnly = true;
            dataGridView.Columns[4].ReadOnly = true;
            foreach (DataGridViewColumn col in dataGridView.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            // Nur bei Vereinssicht, nicht bei Staffelsicht
            dataGridView.Columns[0].Visible = forClubs;
        }

        public static void changeWeek(DataGridView dataGridView, DataGridViewCellEventArgs e, bool forClubs, Group currentGroup, Club currentClub)
        {
            if (e.ColumnIndex == 0)
            {
                // Groß- und Kleinschreibung tolerieren
                string input = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().ToUpper();

                // Auch Leerzeichen und leere Strings erlauben
                if (input.Equals("") || input.Equals(" "))
                    input = "-";

                if (forClubs && currentClub != null)
                {
                    // Vereinssicht
                    if (weeks.Contains(input) && e.RowIndex < currentClub.team.Count)
                        currentClub.team[e.RowIndex].week = input[0];
                    dataGridView.Rows.Clear();
                    Visualization.visualizeClubData(currentClub, dataGridView);
                }
                else if (!forClubs && currentGroup != null)
                {
                    // Staffelsicht
                    if (weeks.Contains(input) && e.RowIndex < currentGroup.team.Length)
                        currentGroup.team[e.RowIndex].week = input[0];
                    dataGridView.Rows.Clear();
                    Visualization.visualizeGroupData(currentGroup, dataGridView);

                }
            }
        }
    }
}
