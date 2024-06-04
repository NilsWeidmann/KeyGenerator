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
            this.boxLinkGroups = new System.Windows.Forms.TextBox();
            this.buttonDeleteTeamNewGroup = new System.Windows.Forms.Button();
            this.dataGridViewGroups = new System.Windows.Forms.DataGridView();
            this.dataGridViewClubs = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.boxLinkClubs = new System.Windows.Forms.TextBox();
            this.buttonDeleteTeamNewClub = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteTeamAddGroup = new System.Windows.Forms.Button();
            this.dataGridViewTeams = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteTeamAddClub = new System.Windows.Forms.Button();
            this.buttonDeleteTeamSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubs)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeams)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxLinkGroups
            // 
            this.boxLinkGroups.Location = new System.Drawing.Point(64, 25);
            this.boxLinkGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxLinkGroups.Name = "boxLinkGroups";
            this.boxLinkGroups.Size = new System.Drawing.Size(774, 26);
            this.boxLinkGroups.TabIndex = 0;
            // 
            // buttonDeleteTeamNewGroup
            // 
            this.buttonDeleteTeamNewGroup.Location = new System.Drawing.Point(849, 22);
            this.buttonDeleteTeamNewGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteTeamNewGroup.Name = "buttonDeleteTeamNewGroup";
            this.buttonDeleteTeamNewGroup.Size = new System.Drawing.Size(112, 35);
            this.buttonDeleteTeamNewGroup.TabIndex = 1;
            this.buttonDeleteTeamNewGroup.Text = "Neu";
            this.buttonDeleteTeamNewGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteTeamNewGroup.Click += new System.EventHandler(this.buttonDeleteTeamNewGroup_Click);
            // 
            // dataGridViewGroups
            // 
            this.dataGridViewGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGroups.Location = new System.Drawing.Point(15, 66);
            this.dataGridViewGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewGroups.Name = "dataGridViewGroups";
            this.dataGridViewGroups.RowHeadersWidth = 62;
            this.dataGridViewGroups.Size = new System.Drawing.Size(525, 152);
            this.dataGridViewGroups.TabIndex = 3;
            this.dataGridViewGroups.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // dataGridViewClubs
            // 
            this.dataGridViewClubs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClubs.Location = new System.Drawing.Point(15, 66);
            this.dataGridViewClubs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewClubs.Name = "dataGridViewClubs";
            this.dataGridViewClubs.RowHeadersWidth = 62;
            this.dataGridViewClubs.Size = new System.Drawing.Size(1068, 152);
            this.dataGridViewClubs.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Link:";
            // 
            // boxLinkClubs
            // 
            this.boxLinkClubs.Location = new System.Drawing.Point(64, 20);
            this.boxLinkClubs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxLinkClubs.Name = "boxLinkClubs";
            this.boxLinkClubs.Size = new System.Drawing.Size(774, 26);
            this.boxLinkClubs.TabIndex = 6;
            // 
            // buttonDeleteTeamNewClub
            // 
            this.buttonDeleteTeamNewClub.Location = new System.Drawing.Point(849, 15);
            this.buttonDeleteTeamNewClub.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteTeamNewClub.Name = "buttonDeleteTeamNewClub";
            this.buttonDeleteTeamNewClub.Size = new System.Drawing.Size(112, 35);
            this.buttonDeleteTeamNewClub.TabIndex = 7;
            this.buttonDeleteTeamNewClub.Text = "Neu";
            this.buttonDeleteTeamNewClub.UseVisualStyleBackColor = true;
            this.buttonDeleteTeamNewClub.Click += new System.EventHandler(this.buttonDeleteTeamNewClub_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDeleteTeamAddGroup);
            this.groupBox1.Controls.Add(this.dataGridViewTeams);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.boxLinkGroups);
            this.groupBox1.Controls.Add(this.buttonDeleteTeamNewGroup);
            this.groupBox1.Controls.Add(this.dataGridViewGroups);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1092, 228);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Staffeln";
            // 
            // buttonDeleteTeamAddGroup
            // 
            this.buttonDeleteTeamAddGroup.Location = new System.Drawing.Point(970, 22);
            this.buttonDeleteTeamAddGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteTeamAddGroup.Name = "buttonDeleteTeamAddGroup";
            this.buttonDeleteTeamAddGroup.Size = new System.Drawing.Size(112, 35);
            this.buttonDeleteTeamAddGroup.TabIndex = 5;
            this.buttonDeleteTeamAddGroup.Text = "Hinzu";
            this.buttonDeleteTeamAddGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteTeamAddGroup.Click += new System.EventHandler(this.buttonDeleteTeamAddGroup_Click);
            // 
            // dataGridViewTeams
            // 
            this.dataGridViewTeams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTeams.Location = new System.Drawing.Point(558, 66);
            this.dataGridViewTeams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewTeams.Name = "dataGridViewTeams";
            this.dataGridViewTeams.RowHeadersWidth = 62;
            this.dataGridViewTeams.Size = new System.Drawing.Size(525, 152);
            this.dataGridViewTeams.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonDeleteTeamAddClub);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.boxLinkClubs);
            this.groupBox2.Controls.Add(this.dataGridViewClubs);
            this.groupBox2.Controls.Add(this.buttonDeleteTeamNewClub);
            this.groupBox2.Location = new System.Drawing.Point(18, 255);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1092, 228);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vereine";
            // 
            // buttonDeleteTeamAddClub
            // 
            this.buttonDeleteTeamAddClub.Location = new System.Drawing.Point(970, 17);
            this.buttonDeleteTeamAddClub.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteTeamAddClub.Name = "buttonDeleteTeamAddClub";
            this.buttonDeleteTeamAddClub.Size = new System.Drawing.Size(112, 35);
            this.buttonDeleteTeamAddClub.TabIndex = 8;
            this.buttonDeleteTeamAddClub.Text = "Hinzu";
            this.buttonDeleteTeamAddClub.UseVisualStyleBackColor = true;
            this.buttonDeleteTeamAddClub.Click += new System.EventHandler(this.buttonDeleteTeamAddClub_Click);
            // 
            // buttonDeleteTeamSave
            // 
            this.buttonDeleteTeamSave.Location = new System.Drawing.Point(988, 492);
            this.buttonDeleteTeamSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteTeamSave.Name = "buttonDeleteTeamSave";
            this.buttonDeleteTeamSave.Size = new System.Drawing.Size(112, 35);
            this.buttonDeleteTeamSave.TabIndex = 10;
            this.buttonDeleteTeamSave.Text = "Speichern";
            this.buttonDeleteTeamSave.UseVisualStyleBackColor = true;
            this.buttonDeleteTeamSave.Click += new System.EventHandler(this.buttonDeleteTeamSave_Click);
            // 
            // ClickTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 543);
            this.Controls.Add(this.buttonDeleteTeamSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ClickTT";
            this.Text = "click-tt Import";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClickTT_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeams)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox boxLinkGroups;
        private System.Windows.Forms.Button buttonDeleteTeamNewGroup;
        private System.Windows.Forms.DataGridView dataGridViewGroups;
        private System.Windows.Forms.DataGridView dataGridViewClubs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox boxLinkClubs;
        private System.Windows.Forms.Button buttonDeleteTeamNewClub;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewTeams;
        private System.Windows.Forms.Button buttonDeleteTeamSave;
        private System.Windows.Forms.Button buttonDeleteTeamAddGroup;
        private System.Windows.Forms.Button buttonDeleteTeamAddClub;
    }
}

