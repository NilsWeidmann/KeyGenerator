namespace KeyGenerator
{
    partial class Miscellaneous
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
            buttonDeleteTeamCancel = new System.Windows.Forms.Button();
            buttonDeleteTeamLoadBackup = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            buttonDeleteTeamDeleteClubsWithoutTeams = new System.Windows.Forms.Button();
            buttonDeleteTeamDeleteKeysForTeams = new System.Windows.Forms.Button();
            buttonDeleteTeamDeleteWeeks = new System.Windows.Forms.Button();
            buttonDeleteTeamDeleteDays = new System.Windows.Forms.Button();
            buttonDeleteTeamDeleteKeysForClubs = new System.Windows.Forms.Button();
            generatorTests = new System.Windows.Forms.Button();
            testsFromFile = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // buttonDeleteTeamCancel
            // 
            buttonDeleteTeamCancel.Location = new System.Drawing.Point(20, 508);
            buttonDeleteTeamCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamCancel.Name = "buttonDeleteTeamCancel";
            buttonDeleteTeamCancel.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamCancel.TabIndex = 5;
            buttonDeleteTeamCancel.Text = "Abbrechen";
            buttonDeleteTeamCancel.UseVisualStyleBackColor = true;
            buttonDeleteTeamCancel.Click += buttonDeleteTeamCancel_Click;
            // 
            // buttonDeleteTeamLoadBackup
            // 
            buttonDeleteTeamLoadBackup.Location = new System.Drawing.Point(18, 72);
            buttonDeleteTeamLoadBackup.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamLoadBackup.Name = "buttonDeleteTeamLoadBackup";
            buttonDeleteTeamLoadBackup.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamLoadBackup.TabIndex = 4;
            buttonDeleteTeamLoadBackup.Text = "Backup laden";
            buttonDeleteTeamLoadBackup.UseVisualStyleBackColor = true;
            buttonDeleteTeamLoadBackup.Click += buttonDeleteTeamLoadBackup_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 18);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(187, 25);
            label1.TabIndex = 3;
            label1.Text = "Was möchten Sie tun?";
            // 
            // buttonDeleteTeamDeleteClubsWithoutTeams
            // 
            buttonDeleteTeamDeleteClubsWithoutTeams.Location = new System.Drawing.Point(20, 129);
            buttonDeleteTeamDeleteClubsWithoutTeams.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamDeleteClubsWithoutTeams.Name = "buttonDeleteTeamDeleteClubsWithoutTeams";
            buttonDeleteTeamDeleteClubsWithoutTeams.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamDeleteClubsWithoutTeams.TabIndex = 6;
            buttonDeleteTeamDeleteClubsWithoutTeams.Text = "Vereine ohne Teams löschen";
            buttonDeleteTeamDeleteClubsWithoutTeams.UseVisualStyleBackColor = true;
            buttonDeleteTeamDeleteClubsWithoutTeams.Click += buttonDeleteTeamDeleteClubsWithoutTeams_Click;
            // 
            // buttonDeleteTeamDeleteKeysForTeams
            // 
            buttonDeleteTeamDeleteKeysForTeams.Location = new System.Drawing.Point(18, 296);
            buttonDeleteTeamDeleteKeysForTeams.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamDeleteKeysForTeams.Name = "buttonDeleteTeamDeleteKeysForTeams";
            buttonDeleteTeamDeleteKeysForTeams.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamDeleteKeysForTeams.TabIndex = 7;
            buttonDeleteTeamDeleteKeysForTeams.Text = "Schlüsselzahlen löschen (Teams)";
            buttonDeleteTeamDeleteKeysForTeams.UseVisualStyleBackColor = true;
            buttonDeleteTeamDeleteKeysForTeams.Click += buttonDeleteTeamDeleteKeysForTeams_Click;
            // 
            // buttonDeleteTeamDeleteWeeks
            // 
            buttonDeleteTeamDeleteWeeks.Location = new System.Drawing.Point(18, 240);
            buttonDeleteTeamDeleteWeeks.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamDeleteWeeks.Name = "buttonDeleteTeamDeleteWeeks";
            buttonDeleteTeamDeleteWeeks.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamDeleteWeeks.TabIndex = 8;
            buttonDeleteTeamDeleteWeeks.Text = "Spielwochenvorgaben löschen";
            buttonDeleteTeamDeleteWeeks.UseVisualStyleBackColor = true;
            buttonDeleteTeamDeleteWeeks.Click += buttonDeleteTeamDeleteWeeks_Click;
            // 
            // buttonDeleteTeamDeleteDays
            // 
            buttonDeleteTeamDeleteDays.Location = new System.Drawing.Point(18, 185);
            buttonDeleteTeamDeleteDays.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamDeleteDays.Name = "buttonDeleteTeamDeleteDays";
            buttonDeleteTeamDeleteDays.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamDeleteDays.TabIndex = 9;
            buttonDeleteTeamDeleteDays.Text = "Spieltagsvorgaben löschen";
            buttonDeleteTeamDeleteDays.UseVisualStyleBackColor = true;
            buttonDeleteTeamDeleteDays.Click += buttonDeleteTeamDeleteDays_Click;
            // 
            // buttonDeleteTeamDeleteKeysForClubs
            // 
            buttonDeleteTeamDeleteKeysForClubs.Location = new System.Drawing.Point(18, 352);
            buttonDeleteTeamDeleteKeysForClubs.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonDeleteTeamDeleteKeysForClubs.Name = "buttonDeleteTeamDeleteKeysForClubs";
            buttonDeleteTeamDeleteKeysForClubs.Size = new System.Drawing.Size(429, 44);
            buttonDeleteTeamDeleteKeysForClubs.TabIndex = 10;
            buttonDeleteTeamDeleteKeysForClubs.Text = "Schlüsselzahlen löschen (Vereine)";
            buttonDeleteTeamDeleteKeysForClubs.UseVisualStyleBackColor = true;
            buttonDeleteTeamDeleteKeysForClubs.Click += buttonDeleteTeamDeleteKeysForClubs_Click;
            // 
            // generatorTests
            // 
            generatorTests.Location = new System.Drawing.Point(20, 405);
            generatorTests.Name = "generatorTests";
            generatorTests.Size = new System.Drawing.Size(429, 44);
            generatorTests.TabIndex = 11;
            generatorTests.Text = "Generator-Tests";
            generatorTests.UseVisualStyleBackColor = true;
            generatorTests.Click += generatorTests_Click;
            // 
            // testsFromFile
            // 
            testsFromFile.Location = new System.Drawing.Point(20, 455);
            testsFromFile.Name = "testsFromFile";
            testsFromFile.Size = new System.Drawing.Size(429, 44);
            testsFromFile.TabIndex = 12;
            testsFromFile.Text = "Tests aus Datei";
            testsFromFile.UseVisualStyleBackColor = true;
            testsFromFile.Click += testsFromFile_Click;
            // 
            // Miscellaneous
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(467, 570);
            Controls.Add(testsFromFile);
            Controls.Add(generatorTests);
            Controls.Add(buttonDeleteTeamDeleteKeysForClubs);
            Controls.Add(buttonDeleteTeamDeleteDays);
            Controls.Add(buttonDeleteTeamDeleteWeeks);
            Controls.Add(buttonDeleteTeamDeleteKeysForTeams);
            Controls.Add(buttonDeleteTeamDeleteClubsWithoutTeams);
            Controls.Add(buttonDeleteTeamCancel);
            Controls.Add(buttonDeleteTeamLoadBackup);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "Miscellaneous";
            Text = "Sonstiges";
            FormClosed += Miscellaneous_FormClosed;
            Resize += Miscellaneous_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonDeleteTeamCancel;
        private System.Windows.Forms.Button buttonDeleteTeamLoadBackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDeleteTeamDeleteClubsWithoutTeams;
        private System.Windows.Forms.Button buttonDeleteTeamDeleteKeysForTeams;
        private System.Windows.Forms.Button buttonDeleteTeamDeleteWeeks;
        private System.Windows.Forms.Button buttonDeleteTeamDeleteDays;
        private System.Windows.Forms.Button buttonDeleteTeamDeleteKeysForClubs;
        private System.Windows.Forms.Button generatorTests;
        private System.Windows.Forms.Button testsFromFile;
    }
}