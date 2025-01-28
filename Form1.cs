using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Xml;
using File = System.IO.File;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace DownloadManagerInstaller
{
    public partial class Form1 : Form
    {
        static public Form1 _instance;
        bool silentInstall = false;
        bool update = false;
        int installStage = 1;
        string fileName;
        string path;
        string md5Hash = "";
        bool installing = false;
        bool noCertInstall = false;

        public Form1(string[]? args)
        {
            _instance = this;
            InitializeComponent();
            try
            {
                if (args[0] == "install")
                {
                    if (args[1] == "nocert")
                    {
                        noCertInstall = true;
                    }

                    silentInstall = true;
                    _instance.Hide();
                    _instance.ShowInTaskbar = false;
                    _instance.WindowState = FormWindowState.Minimized;
                    _instance.Visible = false;
                    textBox1.Text = @"C:\Download Manager\";
                    desktopShortcutCheckbox.Checked = true;
                    startMenuCheckbox.Checked = true;
                    Install();
                }
                else if (args[0] == "update")
                {
                    _instance.Hide();
                    _instance.ShowInTaskbar = false;
                    _instance.WindowState = FormWindowState.Minimized;
                    _instance.Visible = false;
                    update = true;
                    UpdateForm updateForm = new UpdateForm();
                    updateForm.Show();
                }
                else if (args[0] == "uninstall")
                {
                    _instance.Hide();
                    _instance.ShowInTaskbar = false;
                    _instance.WindowState = FormWindowState.Minimized;
                    _instance.Visible = false;
                    try
                    {
                        if (args[1] == null || args[1] == "")
                        {
                            Uninstall();
                        }
                        else
                        {
                            Uninstall(true, args[1]);
                        }
                    }
                    catch
                    {
                        Uninstall();
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Browse Install Folder
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath + "\\";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Next
            if (installStage == 1)
            {
                tabControl1.Visible = false;
                tabControl2.Visible = true;
                button1.Enabled = false;
                licenceCheckbox.Visible = true;
                licenceCheckbox.Enabled = false;
                DownloadProgress progress = new DownloadProgress("https://raw.githubusercontent.com/Download-Manager-Community/Download-Manager/master/LICENSE.txt", System.IO.Path.GetTempPath(), "7ee4bb010f45d9be66db310093fb8f40b19a1cd7b872234864dcbcb7d61a3808b840a360294a22ac7199710892e459fd85a026254ed2d424759bd86182bb8001");
                progress.ShowDialog();
                installStage += 1;
            }
            else if (installStage == 2)
            {
                tabControl2.Visible = false;
                licenceCheckbox.Visible = false;
                groupBox1.Visible = true;
                button1.Text = "Finish";
                button1.Enabled = false;
                Install();
                installStage += 1;
            }
            else
            {
                if (openCheckbox.Checked)
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = path + "DownloadManager.exe";
                    Process.Start(info);
                }

                Process.GetCurrentProcess().Kill();
            }
        }

        public void LicenseFailed()
        {
            DialogResult result = MessageBox.Show("The SHA-512 verification of the license failed.\nRetry Download?", "Download Manager - Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                DownloadProgress progress = new DownloadProgress("https://raw.githubusercontent.com/Soniczac7/Download-Manager/master/LICENSE.txt", System.IO.Path.GetTempPath(), "7ee4bb010f45d9be66db310093fb8f40b19a1cd7b872234864dcbcb7d61a3808b840a360294a22ac7199710892e459fd85a026254ed2d424759bd86182bb8001");
                progress.ShowDialog();
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        public void Install()
        {
            if (silentInstall == false)
            {
                installing = true;
                path = textBox1.Text;
                Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 0;
                    progressBar1.Style = ProgressBarStyle.Blocks;
                }));

                Thread thread = new Thread(() =>
                {
                    try
                    {
                        Action increaseProgress20 = () => progressBar1.Value += 20;
                        Action increaseProgress10 = () => progressBar1.Value += 10;

                        // Check if path exists
                        if (!System.IO.Directory.Exists(textBox1.Text))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        else
                        {
                            System.IO.Directory.Delete(path, true);
                            System.IO.Directory.CreateDirectory(path);
                        }
                        _instance.Invoke(increaseProgress20);

                        // Put zip in directory
                        File.WriteAllBytes(path + "install.zip", Properties.Resources.install);

                        _instance.Invoke(increaseProgress20);

                        ZipFile.ExtractToDirectory(path + "install.zip", path);

                        _instance.Invoke(increaseProgress20);

                        // Delete zip
                        File.Delete(path + "install.zip");

                        _instance.Invoke(increaseProgress20);

                        // Write icon to directory
                        File.WriteAllBytes(path + "icon.ico", Properties.Resources.icon1);

                        // Create desktop shortcut
                        if (desktopShortcutCheckbox.Checked)
                        {
                            string pathToExe = path + "DownloadManager.exe";
                            string commonDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

                            if (!Directory.Exists(commonDesktopPath))
                                Directory.CreateDirectory(commonDesktopPath);

                            string shortcutLocation = Path.Combine(commonDesktopPath, "Download Manager" + ".lnk");
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                            shortcut.Description = "Download Manager helps you download your files faster.";
                            shortcut.TargetPath = pathToExe;
                            shortcut.Save();
                        }

                        _instance.Invoke(increaseProgress10);

                        // Create start menu shortcut
                        if (startMenuCheckbox.Checked)
                        {
                            string pathToExe = path + "DownloadManager.exe";
                            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Download Manager");

                            if (!Directory.Exists(appStartMenuPath))
                                Directory.CreateDirectory(appStartMenuPath);

                            string shortcutLocation = Path.Combine(appStartMenuPath, "Download Manager" + ".lnk");
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                            shortcut.Description = "Download Manager helps you download your files faster.";
                            shortcut.TargetPath = pathToExe;
                            shortcut.Save();
                        }

                        _instance.Invoke(increaseProgress10);

                        // Add an add/remove programs entry
                        string appName = "Download Manager";
                        string installLocation = path + "DownloadManager.exe";
                        string displayIcon = path + "icon.ico";
                        string uninstallString = path + "DownloadManagerInstaller.exe --uninstall";
                        string versionString = GetAssemblyVersion(path + "DownloadManager.exe").ToString();

                        RegisterControlPanelProgram(appName, installLocation, displayIcon, uninstallString, versionString);

                        _instance.Invoke(increaseProgress10);

                        if (!noCertInstall)
                        {
                            if (File.Exists(Path.GetTempPath() + "CertificateInstaller.xml"))
                            {
                                try
                                {
                                    File.Delete(Path.GetTempPath() + "CertificateInstaller.xml");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(String.Format("{0} ({1})\n{2}", ex.Message, ex.GetType().FullName, ex.StackTrace));
                                    return;
                                }
                            }

                            DownloadProgress downloadCertInstallerXml = new DownloadProgress("https://raw.githubusercontent.com/Soniczac7/app-update/main/CertificateInstaller.xml", Path.GetTempPath(), null);
                            downloadCertInstallerXml.ShowDialog();

                            if (!File.Exists(Path.GetTempPath() + "CertificateInstaller.xml"))
                            {
                                MessageBox.Show("The certificate installer could not be downloaded.\n(Unknown Download Error)", "Download Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string version;
                            string url;
                            string mandatory;

                            XmlDocument xml = new XmlDocument();
                            xml.Load(System.IO.Path.GetTempPath() + "CertificateInstaller.xml");
                            version = xml.DocumentElement.ParentNode.ChildNodes.Item(1).ChildNodes.Item(0).InnerText;
                            url = xml.DocumentElement.ParentNode.ChildNodes.Item(1).ChildNodes.Item(1).InnerText;

                            if (version == null || url == null)
                            {
                                MessageBox.Show("XML file is different from expected layout.", "Download Manager - Malformed XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Debug.WriteLine($"Downloading CertificateInstaller version {version} from {url}.");

                            DownloadProgress downloadCertInstaller = new DownloadProgress(url, Path.GetTempPath(), null);
                            downloadCertInstaller.ShowDialog();

                            if (Directory.Exists(Path.GetTempPath() + "CertificateInstaller"))
                            {
                                Directory.Delete(Path.GetTempPath() + "CertificateInstaller", true);
                            }

                            // Unzip CertificateInstaller
                            Directory.CreateDirectory(Path.GetTempPath() + "CertificateInstaller");
                            ZipFile.ExtractToDirectory(Path.GetTempPath() + "CertificateInstaller.zip", Path.GetTempPath() + "CertificateInstaller");

                            // Start CertificateInstaller
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.UseShellExecute = true;
                            startInfo.FileName = Path.GetTempPath() + "CertificateInstaller\\CertificateInstaller.exe";
                            startInfo.Arguments = "--install \"Soniczac7 Code Signing\"";
                            startInfo.WorkingDirectory = Path.GetTempPath() + "CertificateInstaller";
                            startInfo.ErrorDialog = true;
                            Process process = new Process();
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();

                            if (process.ExitCode != 0)
                            {
                                switch (process.ExitCode)
                                {
                                    case 1:
                                        MessageBox.Show($"The certificate could not be installed.\n(Certificate Install Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case 2:
                                        MessageBox.Show($"The certificate could not be installed because the certificate name is invalid.\n(Internal Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case 3:
                                        MessageBox.Show($"The certificate could not be installed because the command-line arguments are incorrect.\n(Internal Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    default:
                                        MessageBox.Show($"The certificate could not be installed.\n(Unknown Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                        }

                        _instance.Invoke(increaseProgress10);

                        Invoke(new MethodInvoker(delegate ()
                            {
                                label5.Text = "Download Manager has installed successfully.";
                                openCheckbox.Visible = true;
                                button1.Enabled = true;
                                installing = false;
                            }));
                    }
                    catch (Exception ex)
                    {
                        DialogResult result = MessageBox.Show(ex.Message, "Setup - Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (result == DialogResult.Retry)
                        {
                            Install();
                        }
                        else
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                });
                thread.Start();
            }
            else
            {
                // Silent Install

                installing = true;
                path = textBox1.Text;

                Thread thread = new Thread(() =>
                {
                    try
                    {
                        // Check if path exists
                        if (!System.IO.Directory.Exists(textBox1.Text))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        else
                        {
                            System.IO.Directory.Delete(path, true);
                            System.IO.Directory.CreateDirectory(path);
                        }

                        // Put zip in directory
                        File.WriteAllBytes(path + "install.zip", Properties.Resources.install);

                        ZipFile.ExtractToDirectory(path + "install.zip", path);

                        // Delete zip
                        File.Delete(path + "install.zip");

                        // Write icon to directory
                        File.WriteAllBytes(path + "icon.ico", Properties.Resources.icon1);

                        // Create desktop shortcut
                        if (desktopShortcutCheckbox.Checked)
                        {
                            string pathToExe = path + "DownloadManager.exe";
                            string commonDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

                            if (!Directory.Exists(commonDesktopPath))
                                Directory.CreateDirectory(commonDesktopPath);

                            string shortcutLocation = Path.Combine(commonDesktopPath, "Download Manager" + ".lnk");
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                            shortcut.Description = "Download Manager helps you download your files faster.";
                            shortcut.TargetPath = pathToExe;
                            shortcut.Save();
                        }

                        // Create start menu shortcut
                        if (startMenuCheckbox.Checked)
                        {
                            string pathToExe = path + "DownloadManager.exe";
                            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Download Manager");

                            if (!Directory.Exists(appStartMenuPath))
                                Directory.CreateDirectory(appStartMenuPath);

                            string shortcutLocation = Path.Combine(appStartMenuPath, "Download Manager" + ".lnk");
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                            shortcut.Description = "Download Manager helps you download your files faster.";
                            shortcut.TargetPath = pathToExe;
                            shortcut.Save();
                        }

                        // Add an add/remove programs entry
                        string appName = "Download Manager";
                        string installLocation = path + "DownloadManager.exe";
                        string displayIcon = path + "icon.ico";
                        string uninstallString = path + "DownloadManagerInstaller.exe --uninstall";
                        string versionString = GetAssemblyVersion(path + "DownloadManager.exe").ToString();

                        RegisterControlPanelProgram(appName, installLocation, displayIcon, uninstallString, versionString);

                        if (!noCertInstall)
                        {
                            if (File.Exists(Path.GetTempPath() + "CertificateInstaller.xml"))
                            {
                                try
                                {
                                    File.Delete(Path.GetTempPath() + "CertificateInstaller.xml");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(String.Format("{0} ({1})\n{2}", ex.Message, ex.GetType().FullName, ex.StackTrace));
                                    return;
                                }
                            }

                            DownloadProgress downloadCertInstallerXml = new DownloadProgress("https://raw.githubusercontent.com/Soniczac7/app-update/main/CertificateInstaller.xml", Path.GetTempPath(), null);
                            downloadCertInstallerXml.ShowDialog();

                            if (!File.Exists(Path.GetTempPath() + "CertificateInstaller.xml"))
                            {
                                MessageBox.Show("The certificate installer could not be downloaded.\n(Unknown Download Error)", "Download Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string version;
                            string url;
                            string mandatory;

                            XmlDocument xml = new XmlDocument();
                            xml.Load(System.IO.Path.GetTempPath() + "CertificateInstaller.xml");
                            version = xml.DocumentElement.ParentNode.ChildNodes.Item(1).ChildNodes.Item(0).InnerText;
                            url = xml.DocumentElement.ParentNode.ChildNodes.Item(1).ChildNodes.Item(1).InnerText;

                            if (version == null || url == null)
                            {
                                MessageBox.Show("XML file is different from expected layout.", "Download Manager - Malformed XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Debug.WriteLine($"Downloading CertificateInstaller version {version} from {url}.");

                            DownloadProgress downloadCertInstaller = new DownloadProgress(url, Path.GetTempPath(), null);
                            downloadCertInstaller.ShowDialog();

                            if (Directory.Exists(Path.GetTempPath() + "CertificateInstaller"))
                            {
                                Directory.Delete(Path.GetTempPath() + "CertificateInstaller", true);
                            }

                            // Unzip CertificateInstaller
                            Directory.CreateDirectory(Path.GetTempPath() + "CertificateInstaller");
                            ZipFile.ExtractToDirectory(Path.GetTempPath() + "CertificateInstaller.zip", Path.GetTempPath() + "CertificateInstaller");

                            // Start CertificateInstaller
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.UseShellExecute = true;
                            startInfo.FileName = Path.GetTempPath() + "CertificateInstaller\\CertificateInstaller.exe";
                            startInfo.Arguments = "--install \"Soniczac7 Code Signing\"";
                            startInfo.WorkingDirectory = Path.GetTempPath() + "CertificateInstaller";
                            startInfo.ErrorDialog = true;
                            Process process = new Process();
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();

                            if (process.ExitCode != 0)
                            {
                                switch (process.ExitCode)
                                {
                                    case 1:
                                        MessageBox.Show($"The certificate could not be installed.\n(Certificate Install Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case 2:
                                        MessageBox.Show($"The certificate could not be installed because the certificate name is invalid.\n(Internal Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case 3:
                                        MessageBox.Show($"The certificate could not be installed because the command-line arguments are incorrect.\n(Internal Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    default:
                                        MessageBox.Show($"The certificate could not be installed.\n(Unknown Error) [Exit Code: {process.ExitCode}]", "Download Manager Installer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                        }

                        Invoke(new MethodInvoker(delegate ()
                        {
                            label5.Text = "Download Manager has installed successfully.";
                            installing = false;
                            Environment.Exit(0);
                        }));
                    }
                    catch (Exception ex)
                    {
                        if (ex == new DriveNotFoundException())
                        {
                            Environment.Exit(1);
                        }
                        else if (ex == new UnauthorizedAccessException())
                        {
                            Environment.Exit(2);
                        }
                        else if (ex == new IOException())
                        {
                            if (ex.Message == "The file exists")
                            {
                                Environment.Exit(4);
                            }
                            else if (ex.Message == "There is not enough space on the disk.")
                            {
                                Environment.Exit(5);
                            }
                            else
                            {
                                Environment.Exit(3);
                            }
                        }
                        else
                        {
                            Debug.WriteLine(ex.Message);
                            Environment.Exit(6);
                        }
                    }
                });
                thread.Start();
            }
        }

        public void Uninstall(bool skipDialog = false, string installationPath = "")
        {
            if (skipDialog == false)
            {
                MessageBox.Show("Are you sure you want to remove Download Manager from this computer?", "Uninstall Download Manager?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (installationPath != "")
            {
                bool doneWithErrors = false;

                try
                {
                    Directory.Delete(installationPath, true);
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show(ex.Message, "Uninstallation Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (result == DialogResult.Abort)
                    {
                        MessageBox.Show("The operation has been canceled by the user.", "Uninstallation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                    else if (result == DialogResult.Retry)
                    {
                        Uninstall(true, installationPath);
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        doneWithErrors = true;
                    }
                }

                try
                {
                    string Install_Reg_Loc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
                    RegistryKey hKey = (Registry.LocalMachine).OpenSubKey(Install_Reg_Loc, true);
                    hKey.DeleteSubKey("Download Manager");
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show(ex.Message, "Uninstallation Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (result == DialogResult.Abort)
                    {
                        MessageBox.Show("The operation has been canceled by the user.", "Uninstallation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                    else if (result == DialogResult.Retry)
                    {
                        Uninstall(true, installationPath);
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        doneWithErrors = true;
                    }
                }

                if (doneWithErrors)
                {
                    MessageBox.Show("Download Manager has been removed from your computer with errors.", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Download Manager has been removed from your computer.", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Application.Exit();
            }
            else
            {
                // Copy installer files to temporary directory
                string newdir = Path.GetTempPath() + "DownloadManagerInstaller\\";
                string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";

                if (Directory.Exists(newdir))
                    Directory.Delete(newdir, true);
                Directory.CreateDirectory(newdir);
                File.Copy(dir + "\\DownloadManagerInstaller.exe", newdir + "\\DownloadManagerInstaller.exe");
                File.Copy(dir + "\\DownloadManagerInstaller.dll", newdir + "\\DownloadManagerInstaller.dll");
                File.Copy(dir + "\\DownloadManagerInstaller.deps.json", newdir + "\\DownloadManagerInstaller.deps.json");
                File.Copy(dir + "\\DownloadManagerInstaller.pdb", newdir + "\\DownloadManagerInstaller.pdb");
                File.Copy(dir + "\\DownloadManagerInstaller.runtimeconfig.json", newdir + "\\DownloadManagerInstaller.runtimeconfig.json");

                ProcessStartInfo info = new ProcessStartInfo();
                //info.FileName = newdir + "DownloadManagerInstaller.exe";
                info.FileName = "C:\\Users\\Sonic\\AppData\\Local\\Temp\\DownloadManagerInstaller\\DownloadManagerInstaller.exe";
                info.Arguments = "--uninstall " + '"' + dir + "\\" + '"';
                info.WorkingDirectory = "C:\\Users\\Sonic\\AppData\\Local\\Temp\\DownloadManagerInstaller\\";
                info.UseShellExecute = true;
                Process.Start(info);

                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// Get the version of an assembly from a file path
        /// </summary>
        /// <param name="executablePath">The path of the assembly.</param>
        /// <returns>The version of the assembly.</returns>
        public static Version GetAssemblyVersion(string executablePath)
        {
            // Load the assembly from the specified path
            Assembly assembly = Assembly.LoadFile(executablePath);

            // Get the assembly name which contains the version information
            AssemblyName assemblyName = assembly.GetName();

            // Return the version
            return assemblyName.Version;
        }

        public static void RegisterControlPanelProgram(string appName, string installLocation, string displayicon, string uninstallString, string version)
        {
            string Install_Reg_Loc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey hKey = (Registry.LocalMachine).OpenSubKey(Install_Reg_Loc, true);

            if (hKey == null)
            {
                MessageBox.Show("Add/Remove programs registry key not found.\nDownload Manager can only be uninstalled with the following command:\n" + uninstallString + "\nIf this error persists, ensure the installer has permissions to access the registry and file a bug report.", "Setup - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RegistryKey appKey = hKey.CreateSubKey(appName);
            appKey.SetValue("DisplayName", (object)appName, RegistryValueKind.String);
            appKey.SetValue("Publisher", (object)"Soniczac7", RegistryValueKind.String);
            appKey.SetValue("InstallLocation", (object)installLocation, RegistryValueKind.ExpandString);
            appKey.SetValue("DisplayIcon", (object)displayicon, RegistryValueKind.String);
            appKey.SetValue("UninstallString", (object)uninstallString, RegistryValueKind.ExpandString);
            appKey.SetValue("DisplayVersion", (object)version, RegistryValueKind.String);
        }

        public void LicenseDownloaded(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    richTextBox1.Text = File.ReadAllText(path);
                    licenceCheckbox.Enabled = true;
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Failed to read license.\n" + ex.Message, "Download Manager Setup - Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry)
                    {
                        LicenseDownloaded(path);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Download Manager Setup - Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }

            }
            else
            {
                DialogResult result = MessageBox.Show("License failed to download.", "Download Manager Setup - Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    DownloadProgress progress = new DownloadProgress("https://raw.githubusercontent.com/Soniczac7/Download-Manager/master/LICENSE.txt", path, "16bea09b03d106138b4aaad4e7a42829");
                    progress.ShowDialog();
                }
                else
                {
                    MessageBox.Show("License failed to download.\nSetup cannot continue.", "Download Manager Setup - Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle button1
            if (licenceCheckbox.Checked)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (installing)
            {
                MessageBox.Show("Download Manager is currently installing on your computer.\nPlease wait until setup has completed to close.", "Download Manager Setup - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel the setup of Download Manager?", "Download Manager Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}