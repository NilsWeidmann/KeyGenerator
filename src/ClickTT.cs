using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace KeyGenerator
{
    public partial class ClickTT : Form
    {
        Hashtable clubs;
        Hashtable groups;
        Club currentClub;
        Group currentGroup;
        readonly KeyGenerator caller;

        public ClickTT(KeyGenerator caller, Group[] group, Club[] club)
        {
            InitializeComponent();
            this.caller = caller;
            clubs = Util.toHashtable(club.ToList());
            groups = Util.toHashtable(group.ToList());
            currentClub = null;
            currentGroup = null;
            init();
        }

        private void init()
        {
            dataGridViewGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initGroupGrid(dataGridViewGroups, true);

            dataGridViewClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initClubGrid(dataGridViewClubs, true);

            dataGridViewTeamsOfGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initTeamGrid(dataGridViewTeamsOfGroups, false);

            dataGridViewTeamsOfClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initTeamGrid(dataGridViewTeamsOfClubs, true);

            Visualization.fillGroupGrid(dataGridViewGroups, Util.toGroupArray(groups));
            Visualization.fillClubGrid(dataGridViewClubs, Util.toClubArray(clubs));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();

            SuspendLayout();
            // Set proper heights
            groupBox2.Top = (this.Height - 95) / 2 + 20;
            groupBox1.Height = (this.Height - 95) / 2;
            groupBox2.Height = (this.Height - 95) / 2;
            dataGridViewGroups.Height = (this.Height - 200) / 2;
            dataGridViewClubs.Height = (this.Height - 200) / 2;
            dataGridViewTeamsOfGroups.Height = (this.Height - 200) / 2;
            dataGridViewTeamsOfClubs.Height = (this.Height - 200) / 2;
            // Set proper widths
            dataGridViewGroups.Width = (this.Width - 68) / 2;
            dataGridViewClubs.Width = (this.Width - 68) / 2;
            dataGridViewTeamsOfGroups.Width = (this.Width - 68) / 2;
            dataGridViewTeamsOfClubs.Width = (this.Width - 68) / 2;
            groupBox1.Width = this.Width - 40;
            groupBox2.Width = this.Width - 40;
            boxLinkGroups.Width = this.Width - 250;
            boxLinkClubs.Width = this.Width - 250;
            buttonDeleteTeamAddGroup.Left = groupBox1.Width - 160;
            buttonDeleteTeamAddClub.Left = groupBox2.Width - 160;
            dataGridViewTeamsOfGroups.Left = dataGridViewGroups.Left + dataGridViewGroups.Width + 12;
            dataGridViewTeamsOfClubs.Left = dataGridViewGroups.Left + dataGridViewGroups.Width + 12;
            buttonDeleteTeamSave.Left = this.Width - 110;
            buttonDeleteTeamSave.Top = this.Height - 70;
            ResumeLayout();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTeamsOfGroups.Rows.Clear();
            if (groups != null && e.RowIndex < groups.Count)
            {
                string groupname = dataGridViewGroups.Rows[e.RowIndex].Cells[0].Value.ToString();
                Visualization.visualizeGroupData((Group)groups[groupname], dataGridViewTeamsOfGroups);
                currentGroup = (Group)groups[groupname];
            }
        }

        private void dataGridViewClubs_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTeamsOfClubs.Rows.Clear();
            if (clubs != null && e.RowIndex < clubs.Count)
            {
                string clubname = dataGridViewClubs.Rows[e.RowIndex].Cells[0].Value.ToString();
                Visualization.visualizeClubData((Club)clubs[clubname], dataGridViewTeamsOfClubs);
                currentClub = (Club)clubs[clubname];
            }
        }

        private void buttonDeleteTeamSave_Click(object sender, EventArgs e)
        {
            buttonDeleteTeamSave.Enabled = false;
            Data.save(Util.toGroupArray(groups), Util.toClubArray(clubs), Club.file, Group.file, Team.file);
            caller.loadFromFile(Club.file, Group.file, Team.file);
            buttonDeleteTeamSave.Enabled = true;
        }

        private void ClickTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonDeleteTeamSave.Enabled)
            {
                e.Cancel = !Util.confirm(caller, Util.toGroupArray(groups), Util.toClubArray(clubs));
            }
            else
            {
                this.Visible = false;
                caller.Enabled = true;
                caller.Focus();
            }
        }

        private void buttonDeleteTeamAddGroup_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Wählen Sie zunächst die exportierte CSV-Datei mit der Gruppeneinteilung aus!",
            //        "Gruppeneinteilung auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            //    return;

            clubs = new Hashtable();
            groups = new Hashtable();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                while (!Parser.parseGroupsAndClubs(groups, clubs, openFileDialog1))
                {
                    if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                        "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                        || openFileDialog1.ShowDialog() != DialogResult.OK)
                        return;
                }
            else
                return;

            boxLinkGroups.Text = openFileDialog1.FileName;

            // Feld ermitteln. Default: so klein wie möglich
            foreach (Group g in groups.Values)
                if (g.field == 0)
                    g.field = g.nrOfTeams < Data.TEAM_MIN ? Data.TEAM_MIN : g.nrOfTeams + (g.nrOfTeams % 2);

            init();
        }

        private void buttonDeleteTeamAddClub_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Wählen Sie nun die HTML-Datei mit den Terminwünschen aus!",
            //        "Terminwünsche auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            //    return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                while (!Parser.addWishes(groups, clubs, openFileDialog1))
                {
                    if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                        "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                        || openFileDialog1.ShowDialog() != DialogResult.OK)
                        return;
                }
            else
                return;

            boxLinkClubs.Text = openFileDialog1.FileName;
            init();
        }

        private void dataGridViewTeamsOfClubs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentClub != null)
                Visualization.changeWeek(dataGridViewTeamsOfClubs, e, true, null, currentClub);
        }

        private void dataGridViewTeamsOfGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentGroup != null)
                Visualization.changeWeek(dataGridViewTeamsOfGroups, e, false, currentGroup, null);
        }

        private void dataGridViewClubs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Visualization.changeClubData(e, dataGridViewClubs, Util.toClubArray(clubs).ToList());
        }

        private void dataGridViewGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Visualization.changeField(dataGridViewGroups, e, currentGroup);
        }

        private void ClickTT_SizeChanged(object sender, EventArgs e)
        {

        }

        private void ClickTT_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
