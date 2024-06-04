namespace KeyGenerator
{
    partial class KeyGenerator
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonInputFromFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.boxClubs = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.boxDirectory = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.boxGroups = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMiscellaneous = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.boxWeekA = new System.Windows.Forms.ComboBox();
            this.boxWeekB = new System.Windows.Forms.ComboBox();
            this.boxWeekY = new System.Windows.Forms.ComboBox();
            this.boxWeekX = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.boxFieldAB = new System.Windows.Forms.ComboBox();
            this.boxFieldXY = new System.Windows.Forms.ComboBox();
            this.boxCapacity = new System.Windows.Forms.CheckBox();
            this.buttonClubView = new System.Windows.Forms.RadioButton();
            this.buttonGroupView = new System.Windows.Forms.RadioButton();
            this.buttonManualInput = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.boxRuntimeMinutes = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.boxRuntimeSeconds = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonPartner = new System.Windows.Forms.Button();
            this.buttonClickTTInput = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.boxFields = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(24, 301);
            this.buttonGenerate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(128, 35);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generieren";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonInputFromFile
            // 
            this.buttonInputFromFile.Location = new System.Drawing.Point(298, 372);
            this.buttonInputFromFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonInputFromFile.Name = "buttonInputFromFile";
            this.buttonInputFromFile.Size = new System.Drawing.Size(128, 35);
            this.buttonInputFromFile.TabIndex = 13;
            this.buttonInputFromFile.Text = "Aus Datei";
            this.buttonInputFromFile.UseVisualStyleBackColor = true;
            this.buttonInputFromFile.Click += new System.EventHandler(this.buttonInputFromFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Vereine:";
            // 
            // boxClubs
            // 
            this.boxClubs.FormattingEnabled = true;
            this.boxClubs.Location = new System.Drawing.Point(18, 168);
            this.boxClubs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxClubs.Name = "boxClubs";
            this.boxClubs.Size = new System.Drawing.Size(270, 28);
            this.boxClubs.TabIndex = 20;
            this.boxClubs.SelectedIndexChanged += new System.EventHandler(this.boxClubs_SelectedIndexChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(459, 83);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.Size = new System.Drawing.Size(780, 494);
            this.dataGridView.TabIndex = 23;
            this.dataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Verzeichnis:";
            // 
            // boxDirectory
            // 
            this.boxDirectory.Location = new System.Drawing.Point(20, 83);
            this.boxDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxDirectory.Name = "boxDirectory";
            this.boxDirectory.Size = new System.Drawing.Size(268, 26);
            this.boxDirectory.TabIndex = 25;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(298, 80);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(128, 35);
            this.buttonBrowse.TabIndex = 26;
            this.buttonBrowse.Text = "Durchsuchen";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // boxGroups
            // 
            this.boxGroups.FormattingEnabled = true;
            this.boxGroups.Location = new System.Drawing.Point(20, 243);
            this.boxGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxGroups.Name = "boxGroups";
            this.boxGroups.Size = new System.Drawing.Size(268, 28);
            this.boxGroups.TabIndex = 28;
            this.boxGroups.SelectedIndexChanged += new System.EventHandler(this.boxGroups_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 217);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Ligen:";
            // 
            // buttonMiscellaneous
            // 
            this.buttonMiscellaneous.Location = new System.Drawing.Point(298, 301);
            this.buttonMiscellaneous.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMiscellaneous.Name = "buttonMiscellaneous";
            this.buttonMiscellaneous.Size = new System.Drawing.Size(126, 35);
            this.buttonMiscellaneous.TabIndex = 33;
            this.buttonMiscellaneous.Text = "Sonstiges";
            this.buttonMiscellaneous.UseVisualStyleBackColor = true;
            this.buttonMiscellaneous.Click += new System.EventHandler(this.buttonMiscellaneous_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(160, 301);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(129, 35);
            this.buttonSave.TabIndex = 38;
            this.buttonSave.Text = "Speichern";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // boxWeekA
            // 
            this.boxWeekA.Enabled = false;
            this.boxWeekA.FormattingEnabled = true;
            this.boxWeekA.Location = new System.Drawing.Point(495, 42);
            this.boxWeekA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxWeekA.Name = "boxWeekA";
            this.boxWeekA.Size = new System.Drawing.Size(73, 28);
            this.boxWeekA.TabIndex = 42;
            this.boxWeekA.SelectedIndexChanged += new System.EventHandler(this.boxWeekA_SelectedIndexChanged);
            // 
            // boxWeekB
            // 
            this.boxWeekB.Enabled = false;
            this.boxWeekB.FormattingEnabled = true;
            this.boxWeekB.Location = new System.Drawing.Point(615, 42);
            this.boxWeekB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxWeekB.Name = "boxWeekB";
            this.boxWeekB.Size = new System.Drawing.Size(73, 28);
            this.boxWeekB.TabIndex = 43;
            this.boxWeekB.SelectedIndexChanged += new System.EventHandler(this.boxWeekB_SelectedIndexChanged);
            // 
            // boxWeekY
            // 
            this.boxWeekY.Enabled = false;
            this.boxWeekY.FormattingEnabled = true;
            this.boxWeekY.Location = new System.Drawing.Point(855, 42);
            this.boxWeekY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxWeekY.Name = "boxWeekY";
            this.boxWeekY.Size = new System.Drawing.Size(73, 28);
            this.boxWeekY.TabIndex = 49;
            this.boxWeekY.SelectedIndexChanged += new System.EventHandler(this.boxWeekY_SelectedIndexChanged);
            // 
            // boxWeekX
            // 
            this.boxWeekX.Enabled = false;
            this.boxWeekX.FormattingEnabled = true;
            this.boxWeekX.Location = new System.Drawing.Point(735, 42);
            this.boxWeekX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxWeekX.Name = "boxWeekX";
            this.boxWeekX.Size = new System.Drawing.Size(73, 28);
            this.boxWeekX.TabIndex = 48;
            this.boxWeekX.SelectedIndexChanged += new System.EventHandler(this.boxWeekX_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(825, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 20);
            this.label8.TabIndex = 50;
            this.label8.Text = "Y:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(705, 46);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 20);
            this.label9.TabIndex = 51;
            this.label9.Text = "X:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(585, 46);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 20);
            this.label11.TabIndex = 52;
            this.label11.Text = "B:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(465, 46);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 20);
            this.label12.TabIndex = 53;
            this.label12.Text = "A:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(298, 217);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 20);
            this.label13.TabIndex = 54;
            this.label13.Text = "Feld";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 428);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 20);
            this.label7.TabIndex = 56;
            this.label7.Text = "Referenzfeld für Woche A/B:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 469);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(212, 20);
            this.label10.TabIndex = 57;
            this.label10.Text = "Referenzfeld für Woche X/Y:";
            // 
            // boxFieldAB
            // 
            this.boxFieldAB.FormattingEnabled = true;
            this.boxFieldAB.Location = new System.Drawing.Point(298, 423);
            this.boxFieldAB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxFieldAB.Name = "boxFieldAB";
            this.boxFieldAB.Size = new System.Drawing.Size(124, 28);
            this.boxFieldAB.TabIndex = 58;
            this.boxFieldAB.SelectedIndexChanged += new System.EventHandler(this.boxFieldAB_SelectedIndexChanged);
            // 
            // boxFieldXY
            // 
            this.boxFieldXY.FormattingEnabled = true;
            this.boxFieldXY.Location = new System.Drawing.Point(298, 465);
            this.boxFieldXY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxFieldXY.Name = "boxFieldXY";
            this.boxFieldXY.Size = new System.Drawing.Size(124, 28);
            this.boxFieldXY.TabIndex = 59;
            this.boxFieldXY.SelectedIndexChanged += new System.EventHandler(this.boxFieldXY_SelectedIndexChanged);
            // 
            // boxCapacity
            // 
            this.boxCapacity.AutoSize = true;
            this.boxCapacity.Location = new System.Drawing.Point(298, 171);
            this.boxCapacity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxCapacity.Name = "boxCapacity";
            this.boxCapacity.Size = new System.Drawing.Size(159, 24);
            this.boxCapacity.TabIndex = 60;
            this.boxCapacity.Text = "Kapazitätsgrenze";
            this.boxCapacity.UseVisualStyleBackColor = true;
            this.boxCapacity.CheckedChanged += new System.EventHandler(this.boxCapacity_CheckedChanged);
            // 
            // buttonClubView
            // 
            this.buttonClubView.AutoSize = true;
            this.buttonClubView.Enabled = false;
            this.buttonClubView.Location = new System.Drawing.Point(24, 511);
            this.buttonClubView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClubView.Name = "buttonClubView";
            this.buttonClubView.Size = new System.Drawing.Size(121, 24);
            this.buttonClubView.TabIndex = 62;
            this.buttonClubView.Text = "Vereinssicht";
            this.buttonClubView.UseVisualStyleBackColor = true;
            this.buttonClubView.CheckedChanged += new System.EventHandler(this.buttonClubView_CheckedChanged);
            // 
            // buttonGroupView
            // 
            this.buttonGroupView.AutoSize = true;
            this.buttonGroupView.Enabled = false;
            this.buttonGroupView.Location = new System.Drawing.Point(160, 511);
            this.buttonGroupView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGroupView.Name = "buttonGroupView";
            this.buttonGroupView.Size = new System.Drawing.Size(114, 24);
            this.buttonGroupView.TabIndex = 63;
            this.buttonGroupView.Text = "Staffelsicht";
            this.buttonGroupView.UseVisualStyleBackColor = true;
            this.buttonGroupView.CheckedChanged += new System.EventHandler(this.buttonGroupView_CheckedChanged);
            // 
            // buttonManualInput
            // 
            this.buttonManualInput.Enabled = false;
            this.buttonManualInput.Location = new System.Drawing.Point(24, 372);
            this.buttonManualInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonManualInput.Name = "buttonManualInput";
            this.buttonManualInput.Size = new System.Drawing.Size(128, 35);
            this.buttonManualInput.TabIndex = 65;
            this.buttonManualInput.Text = "Manuell";
            this.buttonManualInput.UseVisualStyleBackColor = true;
            this.buttonManualInput.Click += new System.EventHandler(this.buttonManualInput_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 348);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 20);
            this.label14.TabIndex = 66;
            this.label14.Text = "Datenimport:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 555);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 20);
            this.label15.TabIndex = 67;
            this.label15.Text = "Maximale Laufzeit:";
            // 
            // boxRuntimeMinutes
            // 
            this.boxRuntimeMinutes.Location = new System.Drawing.Point(160, 551);
            this.boxRuntimeMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxRuntimeMinutes.Name = "boxRuntimeMinutes";
            this.boxRuntimeMinutes.Size = new System.Drawing.Size(78, 26);
            this.boxRuntimeMinutes.TabIndex = 73;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(249, 555);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 20);
            this.label16.TabIndex = 74;
            this.label16.Text = "Min.";
            // 
            // boxRuntimeSeconds
            // 
            this.boxRuntimeSeconds.Location = new System.Drawing.Point(298, 551);
            this.boxRuntimeSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boxRuntimeSeconds.Name = "boxRuntimeSeconds";
            this.boxRuntimeSeconds.Size = new System.Drawing.Size(72, 26);
            this.boxRuntimeSeconds.TabIndex = 75;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(380, 555);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 20);
            this.label17.TabIndex = 76;
            this.label17.Text = "Sek.";
            // 
            // buttonPartner
            // 
            this.buttonPartner.Enabled = false;
            this.buttonPartner.Location = new System.Drawing.Point(958, 39);
            this.buttonPartner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPartner.Name = "buttonPartner";
            this.buttonPartner.Size = new System.Drawing.Size(128, 35);
            this.buttonPartner.TabIndex = 77;
            this.buttonPartner.Text = "Partner";
            this.buttonPartner.UseVisualStyleBackColor = true;
            this.buttonPartner.Click += new System.EventHandler(this.buttonPartner_Click);
            // 
            // buttonClickTTInput
            // 
            this.buttonClickTTInput.Location = new System.Drawing.Point(163, 372);
            this.buttonClickTTInput.Name = "buttonClickTTInput";
            this.buttonClickTTInput.Size = new System.Drawing.Size(128, 35);
            this.buttonClickTTInput.TabIndex = 80;
            this.buttonClickTTInput.Text = "Aus Click-TT";
            this.buttonClickTTInput.UseVisualStyleBackColor = true;
            this.buttonClickTTInput.Click += new System.EventHandler(this.buttonClickTTInput_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // boxFields
            // 
            this.boxFields.FormattingEnabled = true;
            this.boxFields.Location = new System.Drawing.Point(298, 243);
            this.boxFields.Name = "boxFields";
            this.boxFields.Size = new System.Drawing.Size(122, 28);
            this.boxFields.TabIndex = 81;
            this.boxFields.SelectedIndexChanged += new System.EventHandler(this.boxFields_SelectedIndexChanged);
            // 
            // Schluesselzahlen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 599);
            this.Controls.Add(this.boxFields);
            this.Controls.Add(this.buttonClickTTInput);
            this.Controls.Add(this.buttonPartner);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.boxRuntimeSeconds);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.boxRuntimeMinutes);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonManualInput);
            this.Controls.Add(this.buttonGroupView);
            this.Controls.Add(this.buttonClubView);
            this.Controls.Add(this.boxCapacity);
            this.Controls.Add(this.boxFieldXY);
            this.Controls.Add(this.boxFieldAB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.boxWeekY);
            this.Controls.Add(this.boxWeekX);
            this.Controls.Add(this.boxWeekB);
            this.Controls.Add(this.boxWeekA);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonMiscellaneous);
            this.Controls.Add(this.boxGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.boxDirectory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.boxClubs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonInputFromFile);
            this.Controls.Add(this.buttonGenerate);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Schluesselzahlen";
            this.Text = "Schlüsselzahlen generieren";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Schluesselzahlen_FormClosing);
            this.Resize += new System.EventHandler(this.Schluesselzahlen_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonGenerate;
        public System.Windows.Forms.Button buttonInputFromFile;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox boxClubs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox boxDirectory;
        private System.Windows.Forms.Button buttonBrowse;
        public System.Windows.Forms.ComboBox boxGroups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMiscellaneous;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox boxWeekA;
        private System.Windows.Forms.ComboBox boxWeekB;
        private System.Windows.Forms.ComboBox boxWeekY;
        private System.Windows.Forms.ComboBox boxWeekX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox boxFieldAB;
        private System.Windows.Forms.ComboBox boxFieldXY;
        private System.Windows.Forms.CheckBox boxCapacity;
        public System.Windows.Forms.RadioButton buttonGroupView;
        public System.Windows.Forms.RadioButton buttonClubView;
        private System.Windows.Forms.Button buttonManualInput;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox boxRuntimeMinutes;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox boxRuntimeSeconds;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonPartner;
        private System.Windows.Forms.Button buttonClickTTInput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox boxFields;
    }
}

