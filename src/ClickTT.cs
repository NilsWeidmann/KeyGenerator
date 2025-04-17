using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class ClickTT : Form
    {
        List<Group> ll;
        List<Club> lc;
        readonly KeyGenerator caller;

        public ClickTT(Group[] l, Club[] c, KeyGenerator caller)
        {
            InitializeComponent();
            this.caller = caller;
            init();
            ll = l.ToList();
            foreach (Group group in ll)
                dataGridViewGroups.Rows.Add(group.name);
            lc = c.ToList();
            foreach (Club club in lc)
                dataGridViewClubs.Rows.Add(club.name);
        }

        private void init()
        {
            dataGridViewGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewGroups.Columns.Clear();
            dataGridViewGroups.Columns.Add("Liga", "Liga");
            dataGridViewGroups.Rows.Clear();
            dataGridViewGroups.ReadOnly = true;
            dataGridViewClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewClubs.Columns.Clear();
            dataGridViewClubs.Columns.Add("Verein", "Verein");
            dataGridViewClubs.Rows.Clear();
            dataGridViewClubs.ReadOnly = true;
            dataGridViewTeams.ReadOnly = true;
            dataGridViewTeams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewTeams.Columns.Clear();
            dataGridViewTeams.Columns.Add("Team", "Team");
            dataGridViewTeams.Rows.Clear();
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
                                        if (Data.checkClub(t, lc.ElementAt(i)))
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
            dataGridViewTeams.Rows.Clear();
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
            dataGridViewTeams.Height = (this.Height - 200) / 2;

            // Set proper widths
            dataGridViewGroups.Width = (this.Width - 68) / 2;
            dataGridViewClubs.Width = this.Width - 56;
            dataGridViewTeams.Width = (this.Width - 68) / 2;
            groupBox1.Width = this.Width - 40;
            groupBox2.Width = this.Width - 40;
            boxLinkGroups.Width = this.Width - 250;
            boxLinkClubs.Width = this.Width - 250;
            buttonDeleteTeamNewGroup.Left = groupBox1.Width - 80;
            buttonDeleteTeamNewClub.Left = groupBox2.Width - 80;
            buttonDeleteTeamAddGroup.Left = groupBox1.Width - 160;
            buttonDeleteTeamAddClub.Left = groupBox2.Width - 160;
            dataGridViewTeams.Left = dataGridViewGroups.Left + dataGridViewGroups.Width + 12;
            buttonDeleteTeamSave.Left = this.Width - 110;
            buttonDeleteTeamSave.Top = this.Height - 70;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTeams.Rows.Clear();
            if (ll != null && e.RowIndex < ll.Count)
            {
                for (int i = 0; i < ll[e.RowIndex].nrOfTeams; i++)
                    dataGridViewTeams.Rows.Add(ll[e.RowIndex].team[i].name);
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
            webImportGroups();
        }

        private void buttonDeleteTeamAddClub_Click(object sender, EventArgs e)
        {
            webImportClubs();
        }
    }
}
