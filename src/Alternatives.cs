using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Alternatives : Form
    {
        public Conflict[] conflicts;
        public Group[] group;
        public Club[] club;
        public KeyGenerator caller;

        public Alternatives(Conflict[] conflicts, Group[] groups, Club[] clubs, KeyGenerator caller)
        {
            this.conflicts = conflicts;
            this.group = groups;
            this.club = clubs;
            this.caller = caller;
            InitializeComponent();
            
            boxConflict.Items.Clear();
            for (int j = 0; j < conflicts.Length; j++)
                boxConflict.Items.Add("Konflikt " + (conflicts[j].index + 1));
            boxConflict.SelectedIndex = 0;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Group[] bestGroup = new Group[group.Length];
            Club[] bestClub = new Club[club.Length];
            int[] conflicts = new int[2];
            conflicts[0] = -1;
            conflicts[1] = -1;
            int[] keys = new int[club.Length * 2];

            foreach (Conflict conflict1 in this.conflicts)
                foreach (Team team1 in conflict1.t)
                    foreach (Conflict conflict2 in this.conflicts)
                        foreach (Team team2 in conflict2.t)
                            if (team1.group.Equals(team2.group) && team1.key == team2.key)
                                if (!conflict1.Equals(conflict2))
                                {
                                    // Problem: Verschiedene Konflikte beinhalten dieselbe Schlüsselzahl
                                    MessageBox.Show("Vergeben Sie die Zahl " + team1.key + " in der " + team1.group.name 
                                        + " nur einmal (Konflikt " + (conflict1.index + 1) + "/" + (conflict2.index + 1) + ")!");
                                    return;
                                }
                                else if (!team1.Equals(team2))
                                {
                                    // Problem: Konflikt ist noch nicht aufgelöst
                                    MessageBox.Show("Lösen Sie zunächst Konflikt " + (boxConflict.SelectedIndex + 1) + "!");
                                    return;
                                }

            this.Visible = false;
            caller.solveConflicts(group, club);
            caller.Enabled = true;
        }

        private void boxConflict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxConflict.SelectedIndex == -1)
                return;

            Conflict conflict = conflicts[boxConflict.SelectedIndex];
            boxTeam1.Text = conflict.t[0].name;
            boxTeam2.Text = conflict.t[1].name;
            if (conflict.t.Length == 3)
                boxTeam3.Text = conflict.t[2].name;
            else
                boxTeam3.Text = "";
            boxGroup.Text = conflict.t[0].group.name;

            ComboBox[] keys = { boxKey1, boxKey2, boxKey3 };
            TextBox[] wish = { boxWish1, boxWish2, boxWish3 };

            for (int box = 0; box < 3; box++)
            {
                if (box < conflict.t.Length)
                {
                    Team team = conflict.t[box];
                    keys[box].Items.Clear();
                    keys[box].Text = team.key.ToString();
                    int field = team.week == 'A' || team.week == 'B' ? Data.field[0] : Data.field[1];
                    wish[box].Text = Data.concatenate(Data.km.getParallel(field, team.group.field, team.club.keys[team.week]));
                    for (int j = 0; j < 3; j++)
                        if (conflict.key[j] != 0)
                        {
                            keys[box].Items.Add(conflict.key[j]);
                            if (team.key == conflict.key[j])
                                keys[box].SelectedIndex = j;
                        }
                    keys[box].Enabled = true;
                }
                else
                {
                    keys[box].Items.Clear();
                    keys[box].Text = "";
                    wish[box].Text = "";
                    keys[box].SelectedIndex = -1;
                    keys[box].Enabled = false;
                }
            }
        }

        private void boxKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetSelectedIndices((ComboBox)sender);
        }

        private void boxKey2_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetSelectedIndices((ComboBox)sender);
        }

        private void boxKey3_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetSelectedIndices((ComboBox)sender);
        }

        private void resetSelectedIndices(ComboBox sender)
        {
            ComboBox[] combo = { boxKey1, boxKey2, boxKey3 };

            for (int box = 0; box < 3; box++)
            {
                if (combo[box] != sender && sender.SelectedIndex == combo[box].SelectedIndex)
                    combo[box].SelectedIndex = -1;
                if (combo[box] == sender && combo[box].SelectedIndex != -1)
                    conflicts[boxConflict.SelectedIndex].t[box].key = Util.toInt(combo[box].SelectedItem.ToString());
            }
        }

        private void Alternatives_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;
            e.Cancel = !Util.confirm(caller, group, club);
            this.Enabled = true;
        }

        private void Alternativex_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();
        }

        private void buttonProposal_Click(object sender, EventArgs e)
        {
            foreach (Conflict conflict in conflicts)
            {
                List<int> keysToAssign = conflict.key.ToList();
                List<Team> teamsToAssign = conflict.t.ToList();
                
                // Bereits gelöste Konflikte nicht zurückrollen
                foreach (Team t in conflict.t)
                    if (t.key != conflict.wish)
                    {
                        keysToAssign.Remove(t.key);
                        teamsToAssign.Remove(t);
                    }

                // Rest zuweisen
                foreach (Team t in teamsToAssign)
                {
                    t.key = keysToAssign.First();
                    keysToAssign.Remove(keysToAssign.First());
                }   
            }
            boxConflict.SelectedIndex = -1;
            boxConflict.SelectedIndex = 0;
        }
    }
}
