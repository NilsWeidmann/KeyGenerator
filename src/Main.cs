using HtmlAgilityPack;
using KeyGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class KeyGenerator : Form
    {
        private Club club;
        private string[] weeks = { "-", "A", "B", "X", "Y" };

        public KeyGenerator()
        {
            //new InstanceGenerator();
            InitializeComponent();
            this.dataGridView.ReadOnly = false;
            initDataGridView();
            enableFields();
            boxCapacity.Enabled = false;
            boxDirectory.Enabled = false;
            buttonGenerate.Enabled = false;
            buttonInputFromFile.Enabled = false;
            buttonMiscellaneous.Enabled = false;
            buttonSave.Enabled = false;
            boxGroups.Enabled = false;
            boxClubs.Enabled = false;
            boxFields.Enabled = false;
            fillFields(boxFieldAB);
            boxFieldAB.SelectedIndex = 3;
            fillFields(boxFieldXY);
            boxFieldXY.SelectedIndex = 2;
            boxRuntimeMinutes.Text = "2";
            boxDirectory.Text = Application.StartupPath;
            boxRuntimeSeconds.Text = "0";
            setFiles(Application.StartupPath);
            Data.caller = this;

            enableBoxesAndButtons();
        }

        private void initDataGridView()
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
            dataGridView.Columns[0].Visible = buttonClubView.Checked;
        }
        public void fillFields(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = Data.TEAM_MIN; i <= Data.TEAM_MAX; i += 2)
                cb.Items.Add(i.ToString());
            cb.SelectedIndex = -1;
        }

        public void enableFields()
        {
            boxFields.Enabled = boxGroups.SelectedIndex != -1;
        }

        public void showResults(Group[] best_l, Club[] best_v, int conflicts)
        {
            if (conflicts == -1)
                Data.notification.Add("Es konnten keine Schlüsselzahlen ermittelt werden!");
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                loadFromFile(Club.backup, Group.backup, Team.backup);
            }
            else
            {
                Data.copy(best_l, Data.group, best_v, Data.club);
                solveConflicts(Data.group, Data.club);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Data.runtime = Util.toInt(boxRuntimeMinutes.Text) * 60 + Util.toInt(boxRuntimeSeconds.Text);
            PleaseWait bw = new PleaseWait(this, true);
            bw.Visible = true;
        }

        private void reset(ComboBox box)
        {
            box.Items.Clear();
            box.SelectedIndex = -1;
            box.Text = "";
        }

        public void initUI()
        {
            initDataGridView();
            boxCapacity.Enabled = false;

            ComboBox[] boxes = { boxGroups, boxClubs };
            foreach (ComboBox box in boxes)
            {
                reset(box);
            }

            if (Data.group != null)
                for (int i = 0; i < Data.group.Length; i++)
                    boxGroups.Items.Add(Data.group[i].name);
            if (Data.club != null)
                for (int i = 0; i < Data.club.Length; i++)
                    boxClubs.Items.Add(Data.club[i].name);
            enableFields();
        }

        public bool loadFromFile(TextFile ver, TextFile groupFile, TextFile relationshipFile)
        {
            buttonManualInput.Enabled = buttonClickTTInput.Enabled = buttonInputFromFile.Enabled = false;
            Data.club = Data.getClubs(ver);
            Data.group = Group.getGroups(Data.club, groupFile, Data.notification);
            Data.getRelations(Data.group, relationshipFile);
            Data.allocateTeams(Data.club, Data.group);

            if (Data.notification.Count > 0)
            {
                buttonGenerate.Enabled = false;
                buttonMiscellaneous.Enabled = false;
                buttonSave.Enabled = false;
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                buttonManualInput.Enabled = buttonClickTTInput.Enabled = buttonInputFromFile.Enabled = true;
                return false;
            }
            else
            {
                initUI();
                buttonClubView.Enabled = true;
                buttonGroupView.Enabled = true;
                buttonClubView.Checked = true;
                buttonGenerate.Enabled = true;
                buttonMiscellaneous.Enabled = true;
                buttonSave.Enabled = true;
                buttonManualInput.Enabled = buttonClickTTInput.Enabled = buttonInputFromFile.Enabled = true;
                enableBoxesAndButtons();
                return true;
            }
        }

        private void buttonInputFromFile_Click(object sender, EventArgs e)
        {
            loadFromFile(Club.file, Group.file, Team.file);
        }

        public void enableBoxesAndButtons()
        {
            dataGridView.Rows.Clear();
            boxCapacity.Enabled = boxCapacity.Checked = buttonPartner.Enabled = false;

            boxGroups.Enabled = boxClubs.Enabled = boxFields.Enabled = boxWeekA.Enabled
                 = boxWeekB.Enabled = boxWeekX.Enabled = boxWeekY.Enabled = false;

            boxGroups.Text = boxClubs.Text = boxFields.Text = boxWeekA.Text
                = boxWeekB.Text = boxWeekX.Text = boxWeekY.Text = "";

            if (buttonClubView.Checked)
            {
                // Vereinssicht
                boxClubs.Enabled = true;
                if (boxClubs.SelectedItem != null)
                {
                    boxCapacity.Enabled = boxWeekA.Enabled = boxWeekB.Enabled = boxWeekX.Enabled = boxWeekY.Enabled = true;
                    boxClubs.Text = boxClubs.SelectedItem.ToString();
                    boxWeekA.Text = boxWeekA.SelectedItem.ToString();
                    boxWeekB.Text = boxWeekB.SelectedItem.ToString();
                    boxWeekX.Text = boxWeekX.SelectedItem.ToString();
                    boxWeekY.Text = boxWeekY.SelectedItem.ToString();
                    boxCapacity.Checked = Data.club[boxClubs.SelectedIndex].capacity;
                }
            }
            else if (buttonGroupView.Checked)
            {
                // Staffelsicht
                boxGroups.Enabled = true;
                if (boxGroups.SelectedItem != null)
                {
                    boxGroups.Text = boxGroups.SelectedItem.ToString();
                    boxFields.Enabled = true;
                    boxFields.Text = Data.group[boxGroups.SelectedIndex].field.ToString();
                }
            }
        }

        public void fillDataGridView(Control caller)
        {
            dataGridView.Rows.Clear();
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
            {
                // Vereinssicht
                club = Data.club[boxClubs.SelectedIndex];
                if (boxClubs.SelectedIndex == -1)
                {
                    boxCapacity.Checked = boxCapacity.Enabled = buttonPartner.Enabled = false;
                    boxWeekA.Text = boxWeekB.Text = boxWeekX.Text = boxWeekY.Text = "";
                }
                else
                {
                    boxCapacity.Checked = club.capacity;
                    buttonPartner.Enabled = true;
                }
                if (boxWeekA.Items.Count > club.keys['A'])
                    boxWeekA.SelectedIndex = club.keys['A'];
                if (boxWeekB.Items.Count > club.keys['B'])
                    boxWeekB.SelectedIndex = club.keys['B'];
                if (boxWeekX.Items.Count > club.keys['X'])
                    boxWeekX.SelectedIndex = club.keys['X'];
                if (boxWeekY.Items.Count > club.keys['Y'])
                    boxWeekY.SelectedIndex = club.keys['Y'];
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
                        content[4] = Data.km.getParallel(Data.field[0], club.team[j].group.field, club.keys[club.team[j].week]).ToString();
                    else if (club.team[j].week == 'X' || club.team[j].week == 'Y')
                        content[4] = Data.km.getParallel(Data.field[1], club.team[j].group.field, club.keys[club.team[j].week]).ToString();

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
            else if (buttonGroupView.Checked && boxGroups.SelectedIndex != -1)
            {
                // Staffelsicht
                for (int j = 0; j < Data.group[boxGroups.SelectedIndex].nrOfTeams; j++)
                {
                    String[] content = new String[5];
                    Team team = Data.group[boxGroups.SelectedIndex].team[j];
                    int field = Data.group[boxGroups.SelectedIndex].field;

                    if (team.week != '-')
                        content[0] = team.week.ToString();
                    else
                        content[0] = "";
                    content[1] = Data.group[boxGroups.SelectedIndex].name;
                    content[2] = Data.group[boxGroups.SelectedIndex].team[j].name;

                    if (team.key != 0)
                        content[3] = team.key.ToString();
                    else
                        content[3] = "";

                    if (team.week == '-' || team.club.keys[team.week] == 0)
                        content[4] = "";
                    else if (team.week == 'A' || team.week == 'B')
                        content[4] = Data.km.getParallel(Data.field[0], field, team.club.keys[team.week]).ToString();
                    else if (team.week == 'X' || team.week == 'Y')
                        content[4] = Data.km.getParallel(Data.field[1], field, team.club.keys[team.week]).ToString();

                    dataGridView.Rows.Add(content);
                    Color backgroundColor;

                    if (team.week == '-')
                        if (team.day.Contains('H') || team.day.Contains('A'))
                            backgroundColor = Color.LightBlue;
                        else
                            backgroundColor = Color.White;
                    else if (content[3].Equals("") && content[4].Equals(""))
                        backgroundColor = Color.Yellow;
                    else if (!content[3].Equals(content[4]))
                        backgroundColor = Color.Orange;  // Conflict
                    else
                        backgroundColor = Color.LightGreen;

                    for (int i = 0; i < 5; i++)
                        dataGridView.Rows[j].Cells[i].Style.BackColor = backgroundColor;
                }
                club = null;
            }
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            caller.Focus();
        }

        private void setFiles(string path)
        {
            Group.file = new TextFile(path + @"\Staffeln.csv");
            Group.backup = new TextFile(path + @"\Staffeln_Backup.csv");
            Club.file = new TextFile(path + @"\Vereine.csv");
            Club.backup = new TextFile(path + @"\Vereine_Backup.csv");
            Team.file = new TextFile(path + @"\Beziehungen.csv");
            Team.backup = new TextFile(path + @"\Beziehungen_Backup.csv");
            if (boxFieldAB.SelectedIndex != -1 && boxFieldXY.SelectedIndex != -1)
            {
                buttonInputFromFile.Enabled = true;
                buttonManualInput.Enabled = true;
            }
            Data.km = new KeyMapper(path);
            Data.log = new TextFile(path + @"\Log.csv");
            Data.log.WriteFile("Uhrzeit;Laufzeit (in s);Anzahl Konflikte;Status;\n\n", Data.notification);
        }

        private void boxClubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox[] boxes = { boxWeekA, boxWeekB, boxWeekX, boxWeekY };
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
            {
                boxCapacity.Enabled = buttonPartner.Enabled = true;
                foreach (ComboBox cb in boxes)
                {
                    cb.Enabled = true;
                    cb.Text = cb.SelectedItem.ToString();
                }
                fillDataGridView(boxClubs);
            }
            else
            {
                boxCapacity.Enabled = boxCapacity.Checked = buttonPartner.Enabled = false;
                foreach (ComboBox cb in boxes)
                {
                    cb.Enabled = false;
                    cb.Text = "";
                }
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = boxDirectory.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                boxDirectory.Text = folderBrowserDialog1.SelectedPath;
                setFiles(boxDirectory.Text);
            }
        }

        private void boxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxGroups.SelectedIndex == -1)
            {
                boxFields.SelectedIndex = -1;
                boxFields.Enabled = false;
            }
            else
            {
                for (int i = 0; i < Data.group[boxGroups.SelectedIndex].nrOfTeams; i++)
                {
                    boxFields.Items.Clear();

                    Group currentGroup = Data.group[boxGroups.SelectedIndex];
                    int start = currentGroup.nrOfTeams + currentGroup.nrOfTeams % 2;
                    start = start > Data.TEAM_MIN ? start : Data.TEAM_MIN;

                    for (int j = start, counter = 0; j <= Data.TEAM_MAX; j += 2, counter++)
                    {
                        boxFields.Items.Add(j.ToString());
                        if (j == currentGroup.field)
                            boxFields.SelectedIndex = counter;
                    }

                }

                if (buttonGroupView.Checked)
                    fillDataGridView(boxGroups);
            }
            enableFields();
        }

        private void buttonMiscellaneous_Click(object sender, EventArgs e)
        {
            Miscellaneous i = new Miscellaneous(this);
            this.Enabled = false;
            i.Visible = true;
            i.Focus();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // Groß- und Kleinschreibung tolerieren
                string input = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().ToUpper();

                // Auch Leerzeichen und leere Strings erlauben
                if (input.Equals("") || input.Equals(" "))
                    input = "-";

                if (buttonClubView.Checked && club != null)
                {
                    // Vereinssicht
                    if (weeks.Contains(input) && e.RowIndex < club.team.Count)
                        club.team[e.RowIndex].week = input[0];
                }
                else if (buttonGroupView.Checked && boxGroups.SelectedIndex != -1)
                {
                    // Staffelsicht
                    if (weeks.Contains(input) && e.RowIndex < Data.group[boxGroups.SelectedIndex].team.Length)
                        Data.group[boxGroups.SelectedIndex].team[e.RowIndex].week = input[0];
                }
                fillDataGridView(dataGridView);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Data.save(Data.group, Data.club, Club.file, Group.file, Team.file);
        }

        private void boxWeekA_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('A', 'B', Data.field[0], boxWeekA, boxWeekB);
        }

        private void boxWeekB_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('B', 'A', Data.field[0], boxWeekB, boxWeekA);
        }

        private void boxWeekX_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('X', 'Y', Data.field[1], boxWeekX, boxWeekY);
        }

        private void boxWeekY_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('Y', 'X', Data.field[1], boxWeekY, boxWeekX);
        }

        private void setKeys(char week1, char week2, int field, ComboBox cb1, ComboBox cb2)
        {
            if (boxClubs.SelectedIndex >= 0)
            {
                Data.club[boxClubs.SelectedIndex].keys[week1] = cb1.SelectedIndex;
                if (cb1.SelectedIndex == 0)
                    Data.club[boxClubs.SelectedIndex].keys[week2] = 0;
                else
                    Data.club[boxClubs.SelectedIndex].keys[week2] = Data.km.getOpposed(field, cb1.SelectedIndex);
                cb2.SelectedIndex = Data.club[boxClubs.SelectedIndex].keys[week2];
            }
        }

        private void adjustReferenceFields(ComboBox cb1, ComboBox cb2, int idx, int refField)
        {
            cb1.Items.Clear();
            cb1.Items.Add('-');
            for (int i = 1; i <= refField; i++)
                cb1.Items.Add(i);
            if (cb1.Items.Count > Util.toInt(cb1.Text))
                cb1.SelectedIndex = Util.toInt(cb1.Text);
            else
                cb1.SelectedIndex = 0;
            cb2.Items.Clear();
            cb2.Items.Add('-');
            for (int i = 1; i <= refField; i++)
                cb2.Items.Add(i);
            if (cb2.Items.Count > Util.toInt(cb2.Text))
                cb2.SelectedIndex = Util.toInt(cb2.Text);
            else
                cb2.SelectedIndex = 0;
            Data.field[idx] = refField;
            checkWeeks();
            if (boxFieldAB.SelectedIndex != -1 && boxFieldXY.SelectedIndex != -1 && Group.file != null)
            {
                buttonInputFromFile.Enabled = true;
                buttonManualInput.Enabled = true;
            }
        }

        private void boxFieldAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustReferenceFields(boxWeekA, boxWeekB, 0, Util.toInt(boxFieldAB.SelectedItem.ToString()));
        }

        private void boxFieldXY_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustReferenceFields(boxWeekX, boxWeekY, 1, Util.toInt(boxFieldXY.SelectedItem.ToString()));
        }

        public static void checkWeeks()
        {
            if (Data.club == null)
                return;
            for (int i = 0; i < Data.club.Length; i++)
            {
                if (Data.club[i].keys['A'] > Data.field[0])
                    Data.club[i].keys['A'] = 0;
                if (Data.club[i].keys['B'] > Data.field[0])
                    Data.club[i].keys['B'] = 0;
                if (Data.club[i].keys['X'] > Data.field[1])
                    Data.club[i].keys['X'] = 0;
                if (Data.club[i].keys['Y'] > Data.field[1])
                    Data.club[i].keys['Y'] = 0;
            }
        }

        public void solveConflicts(Group[] l, Club[] v)
        {
            int[] conflicts = new int[2];
            List<Conflict> conflictList = Data.getConflicts(l);
            if (conflictList.Count > 0)
            {
                Alternatives a = new Alternatives(conflictList.ToArray(), l, v, this);
                this.Enabled = false;
                a.Visible = true;
            }
            else
            {
                Data.generateKeys();
                for (int i = 0; i < Data.notification.Count; i++)
                    MessageBox.Show(Data.notification[i]);
                Data.notification.Clear();
            }
        }

        private void boxCapacity_CheckedChanged(object sender, EventArgs e)
        {
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
                Data.club[boxClubs.SelectedIndex].capacity = boxCapacity.Checked;
        }

        private void buttonClubView_CheckedChanged(object sender, EventArgs e)
        {
            buttonGroupView.Checked = !buttonClubView.Checked;
            dataGridView.Columns[0].Visible = buttonClubView.Checked;
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
                fillDataGridView(buttonClubView);
            else
                enableBoxesAndButtons();
        }

        private void buttonGroupView_CheckedChanged(object sender, EventArgs e)
        {
            buttonClubView.Checked = !buttonGroupView.Checked;
            dataGridView.Columns[0].Visible = buttonClubView.Checked;
            if (buttonGroupView.Checked && boxGroups.SelectedIndex != -1)
                fillDataGridView(buttonGroupView);
            else
                enableBoxesAndButtons();
        }

        private void buttonManualInput_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            if (Data.group == null)
                Data.group = new Group[0];
            if (Data.club == null)
                Data.club = new Club[0];

            //Data.save(Data.group, Data.club, Club.file, Group.file, Team.file);
            DataInput d = new DataInput(this, Data.club, Data.group);
            d.Visible = true;
        }

        private void Schluesselzahlen_Resize(object sender, EventArgs e)
        {
            dataGridView.Height = this.Height - 100;
            dataGridView.Width = this.Width - 350;
        }

        private void Schluesselzahlen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;
            e.Cancel = !Util.confirm(this, Data.group, Data.club);
            this.Enabled = true;
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView.Rows[e.RowIndex].IsNewRow)
                    return;
                Team t = null;
                if (buttonClubView.Checked)
                {
                    Club v = Data.club[boxClubs.SelectedIndex];
                    t = v.team.ElementAt(e.RowIndex);
                }
                else if (buttonGroupView.Checked)
                {
                    Group l = Data.group[boxGroups.SelectedIndex];
                    t = l.team.ElementAt(e.RowIndex);
                }
                Additional z = new Additional(t, this);
                this.Enabled = false;
                z.Visible = true;
            }
        }

        private void buttonPartner_Click(object sender, EventArgs e)
        {
            Partner p = new Partner(this, Data.club, boxClubs.SelectedIndex);
            this.Enabled = false;
            p.Visible = true;
        }

        private string replaceUmlauts(string input)
        {
            input = input.Replace("&#196;", "Ä");
            input = input.Replace("&#228;", "ä");
            input = input.Replace("&#214;", "Ö");
            input = input.Replace("&#246;", "ö");
            input = input.Replace("&#220;", "Ü");
            input = input.Replace("&#252;", "ü");
            input = input.Replace("&#223;", "ß");
            return input;
        }

        private bool addWishes(Hashtable groups, Hashtable clubs)
        {

            Stream file = openFileDialog1.OpenFile();
            HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument wishes = new HtmlAgilityPack.HtmlDocument();

            bool isHeader = true;
            wishes.Load(file);
            // Regex clubNameAndID = new Regex("(\\w+\\s)+\\([0-9]{6}\\)");
            Regex clubIDPattern = new Regex(@"^[0-9]{5}$");
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

                    foreach (string line in lines)
                    {
                        string parsedLine = replaceUmlauts(line);
                        parsedLine = Util.clear(parsedLine);

                        // Header vorbei?
                        if (parsedLine.Equals("Terminwuensche"))
                            isHeader = false;

                        if (isHeader)
                        {
                            continue;
                        }

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
                        // Neue Mannschaft
                        foreach (string ageGroup in Data.ageGroups)
                        {
                            if (parsedLine.StartsWith(ageGroup))
                            {
                                string team;
                                if (parsedLine.Equals(ageGroup))
                                    team = "I";
                                else
                                    team = parsedLine.Replace(ageGroup, "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                                if (!Util.isRomanNumber(team))
                                    team = "I";
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
                        if (parsedLine.StartsWith("Spielwoche"))
                        {
                            currentTeam.week = parsedLine.ToCharArray()[11];
                        }
                    }
                }
            }

            return true;
        }

        private void buttonClickTTInput_Click(object sender, EventArgs e)
        {
            // Daten-Sammelstrukturen
            Hashtable clubs = new Hashtable();
            Hashtable groups = new Hashtable();

            try
            {
                if (MessageBox.Show("Wählen Sie zunächst die exportierte CSV-Datei mit der Gruppeneinteilung aus!",
                    "Gruppeneinteilung auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    while (!parseGroupsAndClubs(groups, clubs))
                    {
                        if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                            "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                            || openFileDialog1.ShowDialog() != DialogResult.OK)
                            return;
                    }
                else
                    return;

                if (MessageBox.Show("Wählen Sie nun die HTML-Datei mit den Terminwünschen aus!",
                    "Terminwünsche auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    while (!addWishes(groups, clubs))
                    {
                        if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                            "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                            || openFileDialog1.ShowDialog() != DialogResult.OK)
                            return;
                    }
                else
                    return;
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Beim Dateninport ist ein Fehler aufgetreten. Bitte die Dateien überprüfen!");
                this.Enabled = true;
                return;
            }

            // Alles scheint geklappt zu haben!
            saveInData(groups, clubs);
            initUI();
            buttonGenerate.Enabled = true;
            buttonMiscellaneous.Enabled = true;
            buttonSave.Enabled = true;
            buttonClubView.Enabled = true;
            buttonGroupView.Enabled = true;
            buttonClubView.Checked = true;
            boxGroups.Enabled = boxClubs.Enabled = true;
            buttonInputFromFile.Enabled = true;
        }

        private void saveInData(Hashtable groups, Hashtable clubs)
        {
            List<Club> cl = new List<Club>();
            foreach (Club c in clubs.Values)
            {
                cl.Add(c);
            }
            cl.Sort();
            Data.club = cl.ToArray();

            List<Group> lg = new List<Group>();
            foreach (Group g in groups.Values)
            {
                // Feld ermitteln. Default: so klein wie möglich
                g.field = g.nrOfTeams + (g.nrOfTeams % 2);
                g.field = g.field < Data.TEAM_MIN ? Data.TEAM_MIN : g.field;
                lg.Add(g);
            }
            lg.Sort();
            Data.group = lg.ToArray();
        }

        private bool parseGroupsAndClubs(Hashtable groups, Hashtable clubs)
        {
            Club currentClub = null;
            Team currentTeam = null;
            Group currentGroup = null;

            TextFile groupFile = new TextFile(openFileDialog1.FileName);
            List<String> notification = new List<string>();
            String content = groupFile.ReadFile(false, notification);
            content = Util.clear(content);
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

        private void boxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxFields.SelectedIndex != -1)
            {
                Data.group[boxGroups.SelectedIndex].field = Util.toInt(boxFields.SelectedItem.ToString());
                fillDataGridView(boxFields);
            }
        }

        public void prepare()
        {
            boxGroups.Text = "";
            boxGroups.SelectedIndex = -1;
            boxClubs.Text = "";
            boxClubs.SelectedIndex = -1;
            Enabled = true;
            Focus();
        }

        private void boxRuntimeSeconds_TextChanged(object sender, EventArgs e)
        {
            Data.runtime = Util.toInt(boxRuntimeMinutes.Text) * 60 + Util.toInt(boxRuntimeSeconds.Text);
        }

        private void boxRuntimeMinutes_TextChanged(object sender, EventArgs e)
        {
            Data.runtime = Util.toInt(boxRuntimeMinutes.Text) * 60 + Util.toInt(boxRuntimeSeconds.Text);
        }
    }
}
