using System;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsoleUpdater;

namespace CDS.Framework.Tools.NAntConsoleUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            NAntConsoleUpdate updateForm = new NAntConsoleUpdate();
            updateForm.UpdateArgs = new UpdateArgs(args);
            Application.Run(updateForm);
        }
    }
}