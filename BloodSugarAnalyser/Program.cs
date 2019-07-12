using BloodSugarAnalyser.GUI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace BloodSugarAnalyser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new AnalyserForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            showErrorMessage(e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            showErrorMessage(e.Exception);
        }

        /// <summary>
        /// Shows an error message with information from the exception.
        /// </summary>
        /// <param name="ex">The exception holding information about the error.</param>
        private static void showErrorMessage(object exception)
        {
            var ex = exception as Exception;
            string warning;

            if (ex.Data.Contains("LastLogIndex"))
            {
                var format = "An unexpected error occured.{0}Last finished log line: {1}{0}Error Information: {2}";
                warning = String.Format(format, Environment.NewLine, ex.Data["LastLogIndex"], ex.Message);
            }
            else
            {
                var format = "An unexpected error occured:{0}{1}";
                warning = String.Format(format, Environment.NewLine, ex.Message);
            }

            MessageBox.Show(warning, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
