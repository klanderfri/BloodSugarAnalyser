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
            this.gbxTopGlucoseLimit = new System.Windows.Forms.GroupBox();
            this.tlpMiddleRow = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOuterContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTopRow = new System.Windows.Forms.TableLayoutPanel();
            this.gbxClarityLogFile = new System.Windows.Forms.GroupBox();
            this.tbxClarityLogFile = new System.Windows.Forms.TextBox();
            this.btnSelectClarityLogFile = new System.Windows.Forms.Button();
            this.tlpBottomRow = new System.Windows.Forms.TableLayoutPanel();
            this.rtbAnalyseResult = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).BeginInit();
            this.gbxTopGlucoseLimit.SuspendLayout();
            this.tlpMiddleRow.SuspendLayout();
            this.tlpOuterContainer.SuspendLayout();
            this.tlpTopRow.SuspendLayout();
            this.gbxClarityLogFile.SuspendLayout();
            this.tlpBottomRow.SuspendLayout();
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
            // tlpMiddleRow
            // 
            this.tlpMiddleRow.ColumnCount = 3;
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 273F));
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMiddleRow.Controls.Add(this.gbxTopGlucoseLimit, 0, 0);
            this.tlpMiddleRow.Controls.Add(this.btnAnalyseFile, 2, 0);
            this.tlpMiddleRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMiddleRow.Location = new System.Drawing.Point(3, 73);
            this.tlpMiddleRow.Name = "tlpMiddleRow";
            this.tlpMiddleRow.RowCount = 1;
            this.tlpMiddleRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiddleRow.Size = new System.Drawing.Size(672, 64);
            this.tlpMiddleRow.TabIndex = 8;
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
            this.tlpOuterContainer.Name = "tlpOuterContainer";
            this.tlpOuterContainer.RowCount = 3;
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpOuterContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterContainer.Size = new System.Drawing.Size(678, 381);
            this.tlpOuterContainer.TabIndex = 9;
            // 
            // tlpTopRow
            // 
            this.tlpTopRow.ColumnCount = 1;
            this.tlpTopRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTopRow.Controls.Add(this.gbxClarityLogFile, 0, 0);
            this.tlpTopRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopRow.Location = new System.Drawing.Point(3, 3);
            this.tlpTopRow.Name = "tlpTopRow";
            this.tlpTopRow.RowCount = 1;
            this.tlpTopRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTopRow.Size = new System.Drawing.Size(672, 64);
            this.tlpTopRow.TabIndex = 4;
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
            // tlpBottomRow
            // 
            this.tlpBottomRow.ColumnCount = 1;
            this.tlpBottomRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottomRow.Controls.Add(this.rtbAnalyseResult, 0, 0);
            this.tlpBottomRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBottomRow.Location = new System.Drawing.Point(3, 143);
            this.tlpBottomRow.Name = "tlpBottomRow";
            this.tlpBottomRow.RowCount = 1;
            this.tlpBottomRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottomRow.Size = new System.Drawing.Size(672, 235);
            this.tlpBottomRow.TabIndex = 9;
            // 
            // rtbAnalyseResult
            // 
            this.rtbAnalyseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAnalyseResult.Location = new System.Drawing.Point(3, 3);
            this.rtbAnalyseResult.Name = "rtbAnalyseResult";
            this.rtbAnalyseResult.Size = new System.Drawing.Size(666, 229);
            this.rtbAnalyseResult.TabIndex = 6;
            this.rtbAnalyseResult.Text = "";
            // 
            // AnalyserForm
            // 
            this.AcceptButton = this.btnAnalyseFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 381);
            this.Controls.Add(this.tlpOuterContainer);
            this.MinimumSize = new System.Drawing.Size(570, 330);
            this.Name = "AnalyserForm";
            this.Text = "Excessive Blood Sugar (EBS) Analyser";
            ((System.ComponentModel.ISupportInitialize)(this.nudInclusiveGlucoseTopLimit)).EndInit();
            this.gbxTopGlucoseLimit.ResumeLayout(false);
            this.tlpMiddleRow.ResumeLayout(false);
            this.tlpOuterContainer.ResumeLayout(false);
            this.tlpTopRow.ResumeLayout(false);
            this.gbxClarityLogFile.ResumeLayout(false);
            this.gbxClarityLogFile.PerformLayout();
            this.tlpBottomRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog clarityLogFile;
        private System.Windows.Forms.NumericUpDown nudInclusiveGlucoseTopLimit;
        private System.Windows.Forms.Button btnAnalyseFile;
        private System.Windows.Forms.GroupBox gbxTopGlucoseLimit;
        private System.Windows.Forms.TableLayoutPanel tlpMiddleRow;
        private System.Windows.Forms.TableLayoutPanel tlpOuterContainer;
        private System.Windows.Forms.TableLayoutPanel tlpTopRow;
        private System.Windows.Forms.GroupBox gbxClarityLogFile;
        private System.Windows.Forms.TextBox tbxClarityLogFile;
        private System.Windows.Forms.Button btnSelectClarityLogFile;
        private System.Windows.Forms.TableLayoutPanel tlpBottomRow;
        private System.Windows.Forms.RichTextBox rtbAnalyseResult;
    }
}

