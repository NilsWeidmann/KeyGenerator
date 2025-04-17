using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Miscellaneous : Form
    {
        public KeyGenerator caller;

        public Miscellaneous(KeyGenerator caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        private void buttonDeleteTeamLoadBackup_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                caller.loadFromFile(Club.backup, Group.backup, Team.backup);
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Es ist kein Backup vorhanden!");
                caller.loadFromFile(Club.file, Group.file, Team.file);
            }
            returnToCaller();
        }

        private void buttonDeleteTeamCancel_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            returnToCaller();
        }

        private void Miscellaneous_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = false;
            returnToCaller();
        }

        private void deleteClubsWithoutTeams()
        {
            List<Club> lv = Data.club.ToList();
            List<Club> lv_d = new List<Club>();
            foreach (Club v in lv)
                if (v.team == null || v.team.Count == 0)
                    lv_d.Add(v);
            foreach (Club v in lv_d)
                lv.Remove(v);
            Data.club = lv.ToArray();
            caller.boxClubs.Items.Clear();
            for (int i = 0; i < Data.club.Length; i++)
                caller.boxClubs.Items.Add(Data.club[i].name);
        }

        private void buttonDeleteTeamDeleteClubsWithoutTeams_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            deleteClubsWithoutTeams();
            returnToCaller();
        }

        private void buttonDeleteTeamDeleteWeeks_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.group.Length; i++)
                for (int j = 0; j < Data.group[i].nrOfTeams; j++)
                    Data.group[i].team[j].week = '-';
            returnToCaller();
        }

        private void buttonDeleteTeamDeleteKeysForTeams_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.group.Length; i++)
                for (int j = 0; j < Data.group[i].nrOfTeams; j++)
                    Data.group[i].team[j].key = 0;
            returnToCaller();
        }

        private void buttonDeleteTeamDeleteKeysForClubs_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Enabled = false;
            for (int i = 0; i < Data.club.Length; i++)
                foreach (char c in Data.club[i].keys.Keys)
                    Data.club[i].keys[c] = 0;
            returnToCaller();
        }

        private void buttonDeleteTeamDeleteDays_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.group.Length; i++)
                for (int j = 0; j < Data.group[i].nrOfTeams; j++)
                    for (int k = 0; k < Data.TEAM_MAX; k++)
                        Data.group[i].team[j].day[k] = '-';
            returnToCaller();
        }

        private void returnToCaller()
        {
            this.Visible = false;
            caller.dataGridView.Columns.Clear();
            caller.initUI();
            caller.enableBoxesAndButtons();
            caller.prepare();
        }

        private void Miscellaneous_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void generatorTests_Click(object sender, EventArgs e)
        {
            PleaseWait pw = new PleaseWait(this, false);
            this.Enabled = false;
            pw.Visible = true;
        }

        private void testsFromFile_Click(object sender, EventArgs e)
        {
            PleaseWait pw = new PleaseWait(this, true);
            this.Enabled = false;
            pw.Visible = true;
        }

        private void buttonSaveWeekdays_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try {
                if (caller.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = caller.folderBrowserDialog1.SelectedPath + "/Terminmeldung.csv";
                    TextFile weekdayFile = new TextFile(filePath);
                    weekdayFile.WriteFile("", Data.notification);
                    weekdayFile.Append("Verein;Mannschaft;Spieltag;Ersatzspieltag\n", Data.notification);
                    foreach (Club c in Data.club)
                        foreach (Team t in c.team)
                            weekdayFile.Append(c.name + ";" + t.ageGroup + " " + t.team + ";" + t.weekday + ";" + t.weekday2 + "\n", Data.notification);
                }
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Fehler beim Speichern der Terminmeldung!");
            }
            returnToCaller();
        }
    }
}
