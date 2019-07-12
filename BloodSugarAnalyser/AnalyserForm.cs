using System;
using System.Data;
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
            var log = analyseFile(filepath, nudInclusiveGlucoseTopLimit.Value);

            //Show the analyse result.
            showAnalyseResult(log);
        }

        /// <summary>
        /// Analyses a blood sugar log.
        /// </summary>
        /// <param name="filepath">The full filepath to the blood sugar log.</param>
        /// <param name="inclusiveTopLimit">The inclusive top limit of good blood sugar.</param>
        /// <returns>A log analyser object holding the analyse results.</returns>
        private LogAnalyser analyseFile(string filepath, decimal inclusiveTopLimit)
        {
            var lastIndex = 0;

            try
            {
                var log = new LogAnalyser(inclusiveTopLimit);
                var hasIgnoredTitleRow = false;
                int currentLogIndex = 0;

                //Analyse the file, row by row (to prevent memory problems when analysing big files).
                foreach (var line in File.ReadAllLines(filepath))
                {
                    //Ignore the first line. It's just a header row.
                    if (!hasIgnoredTitleRow)
                    {
                        hasIgnoredTitleRow = true;
                        continue;
                    }

                    //Analyse the data in the log line.
                    var logLine = new ClarityLogLine(line);
                    log.AnalyseLogLine(logLine);

                    //Store the last line index to add to any raised exception.
                    lastIndex = currentLogIndex;
                }

                return log;
            }
            catch (Exception ex)
            {
                ex.Data.Add("LastLogIndex", lastIndex);
                throw;
            }
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
