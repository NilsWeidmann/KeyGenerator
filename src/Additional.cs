using System;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Additional : Form
    {
        private ComboBox[] cb;
        private Team t;
        private KeyGenerator caller;
        private static string[] weeks = { "-", "A", "B", "X", "Y" };
        private static string[] days = { "-", "Heimspiel", "Auswärtsspiel" };

    public Additional(Team t, KeyGenerator caller)
        {
            this.t = t;
            this.caller = caller;
            InitializeComponent();

            boxTeam.Text = t.name;
            boxGroup.Text = t.group.name;

            cb = new ComboBox[] { boxDay01, boxDay02, boxDay03, boxDay04, boxDay05, boxDay06,
                boxDay07, boxDay08, boxDay09, boxDay10, boxDay11, boxDay12, boxDay13};

            for (int i = 0; i < 13; i++)
            {
                cb[i].Items.Clear();
                cb[i].Items.AddRange(days);
            }

            for (int i = 0; i < (t.group.field == 6 ? 10 : t.group.field - 1); i++)
            {
                cb[i].Enabled = true;
                for (int j = 0; j < days.Length; j++)
                    if (days[j].StartsWith(t.day[i].ToString()))
                        cb[i].SelectedIndex = j;
            }
            for (int i = (t.group.field == 6 ? 10 : t.group.field - 1); i < 13; i++)
                cb[i].Enabled = false;

            boxWeek.Items.Clear();
            boxWeek.Items.AddRange(weeks);
            for (int i = 0; i < weeks.Length; i++)
                if (t.week.ToString().Equals(weeks[i]))
                    boxWeek.SelectedIndex = i;
        }

        private void buttonDeleteTeamOkay_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < t.group.field - 1; i++)
                t.day[i] = ((string)cb[i].SelectedItem)[0];

            t.week = ((String)boxWeek.SelectedItem)[0];
            this.Visible = false;
            if (caller.buttonGroupView.Checked)
                if (caller.boxGroups.SelectedIndex != -1)
                    caller.fillDataGridView(buttonDeleteTeamOkay);
            caller.Enabled = true;
            caller.Focus();
        }

        private void buttonDeleteTeamCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Additional_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;
            e.Cancel = !Util.confirm(caller, Data.group, Data.club);
            this.Enabled = true;
        }

        private void buttonDeleteTeamOkay_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }
    }
}
