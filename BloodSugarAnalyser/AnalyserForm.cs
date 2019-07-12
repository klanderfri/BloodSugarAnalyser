using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BloodSugarAnalyser
{
    public partial class AnalyserForm : Form
    {
        public AnalyserForm()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
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
            var result = new StringBuilder();

            result.AppendFormat(
                "Patient: {0} {1} (PID {2})",
                log.PatientInfo.FirstName,
                log.PatientInfo.Surname,
                log.PatientInfo.PatientID);
            result.AppendLine();

            result.AppendFormat("Total EBS: {0} h*mmol/L", Math.Round(log.AreaAboveRange, 2));
            result.AppendLine();

            result.AppendFormat("EBS per day: {0} h*mmol/L", Math.Round(log.AverageAreaAboveRangePerDay, 2));
            result.AppendLine();

            result.AppendFormat("Period start: {0}", log.TimeStampStart);
            result.AppendLine();

            result.AppendFormat("Period end: {0}", log.TimeStampEnd);
            result.AppendLine();
            
            rtbAnalyseResult.Text = result.ToString();
        }
    }
}
