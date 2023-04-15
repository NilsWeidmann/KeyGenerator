using System;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class Additional : Form
    {
        private ComboBox[] cb;
        private Team t;
        private Schluesselzahlen caller;
        private static string[] weeks = { "-", "A", "B", "X", "Y" };
        private static string[] days = { "-", "Heimspiel", "Auswärtsspiel" };

    public Additional(Team t, Schluesselzahlen caller)
        {
            this.t = t;
            this.caller = caller;
            InitializeComponent();

            textBox1.Text = t.name;
            textBox2.Text = t.league.name;

            cb = new ComboBox[] { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6,
                comboBox7, comboBox8, comboBox9, comboBox10, comboBox11, comboBox12, comboBox13};

            for (int i = 0; i < 13; i++)
            {
                cb[i].Items.Clear();
                cb[i].Items.AddRange(days);
            }

            for (int i = 0; i < (t.league.field == 6 ? 10 : t.league.field - 1); i++)
            {
                cb[i].Enabled = true;
                for (int j = 0; j < days.Length; j++)
                    if (days[j].StartsWith(t.day[i].ToString()))
                        cb[i].SelectedIndex = j;
            }
            for (int i = (t.league.field == 6 ? 10 : t.league.field - 1); i < 13; i++)
                cb[i].Enabled = false;

            comboBox14.Items.Clear();
            comboBox14.Items.AddRange(weeks);
            for (int i = 0; i < weeks.Length; i++)
                if (t.week.ToString().Equals(weeks[i]))
                    comboBox14.SelectedIndex = i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < t.league.field - 1; i++)
                t.day[i] = ((string)cb[i].SelectedItem)[0];

            t.week = ((String)comboBox14.SelectedItem)[0];
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
