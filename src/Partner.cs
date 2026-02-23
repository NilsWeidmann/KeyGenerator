using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Partner : Form
    {
        KeyGenerator caller;
        Club[] clubs;
        Group[] groups;
        int index;

        public Partner(KeyGenerator caller, Club[] clubs, Group[] groups, int index, string[] weeks)
        {
            InitializeComponent();
            this.caller = caller;
            this.clubs = clubs;
            this.groups = groups;
            this.index = index;

            dataGridView.Columns.Clear();
            DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
            dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (Club club in clubs)
                dgvc.Items.Add(club.name);

            dgvc.HeaderText = "Verein A";
            dataGridView.Columns.Add(dgvc);

            dgvc = new DataGridViewComboBoxColumn();
            dgvc.HeaderText = "Woche";
            dgvc.Items.AddRange(weeks);
            dataGridView.Columns.Add(dgvc);

            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns[2].ReadOnly = true;

            dgvc = new DataGridViewComboBoxColumn();
            dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (Club club in clubs)
                dgvc.Items.Add(club.name);

            dgvc.HeaderText = "Verein B";
            dataGridView.Columns.Add(dgvc);

            dgvc = new DataGridViewComboBoxColumn();
            dgvc.HeaderText = "Woche";
            dgvc.Items.AddRange(weeks);
            dataGridView.Columns.Add(dgvc);

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.Rows.Clear();

            foreach (Club c in clubs)
                foreach (Partnership p in c.partnerships)
                    if (p.indexA == index || p.indexB == index)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        fillRow(row, p);
                        dataGridView.Rows.Add(row);
                    }
        }

        private void fillRow(DataGridViewRow row, Partnership p)
        {
            if (row.Cells.Count < 5)
                return;

            Club currentClub = clubs[index];
            char currentWeek = index == p.indexA ? p.weekA : p.weekB;
            Club otherClub = index == p.indexA ? clubs[p.indexB] : clubs[p.indexA];
            char otherWeek = index == p.indexA ? p.weekB : p.weekA;

            // Verein A
            row.Cells[0].Value = currentClub.name;

            // Woche aktueller Verein
            DataGridViewComboBoxCell dgvcbc = new DataGridViewComboBoxCell();
            dgvcbc.FlatStyle = FlatStyle.Flat;
            dgvcbc.Items.Add("A");
            dgvcbc.Items.Add("B");
            dgvcbc.Items.Add("X");
            dgvcbc.Items.Add("Y");

            switch (currentWeek)
            {
                case 'A': dgvcbc.Value = dgvcbc.Items[0]; break;
                case 'B': dgvcbc.Value = dgvcbc.Items[1]; break;
                case 'X': dgvcbc.Value = dgvcbc.Items[2]; break;
                case 'Y': dgvcbc.Value = dgvcbc.Items[3]; break;
            }

            row.Cells[1] = dgvcbc;

            // Gleichheitszeichen
            row.Cells[2].Value = "=";

            // Partnerverein
            dgvcbc = new DataGridViewComboBoxCell();
            dgvcbc.FlatStyle = FlatStyle.Flat;
            for (int i = 0; i < clubs.Length; i++)
                dgvcbc.Items.Add(clubs[i].name);
            if (!row.IsNewRow)
                dgvcbc.Value = dgvcbc.Items[otherClub.index];
            row.Cells[3] = dgvcbc;

            // Woche Partnerverein
            dgvcbc = new DataGridViewComboBoxCell();
            dgvcbc.FlatStyle = FlatStyle.Flat;
            dgvcbc.Items.Add("A");
            dgvcbc.Items.Add("B");
            dgvcbc.Items.Add("X");
            dgvcbc.Items.Add("Y");

            switch (otherWeek)
            {
                case 'A': dgvcbc.Value = dgvcbc.Items[0]; break;
                case 'B': dgvcbc.Value = dgvcbc.Items[1]; break;
                case 'X': dgvcbc.Value = dgvcbc.Items[2]; break;
                case 'Y': dgvcbc.Value = dgvcbc.Items[3]; break;
            }

            row.Cells[4] = dgvcbc;

            // AutoResizeColumns
            dataGridView.AutoResizeColumns();
        }

        bool convert(DataGridViewRow row, String[] values)
        {
            values = new string[row.Cells.Count];
            foreach (DataGridViewCell dgvc in row.Cells)
                if (dgvc.Value == null)
                    return false;
                else
                    values[dgvc.RowIndex] = dgvc.Value.ToString();
            return true;
        }

        private void updatePartners()
        {
            // Alte Partnerschaften löschen, die den aktuellen Verein betreffen
            foreach (Club c in clubs)
                foreach (Partnership p in c.partnerships)
                    if (p.indexA == index || p.indexB == index)
                        c.partnerships.Remove(p);

            // Neue Partnerschaften hinzufügen
            String[] values = null;
            foreach (DataGridViewRow row in dataGridView.Rows)
                if (convert(row, values))
                    clubs[getIndex(values[0])].partnerships.Add(new Partnership(getIndex(values[0]), values[1][0], getIndex(values[3]), values[4][0]));
        }
        private void Partner_FormClosing(object sender, FormClosingEventArgs e)
        {
            updatePartners();
            this.Enabled = false;
            if (Util.confirm(caller, groups, clubs))
                caller.prepare();
            this.Enabled = true;
        }

        private void Partner_Resize(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < dataGridView.Rows.Count; i++)
                if (dataGridView.Rows[i].Cells[0].Value != null && dataGridView.Rows[i].Cells[3].Value != null)
                    if (dataGridView.Rows[i].Cells[0].Value.Equals(dataGridView.Rows[i].Cells[3].Value))
                    {
                        dataGridView.Rows[i].Cells[3].Value = null;
                        MessageBox.Show("Partnerschaften mit sich selbst sind unzulässig!");
                        return;
                    }
        }

        private int getIndex(string name)
        {
            for (int i = 0; i < clubs.Length; i++)
                if (clubs[i].name.Equals(name))
                    return i;
            return -1;
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewComboBoxCell dgvcbc = (DataGridViewComboBoxCell)(e.Row.Cells[0]);
            foreach (Club c in clubs)
                dgvcbc.Items.Add(c.name);

            dgvcbc = (DataGridViewComboBoxCell)(e.Row.Cells[3]);
            foreach (Club c in clubs)
                dgvcbc.Items.Add(c.name);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}