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
        private Group[] group;
        private Club[] club;
        private Club currentClub;
        private string[] weeks = { "-", "A", "B", "X", "Y" };

        public KeyGenerator()
        {
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
            group = new Group[0];
            club = new Club[0];

            dataGridView.Height = this.Height - 100;
            dataGridView.Width = this.Width - 350;
            enableBoxesAndButtons();
        }

        private void initDataGridView()
        {
            Visualization.initTeamGrid(dataGridView, buttonClubView.Checked);
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
                Data.copy(best_l, group, best_v, club);
                solveConflicts(group, club);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Data.runtime = Util.toInt(boxRuntimeMinutes.Text) * 60 + Util.toInt(boxRuntimeSeconds.Text);
            PleaseWait bw = new PleaseWait(this, true, group, club);
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

            if (group != null)
                for (int i = 0; i < group.Length; i++)
                    boxGroups.Items.Add(group[i].name);
            if (club != null)
                for (int i = 0; i < club.Length; i++)
                    boxClubs.Items.Add(club[i].name);
            enableFields();
        }

        public bool loadFromFile(TextFile clubFile, TextFile groupFile, TextFile relationshipFile)
        {
            buttonManualInput.Enabled = buttonClickTTInput.Enabled = buttonInputFromFile.Enabled = false;
            club = Club.getClubs(clubFile, Data.notification);
            group = Group.getGroups(club, groupFile, Data.notification);
            Group.getRelations(group, relationshipFile, Data.notification);
            Data.allocateTeams(club, group);

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
                    boxCapacity.Checked = club[boxClubs.SelectedIndex].capacity;
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
                    boxFields.Text = group[boxGroups.SelectedIndex].field.ToString();
                }
            }
        }

        public void fillDataGridView(Control caller)
        {
            dataGridView.Rows.Clear();
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
            {
                // Vereinssicht
                currentClub = club[boxClubs.SelectedIndex];
                if (boxClubs.SelectedIndex == -1)
                {
                    boxCapacity.Checked = boxCapacity.Enabled = buttonPartner.Enabled = false;
                    boxWeekA.Text = boxWeekB.Text = boxWeekX.Text = boxWeekY.Text = "";
                }
                else
                {
                    boxCapacity.Checked = currentClub.capacity;
                    buttonPartner.Enabled = true;
                }
                if (boxWeekA.Items.Count > currentClub.keys['A'])
                    boxWeekA.SelectedIndex = currentClub.keys['A'];
                if (boxWeekB.Items.Count > currentClub.keys['B'])
                    boxWeekB.SelectedIndex = currentClub.keys['B'];
                if (boxWeekX.Items.Count > currentClub.keys['X'])
                    boxWeekX.SelectedIndex = currentClub.keys['X'];
                if (boxWeekY.Items.Count > currentClub.keys['Y'])
                    boxWeekY.SelectedIndex = currentClub.keys['Y'];
                Visualization.visualizeClubData(currentClub, dataGridView);
            }
            else if (buttonGroupView.Checked && boxGroups.SelectedIndex != -1)
            {
                // Staffelsicht
                Visualization.visualizeGroupData(group[boxGroups.SelectedIndex], dataGridView);
                currentClub = null;
            }
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                for (int i = 0; i < group[boxGroups.SelectedIndex].nrOfTeams; i++)
                {
                    boxFields.Items.Clear();

                    Group currentGroup = group[boxGroups.SelectedIndex];
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
            Miscellaneous i = new Miscellaneous(this, group, club);
            this.Enabled = false;
            i.Visible = true;
            i.Focus();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Group currentGroup = boxGroups.SelectedIndex < 0 ? null : group[boxGroups.SelectedIndex];
            Visualization.changeWeek(dataGridView, e, buttonClubView.Checked, currentGroup, currentClub);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Data.save(group, club, Club.file, Group.file, Team.file);
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
                club[boxClubs.SelectedIndex].keys[week1] = cb1.SelectedIndex;
                if (cb1.SelectedIndex == 0)
                    club[boxClubs.SelectedIndex].keys[week2] = 0;
                else
                    club[boxClubs.SelectedIndex].keys[week2] = Data.km.getOpposed(field, cb1.SelectedIndex);
                cb2.SelectedIndex = club[boxClubs.SelectedIndex].keys[week2];
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

        public void checkWeeks()
        {
            if (club == null)
                return;
            for (int i = 0; i < club.Length; i++)
            {
                if (club[i].keys['A'] > Data.field[0])
                    club[i].keys['A'] = 0;
                if (club[i].keys['B'] > Data.field[0])
                    club[i].keys['B'] = 0;
                if (club[i].keys['X'] > Data.field[1])
                    club[i].keys['X'] = 0;
                if (club[i].keys['Y'] > Data.field[1])
                    club[i].keys['Y'] = 0;
            }
        }

        public static List<Conflict> getConflicts(Group[] groups)
        {
            int[] allocation;
            List<Conflict> conflicts = new List<Conflict>();
            int team;
            int number;
            int index = 0;

            foreach (Group group in groups)
            {
                allocation = new int[group.field];
                for (int j = 0; j < group.nrOfTeams; j++)
                    if (group.team[j].key > 0)
                        allocation[group.team[j].key - 1]++;
                for (int j = 0; j < group.field; j++)
                    if (allocation[j] > 1)
                    {
                        team = 0;
                        number = 0;
                        Conflict conflict = new Conflict
                        {
                            wish = j + 1,
                            t = new Team[allocation[j]],
                            index = index++
                        };
                        for (int x = 0; x < group.nrOfTeams; x++)
                            if (group.team[x].key == j + 1)
                                conflict.t[team++] = group.team[x];

                        int alt1 = Data.km.getSimilar(group.field, j + 1).Item1;
                        int alt2 = Data.km.getSimilar(group.field, j + 1).Item2;
                        conflict.key[number++] = j + 1;
                        if (alt1 > 0 && allocation[alt1 - 1] == 0)
                            conflict.key[number++] = alt1;
                        if (alt2 > 0 && allocation[alt2 - 1] == 0)
                            conflict.key[number++] = alt2;
                        if (conflict.key[allocation[j] - 1] == 0)
                        {
                            conflict.key[1] = alt1;
                            conflict.key[2] = alt2;
                        }
                        conflicts.Add(conflict);
                    }
            }
            return conflicts;
        }

        public void solveConflicts(Group[] group, Club[] club)
        {
            int[] conflicts = new int[2];
            List<Conflict> conflictList = getConflicts(group);
            if (conflictList.Count > 0)
            {
                Alternatives a = new Alternatives(conflictList.ToArray(), group, club, this);
                this.Enabled = false;
                a.Visible = true;
            }
            else
            {
                Data.setOptions(group);
                KeyAssignmentFinalizer.generateKeys(group, club,this,Data.notification);
                for (int i = 0; i < Data.notification.Count; i++)
                    MessageBox.Show(Data.notification[i]);
                Data.notification.Clear();
            }
        }

        private void boxCapacity_CheckedChanged(object sender, EventArgs e)
        {
            if (buttonClubView.Checked && boxClubs.SelectedIndex != -1)
                club[boxClubs.SelectedIndex].capacity = boxCapacity.Checked;
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

            if (group == null)
                group = new Group[0];
            if (club == null)
                club = new Club[0];

            //Data.save(group, club, Club.file, Group.file, Team.file);
            DataInput d = new DataInput(this, club, group);
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
            e.Cancel = !Util.confirm(this, group, club);
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
                    Club v = club[boxClubs.SelectedIndex];
                    t = v.team.ElementAt(e.RowIndex);
                }
                else if (buttonGroupView.Checked)
                {
                    Group l = group[boxGroups.SelectedIndex];
                    t = l.team.ElementAt(e.RowIndex);
                }
                Additional z = new Additional(group, club, t, this);
                this.Enabled = false;
                z.Visible = true;
            }
        }

        private void buttonPartner_Click(object sender, EventArgs e)
        {
            Partner p = new Partner(this, club, group, boxClubs.SelectedIndex, new string[]{ "A", "B", "X", "Y"});
            this.Enabled = false;
            p.Visible = true;
        }

        private void buttonClickTTInput_Click(object sender, EventArgs e)
        {
            ClickTT clickTT = new ClickTT(this,group,club);
            this.Enabled = false;
            clickTT.Visible = true;
        }      

        private void boxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxFields.SelectedIndex != -1)
            {
                group[boxGroups.SelectedIndex].field = Util.toInt(boxFields.SelectedItem.ToString());
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
