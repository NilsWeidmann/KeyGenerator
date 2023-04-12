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
    public partial class Additional : Form
    {
        ComboBox[] cb = new ComboBox[13];
        Team t;
        Schluesselzahlen caller;

        public Additional(Team t, Schluesselzahlen caller)
        {
            InitializeComponent();
            this.t = t;
            this.caller = caller;

            textBox1.Text = t.name;
            textBox2.Text = t.league.name;

            cb[0] = comboBox1;
            cb[1] = comboBox2;
            cb[2] = comboBox3;
            cb[3] = comboBox4;
            cb[4] = comboBox5;
            cb[5] = comboBox6;
            cb[6] = comboBox7;
            cb[7] = comboBox8;
            cb[8] = comboBox9;
            cb[9] = comboBox10;
            cb[10] = comboBox11;
            cb[11] = comboBox12;
            cb[12] = comboBox13;

            for (int i = 0; i < 13; i++)
            {
                cb[i].Items.Clear();
                cb[i].Items.Add("-");
                cb[i].Items.Add("Heimspiel");
                cb[i].Items.Add("Auswärtsspiel");
            }

            for (int i = 0; i < (t.league.field == 6 ? 10 : t.league.field - 1); i++)
            {
                cb[i].Enabled = true;
                switch (t.day[i])
                {
                    case '-': cb[i].SelectedIndex = 0; break;
                    case 'H': cb[i].SelectedIndex = 1; break;
                    case 'A': cb[i].SelectedIndex = 2; break;
                    default: cb[i].SelectedIndex = 0; break;
                }
            }
            for (int i = (t.league.field == 6 ? 10 : t.league.field - 1); i < 13; i++)
                cb[i].Enabled = false;

            comboBox14.Items.Clear();
            comboBox14.Items.Add("-");
            comboBox14.Items.Add("A");
            comboBox14.Items.Add("B");
            comboBox14.Items.Add("X");
            comboBox14.Items.Add("Y");

            switch (t.week)
            {
                case '-': comboBox14.SelectedIndex = 0; break;
                case 'A': comboBox14.SelectedIndex = 1; break;
                case 'B': comboBox14.SelectedIndex = 2; break;
                case 'X': comboBox14.SelectedIndex = 3; break;
                case 'Y': comboBox14.SelectedIndex = 4; break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < t.league.field - 1; i++)
                switch (cb[i].SelectedIndex)
                {
                    case 0: t.day[i] = '-'; break;
                    case 1: t.day[i] = 'H'; break;
                    case 2: t.day[i] = 'A'; break;
                }
            switch (comboBox14.SelectedIndex)
            {
                case 0: t.week = '-'; break;
                case 1: t.week = 'A'; break;
                case 2: t.week = 'B'; break;
                case 3: t.week = 'X'; break;
                case 4: t.week = 'Y'; break;
            }
            this.Visible = false;
            if (caller.radioButton2.Checked)
                if (caller.comboBox1.SelectedIndex != -1)
                    caller.init();
            caller.Enabled = true;
            caller.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            caller.Enabled = true;
            caller.Focus();
        }

        private void Zusatz_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            caller.Enabled = true;
            caller.Focus();
        }

        private void button1_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
