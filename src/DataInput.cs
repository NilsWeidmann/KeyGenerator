using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class DataInput : Form
    {
        bool automatic;
        CheckBox[] external;
        TextBox[] teamName;
        TextBox[] teamIdent;
        Button[] reset;
        public List<Club> club;
        public List<Group> group;
        public List<Partnership> partnership;
        Schluesselzahlen caller;

        public DataInput(Schluesselzahlen caller)
        {
            this.caller = caller;
            automatic = false;
            external = new CheckBox[Data.TEAM_MAX];
            teamName = new TextBox[Data.TEAM_MAX];
            teamIdent = new TextBox[Data.TEAM_MAX];
            reset = new Button[Data.TEAM_MAX];
            InitializeComponent();
            initGrid();
            assignGUIElements();
            enableGUIElements();
            loadData();
        }

        private void initGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            string[] values = { "Verein", "A", "B", "X", "Y", "Kap." };
            foreach (string s in values)
            {
                dataGridView1.Columns.Add(s, s);
            }
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void loadData()
        {
            button18.Enabled = false;
            String[] values = new String[6];
            partnership = new List<Partnership>();
            Club[] clubArray = Data.getClubs(Club.file, partnership);
            initGrid();
            for (int i = 0; i < clubArray.Length; i++)
            {
                Club v = clubArray[i];
                values[0] = v.name;

                int[] intValues = new int[] { v.keys['A'], v.keys['B'], v.keys['X'], v.keys['Y'] };
                for (int j = 1; j < 5; j++)
                    values[j] = intValues[j - 1].ToString() == "0" ? "" : intValues[j - 1].ToString();

                values[5] = v.capacity ? "X" : "";
                dataGridView1.Rows.Add(values);
            }

            Group[] groupArray = Group.getGroups(clubArray, Group.file, Data.notification);
            Data.getRelations(groupArray, Team.file);
            Data.allocateTeams(clubArray, groupArray);

            club = clubArray.ToList<Club>();
            group = groupArray.ToList<Group>();
            comboBox1.Items.Clear();
            for (int i = 0; i < groupArray.Length; i++)
                comboBox1.Items.Add(groupArray[i].name);
            comboBox1.SelectedIndex = -1;
            button18.Enabled = true;
        }

        private void Dateninput_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show("Wollen Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.No:
                    caller.Enabled = true;
                    caller.Focus();
                    break;
                case DialogResult.Yes:
                    Data.save(group.ToArray(), club.ToArray(), partnership, Club.file, Group.file, Team.file);
                    caller.loadFromFile(Club.file, Group.file, Team.file);
                    caller.Enabled = true;
                    caller.Focus();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Width - 400;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Data.save(group.ToArray(), club.ToArray(), partnership, Club.file, Group.file, Team.file);
            caller.loadFromFile(Club.file, Group.file, Team.file);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableGUIElements();
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            // Mögliche Feldgrößen ermitteln
            if (comboBox1.SelectedIndex == -1)
                comboBox2.SelectedIndex = -1;
            else
            {
                Group l = group.ElementAt(comboBox1.SelectedIndex);
                int minFieldSize = l.nrOfTeams < Data.TEAM_MIN ? Data.TEAM_MIN : l.nrOfTeams + l.nrOfTeams % 2;
                for (int i = minFieldSize; i <= Data.TEAM_MAX; i += 2)
                    comboBox2.Items.Add(i);
                for (int i = 0; i < comboBox2.Items.Count; i++)
                    if (Util.toInt(comboBox2.Items[i].ToString()) == l.field)
                        comboBox2.SelectedIndex = i;
            }
        }

        public void enableGUIElements()
        {
            automatic = true;
            if (comboBox1.SelectedIndex == -1)
                for (int i = 0; i < Data.TEAM_MAX; i++)
                {
                    external[i].Enabled = false;
                    external[i].Checked = false;
                    teamName[i].Enabled = false;
                    teamName[i].Text = "";
                    teamIdent[i].Enabled = false;
                    teamIdent[i].Text = "";
                    reset[i].Enabled = false;
                }
            else
            {
                for (int i = 0; i < group.ElementAt(comboBox1.SelectedIndex).nrOfTeams; i++)
                {
                    external[i].Enabled = false;
                    teamIdent[i].Enabled = true;
                    reset[i].Enabled = true;

                    if (club.Contains(group.ElementAt(comboBox1.SelectedIndex).team[i].club))
                    {
                        external[i].Checked = false;
                        teamName[i].Enabled = false;
                    }
                    else
                    {
                        external[i].Checked = true;
                        teamName[i].Enabled = true;
                    }

                    teamName[i].Text = group.ElementAt(comboBox1.SelectedIndex).team[i].club.name;
                    teamIdent[i].Text = group.ElementAt(comboBox1.SelectedIndex).team[i].team;

                }
                for (int i = group.ElementAt(comboBox1.SelectedIndex).nrOfTeams; i < Data.TEAM_MAX; i++)
                {
                    external[i].Enabled = false;
                    external[i].Checked = false;
                    teamName[i].Enabled = false;
                    teamName[i].Text = "";
                    teamIdent[i].Enabled = false;
                    teamIdent[i].Text = "";
                    reset[i].Enabled = false;
                }
                if (group.ElementAt(comboBox1.SelectedIndex).nrOfTeams < group.ElementAt(comboBox1.SelectedIndex).field)
                    external[group.ElementAt(comboBox1.SelectedIndex).nrOfTeams].Enabled = true; ;
            }
            automatic = false;
        }

        private void deleteTeam(Group l, int t)
        {
            l.nrOfTeams--;
            for (int i = t; i < l.nrOfTeams; i++)
                l.team[i] = l.team[i + 1];
            l.team[l.nrOfTeams] = null;
            enableGUIElements();
        }

        private void createExternal(Group l, int t)
        {
            if (l.nrOfTeams == t)
                l.nrOfTeams++;
            l.team[t] = new Team();
            l.team[t].index = t;
            l.team[t].group = l;
            l.team[t].week = '-';
            l.team[t].key = 0;
            l.team[t].club = new Club();
            l.team[t].club.index = -1;
            enableGUIElements();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;
            Group g = group.ElementAt(comboBox1.SelectedIndex);
            if (e.ColumnIndex == 0 && g.nrOfTeams < g.field)
            {
                g.team[g.nrOfTeams] = new Team();
                g.team[g.nrOfTeams].index = g.nrOfTeams;
                g.team[g.nrOfTeams].name = club.ElementAt(e.RowIndex).name;
                g.team[g.nrOfTeams].team = "";
                g.team[g.nrOfTeams].group = g;
                g.team[g.nrOfTeams].club = club.ElementAt(e.RowIndex);
                g.team[g.nrOfTeams].week = '-';
                g.team[g.nrOfTeams].key = 0;
                g.nrOfTeams++;
                enableGUIElements();
            }
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.TEAM_MAX; i++)
                if (sender.Equals(reset[i]))
                    deleteTeam(group.ElementAt(comboBox1.SelectedIndex), i);
        }

        private void checkBoxI_CheckedChanged(object sender, EventArgs e)
        {
            if (!automatic)
                for (int i = 0; i < Data.TEAM_MAX; i++)
                    if (sender.Equals(external[i]))
                    {
                        if (external[i].Checked)
                            createExternal(group.ElementAt(comboBox1.SelectedIndex), i);
                        else
                            deleteTeam(group.ElementAt(comboBox1.SelectedIndex), i);
                    }
        }

        private void textBoxI_Leave(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.TEAM_MAX; i++)
                if (sender.Equals(teamName[i]) || sender.Equals(teamIdent[i]))
                {
                    teamName[i].Text = Util.clear(teamName[i].Text);
                    teamIdent[i].Text = Util.clear(teamIdent[i].Text);
                    group.ElementAt(comboBox1.SelectedIndex).team[i].name = teamName[i].Text + " " + teamIdent[i].Text;
                    group.ElementAt(comboBox1.SelectedIndex).team[i].team = teamIdent[i].Text;
                    group.ElementAt(comboBox1.SelectedIndex).team[i].club.name = teamName[i].Text;
                }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow && e.ColumnIndex != 0)
                return;
            String value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
            int zahl = Util.toInt(value);
            switch (e.ColumnIndex)
            {
                case 0:
                    if (value.Equals(""))
                        break;
                    club.ElementAt(e.RowIndex).name = Util.clear(value);
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = club.ElementAt(e.RowIndex).name;
                    break;
                case 1:
                    assignValue('A', 'B', zahl, Data.field[0], e.RowIndex, 1, 2);
                    break;
                case 2:
                    assignValue('B', 'A', zahl, Data.field[0], e.RowIndex, 2, 1);
                    break;
                case 3:
                    assignValue('X', 'Y', zahl, Data.field[1], e.RowIndex, 3, 4);
                    break;
                case 4:
                    assignValue('Y', 'X', zahl, Data.field[1], e.RowIndex, 4, 3);
                    break;
                case 5:
                    if (value.Equals(""))
                        club.ElementAt(e.RowIndex).capacity = false;
                    else
                        club.ElementAt(e.RowIndex).capacity = true;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = club.ElementAt(e.RowIndex).capacity ? "X" : "";
                    break;
            }
        }

        private void assignValue(char week1, char week2, int key, int field, int rowIdx, int colIdx1, int colIdx2)
        {
            if (key > 0 && key <= field)
            {
                club.ElementAt(rowIdx).keys[week1] = key;
                club.ElementAt(rowIdx).keys[week2] = Data.km.getOpposed(field, field, key);
            }
            else
            {
                club.ElementAt(rowIdx).keys[week1] = 0;
                club.ElementAt(rowIdx).keys[week2] = 0;
            }
            if (club.ElementAt(rowIdx).keys[week1] == 0)
                dataGridView1.Rows[rowIdx].Cells[colIdx1].Value = "";
            else
                dataGridView1.Rows[rowIdx].Cells[colIdx1].Value = club.ElementAt(rowIdx).keys[week1].ToString();
            if (club.ElementAt(rowIdx).keys[week2] == 0)
                dataGridView1.Rows[rowIdx].Cells[colIdx2].Value = "";
            else
                dataGridView1.Rows[rowIdx].Cells[colIdx2].Value = club.ElementAt(rowIdx).keys[week2].ToString();
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Club v = new Club();
            v.index = e.Row.Index - 1;
            for (int i = e.Row.Index - 1; i < club.Count; i++)
                club.ElementAt(i).index++;
            club.Insert(e.Row.Index - 1, v);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (Partnership p in partnership)
                if (p.clubA.index == e.Row.Index - 1 || p.clubB.index == e.Row.Index - 1)
                    partnership.Remove(p);
            club.RemoveAt(e.Row.Index - 1);
            for (int i = e.Row.Index - 1; i < club.Count; i++)
                club.ElementAt(i).index--;

        }

        private void assignGUIElements()
        {
            CheckBox[] checkBoxes = {checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7,
                                     checkBox8, checkBox9,checkBox10,checkBox11,checkBox12,checkBox13,checkBox14};

            for (int i = 0; i < 14; i++)
                external[i] = checkBoxes[i];

            TextBox[] textBoxes = { textBox1,  textBox2, textBox3, textBox4, textBox5, textBox6, textBox7,
                                    textBox8,  textBox9,textBox10,textBox11,textBox12,textBox13,textBox14,
                                   textBox15, textBox16,textBox17,textBox18,textBox19,textBox20,textBox21,
                                   textBox22, textBox23,textBox24,textBox25,textBox26,textBox27,textBox28 };

            for (int i = 0; i < 14; i++)
                teamName[i] = textBoxes[i];

            for (int i = 0; i < 14; i++)
                teamIdent[i] = textBoxes[i + 14];

            Button[] buttons = {button1, button2, button3, button4, button5, button6, button7,
                                button8, button9,button10,button11,button12,button13,button14};

            for (int i = 0; i < 14; i++)
                reset[i] = buttons[i];
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("")) 
            { 
                MessageBox.Show("Geben Sie zunächst einen Namen für die neue Liga an!", "Liga anlegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                foreach (Group l in this.group)
                    if (l.name.Equals(comboBox1.Text))
                    {
                        MessageBox.Show("Der Name " + comboBox1.Text + " ist bereits vergeben!", "Liga anlegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
            Group group = new Group();
            group.name = Util.clear(comboBox1.Text);
            group.field = Data.field[0];
            group.team = new Team[Data.TEAM_MAX];
            group.index = this.group.Count;
            this.group.Add(group);
            comboBox1.Items.Add(group.name);
            comboBox1.SelectedIndex = group.index;
            enableGUIElements();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                switch (MessageBox.Show("Wollen Sie die " + group[comboBox1.SelectedIndex].name + " löschen?", "Liga löschen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        group.RemoveAt(comboBox1.SelectedIndex);
                        for (int i = comboBox1.SelectedIndex; i < group.Count; i++)
                            group[i].index--;
                        comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                        enableGUIElements();
                        this.Enabled = true;
                        this.Focus();
                        break;
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.Abort:
                        break;
                }
        }

        private void Dateninput_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
            {
                group.ElementAt(comboBox1.SelectedIndex).field = Util.toInt(comboBox2.SelectedItem.ToString());
                enableGUIElements();
            }
        }
    }
}
