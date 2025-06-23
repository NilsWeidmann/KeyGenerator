using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace KeyGenerator
{
    public partial class ClickTT : Form
    {
        List<Group> ll;
        List<Club> lc;
        Hashtable clubs;
        Hashtable groups;
        Club currentClub;
        Group currentGroup;
        readonly KeyGenerator caller;

        public ClickTT(KeyGenerator caller)
        {
            InitializeComponent();
            this.caller = caller;
            ll = new List<Group>();
            lc = new List<Club>();
            clubs = new Hashtable();
            groups = new Hashtable();
            currentClub = null;
            currentGroup = null;
            init();
        }

        private void init()
        {
            dataGridViewGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initGroupGrid(dataGridViewGroups, true);

            dataGridViewClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initClubGrid(dataGridViewClubs, true);

            dataGridViewTeamsOfGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initTeamGrid(dataGridViewTeamsOfGroups, false);

            dataGridViewTeamsOfClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Visualization.initTeamGrid(dataGridViewTeamsOfClubs, true);
        }

        private void webImportGroups()
        {
            this.Enabled = false;
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument groups;
            HtmlAgilityPack.HtmlDocument teams;
            string ageGroup = "";
            string division;
            int index_l = ll.Count;
            int index_t;

            try
            {
                string[] seperators = { "/" };
                string protocol = boxLinkGroups.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0];
                string domain = boxLinkGroups.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[1];
                HtmlNodeCollection tables = null;
                HtmlNode group_table = null;
                HtmlNode team_table = null;

                groups = web.Load(boxLinkGroups.Text);
                tables = groups.DocumentNode.SelectNodes("//table");
                foreach (HtmlNode tab in tables)
                    if (tab.GetAttributeValue("class", "").Equals("matrix"))
                        group_table = tab;
                foreach (HtmlNode node_s in group_table.Descendants())
                {
                    if (node_s.Name.Equals("h2"))
                        ageGroup = node_s.InnerText;
                    if (node_s.Name.Equals("a") && !node_s.ParentNode.GetAttributeValue("class", "").Equals("matrix-relegation-more"))
                    {
                        division = node_s.InnerText;
                        Group l = new Group
                        {
                            name = ageGroup + " " + division,
                            index = index_l++
                        };
                        index_t = 0;
                        List<Team> lt = new List<Team>();

                        string reference = node_s.GetAttributeValue("href", "");
                        reference = reference.Replace("&amp;", "&");
                        string uri = protocol + "//" + domain + reference;
                        teams = web.Load(uri);
                        tables = teams.DocumentNode.SelectNodes("//table");
                        if (tables == null)
                            continue;
                        foreach (HtmlNode tab in tables)
                            if (tab.GetAttributeValue("class", "").Equals("result-set"))
                            {
                                team_table = tab;
                                break;
                            }
                        if (team_table == null)
                            continue;
                        foreach (HtmlNode node_t in team_table.Descendants())
                            if (node_t.Name.Equals("td"))
                                if (node_t.GetAttributeValue("nowrap", "").Equals("nowrap"))
                                {
                                    Team t = new Team();
                                    char[] trimChars = { ' ' };
                                    t.name = node_t.InnerText.Replace("\n", "");
                                    t.name = t.name.TrimStart(trimChars);
                                    t.name = t.name.TrimEnd(trimChars);
                                    t.group = l;
                                    l.nrOfTeams++;
                                    t.week = '-';
                                    t.index = index_t++;
                                    for (int i = 0; i < lc.Count; i++)
                                        if (Data.isTeamOfClub(t, lc.ElementAt(i)))
                                            t.club = lc.ElementAt(i);
                                    if (t.club == null)
                                    {
                                        t.club = new Club
                                        {
                                            name = t.name,
                                            index = -1
                                        };
                                        t.week = '-';
                                    }
                                    lt.Add(t);
                                }
                        while (lt.Count < 14)
                            lt.Add(null);
                        l.team = lt.ToArray();
                        l.field = l.nrOfTeams + l.nrOfTeams % 2;
                        ll.Add(l);
                        dataGridViewGroups.Rows.Add(l.name);
                    }
                }
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Beim Dateninport ist ein Fehler aufgetreten. Bitte den Link überprüfen!");
                this.Enabled = true;
                return;
            }
        }

        private void webImportClubs()
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument clubs;

            try
            {
                clubs = web.Load(boxLinkClubs.Text);
                HtmlNode club_table = null;

                string[] seperators = { "/" };
                string domain = boxLinkClubs.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[1];
                int index = lc.Count;

                HtmlNodeCollection tables = clubs.DocumentNode.SelectNodes("//table");

                foreach (HtmlNode tab in tables)
                    if (tab.Attributes["class"].Value.ToString().Equals("result-set"))
                        club_table = tab;
                foreach (HtmlNode link in club_table.Descendants())
                {
                    if (link.Name.Equals("a"))
                    {
                        Club club = new Club
                        {
                            name = Util.clear(link.InnerText),
                            index = index++
                        };
                        lc.Add(club);
                        dataGridViewClubs.Rows.Add(club.name);
                    }
                }
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Beim Dateninport ist ein Fehler aufgetreten. Bitte den Link überprüfen!");
                return;
            }
        }

        private void buttonDeleteTeamNewClub_Click(object sender, EventArgs e)
        {
            lc = new List<Club>();
            dataGridViewClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewClubs.Columns.Clear();
            dataGridViewClubs.Columns.Add("Verein", "Verein");
            dataGridViewClubs.Rows.Clear();
            webImportClubs();
        }

        private void buttonDeleteTeamNewGroup_Click(object sender, EventArgs e)
        {
            ll = new List<Group>();
            dataGridViewGroups.Rows.Clear();
            dataGridViewTeamsOfGroups.Rows.Clear();
            webImportGroups();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            caller.WindowState = this.WindowState;
            this.Focus();

            // Set proper heights
            groupBox2.Top = (this.Height - 95) / 2 + 20;
            groupBox1.Height = (this.Height - 95) / 2;
            groupBox2.Height = (this.Height - 95) / 2;
            dataGridViewGroups.Height = (this.Height - 200) / 2;
            dataGridViewClubs.Height = (this.Height - 200) / 2;
            dataGridViewTeamsOfGroups.Height = (this.Height - 200) / 2;
            dataGridViewTeamsOfClubs.Height = (this.Height - 200) / 2;
            // Set proper widths
            dataGridViewGroups.Width = (this.Width - 68) / 2;
            dataGridViewClubs.Width = (this.Width - 68) / 2;
            dataGridViewTeamsOfGroups.Width = (this.Width - 68) / 2;
            dataGridViewTeamsOfClubs.Width = (this.Width - 68) / 2;
            groupBox1.Width = this.Width - 40;
            groupBox2.Width = this.Width - 40;
            boxLinkGroups.Width = this.Width - 250;
            boxLinkClubs.Width = this.Width - 250;
            buttonDeleteTeamAddGroup.Left = groupBox1.Width - 160;
            buttonDeleteTeamAddClub.Left = groupBox2.Width - 160;
            dataGridViewTeamsOfGroups.Left = dataGridViewGroups.Left + dataGridViewGroups.Width + 12;
            dataGridViewTeamsOfClubs.Left = dataGridViewGroups.Left + dataGridViewGroups.Width + 12;
            buttonDeleteTeamSave.Left = this.Width - 110;
            buttonDeleteTeamSave.Top = this.Height - 70;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTeamsOfGroups.Rows.Clear();
            if (ll != null && e.RowIndex < ll.Count)
            {
                Visualization.visualizeGroupData(ll[e.RowIndex], dataGridViewTeamsOfGroups);
                currentGroup = ll[e.RowIndex];
            }
        }

        private void dataGridViewClubs_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTeamsOfClubs.Rows.Clear();
            if (lc != null && e.RowIndex < lc.Count)
            {
                Visualization.visualizeClubData(lc[e.RowIndex], dataGridViewTeamsOfClubs);
                currentClub = lc[e.RowIndex];
            }
        }

        private void buttonDeleteTeamSave_Click(object sender, EventArgs e)
        {
            buttonDeleteTeamSave.Enabled = false;
            Data.save(ll.ToArray(), lc.ToArray(), Club.file, Group.file, Team.file);
            caller.loadFromFile(Club.file, Group.file, Team.file);
            buttonDeleteTeamSave.Enabled = true;
        }

        private void ClickTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonDeleteTeamSave.Enabled)
            {
                e.Cancel = !Util.confirm(caller, ll.ToArray(), lc.ToArray());
            }
            else
            {
                this.Visible = false;
                caller.Enabled = true;
                caller.Focus();
            }
        }

        private void buttonDeleteTeamAddGroup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wählen Sie zunächst die exportierte CSV-Datei mit der Gruppeneinteilung aus!",
                    "Gruppeneinteilung auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return;
            
            clubs = new Hashtable();
            groups = new Hashtable();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                while (!Parser.parseGroupsAndClubs(groups, clubs, openFileDialog1))
                {
                    if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                        "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                        || openFileDialog1.ShowDialog() != DialogResult.OK)
                        return;
                }
            ll = new List<Group>();
            lc = new List<Club>();
            Parser.saveInData(groups, clubs, ll, lc);
            Visualization.initGroupGrid(dataGridViewGroups, true);
            Visualization.fillGroupGrid(dataGridViewGroups, ll.ToArray());
        }

        private void buttonDeleteTeamAddClub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wählen Sie nun die HTML-Datei mit den Terminwünschen aus!",
                    "Terminwünsche auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                while (!Parser.addWishes(groups, clubs, openFileDialog1))
                {
                    if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                        "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                        || openFileDialog1.ShowDialog() != DialogResult.OK)
                        return;
                }
            else
                return;
            ll = new List<Group>();
            lc = new List<Club>();
            Parser.saveInData(groups, clubs, ll, lc);
            Visualization.initClubGrid(dataGridViewClubs, true);
            Visualization.fillClubGrid(dataGridViewClubs, lc.ToArray());
        }

        private void dataGridViewTeamsOfClubs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentClub != null)
                Visualization.changeWeek(dataGridViewTeamsOfClubs, e, true, null, currentClub);
        }

        private void dataGridViewTeamsOfGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentGroup != null)
                Visualization.changeWeek(dataGridViewTeamsOfGroups, e, false, currentGroup, null);
        }

        private void dataGridViewClubs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Visualization.changeClubData(e, dataGridViewClubs, lc);
        }

        private void dataGridViewGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Visualization.changeField(dataGridViewGroups, e, currentGroup);
        }
    }
}
