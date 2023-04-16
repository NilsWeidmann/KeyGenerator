using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Schluesselzahlen
{
    public partial class Schluesselzahlen : Form
    {
        private Club club;
        private string[] weeks = { "-", "A", "B", "X", "Y" };

        public Schluesselzahlen()
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = false;
            initDataGridView();
            enableFields();
            checkBox1.Enabled = false;
            textBox2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            fillFields(comboBox10);
            comboBox10.SelectedIndex = 3;
            fillFields(comboBox11);
            comboBox11.SelectedIndex = 2;
            textBox1.Text = "2";
            textBox2.Text = Application.StartupPath;
            textBox3.Text = "0";
            setFiles(Application.StartupPath);
            Data.path = Application.StartupPath;
            Data.caller = this;
            enableBoxesAndButtons();
        }

        private void initDataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Woche", "Woche");
            dataGridView1.Columns.Add("Liga", "Liga");
            dataGridView1.Columns.Add("Team", "Team");
            dataGridView1.Columns.Add("Schlüssel", "Schlüssel");
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Nur bei Vereinssicht, nicht bei Staffelsicht
            dataGridView1.Columns[0].Visible = radioButton1.Checked;
        }
        public void fillFields(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = Data.TEAM_MIN; i <= Data.TEAM_MAX; i += 2)
                cb.Items.Add(i.ToString());
            cb.SelectedIndex = -1;
        }

        public void enableFields()
        {
            comboBox4.Enabled = comboBox1.SelectedIndex != -1;
        }

        public void showResults(League[] best_l, Club[] best_v, int conflicts, bool cancelled)
        {
            if (conflicts == -1)
                Data.notification.Add("Es konnten keine Schlüsselzahlen ermittelt werden!");
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                loadFromFile(Club.backup, League.backup, Team.backup);
            }
            else if (cancelled)
                loadFromFile(Club.backup, League.backup, Team.backup);
            else
            {
                Data.copy(best_l, Data.league, best_v, Data.club, Data.partnership, Data.partnership);
                solveConflicts(Data.league, Data.club);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Data.runtime = Util.toInt(textBox1.Text) * 60 + Util.toInt(textBox3.Text);
            Data.ht.Clear();
            PleaseWait bw = new PleaseWait(this);
            bw.Visible = true;
        }

        private void reset(ComboBox box)
        {
            box.Items.Clear();
            box.SelectedIndex = -1;
            box.Text = "";
        }

        public void initUI()
        {
            initDataGridView();
            checkBox1.Enabled = false;

            ComboBox[] boxes = { comboBox1, comboBox3 };
            foreach (ComboBox box in boxes) {
                reset(box);
            }
            
            if (Data.league != null)
                for (int i = 0; i < Data.league.Length; i++)
                    comboBox1.Items.Add(Data.league[i].name);
            if (Data.club != null)
                for (int i = 0; i < Data.club.Length; i++)
                    comboBox3.Items.Add(Data.club[i].name);
            enableFields();
        }

        public bool loadFromFile(TextFile ver, TextFile sta, TextFile bez)
        {
            button2.Enabled = button5.Enabled = button6.Enabled = false;
            Data.club = Data.getClubs(ver, Data.partnership);
            Data.league = League.getGroups(Data.club, sta, Data.notification);
            Data.getRelations(Data.league, bez);
            Data.allocateTeams(Data.club, Data.league);
            
            if (Data.notification.Count > 0)
            {
                button3.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                button2.Enabled = button5.Enabled = button6.Enabled = true;
                return false;
            }
            else
            {
                initUI();
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton1.Checked = true;
                button3.Enabled = true;
                button14.Enabled = true;
                button15.Enabled = true;
                button2.Enabled = button5.Enabled = button6.Enabled = true;
                enableBoxesAndButtons();
                return true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadFromFile(Club.file, League.file, Team.file);
        }

        public void enableBoxesAndButtons()
        {
            dataGridView1.Rows.Clear();
            checkBox1.Enabled = checkBox1.Checked = button4.Enabled = false;

            comboBox1.Enabled = comboBox3.Enabled = comboBox4.Enabled = comboBox5.Enabled
                 = comboBox6.Enabled = comboBox7.Enabled = comboBox8.Enabled = false;

            comboBox1.Text = comboBox3.Text = comboBox4.Text = comboBox5.Text 
                = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";

            if (radioButton1.Checked)
            {
                // Vereinssicht
                comboBox3.Enabled = true;
                if (comboBox3.SelectedItem != null)
                {
                    checkBox1.Enabled = comboBox5.Enabled = comboBox6.Enabled = comboBox7.Enabled = comboBox8.Enabled = true;
                    comboBox3.Text = comboBox3.SelectedItem.ToString();
                    comboBox5.Text = comboBox5.SelectedItem.ToString();
                    comboBox6.Text = comboBox6.SelectedItem.ToString();
                    comboBox7.Text = comboBox7.SelectedItem.ToString();
                    comboBox8.Text = comboBox8.SelectedItem.ToString();
                    checkBox1.Checked = Data.club[comboBox3.SelectedIndex].capacity;
                }
            }
            else if (radioButton2.Checked)
            {
                // Staffelsicht
                comboBox1.Enabled = true;
                if (comboBox1.SelectedItem != null)
                {
                    comboBox1.Text = comboBox1.SelectedItem.ToString();
                    comboBox4.Enabled = true;
                    comboBox4.Text = Data.league[comboBox1.SelectedIndex].field.ToString();
                }
            }
        }

        public void fillDataGridView(Control caller)
        {
            dataGridView1.Rows.Clear();
            if (radioButton1.Checked && comboBox3.SelectedIndex != -1)
            {
                // Vereinssicht
                club = Data.club[comboBox3.SelectedIndex];
                if (comboBox3.SelectedIndex == -1)
                {
                    checkBox1.Checked = checkBox1.Enabled = button4.Enabled = false;
                    comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";
                }
                else
                {
                    checkBox1.Checked = club.capacity;
                    button4.Enabled = true;
                }
                if (comboBox5.Items.Count > club.keys['A'])
                    comboBox5.SelectedIndex = club.keys['A'];
                if (comboBox6.Items.Count > club.keys['B'])
                    comboBox6.SelectedIndex = club.keys['B'];
                if (comboBox7.Items.Count > club.keys['X'])
                    comboBox7.SelectedIndex = club.keys['X'];
                if (comboBox8.Items.Count > club.keys['Y'])
                    comboBox8.SelectedIndex = club.keys['Y'];
                for (int j = 0; j < club.team.Count; j++)
                {
                    String[] content = new String[4];
                    if (club.team[j].week != '-')
                        content[0] = club.team[j].week.ToString();
                    else
                        content[0] = "";
                    content[1] = club.team[j].league.name;
                    content[2] = club.team[j].name;
                    if (club.team[j].key != 0)
                        content[3] = club.team[j].key.ToString();
                    else if (club.team[j].week == '-' || club.team[j].club.keys[club.team[j].week] == 0)
                        content[3] = "";
                    else if (club.team[j].week == 'A' || club.team[j].week == 'B')
                        content[3] = Data.km.getParallel(Data.field[0], club.team[j].league.field, club.keys[club.team[j].week]).ToString();
                    else if (club.team[j].week == 'X' || club.team[j].week == 'Y')
                        content[3] = Data.km.getParallel(Data.field[1], club.team[j].league.field, club.keys[club.team[j].week]).ToString();

                    dataGridView1.Rows.Add(content);

                    for (int l = 0; l < 4; l++)
                    {
                        Color color;
                        switch (club.team[j].week)
                        {
                            case 'A': color = Color.Yellow; break;
                            case 'B': color = Color.Orange; break;
                            case 'X': color = Color.LightBlue; break;
                            case 'Y': color = Color.LightGreen; break;
                            default: color = Color.White; break;
                        }
                        dataGridView1.Rows[j].Cells[l].Style.BackColor = color;
                    }
                }
            }
            else if (radioButton2.Checked && comboBox1.SelectedIndex != -1)
            {
                // Staffelsicht
                for (int j = 0; j < Data.league[comboBox1.SelectedIndex].nrOfTeams; j++)
                {
                    String[] content = new String[4];
                    Team team = Data.league[comboBox1.SelectedIndex].team[j];
                    int field = Data.league[comboBox1.SelectedIndex].field;

                    if (team.week != '-')
                        content[0] = team.week.ToString();
                    else
                        content[0] = "";
                    content[1] = Data.league[comboBox1.SelectedIndex].name;
                    content[2] = Data.league[comboBox1.SelectedIndex].team[j].name;

                    if (team.key != 0)
                        content[3] = team.key.ToString();
                    else if (team.week == '-' || team.club.keys[team.week] == 0)
                        content[3] = "";
                    else if (team.week == 'A' || team.week == 'B')
                        content[3] = Data.km.getParallel(Data.field[0], field, team.club.keys[team.week]).ToString();
                    else if (team.week == 'X' || team.week == 'Y')
                        content[3] = Data.km.getParallel(Data.field[1], field, team.club.keys[team.week]).ToString();

                    dataGridView1.Rows.Add(content);

                    if (!content[3].Equals(""))
                        for (int i = 0; i < 4; i++)
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    else if (team.week != '-')
                        for (int i = 0; i < 4; i++)
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Yellow;
                }
                club = null;
            }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            caller.Focus();
        }

        private void setFiles(string path)
        {
            League.file = new TextFile(path + @"\Staffeln.csv");
            League.backup = new TextFile(path + @"\Staffeln_Backup.csv");
            Club.file = new TextFile(path + @"\Vereine.csv");
            Club.backup = new TextFile(path + @"\Vereine_Backup.csv");
            Team.file = new TextFile(path + @"\Beziehungen.csv");
            Team.backup = new TextFile(path + @"\Beziehungen_Backup.csv");
            if (comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1)
            {
                button6.Enabled = true;
                button2.Enabled = true;
                //button1.Enabled = true;
            }
            Data.km = new KeyMapper(path);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox[] boxes = { comboBox5, comboBox6, comboBox7, comboBox8};
            if (radioButton1.Checked && comboBox3.SelectedIndex != -1)
            {
                checkBox1.Enabled = button4.Enabled = true;
                foreach (ComboBox cb in boxes)
                {
                    cb.Enabled = true;
                    cb.Text = cb.SelectedItem.ToString();
                }
                fillDataGridView(comboBox3);
            }
            else
            {
                checkBox1.Enabled = checkBox1.Checked = button4.Enabled = false;
                foreach (ComboBox cb in boxes)
                {
                    cb.Enabled = false;
                    cb.Text = "";
                }
            }   
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                Data.path = textBox2.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox4.SelectedIndex = -1;
                comboBox4.Enabled = false;
            }
            else
            {
                for (int i = 0; i < Data.league[comboBox1.SelectedIndex].nrOfTeams; i++)
                {
                    comboBox4.Items.Clear();
                    
                    League currentLeague = Data.league[comboBox1.SelectedIndex];
                    int start = currentLeague.nrOfTeams + currentLeague.nrOfTeams % 2;
                    start = currentLeague.nrOfTeams > Data.TEAM_MIN ? currentLeague.nrOfTeams : Data.TEAM_MIN;

                    for (int j = start, counter = 0; j <= Data.TEAM_MAX; j += 2, counter++)
                    {
                        comboBox4.Items.Add(j.ToString());
                        if (j == currentLeague.field)
                            comboBox4.SelectedIndex = counter;
                    }

                }

                if (radioButton2.Checked)
                    fillDataGridView(comboBox1);
            }
            enableFields();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Initialize i = new Initialize(this);
            this.Enabled = false;
            i.Visible = true;
            i.Focus();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // Groß- und Kleinschreibung tolerieren
                string input = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().ToUpper();

                // Auch Leerzeichen und leere Strings erlauben
                if (input.Equals("") || input.Equals(" "))
                    input = "-";

                if (radioButton1.Checked && club != null) {
                    // Vereinssicht
                    if (weeks.Contains(input) && e.RowIndex < club.team.Count)
                        club.team[e.RowIndex].week = input[0];
                }
                else if (radioButton2.Checked && comboBox1.SelectedIndex != -1)
                    { 
                    // Staffelsicht
                    if (weeks.Contains(input) && e.RowIndex < Data.league[comboBox1.SelectedIndex].team.Length)
                        Data.league[comboBox1.SelectedIndex].team[e.RowIndex].week = input[0];
                }
                fillDataGridView(dataGridView1);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Data.save(Data.league, Data.club, Data.partnership, Club.file, League.file, Team.file);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('A', 'B', Data.field[0], comboBox5, comboBox6);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('B', 'A', Data.field[0], comboBox6, comboBox5);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('X', 'Y', Data.field[1], comboBox7, comboBox8);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            setKeys('Y', 'X', Data.field[1], comboBox8, comboBox7);
        }

        private void setKeys(char week1, char week2, int field, ComboBox cb1, ComboBox cb2)
        {
            if (comboBox3.SelectedIndex >= 0)
            {
                Data.club[comboBox3.SelectedIndex].keys[week1] = cb1.SelectedIndex;
                if (cb1.SelectedIndex == 0)
                    Data.club[comboBox3.SelectedIndex].keys[week2] = 0;
                else
                    Data.club[comboBox3.SelectedIndex].keys[week2] = Data.km.getOpposed(field, field, cb1.SelectedIndex);
                cb2.SelectedIndex = Data.club[comboBox3.SelectedIndex].keys[week2];
            }
        }

        private void adjustReferenceFields(ComboBox cb1, ComboBox cb2, int idx, int refField)
        {
            cb1.Items.Clear();
            cb1.Items.Add('-');
            for (int i = 1; i <= refField; i++)
                cb1.Items.Add(i);
            if (cb1.Items.Count > Util.toInt(cb1.Text))
                cb1.SelectedIndex = Util.toInt(cb1.Text);
            else
                cb1.SelectedIndex = 0;
            cb2.Items.Clear();
            cb2.Items.Add('-');
            for (int i = 1; i <= refField; i++)
                cb2.Items.Add(i);
            if (cb2.Items.Count > Util.toInt(cb2.Text))
                cb2.SelectedIndex = Util.toInt(cb2.Text);
            else
                cb2.SelectedIndex = 0;
            Data.field[idx] = refField;
            checkWeeks();
            if (comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1 && League.file != null)
            {
                button6.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustReferenceFields(comboBox5, comboBox6, 0, Util.toInt(comboBox10.SelectedItem.ToString()));
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustReferenceFields(comboBox7, comboBox8, 1, Util.toInt(comboBox11.SelectedItem.ToString()));
        }

        public static void checkWeeks()
        {
            if (Data.club == null)
                return;
            for (int i = 0; i < Data.club.Length; i++)
            {
                if (Data.club[i].keys['A'] > Data.field[0])
                    Data.club[i].keys['A'] = 0;
                if (Data.club[i].keys['B'] > Data.field[0])
                    Data.club[i].keys['B'] = 0;
                if (Data.club[i].keys['X'] > Data.field[1])
                    Data.club[i].keys['X'] = 0;
                if (Data.club[i].keys['Y'] > Data.field[1])
                    Data.club[i].keys['Y'] = 0;
            }
        }

        public void solveConflicts(League[] l, Club[] v)
        {
            int[] konflikte = new int[2];
            List<Conflict> k = Data.getConflicts(l);
            if (k.Count > 0)
            {
                Alternatives a = new Alternatives(k.ToArray(), l, v, this);
                this.Enabled = false;
                a.Visible = true;
            }
            else
            {
                Data.generateKeys();
                for (int i = 0; i < Data.notification.Count; i++)
                    MessageBox.Show(Data.notification[i]);
                Data.notification.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && comboBox3.SelectedIndex != -1)
                Data.club[comboBox3.SelectedIndex].capacity = checkBox1.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton1.Checked;
            dataGridView1.Columns[0].Visible = radioButton1.Checked;
            if (radioButton1.Checked && comboBox3.SelectedIndex != -1)
                fillDataGridView(radioButton1);
            else
                enableBoxesAndButtons();
        }
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton2.Checked;
            dataGridView1.Columns[0].Visible = radioButton1.Checked;
            if (radioButton2.Checked && comboBox1.SelectedIndex != -1)
                fillDataGridView(radioButton2);
            else
                enableBoxesAndButtons();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            DataInput d = new DataInput(this);
            d.Visible = true;
        }

        private void Schluesselzahlen_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - 100;
            dataGridView1.Width = this.Width - 350;
        }

        private void Schluesselzahlen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(Data.club == null || Data.league == null))
                switch (MessageBox.Show("Wollen Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        Data.save(Data.league, Data.club, Data.partnership, Club.file, League.file, Team.file);
                        this.loadFromFile(Club.file, League.file, Team.file);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                    return;
                Team t = null;
                if (radioButton1.Checked)
                {
                    Club v = Data.club[comboBox3.SelectedIndex];
                    t = v.team.ElementAt(e.RowIndex);
                }
                else if (radioButton2.Checked)
                {
                    League l = Data.league[comboBox1.SelectedIndex];
                    t = l.team.ElementAt(e.RowIndex);
                }
                Additional z = new Additional(t, this);
                this.Enabled = false;
                z.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Partner p = new Partner(this, Data.club[comboBox3.SelectedIndex]);
            this.Enabled = false;
            p.Visible = true;
        }

        private string replaceUmlauts(string input)
        {
            input = input.Replace("&#196;", "Ä");
            input = input.Replace("&#228;", "ä");
            input = input.Replace("&#214;", "Ö");
            input = input.Replace("&#246;", "ö");
            input = input.Replace("&#220;", "Ü");
            input = input.Replace("&#252;", "ü");
            input = input.Replace("&#223;", "ß");
            return input;
        }

        private bool addWishes(Hashtable groups, Hashtable clubs)
        {

            Stream file = openFileDialog1.OpenFile();
            HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument wishes = new HtmlAgilityPack.HtmlDocument();

            bool isHeader = true;
            wishes.Load(file);
            // Regex clubNameAndID = new Regex("(\\w+\\s)+\\([0-9]{6}\\)");
            Regex clubIDPattern = new Regex(@"^[0-9]{6}$");
            HtmlNodeCollection divs = wishes.DocumentNode.SelectNodes("//div");

            if (divs == null)
                return false;

            Club currentClub = null;
            Club dummyClub = new Club();
            Team currentTeam = null;
            Team dummyTeam = new Team();

            foreach (HtmlNode div in divs)
            {
                IEnumerable<HtmlNode> ps = div.Descendants("p");
                isHeader = true;

                if (ps == null)
                    return false;

                foreach (HtmlNode p in ps)
                {
                    string[] lines = p.InnerHtml.Split(new string[] { "<br>", "<br/>", "<b>", "</b>" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        string parsedLine = replaceUmlauts(line);
                        parsedLine = Util.clear(parsedLine);

                        // Header vorbei?
                        if (parsedLine.Equals("Terminwuensche"))
                            isHeader = false;

                        if (isHeader)
                        {
                            continue;
                        }

                        // Neuer Verein
                        string[] clubNameAndID = parsedLine.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        if (clubNameAndID.Length == 2)
                        {
                            if (clubIDPattern.IsMatch(clubNameAndID[1]))
                            {

                                string clubID = clubNameAndID[1];
                                string clubName = clubNameAndID[0].TrimEnd(' ');

                                if (clubs.ContainsKey(clubID))
                                    currentClub = (Club)clubs[clubID];
                                else
                                    currentClub = dummyClub;
                                currentTeam = dummyTeam;
                            }
                        }
                        // Neue Mannschaft
                        foreach (string ageGroup in Data.ageGroups)
                        {
                            if (parsedLine.StartsWith(ageGroup))
                            {
                                string team;
                                if (parsedLine.Equals(ageGroup))
                                    team = "I";
                                else
                                    team = parsedLine.Replace(ageGroup, "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                                if (!Util.isRomanNumber(team))
                                    team = "I";
                                bool found = false;
                                foreach (Team t in currentClub.team)
                                    if (t.club.name.Equals(currentClub.name)
                                        && t.ageGroup.Equals(ageGroup)
                                        && t.team.Equals(team))
                                    {
                                        currentTeam = t;
                                        found = true;
                                        break;
                                    }

                                if (!found)
                                    currentTeam = dummyTeam;
                            }
                        }
                        if (parsedLine.StartsWith("Spielwoche"))
                        {
                            currentTeam.week = parsedLine.ToCharArray()[11];
                        }
                    }
                }
            }

            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Daten-Sammelstrukturen
            Hashtable clubs = new Hashtable();
            Hashtable groups = new Hashtable();

            try
            {
                if (MessageBox.Show("Wählen Sie zunächst die exportierte CSV-Datei mit der Gruppeneinteilung aus!",
                    "Gruppeneinteilung auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    while (!parseGroupsAndClubs(groups, clubs))
                    {
                        if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                            "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                            || openFileDialog1.ShowDialog() != DialogResult.OK)
                            return;
                    }
                else
                    return;

                if (MessageBox.Show("Wählen Sie nun die HTML-Datei mit den Terminwünschen aus!",
                    "Terminwünsche auswählen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    while (!addWishes(groups, clubs))
                    {
                        if (MessageBox.Show("Fehler beim Lesen der Datei, versuchen Sie es noch einmal!",
                            "Fehler beim Lesen der Datei", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel
                            || openFileDialog1.ShowDialog() != DialogResult.OK)
                            return;
                    }
                else
                    return;
            }
            catch (Exception ex)
            {
                Data.notification.Append(ex.ToString());
                MessageBox.Show("Beim Dateninport ist ein Fehler aufgetreten. Bitte die Dateien überprüfen!");
                this.Enabled = true;
                return;
            }

            // Alles scheint geklappt zu haben!
            saveInData(groups, clubs);
            initUI();
            button3.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton1.Checked = true;
            comboBox1.Enabled = comboBox3.Enabled = true;
            button6.Enabled = true;
        }

        private void saveInData(Hashtable groups, Hashtable clubs)
        {
            List<Club> cl = new List<Club>();
            foreach (Club c in clubs.Values)
            {
                cl.Add(c);
            }
            cl.Sort();
            Data.club = cl.ToArray();

            List<League> lg = new List<League>();
            foreach (League g in groups.Values)
            {
                // Feld ermitteln. Default: so klein wie möglich
                g.field = g.nrOfTeams + (g.nrOfTeams % 2);
                g.field = g.field < Data.TEAM_MIN ? Data.TEAM_MIN : g.field;
                lg.Add(g);
            }
            lg.Sort();
            Data.league = lg.ToArray();
        }

        private bool parseGroupsAndClubs(Hashtable groups, Hashtable clubs)
        {
            Club currentClub = null;
            Team currentTeam = null;
            League currentGroup = null;

            TextFile groupFile = new TextFile(openFileDialog1.FileName);
            List<String> notification = new List<string>();
            String content = groupFile.ReadFile(false, notification);
            content = Util.clear(content);
            String[] row = content.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (row.Length == 0)
                return false;

            String[] colNames = row[0].Split(new char[] { ';' }, StringSplitOptions.None);
            Util.Index idx = new Util.Index(-1);

            // Indizes ermitteln
            for (int i = 0; i < colNames.Length; i++)
            {
                switch (colNames[i])
                {
                    case "Region":
                        idx.region = i; break;
                    case "Gruppe":
                        idx.group = i; break;
                    case "VereinNr":
                        idx.clubId = i; break;
                    case "VereinName":
                        idx.clubName = i; break;
                    case "MannschaftAltersklasse":
                        idx.ageGroup = i; break;
                    case "MannschaftNr":
                        idx.teamNo = i; break;
                }
            }

            // Sichergehen, dass alle Indizes gefunden wurden
            if (idx.region == -1 || idx.group == -1 || idx.clubId == -1 || idx.clubName == -1 || idx.ageGroup == -1 || idx.teamNo == -1)
                return false;

            // Daten auslesen
            for (int i = 1; i < row.Length; i++)
            {
                String[] data = row[i].Split(new char[] { ';' }, StringSplitOptions.None);

                // Verein ermitteln oder anlegen
                if (clubs.ContainsKey(data[idx.clubId]))
                    currentClub = (Club)clubs[data[idx.clubId]];
                else
                {
                    currentClub = new Club(data[idx.clubName], Util.toInt(data[idx.clubId]), clubs.Count);
                    clubs.Add(data[idx.clubId], currentClub);
                }

                // Gruppe ermitteln oder anlegen
                String fullName = data[idx.group] + " (" + data[idx.region] + ")";
                if (groups.ContainsKey(fullName))
                    currentGroup = (League)groups[fullName];
                else
                {
                    currentGroup = new League(fullName, groups.Count);
                    groups.Add(fullName, currentGroup);
                }

                // Team anlegen und der Gruppe hinzufügen
                currentTeam = new Team(currentClub.name + " " + Util.toRoman(data[idx.teamNo]), currentClub,
                    Util.toRoman(data[idx.teamNo]), data[idx.ageGroup]);
                currentTeam.league = currentGroup;
                currentGroup.team[currentGroup.nrOfTeams++] = currentTeam;
                currentClub.team.Add(currentTeam);
            }

            return true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                Data.league[comboBox1.SelectedIndex].field = Util.toInt(comboBox4.SelectedItem.ToString());
                fillDataGridView(comboBox4);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
