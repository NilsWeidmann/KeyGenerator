using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class PleaseWait : Form
    {
        KeyGenerator caller;
        Group[] best_l;
        Club[] best_c;
        int[] conflicts;

        public PleaseWait(KeyGenerator caller)
        {
            this.caller = caller;
            caller.Enabled = false;
            best_l = new Group[Data.group.Length];
            best_c = new Club[Data.club.Length];
            InitializeComponent();
            initProgressBar(progressBar);
            backgroundWorker.RunWorkerAsync();
        }

        public void initProgressBar(ProgressBar pb)
        {
            pb.Minimum = 0;
            pb.Maximum = 1000;
            pb.Value = 0;
        }

        private void cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void returnToCaller()
        {
            this.Visible = false;
            caller.dataGridView.Columns.Clear();
            caller.initUI();
            caller.enableBoxesAndButtons();
            caller.prepare();
        }

        private void PleaseWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;   
            e.Cancel = !Util.confirm(caller, best_l, best_c);
            if (!e.Cancel)
            {
                backgroundWorker.CancelAsync();
            }
            this.Enabled = true;
        }

        private void PleaseWait_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Data.save(Data.group, Data.club, Club.backup, Group.backup, Team.backup);
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                return;
            }
            Data.setOptions();
            Data.setWeeks();
            Data.copyKeys();
            Data.createPriority();
            Data.copy(Data.group, best_l, Data.club, best_c);
            Data.checkPlausibility(Data.group, Data.notification);
            Data.checkFatal(Data.group, Data.notification);
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                return;
            }
            int[] keys = new int[Data.club.Length * 2];
            conflicts = new int[]{ 0, -1 };
            Data.findSolution(Data.group, best_l, Data.club, best_c, conflicts, keys, backgroundWorker);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            returnToCaller();
            caller.showResults(best_l, best_c, conflicts[1]);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage * 10;
            int seconds = Data.runtime * (1000 - progressBar.Value) / 1000 % 60;
            int minutes = Data.runtime * (1000 - progressBar.Value) / 60000;

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

            if (!newTime.Equals(labelTime.Text))
            {
                labelTime.Text = newTime;
                labelTime.Refresh();
            }

            string newText = Data.currentConflicts == 0 ? "-" : "" + Data.currentConflicts;
            if (!newText.Equals(labelNrOfConflicts.Text))
            {
                labelNrOfConflicts.Text = newText;
                labelNrOfConflicts.Refresh();
            }
        }
    }
}
