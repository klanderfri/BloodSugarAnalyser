using BloodSugarAnalyser.Logic;
using System;
using System.IO;
using System.Windows.Forms;

namespace BloodSugarAnalyser.GUI
{
    public partial class AnalyserForm : Form
    {
        public AnalyserForm()
        {
            InitializeComponent();
        }

        private void btnSelectClarityLogFile_Click(object sender, EventArgs e)
        {
            if (clarityLogFile.ShowDialog() == DialogResult.OK)
            {
                tbxClarityLogFile.Text = clarityLogFile.FileName;
            }
        }

        private void btnAnalyseFile_Click(object sender, EventArgs e)
        {
            //Fetch the file path.
            var filepath = tbxClarityLogFile.Text;

            //Verify that the file is valid.
            if (!File.Exists(filepath))
            {
                MessageBox.Show("The selected file does not exist.", "File does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Analyse the file.
            var log = new LogAnalyser(filepath, nudInclusiveGlucoseTopLimit.Value);

            //Show the analyse result.
            showAnalyseResult(log);
        }

        /// <summary>
        /// Shows the result from the analyse of the log file.
        /// </summary>
        /// <param name="log">The analyser object holding the result.</param>
        private void showAnalyseResult(LogAnalyser log)
        {
            rtbAnalyseResult.Text = log.GetResult();
        }
    }
}
