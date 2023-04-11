using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                caller.loadFromFile(Data.clubs_b, Data.group_b, Data.relations_b);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es ist kein Backup vorhanden!");
                caller.loadFromFile(Data.clubs, Data.group, Data.relations);
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

        private void loescheVereine()
        {
            List<Club> lv = Data.club.ToList();
            List<Club> lv_d = new List<Club>();
            foreach (Club v in lv)
                if (v.team == null || v.team.Length == 0)
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
            loescheVereine();
            returnToCaller();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nr_of_teams; j++)
                    Data.league[i].team[j].week = '-';
            returnToCaller();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nr_of_teams; j++)
                    Data.league[i].team[j].number = 0;
            returnToCaller();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Enabled = false;
            for (int i = 0; i < Data.club.Length; i++)
                Data.club[i].a = Data.club[i].b = Data.club[i].x = Data.club[i].y = 0;
            returnToCaller();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            for (int i = 0; i < Data.league.Length; i++)
                for (int j = 0; j < Data.league[i].nr_of_teams; j++)
                    for (int k = 0; k < Data.team_max; k++)
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
