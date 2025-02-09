using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace KeyGenerator
{
    public partial class PleaseWait : Form
    {
        Form caller;
        Group[] bestGroup;
        Club[] bestClub;
        int[] conflicts;
        bool testMode;

        public PleaseWait(Form caller)
        {
            this.caller = caller;
            caller.Enabled = false;
            testMode = caller is Miscellaneous; 
            bestGroup = new Group[Data.group.Length];
            bestClub = new Club[Data.club.Length];
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
            if (caller is KeyGenerator kg)
            {
                kg.dataGridView.Columns.Clear();
                kg.initUI();
                kg.enableBoxesAndButtons();
                kg.prepare();
            }
            else if (caller is Miscellaneous m)
            {
                m.Enabled = true;
            }
        }

        private void PleaseWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;
            if (caller is KeyGenerator kg)
                e.Cancel = !Util.confirm(kg, bestGroup, bestClub);
            else if (caller is Miscellaneous m)
            {
                e.Cancel = !confirm();
                m.Enabled = !e.Cancel;
            }

            if (!e.Cancel) { 
                backgroundWorker.CancelAsync();
            }
            this.Enabled = true;
        }

        private bool confirm()
        {
            switch (MessageBox.Show("Wollen Sie die Generierung abbrechen?", "Generierung abbrechen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    return true;
                default:
                    return false;
            }
        }

        private void PleaseWait_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (caller is KeyGenerator kg)
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
                Data.copy(Data.group, bestGroup, Data.club, bestClub);
                Data.checkPlausibility(Data.group, Data.notification);
                Data.checkFatal(Data.group, Data.notification);
                if (Data.notification.Count > 0)
                {
                    MessageBox.Show(Data.notification[0]);
                    return;
                }
                int[] keys = new int[Data.club.Length * 2];
                conflicts = [0, -1];
                char[] week = { 'A', 'B', 'X', 'Y' };
                OptimizationModel om = new OptimizationModel(Data.group, Data.club, week, Data.field, Data.runtime, Data.km, Data.TEAM_MAX);
                om.findSolution(bestGroup, bestClub, conflicts, backgroundWorker);

                //Data.findSolution(Data.group, best_l, Data.club, best_c, conflicts, keys, backgroundWorker);
            }
            else if (caller is Miscellaneous m)
            {
                InstanceGenerator ig = new InstanceGenerator(Data.runtime,(BackgroundWorker)sender);
                ig.runTests();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            returnToCaller();
            if (caller is KeyGenerator kg)
            {
                kg.showResults(bestGroup, bestClub, conflicts[1]);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage * 10;

            if (testMode)
            {
                label3.Text = "";
                label1.Text = "Generator-Tests werden ausgeführt: ";
                labelTime.Text = String.Format("{0,3}%", e.ProgressPercentage);
                labelNrOfConflicts.Text = "";
            }
            else
            {
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
}
