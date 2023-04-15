using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class Initialize : Form
    {
        public Schluesselzahlen caller;

        public Initialize(Schluesselzahlen caller)
        {
            InitializeComponent();
            this.caller = caller;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                caller.loadFromFile(Club.backup, League.backup, Team.backup);
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Es ist kein Backup vorhanden!");
                caller.loadFromFile(Club.file, League.file, Team.file);
            }
            returnToCaller();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            returnToCaller();
        }

        private void Initialisieren_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = false;
            returnToCaller();
        }

        private void deleteClubs()
        {
            List<Club> lv = Data.club.ToList();
            List<Club> lv_d = new List<Club>();
            foreach (Club v in lv)
                if (v.team == null || v.team.Count == 0)
                    lv_d.Add(v);
            foreach (Club v in lv_d)
                lv.Remove(v);
            Data.club = lv.ToArray();
            caller.comboBox3.Items.Clear();
            for (int i = 0; i < Data.club.Length; i++)
                caller.comboBox3.Items.Add(Data.club[i].name);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            deleteClubs();
            returnToCaller();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nrOfTeams; j++)
                    Data.league[i].team[j].week = '-';
            returnToCaller();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nrOfTeams; j++)
                    Data.league[i].team[j].key = 0;
            returnToCaller();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Enabled = false;
            for (int i = 0; i < Data.club.Length; i++)
                foreach (char c in Data.club[i].keys.Keys)
                    Data.club[i].keys[c] = 0;
            returnToCaller();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nrOfTeams; j++)
                    for (int k = 0; k < Data.TEAM_MAX; k++)
                        Data.league[i].team[j].day[k] = '-';
            returnToCaller();
        }

        private void returnToCaller()
        {
            this.Visible = false;
            caller.comboBox1.Text = "";
            caller.comboBox1.SelectedIndex = -1;
            caller.comboBox2.Text = "";
            caller.comboBox2.SelectedIndex = -1;
            caller.comboBox3.Text = "";
            caller.comboBox3.SelectedIndex = -1;
            caller.comboBox12.Text = "";
            caller.dataGridView1.Columns.Clear();
            caller.clearGrid();
            caller.Enabled = true;
            caller.Focus();
        }

        private void Initialisieren_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }
    }
}
