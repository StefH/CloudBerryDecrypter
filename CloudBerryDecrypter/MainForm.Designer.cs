namespace CloudBerryDecrypter
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("File.txt");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Folder", new System.Windows.Forms.TreeNode[] {
            treeNode1});
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Bucket", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.btnGo = new System.Windows.Forms.Button();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.lblOutputFolder = new System.Windows.Forms.Label();
			this.btnBrowseOutputFolder = new System.Windows.Forms.Button();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.lblUnzip = new System.Windows.Forms.Label();
			this.chkUnzip = new System.Windows.Forms.CheckBox();
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList_16x16_32Bit = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.amazonS3AccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.amazonsS3Menu = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.s3CredentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblStatusValue = new System.Windows.Forms.Label();
			this.chkCreateFolders = new System.Windows.Forms.CheckBox();
			this.lblCreateFolders = new System.Windows.Forms.Label();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.imageList_32x32_32Bit = new System.Windows.Forms.ImageList(this.components);
			this.backgroundWorkerListObjects = new System.ComponentModel.BackgroundWorker();
			this.lblSelectedFiles = new System.Windows.Forms.Label();
			this.lblTotalSize = new System.Windows.Forms.Label();
			this.lblSelectedFilesValue = new System.Windows.Forms.Label();
			this.lblTotalSizeValue = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.menuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(157, 17);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(300, 20);
			this.txtPassword.TabIndex = 3;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(8, 20);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 3;
			this.lblPassword.Text = "Password:";
			// 
			// btnGo
			// 
			this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGo.Location = new System.Drawing.Point(408, 128);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 100;
			this.btnGo.Text = "Decrypt";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Location = new System.Drawing.Point(157, 57);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.Size = new System.Drawing.Size(300, 20);
			this.txtOutputFolder.TabIndex = 4;
			// 
			// lblOutputFolder
			// 
			this.lblOutputFolder.AutoSize = true;
			this.lblOutputFolder.Location = new System.Drawing.Point(8, 60);
			this.lblOutputFolder.Name = "lblOutputFolder";
			this.lblOutputFolder.Size = new System.Drawing.Size(74, 13);
			this.lblOutputFolder.TabIndex = 9;
			this.lblOutputFolder.Text = "Output Folder:";
			// 
			// btnBrowseOutputFolder
			// 
			this.btnBrowseOutputFolder.Location = new System.Drawing.Point(459, 56);
			this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
			this.btnBrowseOutputFolder.Size = new System.Drawing.Size(24, 22);
			this.btnBrowseOutputFolder.TabIndex = 5;
			this.btnBrowseOutputFolder.Text = "...";
			this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
			this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowseOutputFolder_Click);
			// 
			// lblUnzip
			// 
			this.lblUnzip.AutoSize = true;
			this.lblUnzip.Location = new System.Drawing.Point(8, 99);
			this.lblUnzip.Name = "lblUnzip";
			this.lblUnzip.Size = new System.Drawing.Size(108, 13);
			this.lblUnzip.TabIndex = 11;
			this.lblUnzip.Text = "Unzip GZipped file(s):";
			// 
			// chkUnzip
			// 
			this.chkUnzip.AutoSize = true;
			this.chkUnzip.Checked = true;
			this.chkUnzip.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUnzip.Location = new System.Drawing.Point(159, 99);
			this.chkUnzip.Name = "chkUnzip";
			this.chkUnzip.Size = new System.Drawing.Size(15, 14);
			this.chkUnzip.TabIndex = 10;
			this.chkUnzip.UseVisualStyleBackColor = true;
			// 
			// treeView
			// 
			this.treeView.CheckBoxes = true;
			this.treeView.ImageKey = "HardDiskWithKey";
			this.treeView.ImageList = this.imageList_16x16_32Bit;
			this.treeView.Indent = 20;
			this.treeView.Location = new System.Drawing.Point(9, 32);
			this.treeView.Name = "treeView";
			treeNode1.ImageKey = "File";
			treeNode1.Name = "File.txt";
			treeNode1.SelectedImageKey = "File";
			treeNode1.Text = "File.txt";
			treeNode2.ImageKey = "Folder";
			treeNode2.Name = "Folder";
			treeNode2.SelectedImageKey = "Folder";
			treeNode2.Text = "Folder";
			treeNode3.ImageKey = "HardDiskWithKey";
			treeNode3.Name = "Bucket";
			treeNode3.SelectedImageKey = "HardDiskWithKey";
			treeNode3.Text = "Bucket";
			this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
			this.treeView.SelectedImageKey = "HardDiskWithKey";
			this.treeView.Size = new System.Drawing.Size(474, 294);
			this.treeView.TabIndex = 1;
			this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			// 
			// imageList_16x16_32Bit
			// 
			this.imageList_16x16_32Bit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_16x16_32Bit.ImageStream")));
			this.imageList_16x16_32Bit.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_16x16_32Bit.Images.SetKeyName(0, "Folder");
			this.imageList_16x16_32Bit.Images.SetKeyName(1, "File");
			this.imageList_16x16_32Bit.Images.SetKeyName(2, "Refresh");
			this.imageList_16x16_32Bit.Images.SetKeyName(3, "Drive");
			this.imageList_16x16_32Bit.Images.SetKeyName(4, "HardDiskWithKey");
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1});
			this.menuStrip.Location = new System.Drawing.Point(2, 2);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip.Size = new System.Drawing.Size(498, 24);
			this.menuStrip.TabIndex = 14;
			this.menuStrip.Text = "Menu";
			// 
			// optionsToolStripMenuItem1
			// 
			this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amazonS3AccountToolStripMenuItem});
			this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
			this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem1.Text = "&Options";
			// 
			// amazonS3AccountToolStripMenuItem
			// 
			this.amazonS3AccountToolStripMenuItem.Name = "amazonS3AccountToolStripMenuItem";
			this.amazonS3AccountToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.amazonS3AccountToolStripMenuItem.Text = "Amazon S3 Account";
			this.amazonS3AccountToolStripMenuItem.Click += new System.EventHandler(this.amazonS3AccountToolStripMenuItem_Click);
			// 
			// optionsMenu
			// 
			this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amazonsS3Menu});
			this.optionsMenu.Name = "optionsMenu";
			this.optionsMenu.Size = new System.Drawing.Size(61, 20);
			this.optionsMenu.Text = "&Options";
			// 
			// amazonsS3Menu
			// 
			this.amazonsS3Menu.Name = "amazonsS3Menu";
			this.amazonsS3Menu.Size = new System.Drawing.Size(78, 22);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.s3CredentialsToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// s3CredentialsToolStripMenuItem
			// 
			this.s3CredentialsToolStripMenuItem.Name = "s3CredentialsToolStripMenuItem";
			this.s3CredentialsToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnRefresh.ImageKey = "Refresh";
			this.btnRefresh.ImageList = this.imageList_16x16_32Bit;
			this.btnRefresh.Location = new System.Drawing.Point(9, 332);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(72, 23);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(6, 16);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(46, 13);
			this.lblStatus.TabIndex = 17;
			this.lblStatus.Text = "Status : ";
			// 
			// lblStatusValue
			// 
			this.lblStatusValue.AutoSize = true;
			this.lblStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatusValue.Location = new System.Drawing.Point(99, 16);
			this.lblStatusValue.Name = "lblStatusValue";
			this.lblStatusValue.Size = new System.Drawing.Size(11, 13);
			this.lblStatusValue.TabIndex = 18;
			this.lblStatusValue.Text = "-";
			// 
			// chkCreateFolders
			// 
			this.chkCreateFolders.AutoSize = true;
			this.chkCreateFolders.Checked = true;
			this.chkCreateFolders.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCreateFolders.Location = new System.Drawing.Point(159, 130);
			this.chkCreateFolders.Name = "chkCreateFolders";
			this.chkCreateFolders.Size = new System.Drawing.Size(15, 14);
			this.chkCreateFolders.TabIndex = 11;
			this.chkCreateFolders.UseVisualStyleBackColor = true;
			// 
			// lblCreateFolders
			// 
			this.lblCreateFolders.AutoSize = true;
			this.lblCreateFolders.Location = new System.Drawing.Point(8, 130);
			this.lblCreateFolders.Name = "lblCreateFolders";
			this.lblCreateFolders.Size = new System.Drawing.Size(114, 13);
			this.lblCreateFolders.TabIndex = 19;
			this.lblCreateFolders.Text = "Create folder structure:";
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(9, 162);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(474, 23);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 101;
			// 
			// imageList_32x32_32Bit
			// 
			this.imageList_32x32_32Bit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_32x32_32Bit.ImageStream")));
			this.imageList_32x32_32Bit.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_32x32_32Bit.Images.SetKeyName(0, "CloudBerryExplorer");
			// 
			// backgroundWorkerListObjects
			// 
			this.backgroundWorkerListObjects.WorkerReportsProgress = true;
			this.backgroundWorkerListObjects.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerListObjects_DoWork);
			this.backgroundWorkerListObjects.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerListObjects_ProgressChanged);
			this.backgroundWorkerListObjects.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerListObjects_RunWorkerCompleted);
			// 
			// lblSelectedFiles
			// 
			this.lblSelectedFiles.AutoSize = true;
			this.lblSelectedFiles.Location = new System.Drawing.Point(362, 329);
			this.lblSelectedFiles.Name = "lblSelectedFiles";
			this.lblSelectedFiles.Size = new System.Drawing.Size(76, 13);
			this.lblSelectedFiles.TabIndex = 102;
			this.lblSelectedFiles.Text = "Selected Files:";
			// 
			// lblTotalSize
			// 
			this.lblTotalSize.AutoSize = true;
			this.lblTotalSize.Location = new System.Drawing.Point(362, 348);
			this.lblTotalSize.Name = "lblTotalSize";
			this.lblTotalSize.Size = new System.Drawing.Size(57, 13);
			this.lblTotalSize.TabIndex = 103;
			this.lblTotalSize.Text = "Total Size:";
			// 
			// lblSelectedFilesValue
			// 
			this.lblSelectedFilesValue.AutoSize = true;
			this.lblSelectedFilesValue.Location = new System.Drawing.Point(444, 329);
			this.lblSelectedFilesValue.Name = "lblSelectedFilesValue";
			this.lblSelectedFilesValue.Size = new System.Drawing.Size(13, 13);
			this.lblSelectedFilesValue.TabIndex = 104;
			this.lblSelectedFilesValue.Text = "0";
			// 
			// lblTotalSizeValue
			// 
			this.lblTotalSizeValue.AutoSize = true;
			this.lblTotalSizeValue.Location = new System.Drawing.Point(444, 348);
			this.lblTotalSizeValue.Name = "lblTotalSizeValue";
			this.lblTotalSizeValue.Size = new System.Drawing.Size(13, 13);
			this.lblTotalSizeValue.TabIndex = 105;
			this.lblTotalSizeValue.Text = "0";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkSelectAll);
			this.groupBox1.Controls.Add(this.lblStatus);
			this.groupBox1.Controls.Add(this.lblSelectedFilesValue);
			this.groupBox1.Controls.Add(this.lblTotalSizeValue);
			this.groupBox1.Controls.Add(this.lblSelectedFiles);
			this.groupBox1.Controls.Add(this.lblStatusValue);
			this.groupBox1.Controls.Add(this.treeView);
			this.groupBox1.Controls.Add(this.lblTotalSize);
			this.groupBox1.Controls.Add(this.btnRefresh);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(2, 26);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(498, 372);
			this.groupBox1.TabIndex = 106;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Amazon S3";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblPassword);
			this.groupBox2.Controls.Add(this.txtPassword);
			this.groupBox2.Controls.Add(this.progressBar);
			this.groupBox2.Controls.Add(this.btnGo);
			this.groupBox2.Controls.Add(this.chkCreateFolders);
			this.groupBox2.Controls.Add(this.txtOutputFolder);
			this.groupBox2.Controls.Add(this.lblCreateFolders);
			this.groupBox2.Controls.Add(this.lblOutputFolder);
			this.groupBox2.Controls.Add(this.chkUnzip);
			this.groupBox2.Controls.Add(this.btnBrowseOutputFolder);
			this.groupBox2.Controls.Add(this.lblUnzip);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(2, 407);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(498, 204);
			this.groupBox2.TabIndex = 107;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Decryption";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.Location = new System.Drawing.Point(157, 336);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(123, 17);
			this.chkSelectAll.TabIndex = 106;
			this.chkSelectAll.Text = "Select / Deselect All";
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(502, 613);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CloudBerry File(s) Decrypter";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.Label lblOutputFolder;
		private System.Windows.Forms.Button btnBrowseOutputFolder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Label lblUnzip;
		private System.Windows.Forms.CheckBox chkUnzip;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem s3CredentialsToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList_16x16_32Bit;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.ToolStripMenuItem optionsMenu;
		private System.Windows.Forms.ToolStripMenuItem amazonsS3Menu;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblStatusValue;
		private System.Windows.Forms.CheckBox chkCreateFolders;
		private System.Windows.Forms.Label lblCreateFolders;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.ImageList imageList_32x32_32Bit;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem amazonS3AccountToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker backgroundWorkerListObjects;
		private System.Windows.Forms.Label lblSelectedFiles;
		private System.Windows.Forms.Label lblTotalSize;
		private System.Windows.Forms.Label lblSelectedFilesValue;
		private System.Windows.Forms.Label lblTotalSizeValue;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkSelectAll;
	}
}

