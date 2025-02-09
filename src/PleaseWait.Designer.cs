namespace KeyGenerator
{
    partial class PleaseWait
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
            components = new System.ComponentModel.Container();
            label1 = new System.Windows.Forms.Label();
            progressBar = new System.Windows.Forms.ProgressBar();
            buttonCancel = new System.Windows.Forms.Button();
            labelTime = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            labelNrOfConflicts = new System.Windows.Forms.Label();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            timer = new System.Windows.Forms.Timer(components);
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(22, 25);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(421, 25);
            label1.TabIndex = 0;
            label1.Text = "Schlüsselzahlen werden generiert. Verbleibende Zeit:";
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(27, 132);
            progressBar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(357, 44);
            progressBar.TabIndex = 1;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(393, 132);
            buttonCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(124, 44);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Abbrechen";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += cancel;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Location = new System.Drawing.Point(462, 25);
            labelTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTime.Name = "labelTime";
            labelTime.Size = new System.Drawing.Size(56, 25);
            labelTime.TabIndex = 3;
            labelTime.Text = "00:00";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(87, 66);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(365, 25);
            label3.TabIndex = 4;
            label3.Text = "Anzahl an Konflikten in der aktuellen Lösung:";
            // 
            // labelNrOfConflicts
            // 
            labelNrOfConflicts.AutoSize = true;
            labelNrOfConflicts.Location = new System.Drawing.Point(462, 66);
            labelNrOfConflicts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelNrOfConflicts.Name = "labelNrOfConflicts";
            labelNrOfConflicts.Size = new System.Drawing.Size(19, 25);
            labelNrOfConflicts.TabIndex = 5;
            labelNrOfConflicts.Text = "-";
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new System.Drawing.Point(277, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(8, 8);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // PleaseWait
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(533, 198);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(labelNrOfConflicts);
            Controls.Add(label3);
            Controls.Add(labelTime);
            Controls.Add(buttonCancel);
            Controls.Add(progressBar);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "PleaseWait";
            Text = "Bitte Warten";
            FormClosing += PleaseWait_FormClosing;
            Resize += PleaseWait_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelNrOfConflicts;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}