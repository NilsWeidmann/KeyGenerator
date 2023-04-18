using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class PleaseWait : Form
    {
        Schluesselzahlen caller;
        Group[] best_l;
        Club[] best_v;
        int[] conflicts;

        public PleaseWait(Schluesselzahlen caller)
        {
            this.caller = caller;
            caller.Enabled = false;
            best_l = new Group[Data.group.Length];
            best_v = new Club[Data.club.Length];
            InitializeComponent();
            initProgressBar(progressBar1);
            backgroundWorker2.RunWorkerAsync();
        }

        public void initProgressBar(ProgressBar pb)
        {
            pb.Minimum = 0;
            pb.Maximum = 1000;
            pb.Value = 0;
        }

        private void cancel(object sender, EventArgs e)
        {
            backgroundWorker2.CancelAsync();
            returnToCaller();
        }

        private void returnToCaller()
        {
            this.Visible = false;
            caller.comboBox1.Text = "";
            caller.comboBox1.SelectedIndex = -1;
            caller.comboBox3.Text = "";
            caller.comboBox3.SelectedIndex = -1;
            caller.dataGridView1.Columns.Clear();
            caller.initUI();
            caller.enableBoxesAndButtons();
            caller.Enabled = true;
            caller.Focus();
        }

        private void BitteWarten_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker2.CancelAsync();
            returnToCaller();
        }

        private void BitteWarten_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Data.save(Data.group, Data.club, Data.partnership, Club.backup, Group.backup, Team.backup);
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                return;
            }
            Data.setOptions();
            Data.setWeeks();
            Data.copyKeys();
            Data.createPriority();
            Data.copy(Data.group, best_l, Data.club, best_v, Data.partnership, Data.partnership);
            Data.checkPlausibility(Data.group, Data.notification);
            Data.checkFatal(Data.group, Data.notification);
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                return;
            }
            int[] keys = new int[Data.club.Length * 2];
            conflicts = new int[]{ 0, -1 };
            Data.findSolution(Data.group, best_l, Data.club, best_v, conflicts, keys, backgroundWorker2);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            returnToCaller();
            caller.showResults(best_l, best_v, conflicts[1], e.Cancelled);
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage * 10;
            int seconds = Data.runtime * (1000 - progressBar1.Value) / 1000 % 60;
            int minutes = Data.runtime * (1000 - progressBar1.Value) / 60000;

            string newTime = "";
            if (minutes < 10)
                newTime += "0" + minutes;
            else
                newTime += minutes;
            newTime += ":";
            if (seconds < 10)
                newTime += "0" + seconds;
            else
                newTime += seconds;

            if (!newTime.Equals(label2.Text))
            {
                label2.Text = newTime;
                label2.Refresh();
            }

            string newText = Data.currentConflicts == 0 ? "-" : "" + Data.currentConflicts;
            if (!newText.Equals(label4.Text))
            {
                label4.Text = newText;
                label4.Refresh();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
