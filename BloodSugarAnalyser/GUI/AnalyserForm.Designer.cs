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
            this.bloodsugarLogFile = new System.Windows.Forms.OpenFileDialog();
            this.nudInclusiveGlucoseTopLimit = new System.Windows.Forms.NumericUpDown();
            this.btnAnalyseFile = new System.Windows.Forms.Button();
            this.gbxTopGlucoseLimit = new System.Windows.Forms.GroupBox();
            this.tlpMiddleRow = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOuterContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBottomRow = new System.Windows.Forms.TableLayoutPanel();
            this.rtbAnalyseResult = new System.Windows.Forms.RichTextBox();
            this.tlpTopRow = new System.Windows.Forms.TableLayoutPanel();
            this.gbxBloodLogFile = new System.Windows.Forms.GroupBox();
            this.tbxClarityLogFile = new System.Windows.Forms.TextBox();
            this.btnSelectClarityLogFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).BeginInit();
            this.gbxTopGlucoseLimit.SuspendLayout();
            this.tlpMiddleRow.SuspendLayout();
            this.tlpOuterContainer.SuspendLayout();
            this.tlpBottomRow.SuspendLayout();
            this.tlpTopRow.SuspendLayout();
            this.gbxBloodLogFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // bloodsugarLogFile
            // 
            this.bloodsugarLogFile.Filter = "Clarity Export Files (CLARITY_*.csv)|CLARITY_*.csv|Freestyle Libre Export Files (" +
    "*.txt)|*.txt";
            this.bloodsugarLogFile.Title = "Open Bloodsugar Log File";
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
            this.nudInclusiveGlucoseTopLimit.Location = new System.Drawing.Point(4, 16);
            this.nudInclusiveGlucoseTopLimit.Margin = new System.Windows.Forms.Padding(2);
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
            this.nudInclusiveGlucoseTopLimit.Size = new System.Drawing.Size(170, 20);
            this.nudInclusiveGlucoseTopLimit.TabIndex = 7;
            this.nudInclusiveGlucoseTopLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnAnalyseFile
            // 
            this.btnAnalyseFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAnalyseFile.Location = new System.Drawing.Point(356, 2);
            this.btnAnalyseFile.Margin = new System.Windows.Forms.Padding(2, 2, 7, 2);
            this.btnAnalyseFile.Name = "btnAnalyseFile";
            this.btnAnalyseFile.Size = new System.Drawing.Size(85, 36);
            this.btnAnalyseFile.TabIndex = 8;
            this.btnAnalyseFile.Text = "Analyse File";
            this.btnAnalyseFile.UseVisualStyleBackColor = true;
            this.btnAnalyseFile.Click += new System.EventHandler(this.btnAnalyseFile_Click);
            // 
            // gbxTopGlucoseLimit
            // 
            this.gbxTopGlucoseLimit.Controls.Add(this.nudInclusiveGlucoseTopLimit);
            this.gbxTopGlucoseLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTopGlucoseLimit.Location = new System.Drawing.Point(2, 2);
            this.gbxTopGlucoseLimit.Margin = new System.Windows.Forms.Padding(2);
            this.gbxTopGlucoseLimit.Name = "gbxTopGlucoseLimit";
            this.gbxTopGlucoseLimit.Padding = new System.Windows.Forms.Padding(2);
            this.gbxTopGlucoseLimit.Size = new System.Drawing.Size(178, 37);
            this.gbxTopGlucoseLimit.TabIndex = 6;
            this.gbxTopGlucoseLimit.TabStop = false;
            this.gbxTopGlucoseLimit.Text = "Inclusive top glucose limit";
            // 
            // tlpMiddleRow
            // 
            this.tlpMiddleRow.ColumnCount = 3;
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMiddleRow.Controls.Add(this.gbxTopGlucoseLimit, 0, 0);
            this.tlpMiddleRow.Controls.Add(this.btnAnalyseFile, 2, 0);
            this.tlpMiddleRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMiddleRow.Location = new System.Drawing.Point(2, 47);
            this.tlpMiddleRow.Margin = new System.Windows.Forms.Padding(2);
            this.tlpMiddleRow.Name = "tlpMiddleRow";
            this.tlpMiddleRow.RowCount = 1;
            this.tlpMiddleRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiddleRow.Size = new System.Drawing.Size(448, 41);
            this.tlpMiddleRow.TabIndex = 5;
            // 
            // tlpOuterContainer
            // 
            this.tlpOuterContainer.ColumnCount = 1;
            this.tlpOuterContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterContainer.Controls.Add(this.tlpBottomRow, 0, 2);
            this.tlpOuterContainer.Controls.Add(this.tlpTopRow, 0, 0);
            this.tlpOuterContainer.Controls.Add(this.tlpMiddleRow, 0, 1);
            this.tlpOuterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOuterContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpOuterContainer.Margin = new System.Windows.Forms.Padding(2);
            this.tlpOuterContainer.Name = "tlpOuterContainer";
            this.tlpOuterContainer.RowCount = 3;
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterContainer.Size = new System.Drawing.Size(452, 248);
            this.tlpOuterContainer.TabIndex = 0;
            // 
            // tlpBottomRow
            // 
            this.tlpBottomRow.ColumnCount = 1;
            this.tlpBottomRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottomRow.Controls.Add(this.rtbAnalyseResult, 0, 0);
            this.tlpBottomRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBottomRow.Location = new System.Drawing.Point(2, 92);
            this.tlpBottomRow.Margin = new System.Windows.Forms.Padding(2);
            this.tlpBottomRow.Name = "tlpBottomRow";
            this.tlpBottomRow.RowCount = 1;
            this.tlpBottomRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottomRow.Size = new System.Drawing.Size(448, 154);
            this.tlpBottomRow.TabIndex = 9;
            // 
            // rtbAnalyseResult
            // 
            this.rtbAnalyseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAnalyseResult.Location = new System.Drawing.Point(2, 2);
            this.rtbAnalyseResult.Margin = new System.Windows.Forms.Padding(2);
            this.rtbAnalyseResult.Name = "rtbAnalyseResult";
            this.rtbAnalyseResult.Size = new System.Drawing.Size(444, 150);
            this.rtbAnalyseResult.TabIndex = 10;
            this.rtbAnalyseResult.Text = "";
            // 
            // tlpTopRow
            // 
            this.tlpTopRow.ColumnCount = 1;
            this.tlpTopRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTopRow.Controls.Add(this.gbxBloodLogFile, 0, 0);
            this.tlpTopRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopRow.Location = new System.Drawing.Point(2, 2);
            this.tlpTopRow.Margin = new System.Windows.Forms.Padding(2);
            this.tlpTopRow.Name = "tlpTopRow";
            this.tlpTopRow.RowCount = 1;
            this.tlpTopRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTopRow.Size = new System.Drawing.Size(448, 41);
            this.tlpTopRow.TabIndex = 1;
            // 
            // gbxBloodLogFile
            // 
            this.gbxBloodLogFile.Controls.Add(this.tbxClarityLogFile);
            this.gbxBloodLogFile.Controls.Add(this.btnSelectClarityLogFile);
            this.gbxBloodLogFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxBloodLogFile.Location = new System.Drawing.Point(2, 2);
            this.gbxBloodLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.gbxBloodLogFile.Name = "gbxBloodLogFile";
            this.gbxBloodLogFile.Padding = new System.Windows.Forms.Padding(2);
            this.gbxBloodLogFile.Size = new System.Drawing.Size(444, 37);
            this.gbxBloodLogFile.TabIndex = 2;
            this.gbxBloodLogFile.TabStop = false;
            this.gbxBloodLogFile.Text = "Bloodsugar Log File";
            // 
            // tbxClarityLogFile
            // 
            this.tbxClarityLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxClarityLogFile.Location = new System.Drawing.Point(4, 14);
            this.tbxClarityLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.tbxClarityLogFile.Name = "tbxClarityLogFile";
            this.tbxClarityLogFile.Size = new System.Drawing.Size(347, 20);
            this.tbxClarityLogFile.TabIndex = 3;
            // 
            // btnSelectClarityLogFile
            // 
            this.btnSelectClarityLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectClarityLogFile.Location = new System.Drawing.Point(354, 13);
            this.btnSelectClarityLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectClarityLogFile.Name = "btnSelectClarityLogFile";
            this.btnSelectClarityLogFile.Size = new System.Drawing.Size(86, 22);
            this.btnSelectClarityLogFile.TabIndex = 4;
            this.btnSelectClarityLogFile.Text = "Select File";
            this.btnSelectClarityLogFile.UseVisualStyleBackColor = true;
            this.btnSelectClarityLogFile.Click += new System.EventHandler(this.btnSelectClarityLogFile_Click);
            // 
            // AnalyserForm
            // 
            this.AcceptButton = this.btnAnalyseFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 248);
            this.Controls.Add(this.tlpOuterContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(385, 228);
            this.Name = "AnalyserForm";
            this.Text = "Excessive Blood Sugar (EBS) Analyser";
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).EndInit();
            this.gbxTopGlucoseLimit.ResumeLayout(false);
            this.tlpMiddleRow.ResumeLayout(false);
            this.tlpOuterContainer.ResumeLayout(false);
            this.tlpBottomRow.ResumeLayout(false);
            this.tlpTopRow.ResumeLayout(false);
            this.gbxBloodLogFile.ResumeLayout(false);
            this.gbxBloodLogFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog bloodsugarLogFile;
        private System.Windows.Forms.NumericUpDown nudInclusiveGlucoseTopLimit;
        private System.Windows.Forms.Button btnAnalyseFile;
        private System.Windows.Forms.GroupBox gbxTopGlucoseLimit;
        private System.Windows.Forms.TableLayoutPanel tlpMiddleRow;
        private System.Windows.Forms.TableLayoutPanel tlpOuterContainer;
        private System.Windows.Forms.TableLayoutPanel tlpTopRow;
        private System.Windows.Forms.GroupBox gbxBloodLogFile;
        private System.Windows.Forms.TextBox tbxClarityLogFile;
        private System.Windows.Forms.Button btnSelectClarityLogFile;
        private System.Windows.Forms.TableLayoutPanel tlpBottomRow;
        private System.Windows.Forms.RichTextBox rtbAnalyseResult;
    }
}

