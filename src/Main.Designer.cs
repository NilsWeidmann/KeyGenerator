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
            buttonGenerate = new System.Windows.Forms.Button();
            buttonInputFromFile = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            boxClubs = new System.Windows.Forms.ComboBox();
            dataGridView = new System.Windows.Forms.DataGridView();
            label5 = new System.Windows.Forms.Label();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            boxDirectory = new System.Windows.Forms.TextBox();
            buttonBrowse = new System.Windows.Forms.Button();
            boxGroups = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            buttonMiscellaneous = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            boxWeekA = new System.Windows.Forms.ComboBox();
            boxWeekB = new System.Windows.Forms.ComboBox();
            boxWeekY = new System.Windows.Forms.ComboBox();
            boxWeekX = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            boxFieldAB = new System.Windows.Forms.ComboBox();
            boxFieldXY = new System.Windows.Forms.ComboBox();
            boxCapacity = new System.Windows.Forms.CheckBox();
            buttonClubView = new System.Windows.Forms.RadioButton();
            buttonGroupView = new System.Windows.Forms.RadioButton();
            buttonManualInput = new System.Windows.Forms.Button();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            boxRuntimeMinutes = new System.Windows.Forms.TextBox();
            label16 = new System.Windows.Forms.Label();
            boxRuntimeSeconds = new System.Windows.Forms.TextBox();
            label17 = new System.Windows.Forms.Label();
            buttonPartner = new System.Windows.Forms.Button();
            buttonClickTTInput = new System.Windows.Forms.Button();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            boxFields = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new System.Drawing.Point(15, 169);
            buttonGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new System.Drawing.Size(100, 26);
            buttonGenerate.TabIndex = 10;
            buttonGenerate.Text = "Generieren";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // buttonInputFromFile
            // 
            buttonInputFromFile.Location = new System.Drawing.Point(228, 222);
            buttonInputFromFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonInputFromFile.Name = "buttonInputFromFile";
            buttonInputFromFile.Size = new System.Drawing.Size(100, 26);
            buttonInputFromFile.TabIndex = 13;
            buttonInputFromFile.Text = "Aus Datei";
            buttonInputFromFile.UseVisualStyleBackColor = true;
            buttonInputFromFile.Click += buttonInputFromFile_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 50);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(48, 15);
            label3.TabIndex = 19;
            label3.Text = "Vereine:";
            // 
            // boxClubs
            // 
            boxClubs.FormattingEnabled = true;
            boxClubs.Location = new System.Drawing.Point(12, 66);
            boxClubs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxClubs.Name = "boxClubs";
            boxClubs.Size = new System.Drawing.Size(211, 23);
            boxClubs.TabIndex = 20;
            boxClubs.SelectedIndexChanged += boxClubs_SelectedIndexChanged;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new System.Drawing.Point(353, 106);
            dataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 62;
            dataGridView.Size = new System.Drawing.Size(607, 269);
            dataGridView.TabIndex = 23;
            dataGridView.CellMouseClick += dataGridView_CellMouseClick;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(15, 8);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(68, 15);
            label5.TabIndex = 24;
            label5.Text = "Verzeichnis:";
            // 
            // boxDirectory
            // 
            boxDirectory.Location = new System.Drawing.Point(12, 27);
            boxDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxDirectory.Name = "boxDirectory";
            boxDirectory.Size = new System.Drawing.Size(731, 23);
            boxDirectory.TabIndex = 25;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new System.Drawing.Point(748, 23);
            buttonBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new System.Drawing.Size(100, 26);
            buttonBrowse.TabIndex = 26;
            buttonBrowse.Text = "Durchsuchen";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // boxGroups
            // 
            boxGroups.FormattingEnabled = true;
            boxGroups.Location = new System.Drawing.Point(12, 125);
            boxGroups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxGroups.Name = "boxGroups";
            boxGroups.Size = new System.Drawing.Size(209, 23);
            boxGroups.TabIndex = 28;
            boxGroups.SelectedIndexChanged += boxGroups_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 106);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 15);
            label1.TabIndex = 27;
            label1.Text = "Ligen:";
            // 
            // buttonMiscellaneous
            // 
            buttonMiscellaneous.Location = new System.Drawing.Point(228, 169);
            buttonMiscellaneous.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonMiscellaneous.Name = "buttonMiscellaneous";
            buttonMiscellaneous.Size = new System.Drawing.Size(98, 26);
            buttonMiscellaneous.TabIndex = 33;
            buttonMiscellaneous.Text = "Sonstiges";
            buttonMiscellaneous.UseVisualStyleBackColor = true;
            buttonMiscellaneous.Click += buttonMiscellaneous_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new System.Drawing.Point(120, 169);
            buttonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(100, 26);
            buttonSave.TabIndex = 38;
            buttonSave.Text = "Speichern";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // boxWeekA
            // 
            boxWeekA.Enabled = false;
            boxWeekA.FormattingEnabled = true;
            boxWeekA.Location = new System.Drawing.Point(388, 68);
            boxWeekA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxWeekA.Name = "boxWeekA";
            boxWeekA.Size = new System.Drawing.Size(58, 23);
            boxWeekA.TabIndex = 42;
            boxWeekA.SelectedIndexChanged += boxWeekA_SelectedIndexChanged;
            // 
            // boxWeekB
            // 
            boxWeekB.Enabled = false;
            boxWeekB.FormattingEnabled = true;
            boxWeekB.Location = new System.Drawing.Point(481, 68);
            boxWeekB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxWeekB.Name = "boxWeekB";
            boxWeekB.Size = new System.Drawing.Size(58, 23);
            boxWeekB.TabIndex = 43;
            boxWeekB.SelectedIndexChanged += boxWeekB_SelectedIndexChanged;
            // 
            // boxWeekY
            // 
            boxWeekY.Enabled = false;
            boxWeekY.FormattingEnabled = true;
            boxWeekY.Location = new System.Drawing.Point(668, 68);
            boxWeekY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxWeekY.Name = "boxWeekY";
            boxWeekY.Size = new System.Drawing.Size(58, 23);
            boxWeekY.TabIndex = 49;
            boxWeekY.SelectedIndexChanged += boxWeekY_SelectedIndexChanged;
            // 
            // boxWeekX
            // 
            boxWeekX.Enabled = false;
            boxWeekX.FormattingEnabled = true;
            boxWeekX.Location = new System.Drawing.Point(575, 68);
            boxWeekX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxWeekX.Name = "boxWeekX";
            boxWeekX.Size = new System.Drawing.Size(58, 23);
            boxWeekX.TabIndex = 48;
            boxWeekX.SelectedIndexChanged += boxWeekX_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(645, 71);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(17, 15);
            label8.TabIndex = 50;
            label8.Text = "Y:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(551, 71);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(17, 15);
            label9.TabIndex = 51;
            label9.Text = "X:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(458, 71);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(17, 15);
            label11.TabIndex = 52;
            label11.Text = "B:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(365, 71);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(18, 15);
            label12.TabIndex = 53;
            label12.Text = "A:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(228, 106);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(29, 15);
            label13.TabIndex = 54;
            label13.Text = "Feld";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(52, 264);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(156, 15);
            label7.TabIndex = 56;
            label7.Text = "Referenzfeld für Woche A/B:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(52, 295);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(155, 15);
            label10.TabIndex = 57;
            label10.Text = "Referenzfeld für Woche X/Y:";
            // 
            // boxFieldAB
            // 
            boxFieldAB.FormattingEnabled = true;
            boxFieldAB.Location = new System.Drawing.Point(228, 260);
            boxFieldAB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxFieldAB.Name = "boxFieldAB";
            boxFieldAB.Size = new System.Drawing.Size(97, 23);
            boxFieldAB.TabIndex = 58;
            boxFieldAB.SelectedIndexChanged += boxFieldAB_SelectedIndexChanged;
            // 
            // boxFieldXY
            // 
            boxFieldXY.FormattingEnabled = true;
            boxFieldXY.Location = new System.Drawing.Point(228, 292);
            boxFieldXY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxFieldXY.Name = "boxFieldXY";
            boxFieldXY.Size = new System.Drawing.Size(97, 23);
            boxFieldXY.TabIndex = 59;
            boxFieldXY.SelectedIndexChanged += boxFieldXY_SelectedIndexChanged;
            // 
            // boxCapacity
            // 
            boxCapacity.AutoSize = true;
            boxCapacity.Location = new System.Drawing.Point(228, 66);
            boxCapacity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxCapacity.Name = "boxCapacity";
            boxCapacity.Size = new System.Drawing.Size(114, 19);
            boxCapacity.TabIndex = 60;
            boxCapacity.Text = "Kapazitätsgrenze";
            boxCapacity.UseVisualStyleBackColor = true;
            boxCapacity.CheckedChanged += boxCapacity_CheckedChanged;
            // 
            // buttonClubView
            // 
            buttonClubView.AutoSize = true;
            buttonClubView.Enabled = false;
            buttonClubView.Location = new System.Drawing.Point(15, 326);
            buttonClubView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonClubView.Name = "buttonClubView";
            buttonClubView.Size = new System.Drawing.Size(87, 19);
            buttonClubView.TabIndex = 62;
            buttonClubView.Text = "Vereinssicht";
            buttonClubView.UseVisualStyleBackColor = true;
            buttonClubView.CheckedChanged += buttonClubView_CheckedChanged;
            // 
            // buttonGroupView
            // 
            buttonGroupView.AutoSize = true;
            buttonGroupView.Enabled = false;
            buttonGroupView.Location = new System.Drawing.Point(120, 326);
            buttonGroupView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonGroupView.Name = "buttonGroupView";
            buttonGroupView.Size = new System.Drawing.Size(83, 19);
            buttonGroupView.TabIndex = 63;
            buttonGroupView.Text = "Staffelsicht";
            buttonGroupView.UseVisualStyleBackColor = true;
            buttonGroupView.CheckedChanged += buttonGroupView_CheckedChanged;
            // 
            // buttonManualInput
            // 
            buttonManualInput.Enabled = false;
            buttonManualInput.Location = new System.Drawing.Point(15, 222);
            buttonManualInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonManualInput.Name = "buttonManualInput";
            buttonManualInput.Size = new System.Drawing.Size(100, 26);
            buttonManualInput.TabIndex = 65;
            buttonManualInput.Text = "Manuell";
            buttonManualInput.UseVisualStyleBackColor = true;
            buttonManualInput.Click += buttonManualInput_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(10, 204);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(77, 15);
            label14.TabIndex = 66;
            label14.Text = "Datenimport:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(13, 359);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(105, 15);
            label15.TabIndex = 67;
            label15.Text = "Maximale Laufzeit:";
            // 
            // boxRuntimeMinutes
            // 
            boxRuntimeMinutes.Location = new System.Drawing.Point(120, 356);
            boxRuntimeMinutes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxRuntimeMinutes.Name = "boxRuntimeMinutes";
            boxRuntimeMinutes.Size = new System.Drawing.Size(62, 23);
            boxRuntimeMinutes.TabIndex = 73;
            boxRuntimeMinutes.TextChanged += boxRuntimeMinutes_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(190, 359);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(31, 15);
            label16.TabIndex = 74;
            label16.Text = "Min.";
            // 
            // boxRuntimeSeconds
            // 
            boxRuntimeSeconds.Location = new System.Drawing.Point(228, 356);
            boxRuntimeSeconds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            boxRuntimeSeconds.Name = "boxRuntimeSeconds";
            boxRuntimeSeconds.Size = new System.Drawing.Size(57, 23);
            boxRuntimeSeconds.TabIndex = 75;
            boxRuntimeSeconds.TextChanged += boxRuntimeSeconds_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(292, 359);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(28, 15);
            label17.TabIndex = 76;
            label17.Text = "Sek.";
            // 
            // buttonPartner
            // 
            buttonPartner.Enabled = false;
            buttonPartner.Location = new System.Drawing.Point(748, 65);
            buttonPartner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonPartner.Name = "buttonPartner";
            buttonPartner.Size = new System.Drawing.Size(100, 26);
            buttonPartner.TabIndex = 77;
            buttonPartner.Text = "Partner";
            buttonPartner.UseVisualStyleBackColor = true;
            buttonPartner.Click += buttonPartner_Click;
            // 
            // buttonClickTTInput
            // 
            buttonClickTTInput.Location = new System.Drawing.Point(122, 222);
            buttonClickTTInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            buttonClickTTInput.Name = "buttonClickTTInput";
            buttonClickTTInput.Size = new System.Drawing.Size(100, 26);
            buttonClickTTInput.TabIndex = 80;
            buttonClickTTInput.Text = "Aus Click-TT";
            buttonClickTTInput.UseVisualStyleBackColor = true;
            buttonClickTTInput.Click += buttonClickTTInput_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // boxFields
            // 
            boxFields.FormattingEnabled = true;
            boxFields.Location = new System.Drawing.Point(228, 125);
            boxFields.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            boxFields.Name = "boxFields";
            boxFields.Size = new System.Drawing.Size(96, 23);
            boxFields.TabIndex = 81;
            boxFields.SelectedIndexChanged += boxFields_SelectedIndexChanged;
            // 
            // KeyGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(981, 391);
            Controls.Add(boxFields);
            Controls.Add(buttonClickTTInput);
            Controls.Add(buttonPartner);
            Controls.Add(label17);
            Controls.Add(boxRuntimeSeconds);
            Controls.Add(label16);
            Controls.Add(boxRuntimeMinutes);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(buttonManualInput);
            Controls.Add(buttonGroupView);
            Controls.Add(buttonClubView);
            Controls.Add(boxCapacity);
            Controls.Add(boxFieldXY);
            Controls.Add(boxFieldAB);
            Controls.Add(label10);
            Controls.Add(label7);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(boxWeekY);
            Controls.Add(boxWeekX);
            Controls.Add(boxWeekB);
            Controls.Add(boxWeekA);
            Controls.Add(buttonSave);
            Controls.Add(buttonMiscellaneous);
            Controls.Add(boxGroups);
            Controls.Add(label1);
            Controls.Add(buttonBrowse);
            Controls.Add(boxDirectory);
            Controls.Add(label5);
            Controls.Add(dataGridView);
            Controls.Add(boxClubs);
            Controls.Add(label3);
            Controls.Add(buttonInputFromFile);
            Controls.Add(buttonGenerate);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "KeyGenerator";
            Text = "Schlüsselzahlen generieren";
            FormClosing += Schluesselzahlen_FormClosing;
            Resize += Schluesselzahlen_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public System.Windows.Forms.Button buttonGenerate;
        public System.Windows.Forms.Button buttonInputFromFile;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox boxClubs;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
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
        public System.Windows.Forms.TextBox boxDirectory;
    }
}

