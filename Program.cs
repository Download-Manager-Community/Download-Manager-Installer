using System.Diagnostics;

namespace DownloadManagerInstaller
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[]? args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();

            try
            {
                if (args != null)
                {
                    if (args.Length > 0)
                    {
                        if (args[0] == "--install")
                        {
                            // Install
                            Application.Run(new Form1(new string[] { "install" }));
                        }
                        else if (args[0] == "--update")
                        {
                            // Update
                            Application.Run(new Form1(new string[] { "update" }));
                        }
                        else if (args[0] == "--uninstall")
                        {
                            // Uninstall
                            try
                            {
                                if (args[1] == null || args[1] == "")
                                {
                                    Application.Run(new Form1(new string[] { "uninstall" }));
                                }
                                else
                                {
                                    Application.Run(new Form1(new string[]{
                                    "uninstall",
                                    args[1]
                                }));
                                }
                            }
                            catch
                            {
                                Application.Run(new Form1(new string[] { "uninstall" }));
                            }
                        }
                        else
                        {
                            Application.Run(new Form1(null));
                        }
                    }
                    else
                    {
                        Application.Run(new Form1(null));
                    }
                }
                else
                {
                    Application.Run(new Form1(null));
                }
            }
            catch (Exception ex)
            {
                Application.ExitThread();
                DialogResult result = MessageBox.Show("A fatal error occurred and Download Manager installer cannot continue.\nClick 'Cancel' to exit the application now.\nClick 'Retry' to open a new instance of the installer.\nPlease file a bug report with the following information.\n" + ex.Message + Environment.NewLine + ex.StackTrace, "Fatal Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    try
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = Application.ExecutablePath;
                        info.Arguments = args.ToString();
                        info.UseShellExecute = true;
                        Process.Start(info);
                    }
                    catch
                    {
                        MessageBox.Show("Failed to start a new instance of the installer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Application.Exit();
                }
            }
        }
    }
}