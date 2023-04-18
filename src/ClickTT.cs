using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class ClickTT : Form
    {
        List<Group> ll;
        List<Club> lc;
        List<Partnership> lp;
        readonly Schluesselzahlen caller;

        public ClickTT(Group[] l, Club[] c, List<Partnership> p, Schluesselzahlen caller)
        {
            InitializeComponent();
            this.caller = caller;
            init();
            ll = l.ToList();
            foreach (Group group in ll)
                dataGridView1.Rows.Add(group.name);
            lc = c.ToList();
            foreach (Club club in lc)
                dataGridView2.Rows.Add(club.name);
            lp = p;
        }

        private void init()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Liga", "Liga");
            dataGridView1.Rows.Clear();
            dataGridView1.ReadOnly = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Verein", "Verein");
            dataGridView2.Rows.Clear();
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("Team", "Team");
            dataGridView3.Rows.Clear();
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
                string protocol = textBox1.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0];
                string domain = textBox1.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[1];
                HtmlNodeCollection tables = null;
                HtmlNode group_table = null;
                HtmlNode team_table = null;

                groups = web.Load(textBox1.Text);
                tables = groups.DocumentNode.SelectNodes("//table");
                foreach (HtmlNode tab in tables)
                    if (tab.GetAttributeValue("class", "").Equals("matrix"))
                        group_table = tab;
                foreach (HtmlNode node_s in group_table.Descendants())
                {
                    if (node_s.Name.Equals("h2"))
                        ageGroup = Util.clear(node_s.InnerText);
                    if (node_s.Name.Equals("a") && !node_s.ParentNode.GetAttributeValue("class", "").Equals("matrix-relegation-more"))
                    {
                        division = Util.clear(node_s.InnerText);
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
                                    t.name = Util.clear(t.name);
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
                        dataGridView1.Rows.Add(l.name);
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
                clubs = web.Load(textBox2.Text);
                HtmlNode club_table = null;

                string[] seperators = { "/" };
                string domain = textBox2.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[1];
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
                        dataGridView2.Rows.Add(club.name);
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

        private void button2_Click(object sender, EventArgs e)
        {
            lc = new List<Club>();
            lp = new List<Partnership>();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Verein", "Verein");
            dataGridView2.Rows.Clear();
            webImportClubs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ll = new List<Group>();
            dataGridView1.Rows.Clear();
            dataGridView3.Rows.Clear();
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
            dataGridView1.Height = (this.Height - 200) / 2;
            dataGridView2.Height = (this.Height - 200) / 2;
            dataGridView3.Height = (this.Height - 200) / 2;

            // Set proper widths
            dataGridView1.Width = (this.Width - 68) / 2;
            dataGridView2.Width = this.Width - 56;
            dataGridView3.Width = (this.Width - 68) / 2;
            groupBox1.Width = this.Width - 40;
            groupBox2.Width = this.Width - 40;
            textBox1.Width = this.Width - 250;
            textBox2.Width = this.Width - 250;
            button1.Left = groupBox1.Width - 80;
            button2.Left = groupBox2.Width - 80;
            button4.Left = groupBox1.Width - 160;
            button5.Left = groupBox2.Width - 160;
            dataGridView3.Left = dataGridView1.Left + dataGridView1.Width + 12;
            button3.Left = this.Width - 110;
            button3.Top = this.Height - 70;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Rows.Clear();
            if (ll != null && e.RowIndex < ll.Count)
            {
                for (int i = 0; i < ll[e.RowIndex].nrOfTeams; i++)
                    dataGridView3.Rows.Add(ll[e.RowIndex].team[i].name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            Data.save(ll.ToArray(), lc.ToArray(), lp, Club.file, Group.file, Team.file);
            caller.loadFromFile(Club.file, Group.file, Team.file);
            button3.Enabled = true;
        }

        private void click_tt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button3.Enabled)
            {
                switch (MessageBox.Show("Wollen Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        caller.Enabled = true;
                        caller.Focus();
                        break;
                    case DialogResult.Yes:
                        Data.save(ll.ToArray(), lc.ToArray(), lp, Club.file, Group.file, Team.file);
                        caller.loadFromFile(Club.file, Group.file, Team.file);
                        caller.Enabled = true;
                        caller.Focus();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                this.Visible = false;
                caller.Enabled = true;
                caller.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webImportGroups();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webImportClubs();
        }
    }
}
