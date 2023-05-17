namespace DownloadManagerInstaller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button2 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            certCheckbox = new CheckBox();
            label6 = new Label();
            startMenuCheckbox = new CheckBox();
            label4 = new Label();
            desktopShortcutCheckbox = new CheckBox();
            label3 = new Label();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            toolTip1 = new ToolTip(components);
            tabControl2 = new TabControl();
            tabPage3 = new TabPage();
            richTextBox1 = new RichTextBox();
            licenceCheckbox = new CheckBox();
            groupBox1 = new GroupBox();
            progressBar1 = new ProgressBar();
            label5 = new Label();
            openCheckbox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.icon;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(29, 28);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(47, 12);
            label1.Name = "label1";
            label1.Size = new Size(185, 28);
            label1.TabIndex = 1;
            label1.Text = "Download Manager";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 50);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(499, 283);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(491, 255);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Location";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(464, 25);
            button2.Name = "button2";
            button2.Size = new Size(24, 23);
            button2.TabIndex = 2;
            button2.Text = "...";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 25);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(452, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "C:\\Download Manager\\";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = Cursors.Help;
            label2.Location = new Point(6, 7);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 0;
            label2.Text = "Install Location";
            toolTip1.SetToolTip(label2, "The location to install Download Manager.");
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(certCheckbox);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(startMenuCheckbox);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(desktopShortcutCheckbox);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(491, 255);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Components";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // certCheckbox
            // 
            certCheckbox.AutoSize = true;
            certCheckbox.Checked = true;
            certCheckbox.CheckState = CheckState.Checked;
            certCheckbox.Location = new Point(12, 116);
            certCheckbox.Name = "certCheckbox";
            certCheckbox.Size = new Size(15, 14);
            certCheckbox.TabIndex = 8;
            certCheckbox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Cursor = Cursors.Help;
            label6.Location = new Point(6, 97);
            label6.Name = "label6";
            label6.Size = new Size(258, 15);
            label6.TabIndex = 7;
            label6.Text = "Install Code Signing Certificate (recommended)";
            toolTip1.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // startMenuCheckbox
            // 
            startMenuCheckbox.AutoSize = true;
            startMenuCheckbox.Checked = true;
            startMenuCheckbox.CheckState = CheckState.Checked;
            startMenuCheckbox.Location = new Point(12, 71);
            startMenuCheckbox.Name = "startMenuCheckbox";
            startMenuCheckbox.Size = new Size(15, 14);
            startMenuCheckbox.TabIndex = 6;
            startMenuCheckbox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Cursor = Cursors.Help;
            label4.Location = new Point(6, 52);
            label4.Name = "label4";
            label4.Size = new Size(113, 15);
            label4.TabIndex = 5;
            label4.Text = "Start Menu Shortcut";
            toolTip1.SetToolTip(label4, "If checked the installer will create a shortcut in your start menu for Download Manager.");
            // 
            // desktopShortcutCheckbox
            // 
            desktopShortcutCheckbox.AutoSize = true;
            desktopShortcutCheckbox.Checked = true;
            desktopShortcutCheckbox.CheckState = CheckState.Checked;
            desktopShortcutCheckbox.Location = new Point(12, 26);
            desktopShortcutCheckbox.Name = "desktopShortcutCheckbox";
            desktopShortcutCheckbox.Size = new Size(15, 14);
            desktopShortcutCheckbox.TabIndex = 4;
            desktopShortcutCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.Help;
            label3.Location = new Point(6, 7);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 2;
            label3.Text = "Desktop Shortcut";
            toolTip1.SetToolTip(label3, "If checked the installer will create a shortcut on your desktop for Download Manager.");
            // 
            // button1
            // 
            button1.Location = new Point(436, 339);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Next";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // toolTip1
            // 
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.ToolTipTitle = "Help";
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Location = new Point(12, 50);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(499, 283);
            tabControl2.TabIndex = 3;
            tabControl2.Visible = false;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(richTextBox1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(491, 255);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "License";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(485, 249);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // licenceCheckbox
            // 
            licenceCheckbox.AutoSize = true;
            licenceCheckbox.Location = new Point(12, 343);
            licenceCheckbox.Name = "licenceCheckbox";
            licenceCheckbox.Size = new Size(135, 19);
            licenceCheckbox.TabIndex = 4;
            licenceCheckbox.Text = "I agree to the licence";
            licenceCheckbox.UseVisualStyleBackColor = true;
            licenceCheckbox.Visible = false;
            licenceCheckbox.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(progressBar1);
            groupBox1.Controls.Add(label5);
            groupBox1.Location = new Point(12, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(499, 283);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(6, 139);
            progressBar1.MarqueeAnimationSpeed = 10;
            progressBar1.Maximum = 120;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(483, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 1;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(109, 107);
            label5.Name = "label5";
            label5.Size = new Size(268, 15);
            label5.TabIndex = 0;
            label5.Text = "Please wait while setup installs the latest version...";
            // 
            // openCheckbox
            // 
            openCheckbox.AutoSize = true;
            openCheckbox.Checked = true;
            openCheckbox.CheckState = CheckState.Checked;
            openCheckbox.Location = new Point(12, 343);
            openCheckbox.Name = "openCheckbox";
            openCheckbox.Size = new Size(162, 19);
            openCheckbox.TabIndex = 6;
            openCheckbox.Text = "Open Download Manager";
            openCheckbox.UseVisualStyleBackColor = true;
            openCheckbox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 374);
            Controls.Add(licenceCheckbox);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(openCheckbox);
            Controls.Add(tabControl1);
            Controls.Add(groupBox1);
            Controls.Add(tabControl2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            Text = "Download Manager Setup";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabControl2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolTip toolTip1;
        private CheckBox startMenuCheckbox;
        private Label label4;
        private CheckBox desktopShortcutCheckbox;
        private Label label3;
        private TabControl tabControl2;
        private TabPage tabPage3;
        private RichTextBox richTextBox1;
        private CheckBox licenceCheckbox;
        private GroupBox groupBox1;
        private ProgressBar progressBar1;
        private Label label5;
        private CheckBox openCheckbox;
        private CheckBox certCheckbox;
        private Label label6;
    }
}