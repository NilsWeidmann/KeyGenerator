using System;
using System.CodeDom;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace KeyGenerator
{
    public partial class PleaseWait : Form
    {
        Form caller;
        InstanceGenerator ig;
        Group[] group;
        Club[] club;
        Group[] bestGroup;
        Club[] bestClub;
        int[] conflicts;
        bool fromFile;
        bool useCPSATSolver = true;

        public PleaseWait(Form caller, bool fromFile, Group[] group, Club[] club)
        {
            this.caller = caller;
            caller.Enabled = false;
            this.fromFile = fromFile;
            this.group = group;
            this.club = club;
            bestGroup = new Group[group.Length];
            bestClub = new Club[club.Length];
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
                Data.save(group, club, Club.backup, Group.backup, Team.backup);
                if (Data.notification.Count > 0)
                {
                    MessageBox.Show(Data.notification[0]);
                    return;
                }
                Data.setOptions(group);
                Data.setWeeks(group);
                Data.copyKeys(group, club);
                Data.createPriority(club);
                Data.copy(group, bestGroup, club, bestClub);
                Data.checkPlausibility(group, club, Data.notification);
                Data.checkFatal(group, Data.notification);
                if (Data.notification.Count > 0)
                {
                    MessageBox.Show(Data.notification[0]);
                    return;
                }
                int[] keys = new int[club.Length * 2];
                conflicts = [0, -1];
                char[] week = { 'A', 'B', 'X', 'Y' };

                if (useCPSATSolver)
                {
                    OptimizationModel om = new OptimizationModel(group, club, week, Data.field, Data.runtime, Data.km, Data.TEAM_MAX, Data.log, backgroundWorker, Data.notification);
                    om.findSolution(bestGroup, bestClub, conflicts);
                }
                else
                {
                    DFSSolver solver = new DFSSolver(Data.field, Data.prio, Data.currentConflicts, Data.runtime, Data.km, Data.log, Data.notification);
                    solver.findSolution(group, bestGroup, club, bestClub, conflicts, keys, backgroundWorker);
                }
                
            }
            else if (caller is Miscellaneous m)
            {
                ig = new InstanceGenerator((BackgroundWorker)sender, Data.runtime, Data.log, m.caller.boxDirectory.Text);
                if (fromFile)
                    ig.runTests(m.caller.boxDirectory.Text);
                else
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

            if (caller is Miscellaneous m)
            {
                label3.Text = "";
                label1.Text = "Generator-Tests werden ausgeführt: ";
                ig.refreshStatusText(labelTime);
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
