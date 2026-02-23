namespace KeyGenerator
{
    partial class ClickTT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            boxLinkGroups = new System.Windows.Forms.TextBox();
            dataGridViewGroups = new System.Windows.Forms.DataGridView();
            dataGridViewClubs = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            boxLinkClubs = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            buttonDeleteTeamAddGroup = new System.Windows.Forms.Button();
            dataGridViewTeamsOfGroups = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            dataGridViewTeamsOfClubs = new System.Windows.Forms.DataGridView();
            buttonDeleteTeamAddClub = new System.Windows.Forms.Button();
            buttonDeleteTeamSave = new System.Windows.Forms.Button();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClubs).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTeamsOfGroups).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTeamsOfClubs).BeginInit();
            SuspendLayout();
            // 
            // boxLinkGroups
            // 
            boxLinkGroups.Enabled = false;
            boxLinkGroups.Location = new System.Drawing.Point(50, 19);
            boxLinkGroups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxLinkGroups.Name = "boxLinkGroups";
            boxLinkGroups.Size = new System.Drawing.Size(698, 23);
            boxLinkGroups.TabIndex = 0;
            // 
            // dataGridViewGroups
            // 
            dataGridViewGroups.AllowUserToAddRows = false;
            dataGridViewGroups.AllowUserToDeleteRows = false;
            dataGridViewGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGroups.Location = new System.Drawing.Point(12, 50);
            dataGridViewGroups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridViewGroups.Name = "dataGridViewGroups";
            dataGridViewGroups.RowHeadersWidth = 62;
            dataGridViewGroups.Size = new System.Drawing.Size(408, 114);
            dataGridViewGroups.TabIndex = 3;
            dataGridViewGroups.CellValueChanged += dataGridViewGroups_CellValueChanged;
            dataGridViewGroups.RowEnter += dataGridView1_RowEnter;
            // 
            // dataGridViewClubs
            // 
            dataGridViewClubs.AllowUserToAddRows = false;
            dataGridViewClubs.AllowUserToDeleteRows = false;
            dataGridViewClubs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClubs.Location = new System.Drawing.Point(12, 50);
            dataGridViewClubs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridViewClubs.Name = "dataGridViewClubs";
            dataGridViewClubs.RowHeadersWidth = 62;
            dataGridViewClubs.Size = new System.Drawing.Size(408, 114);
            dataGridViewClubs.TabIndex = 4;
            dataGridViewClubs.CellValueChanged += dataGridViewClubs_CellValueChanged;
            dataGridViewClubs.RowEnter += dataGridViewClubs_RowEnter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(32, 15);
            label2.TabIndex = 5;
            label2.Text = "Link:";
            // 
            // boxLinkClubs
            // 
            boxLinkClubs.Enabled = false;
            boxLinkClubs.Location = new System.Drawing.Point(50, 15);
            boxLinkClubs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxLinkClubs.Name = "boxLinkClubs";
            boxLinkClubs.Size = new System.Drawing.Size(698, 23);
            boxLinkClubs.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonDeleteTeamAddGroup);
            groupBox1.Controls.Add(dataGridViewTeamsOfGroups);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(boxLinkGroups);
            groupBox1.Controls.Add(dataGridViewGroups);
            groupBox1.Location = new System.Drawing.Point(14, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Size = new System.Drawing.Size(849, 171);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Staffeln";
            // 
            // buttonDeleteTeamAddGroup
            // 
            buttonDeleteTeamAddGroup.Location = new System.Drawing.Point(754, 16);
            buttonDeleteTeamAddGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonDeleteTeamAddGroup.Name = "buttonDeleteTeamAddGroup";
            buttonDeleteTeamAddGroup.Size = new System.Drawing.Size(87, 26);
            buttonDeleteTeamAddGroup.TabIndex = 5;
            buttonDeleteTeamAddGroup.Text = "Durchsuchen";
            buttonDeleteTeamAddGroup.UseVisualStyleBackColor = true;
            buttonDeleteTeamAddGroup.Click += buttonDeleteTeamAddGroup_Click;
            // 
            // dataGridViewTeamsOfGroups
            // 
            dataGridViewTeamsOfGroups.AllowUserToAddRows = false;
            dataGridViewTeamsOfGroups.AllowUserToDeleteRows = false;
            dataGridViewTeamsOfGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTeamsOfGroups.Location = new System.Drawing.Point(434, 50);
            dataGridViewTeamsOfGroups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridViewTeamsOfGroups.Name = "dataGridViewTeamsOfGroups";
            dataGridViewTeamsOfGroups.RowHeadersWidth = 62;
            dataGridViewTeamsOfGroups.Size = new System.Drawing.Size(408, 114);
            dataGridViewTeamsOfGroups.TabIndex = 4;
            dataGridViewTeamsOfGroups.CellValueChanged += dataGridViewTeamsOfGroups_CellValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 23);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(32, 15);
            label1.TabIndex = 0;
            label1.Text = "Link:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridViewTeamsOfClubs);
            groupBox2.Controls.Add(buttonDeleteTeamAddClub);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(boxLinkClubs);
            groupBox2.Controls.Add(dataGridViewClubs);
            groupBox2.Location = new System.Drawing.Point(14, 191);
            groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox2.Size = new System.Drawing.Size(849, 171);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Vereine";
            // 
            // dataGridViewTeamsOfClubs
            // 
            dataGridViewTeamsOfClubs.AllowUserToAddRows = false;
            dataGridViewTeamsOfClubs.AllowUserToDeleteRows = false;
            dataGridViewTeamsOfClubs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTeamsOfClubs.Location = new System.Drawing.Point(433, 50);
            dataGridViewTeamsOfClubs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridViewTeamsOfClubs.Name = "dataGridViewTeamsOfClubs";
            dataGridViewTeamsOfClubs.RowHeadersWidth = 62;
            dataGridViewTeamsOfClubs.Size = new System.Drawing.Size(408, 114);
            dataGridViewTeamsOfClubs.TabIndex = 6;
            dataGridViewTeamsOfClubs.CellValueChanged += dataGridViewTeamsOfClubs_CellValueChanged;
            // 
            // buttonDeleteTeamAddClub
            // 
            buttonDeleteTeamAddClub.Location = new System.Drawing.Point(754, 13);
            buttonDeleteTeamAddClub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonDeleteTeamAddClub.Name = "buttonDeleteTeamAddClub";
            buttonDeleteTeamAddClub.Size = new System.Drawing.Size(87, 26);
            buttonDeleteTeamAddClub.TabIndex = 8;
            buttonDeleteTeamAddClub.Text = "Durchsuchen";
            buttonDeleteTeamAddClub.UseVisualStyleBackColor = true;
            buttonDeleteTeamAddClub.Click += buttonDeleteTeamAddClub_Click;
            // 
            // buttonDeleteTeamSave
            // 
            buttonDeleteTeamSave.Location = new System.Drawing.Point(768, 369);
            buttonDeleteTeamSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonDeleteTeamSave.Name = "buttonDeleteTeamSave";
            buttonDeleteTeamSave.Size = new System.Drawing.Size(87, 26);
            buttonDeleteTeamSave.TabIndex = 10;
            buttonDeleteTeamSave.Text = "Speichern";
            buttonDeleteTeamSave.UseVisualStyleBackColor = true;
            buttonDeleteTeamSave.Click += buttonDeleteTeamSave_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ClickTT
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(877, 407);
            Controls.Add(buttonDeleteTeamSave);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "ClickTT";
            Text = "click-tt Import";
            FormClosing += ClickTT_FormClosing;
            SizeChanged += ClickTT_SizeChanged;
            MouseUp += ClickTT_MouseUp;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridViewGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClubs).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTeamsOfGroups).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTeamsOfClubs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox boxLinkGroups;
        private System.Windows.Forms.DataGridView dataGridViewGroups;
        private System.Windows.Forms.DataGridView dataGridViewClubs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox boxLinkClubs;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewTeamsOfGroups;
        private System.Windows.Forms.Button buttonDeleteTeamSave;
        private System.Windows.Forms.Button buttonDeleteTeamAddGroup;
        private System.Windows.Forms.Button buttonDeleteTeamAddClub;
        private System.Windows.Forms.DataGridView dataGridViewTeamsOfClubs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

