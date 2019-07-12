namespace BloodSugarAnalyser.GUI
{
    partial class AnalyserForm
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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.clarityLogFile = new System.Windows.Forms.OpenFileDialog();
            this.nudInclusiveGlucoseTopLimit = new System.Windows.Forms.NumericUpDown();
            this.lblTopGlucoseLimit = new System.Windows.Forms.Label();
            this.tbxClarityLogFile = new System.Windows.Forms.TextBox();
            this.btnAnalyseFile = new System.Windows.Forms.Button();
            this.rtbAnalyseResult = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(606, 90);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(129, 32);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // clarityLogFile
            // 
            this.clarityLogFile.Filter = "Clarity Export Files (*.csv)|*.csv";
            this.clarityLogFile.Title = "Open Clarity Export File";
            // 
            // nudInclusiveGlucoseTopLimit
            // 
            this.nudInclusiveGlucoseTopLimit.DecimalPlaces = 1;
            this.nudInclusiveGlucoseTopLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudInclusiveGlucoseTopLimit.Location = new System.Drawing.Point(28, 46);
            this.nudInclusiveGlucoseTopLimit.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.nudInclusiveGlucoseTopLimit.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudInclusiveGlucoseTopLimit.Name = "nudInclusiveGlucoseTopLimit";
            this.nudInclusiveGlucoseTopLimit.Size = new System.Drawing.Size(120, 26);
            this.nudInclusiveGlucoseTopLimit.TabIndex = 1;
            this.nudInclusiveGlucoseTopLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblTopGlucoseLimit
            // 
            this.lblTopGlucoseLimit.AutoSize = true;
            this.lblTopGlucoseLimit.Location = new System.Drawing.Point(22, 23);
            this.lblTopGlucoseLimit.Name = "lblTopGlucoseLimit";
            this.lblTopGlucoseLimit.Size = new System.Drawing.Size(187, 20);
            this.lblTopGlucoseLimit.TabIndex = 2;
            this.lblTopGlucoseLimit.Text = "Inclusive top glucose limit";
            // 
            // tbxClarityLogFile
            // 
            this.tbxClarityLogFile.Location = new System.Drawing.Point(26, 93);
            this.tbxClarityLogFile.Name = "tbxClarityLogFile";
            this.tbxClarityLogFile.Size = new System.Drawing.Size(574, 26);
            this.tbxClarityLogFile.TabIndex = 3;
            // 
            // btnAnalyseFile
            // 
            this.btnAnalyseFile.Location = new System.Drawing.Point(299, 149);
            this.btnAnalyseFile.Name = "btnAnalyseFile";
            this.btnAnalyseFile.Size = new System.Drawing.Size(144, 58);
            this.btnAnalyseFile.TabIndex = 4;
            this.btnAnalyseFile.Text = "Analyse File";
            this.btnAnalyseFile.UseVisualStyleBackColor = true;
            this.btnAnalyseFile.Click += new System.EventHandler(this.btnAnalyseFile_Click);
            // 
            // rtbAnalyseResult
            // 
            this.rtbAnalyseResult.Location = new System.Drawing.Point(12, 227);
            this.rtbAnalyseResult.Name = "rtbAnalyseResult";
            this.rtbAnalyseResult.Size = new System.Drawing.Size(776, 211);
            this.rtbAnalyseResult.TabIndex = 5;
            this.rtbAnalyseResult.Text = "";
            // 
            // AnalyserForm
            // 
            this.AcceptButton = this.btnAnalyseFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudInclusiveGlucoseTopLimit);
            this.Controls.Add(this.rtbAnalyseResult);
            this.Controls.Add(this.btnAnalyseFile);
            this.Controls.Add(this.tbxClarityLogFile);
            this.Controls.Add(this.lblTopGlucoseLimit);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "AnalyserForm";
            this.Text = "Excessive Blood Sugar (EBS) Analyser";
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.OpenFileDialog clarityLogFile;
        private System.Windows.Forms.NumericUpDown nudInclusiveGlucoseTopLimit;
        private System.Windows.Forms.Label lblTopGlucoseLimit;
        private System.Windows.Forms.TextBox tbxClarityLogFile;
        private System.Windows.Forms.Button btnAnalyseFile;
        private System.Windows.Forms.RichTextBox rtbAnalyseResult;
    }
}

