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
            buttonGenerate.Location = new System.Drawing.Point(27, 377);
            buttonGenerate.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new System.Drawing.Size(143, 43);
            buttonGenerate.TabIndex = 10;
            buttonGenerate.Text = "Generieren";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // buttonInputFromFile
            // 
            buttonInputFromFile.Location = new System.Drawing.Point(331, 465);
            buttonInputFromFile.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonInputFromFile.Name = "buttonInputFromFile";
            buttonInputFromFile.Size = new System.Drawing.Size(143, 43);
            buttonInputFromFile.TabIndex = 13;
            buttonInputFromFile.Text = "Aus Datei";
            buttonInputFromFile.UseVisualStyleBackColor = true;
            buttonInputFromFile.Click += buttonInputFromFile_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(23, 178);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(73, 25);
            label3.TabIndex = 19;
            label3.Text = "Vereine:";
            // 
            // boxClubs
            // 
            boxClubs.FormattingEnabled = true;
            boxClubs.Location = new System.Drawing.Point(20, 210);
            boxClubs.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxClubs.Name = "boxClubs";
            boxClubs.Size = new System.Drawing.Size(300, 33);
            boxClubs.TabIndex = 20;
            boxClubs.SelectedIndexChanged += boxClubs_SelectedIndexChanged;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new System.Drawing.Point(510, 103);
            dataGridView.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 62;
            dataGridView.Size = new System.Drawing.Size(867, 617);
            dataGridView.TabIndex = 23;
            dataGridView.CellMouseClick += dataGridView_CellMouseClick;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(23, 73);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(102, 25);
            label5.TabIndex = 24;
            label5.Text = "Verzeichnis:";
            // 
            // boxDirectory
            // 
            boxDirectory.Location = new System.Drawing.Point(23, 103);
            boxDirectory.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxDirectory.Name = "boxDirectory";
            boxDirectory.Size = new System.Drawing.Size(297, 31);
            boxDirectory.TabIndex = 25;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new System.Drawing.Point(331, 100);
            buttonBrowse.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new System.Drawing.Size(143, 43);
            buttonBrowse.TabIndex = 26;
            buttonBrowse.Text = "Durchsuchen";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // boxGroups
            // 
            boxGroups.FormattingEnabled = true;
            boxGroups.Location = new System.Drawing.Point(23, 303);
            boxGroups.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxGroups.Name = "boxGroups";
            boxGroups.Size = new System.Drawing.Size(297, 33);
            boxGroups.TabIndex = 28;
            boxGroups.SelectedIndexChanged += boxGroups_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 272);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(58, 25);
            label1.TabIndex = 27;
            label1.Text = "Ligen:";
            // 
            // buttonMiscellaneous
            // 
            buttonMiscellaneous.Location = new System.Drawing.Point(331, 377);
            buttonMiscellaneous.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonMiscellaneous.Name = "buttonMiscellaneous";
            buttonMiscellaneous.Size = new System.Drawing.Size(140, 43);
            buttonMiscellaneous.TabIndex = 33;
            buttonMiscellaneous.Text = "Sonstiges";
            buttonMiscellaneous.UseVisualStyleBackColor = true;
            buttonMiscellaneous.Click += buttonMiscellaneous_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new System.Drawing.Point(177, 377);
            buttonSave.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(143, 43);
            buttonSave.TabIndex = 38;
            buttonSave.Text = "Speichern";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // boxWeekA
            // 
            boxWeekA.Enabled = false;
            boxWeekA.FormattingEnabled = true;
            boxWeekA.Location = new System.Drawing.Point(550, 53);
            boxWeekA.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxWeekA.Name = "boxWeekA";
            boxWeekA.Size = new System.Drawing.Size(81, 33);
            boxWeekA.TabIndex = 42;
            boxWeekA.SelectedIndexChanged += boxWeekA_SelectedIndexChanged;
            // 
            // boxWeekB
            // 
            boxWeekB.Enabled = false;
            boxWeekB.FormattingEnabled = true;
            boxWeekB.Location = new System.Drawing.Point(683, 53);
            boxWeekB.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxWeekB.Name = "boxWeekB";
            boxWeekB.Size = new System.Drawing.Size(81, 33);
            boxWeekB.TabIndex = 43;
            boxWeekB.SelectedIndexChanged += boxWeekB_SelectedIndexChanged;
            // 
            // boxWeekY
            // 
            boxWeekY.Enabled = false;
            boxWeekY.FormattingEnabled = true;
            boxWeekY.Location = new System.Drawing.Point(950, 53);
            boxWeekY.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxWeekY.Name = "boxWeekY";
            boxWeekY.Size = new System.Drawing.Size(81, 33);
            boxWeekY.TabIndex = 49;
            boxWeekY.SelectedIndexChanged += boxWeekY_SelectedIndexChanged;
            // 
            // boxWeekX
            // 
            boxWeekX.Enabled = false;
            boxWeekX.FormattingEnabled = true;
            boxWeekX.Location = new System.Drawing.Point(817, 53);
            boxWeekX.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxWeekX.Name = "boxWeekX";
            boxWeekX.Size = new System.Drawing.Size(81, 33);
            boxWeekX.TabIndex = 48;
            boxWeekX.SelectedIndexChanged += boxWeekX_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(917, 57);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(26, 25);
            label8.TabIndex = 50;
            label8.Text = "Y:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(783, 57);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(27, 25);
            label9.TabIndex = 51;
            label9.Text = "X:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(650, 57);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(26, 25);
            label11.TabIndex = 52;
            label11.Text = "B:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(517, 57);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(28, 25);
            label12.TabIndex = 53;
            label12.Text = "A:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(331, 272);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(45, 25);
            label13.TabIndex = 54;
            label13.Text = "Feld";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(80, 535);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(233, 25);
            label7.TabIndex = 56;
            label7.Text = "Referenzfeld für Woche A/B:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(80, 587);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(232, 25);
            label10.TabIndex = 57;
            label10.Text = "Referenzfeld für Woche X/Y:";
            // 
            // boxFieldAB
            // 
            boxFieldAB.FormattingEnabled = true;
            boxFieldAB.Location = new System.Drawing.Point(331, 528);
            boxFieldAB.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxFieldAB.Name = "boxFieldAB";
            boxFieldAB.Size = new System.Drawing.Size(137, 33);
            boxFieldAB.TabIndex = 58;
            boxFieldAB.SelectedIndexChanged += boxFieldAB_SelectedIndexChanged;
            // 
            // boxFieldXY
            // 
            boxFieldXY.FormattingEnabled = true;
            boxFieldXY.Location = new System.Drawing.Point(331, 582);
            boxFieldXY.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxFieldXY.Name = "boxFieldXY";
            boxFieldXY.Size = new System.Drawing.Size(137, 33);
            boxFieldXY.TabIndex = 59;
            boxFieldXY.SelectedIndexChanged += boxFieldXY_SelectedIndexChanged;
            // 
            // boxCapacity
            // 
            boxCapacity.AutoSize = true;
            boxCapacity.Location = new System.Drawing.Point(331, 213);
            boxCapacity.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxCapacity.Name = "boxCapacity";
            boxCapacity.Size = new System.Drawing.Size(171, 29);
            boxCapacity.TabIndex = 60;
            boxCapacity.Text = "Kapazitätsgrenze";
            boxCapacity.UseVisualStyleBackColor = true;
            boxCapacity.CheckedChanged += boxCapacity_CheckedChanged;
            // 
            // buttonClubView
            // 
            buttonClubView.AutoSize = true;
            buttonClubView.Enabled = false;
            buttonClubView.Location = new System.Drawing.Point(27, 638);
            buttonClubView.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonClubView.Name = "buttonClubView";
            buttonClubView.Size = new System.Drawing.Size(129, 29);
            buttonClubView.TabIndex = 62;
            buttonClubView.Text = "Vereinssicht";
            buttonClubView.UseVisualStyleBackColor = true;
            buttonClubView.CheckedChanged += buttonClubView_CheckedChanged;
            // 
            // buttonGroupView
            // 
            buttonGroupView.AutoSize = true;
            buttonGroupView.Enabled = false;
            buttonGroupView.Location = new System.Drawing.Point(177, 638);
            buttonGroupView.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonGroupView.Name = "buttonGroupView";
            buttonGroupView.Size = new System.Drawing.Size(122, 29);
            buttonGroupView.TabIndex = 63;
            buttonGroupView.Text = "Staffelsicht";
            buttonGroupView.UseVisualStyleBackColor = true;
            buttonGroupView.CheckedChanged += buttonGroupView_CheckedChanged;
            // 
            // buttonManualInput
            // 
            buttonManualInput.Enabled = false;
            buttonManualInput.Location = new System.Drawing.Point(27, 465);
            buttonManualInput.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonManualInput.Name = "buttonManualInput";
            buttonManualInput.Size = new System.Drawing.Size(143, 43);
            buttonManualInput.TabIndex = 65;
            buttonManualInput.Text = "Manuell";
            buttonManualInput.UseVisualStyleBackColor = true;
            buttonManualInput.Click += buttonManualInput_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(20, 435);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(117, 25);
            label14.TabIndex = 66;
            label14.Text = "Datenimport:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(24, 693);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(156, 25);
            label15.TabIndex = 67;
            label15.Text = "Maximale Laufzeit:";
            // 
            // boxRuntimeMinutes
            // 
            boxRuntimeMinutes.Location = new System.Drawing.Point(177, 688);
            boxRuntimeMinutes.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxRuntimeMinutes.Name = "boxRuntimeMinutes";
            boxRuntimeMinutes.Size = new System.Drawing.Size(87, 31);
            boxRuntimeMinutes.TabIndex = 73;
            boxRuntimeMinutes.TextChanged += boxRuntimeMinutes_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(277, 693);
            label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(46, 25);
            label16.TabIndex = 74;
            label16.Text = "Min.";
            // 
            // boxRuntimeSeconds
            // 
            boxRuntimeSeconds.Location = new System.Drawing.Point(331, 688);
            boxRuntimeSeconds.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            boxRuntimeSeconds.Name = "boxRuntimeSeconds";
            boxRuntimeSeconds.Size = new System.Drawing.Size(80, 31);
            boxRuntimeSeconds.TabIndex = 75;
            boxRuntimeSeconds.TextChanged += boxRuntimeSeconds_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(423, 693);
            label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(44, 25);
            label17.TabIndex = 76;
            label17.Text = "Sek.";
            // 
            // buttonPartner
            // 
            buttonPartner.Enabled = false;
            buttonPartner.Location = new System.Drawing.Point(1064, 48);
            buttonPartner.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            buttonPartner.Name = "buttonPartner";
            buttonPartner.Size = new System.Drawing.Size(143, 43);
            buttonPartner.TabIndex = 77;
            buttonPartner.Text = "Partner";
            buttonPartner.UseVisualStyleBackColor = true;
            buttonPartner.Click += buttonPartner_Click;
            // 
            // buttonClickTTInput
            // 
            buttonClickTTInput.Location = new System.Drawing.Point(181, 465);
            buttonClickTTInput.Name = "buttonClickTTInput";
            buttonClickTTInput.Size = new System.Drawing.Size(143, 43);
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
            boxFields.Location = new System.Drawing.Point(331, 303);
            boxFields.Name = "boxFields";
            boxFields.Size = new System.Drawing.Size(135, 33);
            boxFields.TabIndex = 81;
            boxFields.SelectedIndexChanged += boxFields_SelectedIndexChanged;
            // 
            // KeyGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1401, 748);
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
            Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
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

