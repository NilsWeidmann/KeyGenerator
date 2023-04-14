using System;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class Group : Form
    {
        DataInput caller;
        League group;
        bool isNew;

        public Group(League group, bool isNew, DataInput caller)
        {
            this.isNew = isNew;
            this.group = group;
            this.caller = caller;
            InitializeComponent();

            textBox1.Text = group.name;
            int minFieldSize = Data.TEAM_MIN;
            if (!isNew)
                minFieldSize = group.nrOfTeams < Data.TEAM_MIN ?
                    Data.TEAM_MIN : group.nrOfTeams + group.nrOfTeams % 2;
            for (int i = minFieldSize; i <= Data.TEAM_MAX; i += 2)
                comboBox1.Items.Add(i);
            if (isNew)
                comboBox1.SelectedIndex = 0;
            else
                for (int i = 0; i < comboBox1.Items.Count; i++)
                    if (Util.toInt(comboBox1.Items[i].ToString()) == group.field)
                        comboBox1.SelectedIndex = i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            caller.Enabled = true;
            caller.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            group.name = Util.clear(textBox1.Text);
            group.field = Util.toInt(comboBox1.SelectedItem.ToString());
            if (isNew)
            {
                group.team = new Team[Data.TEAM_MAX];
                group.index = caller.league.Count;
                caller.league.Add(group);
                caller.comboBox1.Items.Add(group.name);
                caller.comboBox1.SelectedIndex = group.index;
            }
            else
                caller.comboBox1.Items[caller.comboBox1.SelectedIndex] = group.name;
            this.Close();
            caller.enableGUIElements();
            caller.Enabled = true;
            caller.Focus();
        }

        private void Staffel_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.Enabled = true;
            caller.Focus();
        }

        private void Staffel_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
