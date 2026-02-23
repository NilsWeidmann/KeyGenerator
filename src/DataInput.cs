using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyGenerator
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
        KeyGenerator caller;

        public DataInput(KeyGenerator caller, Club[] clubs, Group[] groups)
        {
            this.caller = caller;
            automatic = false;
            external = new CheckBox[Data.TEAM_MAX];
            teamName = new TextBox[Data.TEAM_MAX];
            teamIdent = new TextBox[Data.TEAM_MAX];
            reset = new Button[Data.TEAM_MAX];
            InitializeComponent();
            Visualization.initClubGrid(dataGridViewClubsAndKeys, false);
            assignGUIElements();
            enableGUIElements();
            club = clubs.ToList();
            group = groups.ToList();
            loadData(clubs, groups);
        }

        private void loadData(Club[] clubArray, Group[] groupArray)
        {
            buttonSave.Enabled = false;
            partnership = new List<Partnership>();

            // DataGridView füllen
            Visualization.initClubGrid(dataGridViewClubsAndKeys, false);
            Visualization.fillClubGrid(dataGridViewClubsAndKeys, clubArray);

            // Gruppenliste füllen
            club = clubArray.ToList<Club>();
            group = groupArray.ToList<Group>();
            boxGroup.Items.Clear();
            for (int i = 0; i < groupArray.Length; i++)
                boxGroup.Items.Add(groupArray[i].name);
            boxGroup.SelectedIndex = -1;
            buttonSave.Enabled = true;
        }

        private void DataInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;
            e.Cancel = !Util.confirm(caller, group.ToArray(), club.ToArray());
            this.Enabled = true;
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            SuspendLayout();
            splitContainer1.SplitterDistance = splitContainer1.Width - 480;
            ResumeLayout();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Data.save(group.ToArray(), club.ToArray(), Club.file, Group.file, Team.file);
            caller.loadFromFile(Club.file, Group.file, Team.file);
        }

        private void boxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableGUIElements();
            boxField.Items.Clear();
            boxField.Text = "";

            // Mögliche Feldgrößen ermitteln
            if (boxGroup.SelectedIndex == -1)
                boxField.SelectedIndex = -1;
            else
            {
                fillBoxField();
            }
        }

        private void fillBoxField()
        {
            boxField.Items.Clear();
            boxField.Text = "";
            Group l = group.ElementAt(boxGroup.SelectedIndex);
            int minFieldSize = l.nrOfTeams < Data.TEAM_MIN ? Data.TEAM_MIN : l.nrOfTeams + l.nrOfTeams % 2;
            for (int i = minFieldSize; i <= Data.TEAM_MAX; i += 2)
                boxField.Items.Add(i);
            for (int i = 0; i < boxField.Items.Count; i++)
                if (Util.toInt(boxField.Items[i].ToString()) == l.field)
                    boxField.SelectedIndex = i;
            boxField.Text = boxField.SelectedItem.ToString();
        }
        public void enableGUIElements()
        {
            automatic = true;
            if (boxGroup.SelectedIndex == -1)
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
                for (int i = 0; i < group.ElementAt(boxGroup.SelectedIndex).nrOfTeams; i++)
                {
                    external[i].Enabled = false;
                    teamIdent[i].Enabled = true;
                    reset[i].Enabled = true;

                    if (club.Contains(group.ElementAt(boxGroup.SelectedIndex).team[i].club))
                    {
                        external[i].Checked = false;
                        teamName[i].Enabled = false;
                    }
                    else
                    {
                        external[i].Checked = true;
                        teamName[i].Enabled = true;
                    }

                    teamName[i].Text = group.ElementAt(boxGroup.SelectedIndex).team[i].club.name;
                    teamIdent[i].Text = group.ElementAt(boxGroup.SelectedIndex).team[i].team;

                }
                for (int i = group.ElementAt(boxGroup.SelectedIndex).nrOfTeams; i < Data.TEAM_MAX; i++)
                {
                    external[i].Enabled = false;
                    external[i].Checked = false;
                    teamName[i].Enabled = false;
                    teamName[i].Text = "";
                    teamIdent[i].Enabled = false;
                    teamIdent[i].Text = "";
                    reset[i].Enabled = false;
                }
                if (group.ElementAt(boxGroup.SelectedIndex).nrOfTeams < group.ElementAt(boxGroup.SelectedIndex).field)
                    external[group.ElementAt(boxGroup.SelectedIndex).nrOfTeams].Enabled = true;
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
            if (boxGroup.SelectedIndex == -1)
                return;
            Group g = group.ElementAt(boxGroup.SelectedIndex);
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
                fillBoxField();
            }
        }

        private void buttonDeleteTeamI_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.TEAM_MAX; i++)
                if (sender.Equals(reset[i]))
                    deleteTeam(group.ElementAt(boxGroup.SelectedIndex), i);
            fillBoxField();
        }

        private void boxExternalI_CheckedChanged(object sender, EventArgs e)
        {
            if (!automatic)
                for (int i = 0; i < Data.TEAM_MAX; i++)
                    if (sender.Equals(external[i]))
                    {
                        if (external[i].Checked)
                            createExternal(group.ElementAt(boxGroup.SelectedIndex), i);
                        else
                            deleteTeam(group.ElementAt(boxGroup.SelectedIndex), i);
                    }
        }

        private void boxClubI_Leave(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.TEAM_MAX; i++)
                if (sender.Equals(teamName[i]) || sender.Equals(teamIdent[i]))
                {
                    teamName[i].Text = Util.clear(teamName[i].Text);
                    teamIdent[i].Text = Util.clear(teamIdent[i].Text);
                    group.ElementAt(boxGroup.SelectedIndex).team[i].name = teamName[i].Text + " " + teamIdent[i].Text;
                    group.ElementAt(boxGroup.SelectedIndex).team[i].team = teamIdent[i].Text;
                    group.ElementAt(boxGroup.SelectedIndex).team[i].club.name = teamName[i].Text;
                }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Visualization.changeClubData(e, dataGridViewClubsAndKeys, club);
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Visualization.addClub(e, club);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Visualization.deleteClub(e, club, partnership);

        }

        private void assignGUIElements()
        {
            CheckBox[] boxExternales = {boxExternal1, boxExternal2, boxExternal3, boxExternal4, boxExternal5, boxExternal6, boxExternal7,
                                     boxExternal8, boxExternal9,boxExternal10,boxExternal11,boxExternal12,boxExternal13,boxExternal14};

            for (int i = 0; i < 14; i++)
                external[i] = boxExternales[i];

            TextBox[] boxClubes = { boxClub1,  boxClub2, boxClub3, boxClub4, boxClub5, boxClub6, boxClub7,
                                    boxClub8,  boxClub9,boxClub10,boxClub11,boxClub12,boxClub13,boxClub14,
                                   boxTeam1, boxTeam2,boxTeam3,boxTeam4,boxTeam5,boxTeam6,boxTeam7,
                                   boxTeam8, boxTeam9,boxTeam10,boxTeam11,boxTeam12,boxTeam13,boxTeam14 };

            for (int i = 0; i < 14; i++)
                teamName[i] = boxClubes[i];

            for (int i = 0; i < 14; i++)
                teamIdent[i] = boxClubes[i + 14];

            Button[] buttonDeleteTeams = {buttonDeleteTeam1, buttonDeleteTeam2, buttonDeleteTeam3, buttonDeleteTeam4, buttonDeleteTeam5, buttonDeleteTeam6, buttonDeleteTeam7,
                                buttonDeleteTeam8, buttonDeleteTeam9,buttonDeleteTeam10,buttonDeleteTeam11,buttonDeleteTeam12,buttonDeleteTeam13,buttonDeleteTeam14};

            for (int i = 0; i < 14; i++)
                reset[i] = buttonDeleteTeams[i];
        }

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            if (boxGroup.Text.Equals("")) 
            { 
                MessageBox.Show("Geben Sie zunächst einen Namen für die neue Gruppe an!", "Gruppe anlegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                foreach (Group l in this.group)
                    if (l.name.Equals(boxGroup.Text))
                    {
                        MessageBox.Show("Der Name " + boxGroup.Text + " ist bereits vergeben!", "Gruppe anlegen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
            Group group = new Group();
            group.name = Util.clear(boxGroup.Text);
            group.field = Data.field[0];
            group.team = new Team[Data.TEAM_MAX];
            group.nrOfTeams = 0;
            group.index = this.group.Count;
            this.group.Add(group);
            boxGroup.Items.Add(group.name);
            boxGroup.SelectedIndex = group.index;
            enableGUIElements();
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            if (boxGroup.SelectedIndex != -1)
                switch (MessageBox.Show("Wollen Sie die " + group[boxGroup.SelectedIndex].name + " löschen?", "Gruppe löschen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        group.RemoveAt(boxGroup.SelectedIndex);
                        for (int i = boxGroup.SelectedIndex; i < group.Count; i++)
                            group[i].index--;
                        boxGroup.Items.RemoveAt(boxGroup.SelectedIndex);
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

        private void DataInput_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void boxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxField.SelectedIndex != -1 && boxGroup.SelectedIndex != -1)
            {
                int newField = Util.toInt(boxField.SelectedItem.ToString());
                if (newField < group.ElementAt(boxGroup.SelectedIndex).nrOfTeams)
                    boxField.SelectedIndex = -1;
                else
                    group.ElementAt(boxGroup.SelectedIndex).field = newField;
                enableGUIElements();
            }
        }
    }
}
