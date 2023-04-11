using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Schluesselzahlen
{
    public partial class Schluesselzahlen : Form
    {
        public Club verein;

        public Schluesselzahlen()
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = false;
            enableFields();
            checkBox1.Enabled = false;
            comboBox12.Enabled = false;
            textBox2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox4.Items.Add("-");
            comboBox4.Items.Add("A");
            comboBox4.Items.Add("B");
            comboBox4.Items.Add("X");
            comboBox4.Items.Add("Y");
            comboBox4.SelectedIndex = 0;
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
            clearGrid();
        }

        public void fillFields(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = Data.team_min; i<= Data.team_max; i+=2)
                cb.Items.Add(i.ToString());
            cb.SelectedIndex = -1;
        }

        public void enableFields()
        {
            textBox4.Enabled = false;
            comboBox12.Enabled = comboBox2.SelectedIndex != -1;
        }

        public void showResults(League[] best_l, Club[] best_v, bool cancelled)
        {
            if (Data.notification.Count > 0)
            {
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                loadFromFile(Data.clubs_b, Data.group_b, Data.relations_b);
            }
            else if (cancelled)
                loadFromFile(Data.clubs_b, Data.group_b, Data.relations_b);
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

        public void initUI()
        {
            dataGridView1.Rows.Clear();
            checkBox1.Enabled = false;
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            for (int i = 0; i < Data.league.Length; i++)
                comboBox1.Items.Add(Data.league[i].name);
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "";
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            comboBox3.Text = "";
            comboBox3.Items.Clear();
            for (int i = 0; i < Data.club.Length; i++)
                comboBox3.Items.Add(Data.club[i].name);
            enableFields();
        }

        public bool loadFromFile(TextFile ver, TextFile sta, TextFile bez)
        {
            button6.Enabled = false;
            Data.club = Data.getClubs(ver, Data.partnership);
            Data.league = Data.getGroups(Data.club, sta);
            Data.getRelations(Data.league, bez);
            Data.allocateTeams(Data.club, Data.league);
            Data.getSpielplan(Data.path);
            if (Data.notification.Count > 0)
            {
                button3.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                MessageBox.Show(Data.notification[0]);
                Data.notification.Clear();
                button6.Enabled = true;
                return false;
            }
            else
            {
                initUI();
                button3.Enabled = true;
                button14.Enabled = true;
                button15.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton1.Checked = true;
                comboBox1.Enabled = comboBox2.Enabled = comboBox3.Enabled = true;
                button6.Enabled = true;
                return true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadFromFile(Data.clubs, Data.group, Data.relations);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "";
            comboBox12.Items.Clear();
            comboBox12.SelectedIndex = -1;
            comboBox12.Text = "";
            if (!(comboBox1.SelectedIndex == -1))
                for (int i = 0; i < Data.league[comboBox1.SelectedIndex].nr_of_teams; i++)
                    comboBox2.Items.Add(Data.league[comboBox1.SelectedIndex].team[i].name);
            enableFields();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox12.Items.Clear();
            comboBox12.Items.Add("-");
            for (int i = 0; i < Data.league[comboBox1.SelectedIndex].field; i++)
                comboBox12.Items.Add(i + 1);
            if (comboBox2.SelectedIndex != -1)
                comboBox12.SelectedIndex = Data.league[comboBox1.SelectedIndex].team[comboBox2.SelectedIndex].number;
            else
                comboBox12.SelectedIndex = -1;
            enableFields();
        }

        public void clearGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Liga", "Liga");
            dataGridView1.Columns.Add("Team", "Team");
            dataGridView1.Columns.Add("Schlüssel", "Schlüssel");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            comboBox3.Enabled = radioButton1.Checked;
            checkBox1.Enabled = comboBox4.Enabled = comboBox5.Enabled = comboBox6.Enabled = 
            comboBox7.Enabled = comboBox8.Enabled = radioButton1.Checked && comboBox3.SelectedIndex != -1;
            if (!radioButton1.Checked || comboBox3.SelectedIndex == -1)
                comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";
            button4.Enabled = false;
            if (radioButton1.Checked)
            {
                if (comboBox3.SelectedItem != null)
                {
                    comboBox3.Text = comboBox3.SelectedItem.ToString();
                    comboBox4.Text = comboBox4.SelectedItem.ToString();
                    comboBox5.Text = comboBox5.SelectedItem.ToString();
                    comboBox6.Text = comboBox6.SelectedItem.ToString();
                    comboBox7.Text = comboBox7.SelectedItem.ToString();
                    comboBox8.Text = comboBox8.SelectedItem.ToString();
                }
                else
                    comboBox3.Text = comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";
                
            }
            else
            {
                comboBox3.Text = comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";
                checkBox1.Checked = false;
            }
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void init()
        {
            int anzahl_teams = 0;
            clearGrid();
            if (radioButton1.Checked)
            {
                verein = Data.club[comboBox3.SelectedIndex];
                if (comboBox3.SelectedIndex == -1)
                {
                    checkBox1.Checked = checkBox1.Enabled = button4.Enabled = false;
                    comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = "";
                }
                else
                {
                    checkBox1.Checked = verein.capacity;
                    button4.Enabled = true;
                }
                if (comboBox5.Items.Count > verein.a)
                    comboBox5.SelectedIndex = verein.a;
                if (comboBox6.Items.Count > verein.b)
                    comboBox6.SelectedIndex = verein.b;
                if (comboBox7.Items.Count > verein.x)
                    comboBox7.SelectedIndex = verein.x;
                if (comboBox8.Items.Count > verein.y)
                    comboBox8.SelectedIndex = verein.y;
                for (int j = 0; j < verein.team.Length; j++)
                {
                    String[] inhalt = new String[3];
                    inhalt[0] = verein.team[j].league.name;
                    inhalt[1] = verein.team[j].name;
                    if (verein.team[j].number != 0)
                        inhalt[2] = verein.team[j].number.ToString();
                    else
                        inhalt[2] = "";
                    dataGridView1.Rows.Add(inhalt);
                    for (int l = 0; l < 3; l++)
                    {
                        Color color;
                        switch (verein.team[j].week)
                        {
                            case 'A': color = Color.Yellow; break;
                            case 'B': color = Color.Orange; break;
                            case 'X': color = Color.LightBlue; break;
                            case 'Y': color = Color.LightGreen; break;
                            default: color = Color.White; break;
                        }
                        dataGridView1.Rows[anzahl_teams].Cells[l].Style.BackColor = color;
                    }
                    anzahl_teams++;
                }
            }
            else
            {
                for (int j = 0; j < Data.league[comboBox1.SelectedIndex].nr_of_teams; j++)
                {
                    String[] inhalt = new String[3];
                    inhalt[0] = Data.league[comboBox1.SelectedIndex].name;
                    inhalt[1] = Data.league[comboBox1.SelectedIndex].team[j].name;

                    Team team = Data.league[comboBox1.SelectedIndex].team[j];
                    int feld = Data.league[comboBox1.SelectedIndex].field;

                    if (team.number != 0)
                        inhalt[2] = team.number.ToString();
                    else if (team.week == 'A' && team.club.a != 0)
                        if (feld == Data.field[0])
                            inhalt[2] = team.club.a.ToString();
                        else
                            inhalt[2] = Data.nm.getParallel(Data.field[0], feld, team.club.a).ToString();
                    else if (team.week == 'B' && team.club.b != 0)
                        if (feld == Data.field[0])
                            inhalt[2] = team.club.b.ToString();
                        else
                            inhalt[2] = Data.nm.getParallel(Data.field[0], feld, team.club.b).ToString();
                    else if (team.week == 'X' && team.club.x != 0)
                        if (feld == Data.field[1])
                            inhalt[2] = team.club.x.ToString();
                        else
                            inhalt[2] = Data.nm.getParallel(Data.field[1], feld, team.club.x).ToString();
                    else if (team.week == 'Y' && team.club.y != 0)
                        if (feld == Data.field[1])
                            inhalt[2] = team.club.y.ToString();
                        else
                            inhalt[2] = Data.nm.getParallel(Data.field[1], feld, team.club.y).ToString();
                    else
                        inhalt[2] = "";
                    
                    dataGridView1.Rows.Add(inhalt);
                    if (!inhalt[2].Equals(""))
                        for (int i = 0; i < 3; i++)
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    else if (team.week != '-')
                        for (int i = 0; i < 3; i++)
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Yellow;
                }
                verein = null;
            }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void setFiles(string path)
        {
            Data.group = new TextFile(path + @"\Staffeln.csv");
            Data.group_b = new TextFile(path + @"\Staffeln_Backup.csv");
            Data.clubs = new TextFile(path + @"\Vereine.csv");
            Data.clubs_b = new TextFile(path + @"\Vereine_Backup.csv");
            Data.relations = new TextFile(path + @"\Beziehungen.csv");
            Data.relations_b = new TextFile(path + @"\Beziehungen_Backup.csv");
            if (comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1)
            {
                button6.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
            }
            Data.nm = new NumberMapper(path);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                if (comboBox3.SelectedIndex != -1)
                    init();
                else
                    checkBox1.Enabled = checkBox1.Checked = button4.Enabled = false;
            else
                checkBox1.Enabled = checkBox1.Checked = button4.Enabled = false;
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
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                textBox4.Text = "";
                textBox4.Enabled = false;
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                for (int i = 0; i < Data.league[comboBox1.SelectedIndex].nr_of_teams; i++)
                    comboBox2.Items.Add(Data.league[comboBox1.SelectedIndex].team[i].name);
                textBox4.Text = Data.league[comboBox1.SelectedIndex].field.ToString();
                textBox4.Enabled = false;
            }
            enableFields();
            if (radioButton2.Checked)
                if (comboBox1.SelectedIndex != -1)
                    init();
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
            bool frei = true;
            int key = Util.toInt(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            for (int i = 0; i < verein.team[e.RowIndex].league.nr_of_teams; i++)
                if (verein.team[e.RowIndex].league.team[i].number == key)
                    frei = false;
            if (key > 0 && key <= verein.team[e.RowIndex].league.nr_of_teams + verein.team[e.RowIndex].league.nr_of_teams % 2 && frei)
                verein.team[e.RowIndex].number = key;
            else
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = "";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                Data.league[comboBox1.SelectedIndex].field = Util.toInt(textBox4.Text);
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
                Data.league[comboBox1.SelectedIndex].team[comboBox2.SelectedIndex].number = comboBox12.SelectedIndex;
            Data.save(Data.league, Data.club, Data.partnership, Data.clubs, Data.group, Data.relations);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex >= 0)
            {
                Data.club[comboBox3.SelectedIndex].a = comboBox5.SelectedIndex;
                if (comboBox5.SelectedIndex == 0)
                    Data.club[comboBox3.SelectedIndex].b = 0;
                else
                    Data.club[comboBox3.SelectedIndex].b = Data.nm.getGegenlaeufig(Data.field[0], Data.field[0], comboBox5.SelectedIndex);
                comboBox6.SelectedIndex = Data.club[comboBox3.SelectedIndex].b;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex >= 0)
            {
                Data.club[comboBox3.SelectedIndex].b = comboBox6.SelectedIndex;
                if (comboBox6.SelectedIndex == 0)
                    Data.club[comboBox3.SelectedIndex].a = 0;
                else
                    Data.club[comboBox3.SelectedIndex].a = Data.nm.getGegenlaeufig(Data.field[0], Data.field[0], comboBox6.SelectedIndex);
                comboBox5.SelectedIndex = Data.club[comboBox3.SelectedIndex].a;
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex >= 0)
            {
                Data.club[comboBox3.SelectedIndex].x = comboBox7.SelectedIndex;
                if (comboBox7.SelectedIndex == 0)
                    Data.club[comboBox3.SelectedIndex].y = 0;
                else
                    Data.club[comboBox3.SelectedIndex].y = Data.nm.getGegenlaeufig(Data.field[1], Data.field[1], comboBox7.SelectedIndex);
                comboBox8.SelectedIndex = Data.club[comboBox3.SelectedIndex].y;
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex >= 0)
            {
                Data.club[comboBox3.SelectedIndex].y = comboBox8.SelectedIndex;
                if (comboBox8.SelectedIndex == 0)
                    Data.club[comboBox3.SelectedIndex].x = 0;
                else
                    Data.club[comboBox3.SelectedIndex].x = Data.nm.getGegenlaeufig(Data.field[1], Data.field[1], comboBox8.SelectedIndex);
                comboBox7.SelectedIndex = Data.club[comboBox3.SelectedIndex].x;
            }
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            comboBox5.Items.Add('-');
            for (int i = 1; i <= Util.toInt(comboBox10.SelectedItem.ToString()); i++)
                comboBox5.Items.Add(i);
            if (comboBox5.Items.Count > Util.toInt(comboBox5.Text))
                comboBox5.SelectedIndex = Util.toInt(comboBox5.Text);
            else
                comboBox5.SelectedIndex = 0;
            comboBox6.Items.Clear();
            comboBox6.Items.Add('-');
            for (int i = 1; i <= Util.toInt(comboBox10.SelectedItem.ToString()); i++)
                comboBox6.Items.Add(i);
            if (comboBox6.Items.Count > Util.toInt(comboBox6.Text))
                comboBox6.SelectedIndex = Util.toInt(comboBox6.Text);
            else
                comboBox6.SelectedIndex = 0;
            Data.field[0] = Util.toInt(comboBox10.SelectedItem.ToString());
            checkWochen();
            if (comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1 && Data.group != null)
            {
                button6.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            comboBox7.Items.Add('-');
            for (int i = 1; i <= Util.toInt(comboBox11.SelectedItem.ToString()); i++)
                comboBox7.Items.Add(i);
            if (comboBox7.Items.Count > Util.toInt(comboBox7.Text))
                comboBox7.SelectedIndex = Util.toInt(comboBox7.Text);
            else
                comboBox5.SelectedIndex = 0;
            comboBox8.Items.Clear();
            comboBox8.Items.Add('-');
            for (int i = 1; i <= Util.toInt(comboBox11.SelectedItem.ToString()); i++)
                comboBox8.Items.Add(i);
            if (comboBox8.Items.Count > Util.toInt(comboBox8.Text))
                comboBox8.SelectedIndex = Util.toInt(comboBox8.Text);
            else
                comboBox8.SelectedIndex = 0;
            Data.field[1] = Util.toInt(comboBox11.SelectedItem.ToString());
            checkWochen();
            if (comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1 && Data.group != null)
            {
                button6.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
            }
        }

        public static void checkWochen() 
        {
            if (Data.club == null)
                return;
            for (int i = 0; i < Data.club.Length; i++)
            {
                if (Data.club[i].a > Data.field[0])
                    Data.club[i].a = 0;
                if (Data.club[i].b > Data.field[0])
                    Data.club[i].b = 0;
                if (Data.club[i].x > Data.field[1])
                    Data.club[i].x = 0;
                if (Data.club[i].y > Data.field[1])
                    Data.club[i].y = 0;
            }
        }

        public void solveConflicts(League[] l, Club[] v)
        {
            int[] konflikte = new int[2];
            List<Conflict> k = new List<Conflict>();
            Data.checkConflicts(l, k);
            if (k.Count > 0)
            {
                Alternatives a = new Alternatives(k.ToArray(), l, v, this);
                this.Enabled = false;
                a.Visible = true;
            }
            else
            {
                Data.generiereSchluesselzahlen();
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

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.league[comboBox1.SelectedIndex].team[comboBox2.SelectedIndex].number = comboBox12.SelectedIndex;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton1.Checked;
            if (radioButton1.Checked && comboBox3.SelectedIndex != -1)
                init();
            else
                clearGrid();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton2.Checked;
            if (radioButton2.Checked && comboBox1.SelectedIndex != -1)
                init();
            else
                clearGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool erfolg = loadFromFile(Data.clubs, Data.group, Data.relations);
            if (erfolg)
            {
                this.Enabled = false;
                DataInput d = new DataInput(this);
                d.Visible = true;
            }
        }

        private void Schluesselzahlen_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - 100;
            dataGridView1.Width = this.Width - 350;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool erfolg = loadFromFile(Data.clubs, Data.group, Data.relations);
            if (erfolg)
            {
                this.Enabled = false;
                ClickTT ctt = new ClickTT(Data.league, Data.club, Data.partnership, this);
                ctt.Visible = true;
            }
        }

        private void Schluesselzahlen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(Data.club == null || Data.league == null))
                switch (MessageBox.Show("Wollen Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        Data.save(Data.league, Data.club, Data.partnership, Data.clubs, Data.group, Data.relations);
                        this.loadFromFile(Data.clubs, Data.group, Data.relations);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButton2.Checked || comboBox3.SelectedIndex == -1 || e.RowIndex == -1 || e.RowIndex >= verein.team.Length)
                    return;
                if (comboBox4.SelectedIndex >= 0)
                {
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0: verein.team[e.RowIndex].week = '-'; break;
                        case 1: verein.team[e.RowIndex].week = 'A'; break;
                        case 2: verein.team[e.RowIndex].week = 'B'; break;
                        case 3: verein.team[e.RowIndex].week = 'X'; break;
                        case 4: verein.team[e.RowIndex].week = 'Y'; break;
                    }
                    init();
                }
            }

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
