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
            this.clarityLogFile = new System.Windows.Forms.OpenFileDialog();
            this.nudInclusiveGlucoseTopLimit = new System.Windows.Forms.NumericUpDown();
            this.btnAnalyseFile = new System.Windows.Forms.Button();
            this.rtbAnalyseResult = new System.Windows.Forms.RichTextBox();
            this.gbxTopGlucoseLimit = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxClarityLogFile = new System.Windows.Forms.GroupBox();
            this.tbxClarityLogFile = new System.Windows.Forms.TextBox();
            this.btnSelectClarityLogFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).BeginInit();
            this.gbxTopGlucoseLimit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbxClarityLogFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // clarityLogFile
            // 
            this.clarityLogFile.Filter = "Clarity Export Files (*.csv)|*.csv";
            this.clarityLogFile.Title = "Open Clarity Export File";
            // 
            // nudInclusiveGlucoseTopLimit
            // 
            this.nudInclusiveGlucoseTopLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudInclusiveGlucoseTopLimit.DecimalPlaces = 1;
            this.nudInclusiveGlucoseTopLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudInclusiveGlucoseTopLimit.Location = new System.Drawing.Point(6, 26);
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
            this.nudInclusiveGlucoseTopLimit.Size = new System.Drawing.Size(255, 26);
            this.nudInclusiveGlucoseTopLimit.TabIndex = 1;
            this.nudInclusiveGlucoseTopLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnAnalyseFile
            // 
            this.btnAnalyseFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAnalyseFile.Location = new System.Drawing.Point(534, 4);
            this.btnAnalyseFile.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnAnalyseFile.Name = "btnAnalyseFile";
            this.btnAnalyseFile.Size = new System.Drawing.Size(128, 55);
            this.btnAnalyseFile.TabIndex = 4;
            this.btnAnalyseFile.Text = "Analyse File";
            this.btnAnalyseFile.UseVisualStyleBackColor = true;
            this.btnAnalyseFile.Click += new System.EventHandler(this.btnAnalyseFile_Click);
            // 
            // rtbAnalyseResult
            // 
            this.rtbAnalyseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAnalyseResult.Location = new System.Drawing.Point(3, 143);
            this.rtbAnalyseResult.Name = "rtbAnalyseResult";
            this.rtbAnalyseResult.Size = new System.Drawing.Size(672, 235);
            this.rtbAnalyseResult.TabIndex = 5;
            this.rtbAnalyseResult.Text = "";
            // 
            // gbxTopGlucoseLimit
            // 
            this.gbxTopGlucoseLimit.Controls.Add(this.nudInclusiveGlucoseTopLimit);
            this.gbxTopGlucoseLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTopGlucoseLimit.Location = new System.Drawing.Point(3, 3);
            this.gbxTopGlucoseLimit.Name = "gbxTopGlucoseLimit";
            this.gbxTopGlucoseLimit.Size = new System.Drawing.Size(267, 58);
            this.gbxTopGlucoseLimit.TabIndex = 6;
            this.gbxTopGlucoseLimit.TabStop = false;
            this.gbxTopGlucoseLimit.Text = "Inclusive top glucose limit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 273F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.gbxTopGlucoseLimit, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAnalyseFile, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 64);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rtbAnalyseResult, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(678, 381);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.gbxClarityLogFile, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(672, 64);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // gbxClarityLogFile
            // 
            this.gbxClarityLogFile.Controls.Add(this.tbxClarityLogFile);
            this.gbxClarityLogFile.Controls.Add(this.btnSelectClarityLogFile);
            this.gbxClarityLogFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxClarityLogFile.Location = new System.Drawing.Point(3, 3);
            this.gbxClarityLogFile.Name = "gbxClarityLogFile";
            this.gbxClarityLogFile.Size = new System.Drawing.Size(666, 58);
            this.gbxClarityLogFile.TabIndex = 10;
            this.gbxClarityLogFile.TabStop = false;
            this.gbxClarityLogFile.Text = "Clarity Log File";
            // 
            // tbxClarityLogFile
            // 
            this.tbxClarityLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxClarityLogFile.Location = new System.Drawing.Point(6, 23);
            this.tbxClarityLogFile.Name = "tbxClarityLogFile";
            this.tbxClarityLogFile.Size = new System.Drawing.Size(519, 26);
            this.tbxClarityLogFile.TabIndex = 3;
            // 
            // btnSelectClarityLogFile
            // 
            this.btnSelectClarityLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectClarityLogFile.Location = new System.Drawing.Point(531, 20);
            this.btnSelectClarityLogFile.Name = "btnSelectClarityLogFile";
            this.btnSelectClarityLogFile.Size = new System.Drawing.Size(129, 32);
            this.btnSelectClarityLogFile.TabIndex = 0;
            this.btnSelectClarityLogFile.Text = "Select File";
            this.btnSelectClarityLogFile.UseVisualStyleBackColor = true;
            this.btnSelectClarityLogFile.Click += new System.EventHandler(this.btnSelectClarityLogFile_Click);
            // 
            // AnalyserForm
            // 
            this.AcceptButton = this.btnAnalyseFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 381);
            this.Controls.Add(this.tableLayoutPanel2);
            this.MinimumSize = new System.Drawing.Size(570, 330);
            this.Name = "AnalyserForm";
            this.Text = "Excessive Blood Sugar (EBS) Analyser";
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).EndInit();
            this.gbxTopGlucoseLimit.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gbxClarityLogFile.ResumeLayout(false);
            this.gbxClarityLogFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog clarityLogFile;
        private System.Windows.Forms.NumericUpDown nudInclusiveGlucoseTopLimit;
        private System.Windows.Forms.Button btnAnalyseFile;
        private System.Windows.Forms.RichTextBox rtbAnalyseResult;
        private System.Windows.Forms.GroupBox gbxTopGlucoseLimit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gbxClarityLogFile;
        private System.Windows.Forms.TextBox tbxClarityLogFile;
        private System.Windows.Forms.Button btnSelectClarityLogFile;
    }
}

