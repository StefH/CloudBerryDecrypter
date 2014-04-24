using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Amazon.S3.Model;
using CloudBerryDecrypter.Properties;

namespace CloudBerryDecrypter
{
	public partial class MainForm : Form
	{
		private string S3DisplayName;
		private string S3Key;
		private string S3Secret;
		public AmazonService AmazonService;
		private AppCryptoService AppCryptoService = new AppCryptoService();
		private EditAmazonS3AccountForm EditAmazonS3Account;
		private long TotalSize = 0;
		private int numFiles = 0;

		public MainForm()
		{
			InitializeComponent();

			Init();
		}

		private void Init()
		{
			Text += string.Format(" [{0}]", Assembly.GetExecutingAssembly().GetName().Version);

			treeView.Nodes.Clear();
			var refresh = new TreeNode
			{
				Text = "Click Refresh ...",
				ImageKey = "Refresh",
				Tag = "Refresh"
			};

			treeView.Nodes.Add(refresh);

			EditAmazonS3Account = new EditAmazonS3AccountForm(this);
		}

		private void RefreshFiles()
		{
			if (this.AmazonService == null)
			{
				this.CreateAmazonService();
			}

			this.btnRefresh.Text = "Busy...";
			this.btnRefresh.Enabled = false;
			this.BeginBlock();

			treeView.Nodes.Clear();
			this.TotalSize = 0;
			this.numFiles = 0;
			this.lblTotalSizeValue.Text = Utils.Utils.StrFormatByteSize(0);
			this.lblSelectedFilesValue.Text = "0";
			backgroundWorkerListObjects.RunWorkerAsync(this.AmazonService);
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.S3Key) || string.IsNullOrWhiteSpace(this.S3Secret))
			{
				DialogResult result = this.EditAmazonS3Account.ShowDialog();

				if (result == DialogResult.OK)
				{
					this.S3DisplayName = this.EditAmazonS3Account.txtS3DisplayName.Text;
					this.S3Key = this.EditAmazonS3Account.txtS3Key.Text;
					this.S3Secret = this.EditAmazonS3Account.txtS3Secret.Text;

					this.CreateAmazonService();
				}
			}
			else
			{
				RefreshFiles();
			}
		}

		private void BeginBlock()
		{
			this.Cursor = Cursors.WaitCursor;
			this.treeView.Enabled = false;
			this.btnRefresh.Enabled = false;
			this.btnBrowseOutputFolder.Enabled = false;
			this.chkUnzip.Enabled = false;
			this.chkCreateFolders.Enabled = false;
			this.txtPassword.Enabled = false;
			this.txtOutputFolder.Enabled = false;
		}

		private void EndBlock()
		{
			this.Cursor = Cursors.Default;
			this.btnRefresh.Enabled = true;
			this.btnBrowseOutputFolder.Enabled = true;
			this.chkUnzip.Enabled = true;
			this.chkCreateFolders.Enabled = true;
			this.treeView.Enabled = true;
			this.txtPassword.Enabled = true;
			this.txtOutputFolder.Enabled = true;
		}

		private void btnGo_Click(object sender, EventArgs args)
		{
			string password = this.txtPassword.Text;
			string outputFolder = this.txtOutputFolder.Text;

			var selectedNodes = new List<TreeNode>();
			GetCheckedNodes(treeView.Nodes, selectedNodes);

			if (!string.IsNullOrWhiteSpace(password) &&
				selectedNodes.Count > 0 &&
				!string.IsNullOrWhiteSpace(outputFolder))
			{
				if (backgroundWorker.IsBusy)
				{
					backgroundWorker.CancelAsync();
				}
				else
				{
					this.BeginBlock();

					backgroundWorker.RunWorkerAsync(new ProcessSettings
					{
						AmazonService = this.AmazonService,
						CloudBerryPassword = password,
						S3Objects = selectedNodes.Where(n => n.Tag is S3Object).Select(n => n.Tag as S3Object).ToList(),
						CreateFolders = this.chkCreateFolders.Checked,
						Unzip = this.chkUnzip.Checked,
						OutputFolder = outputFolder
					});

					btnGo.Text = "Cancel";
				}
			}
			else
			{
				MessageBox.Show("No files selected or Password is missing or Output Folder is missing.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnBrowseOutputFolder_Click(object sender, EventArgs e)
		{
			DialogResult result = this.folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				this.txtOutputFolder.Text = this.folderBrowserDialog.SelectedPath;
			}
		}

		#region Save / Load
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.txtPassword.Text = this.AppCryptoService.DecryptText(Settings.Default.Password);
			this.txtOutputFolder.Text = Settings.Default.OutputFolder;
			this.chkUnzip.Checked = Settings.Default.UnzipFiles;
			this.chkCreateFolders.Checked = Settings.Default.CreateFolders;

			this.S3DisplayName = Settings.Default.S3DisplayName;
			this.S3Key = this.AppCryptoService.DecryptText(Settings.Default.S3Key);
			this.S3Secret = this.AppCryptoService.DecryptText(Settings.Default.S3Secret);
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.Password = this.AppCryptoService.EncryptText(this.txtPassword.Text);
			Settings.Default.S3Key = this.AppCryptoService.EncryptText(this.S3Key);
			Settings.Default.S3Secret = this.AppCryptoService.EncryptText(this.S3Secret);
			Settings.Default.S3DisplayName = this.S3DisplayName;
			Settings.Default.OutputFolder = this.txtOutputFolder.Text;
			Settings.Default.UnzipFiles = this.chkUnzip.Checked;
			Settings.Default.CreateFolders = this.chkCreateFolders.Checked;

			Settings.Default.Save();
		}
		#endregion

		#region TreeView
		public void RefreshTreeView(IEnumerable<S3Object> s3Objects)
		{
			foreach (var s3Object in s3Objects)
			{
				string[] elements = (s3Object.BucketName + "/" + AmazonService.GetSafeFilepath(s3Object.Key)).Split('/');

				TreeNode parentNode = null;

				for (int i = 0; i < elements.Length - 1; ++i)
				{
					if (parentNode == null)
					{
						bool exists = false;
						foreach (TreeNode node in treeView.Nodes)
						{
							if (node.Text == elements[i])
							{
								exists = true;
								parentNode = node;
							}
						}

						if (!exists)
						{
							var bucketNode = new TreeNode(elements[i]);
							bucketNode.ImageKey = bucketNode.SelectedImageKey = "HardDiskWithKey";

							treeView.Nodes.Add(bucketNode);
							parentNode = bucketNode;
						}
					}
					else
					{
						bool exists = false;

						foreach (TreeNode node in parentNode.Nodes)
						{
							if (node.Text == elements[i])
							{
								exists = true;
								parentNode = node;
							}
						}

						if (!exists)
						{
							var folderNode = new TreeNode(elements[i]);
							folderNode.ImageKey = folderNode.SelectedImageKey = "Folder";

							parentNode.Nodes.Add(folderNode);
							parentNode = folderNode;
						}
					}
				}

				if (parentNode != null)
				{
					string text = elements[elements.Length - 1];

					if (!string.IsNullOrEmpty(text))
					{
						string nodeText = string.Format("{0} ({1})", text, Utils.Utils.StrFormatByteSize(s3Object.Size));
						var fileNode = new TreeNode(nodeText);
						fileNode.Tag = s3Object;
						fileNode.ImageKey = fileNode.SelectedImageKey = "File";
						parentNode.Nodes.Add(fileNode);
					}
				}
			}
		}

		// Updates all child tree nodes recursively.
		private void CheckAllChildNodes(TreeNode node, bool nodeChecked, ref long totalSize, ref int numFiles)
		{
			var fileNode = node.ImageKey == "File" ? node.Tag as S3Object : null;
			if (fileNode != null)
			{
				int upOrDown = (nodeChecked ? 1 : -1);

				totalSize += upOrDown * fileNode.Size;
				numFiles += upOrDown;
			}

			foreach (TreeNode childNode in node.Nodes)
			{
				childNode.Checked = nodeChecked;

				// If the current node has child nodes, call the CheckAllChildsNodes method recursively.
				this.CheckAllChildNodes(childNode, nodeChecked, ref totalSize, ref numFiles);
			}

			this.lblSelectedFilesValue.Text = numFiles.ToString();
			this.lblTotalSizeValue.Text = Utils.Utils.StrFormatByteSize(TotalSize);
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			// The code only executes if the user caused the checked state to change.
			if (e.Action != TreeViewAction.Unknown)
			{
				// Calls the CheckAllChildNodes method, passing in the current 
				// Checked value of the TreeNode whose checked state changed.
				this.CheckAllChildNodes(e.Node, e.Node.Checked, ref TotalSize, ref numFiles);
			}
		}

		private void GetCheckedNodes(TreeNodeCollection nodes, List<TreeNode> checkedNodes)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Checked)
				{
					checkedNodes.Add(node);
				}

				GetCheckedNodes(node.Nodes, checkedNodes);
			}
		}

		private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			string tag = e.Node.Tag as string;
			if (tag == "Refresh")
			{
				this.RefreshFiles();
			}
		}
		#endregion

		private void CreateAmazonService()
		{
			try
			{
				this.AmazonService = new AmazonService(this.S3Key, this.S3Secret);
				this.lblStatusValue.Text = string.Format("Connected to Amazon S3 '{0}'", this.S3DisplayName);
				this.lblStatusValue.ForeColor = Color.Green;
			}
			catch
			{
				this.lblStatusValue.Text = string.Format("Error connecting to Amazon S3 '{0}'", this.S3DisplayName);
				this.lblStatusValue.ForeColor = Color.Red;
			}
		}

		#region BackgroundWorker
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var settings = e.Argument as ProcessSettings;

			try
			{
				var srv = new CloudBerryCryptoService(settings.CloudBerryPassword);

				// Decrypt and Unzip files
				int count = 0;
				foreach (var file in settings.S3Objects)
				{
					if (backgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}

					var s3File = settings.AmazonService.GetS3File(file);

					srv.ProcessFile(s3File.Content, s3File.SafeFilepath, s3File.EncryptionInfo, settings.OutputFolder,
						settings.CreateFolders, settings.Unzip);

					backgroundWorker.ReportProgress((100 * count) / settings.S3Objects.Count);
					count++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("There was an error during decryption / unzipping : {0}", ex.Message),
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

				e.Cancel = true;
			}
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				progressBar.Value = 0;
			}
			else
			{
				progressBar.Value = 100;
			}

			this.btnGo.Enabled = true;
			this.btnGo.Text = "Decrypt";

			this.EndBlock();
		}

		private void backgroundWorkerListObjects_DoWork(object sender, DoWorkEventArgs e)
		{
			var amazonService = e.Argument as AmazonService;

			//var keys = new List<S3Object>();
			int count = 0;
			var buckets = amazonService.GetBuckets();
			foreach (var bucket in buckets)
			{
				var objects = AmazonService.ListObjects(bucket);
				//keys.AddRange(objects);

				backgroundWorkerListObjects.ReportProgress((100 * count) / buckets.Count, objects);
				count++;
			}
		}

		private void backgroundWorkerListObjects_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			var objects = e.UserState as ReadOnlyCollection<S3Object>;

			RefreshTreeView(objects);
		}

		private void backgroundWorkerListObjects_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.btnRefresh.Enabled = true;
			this.btnRefresh.Text = "Refresh";
			this.EndBlock();
		}
		#endregion BackgroundWorker

		private void amazonS3AccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.EditAmazonS3Account.txtS3DisplayName.Text = this.S3DisplayName;
			this.EditAmazonS3Account.txtS3Key.Text = this.S3Key;
			this.EditAmazonS3Account.txtS3Secret.Text = this.S3Secret;
			DialogResult result = this.EditAmazonS3Account.ShowDialog();

			if (result == DialogResult.OK)
			{
				this.S3DisplayName = this.EditAmazonS3Account.txtS3DisplayName.Text;
				this.S3Key = this.EditAmazonS3Account.txtS3Key.Text;
				this.S3Secret = this.EditAmazonS3Account.txtS3Secret.Text;

				this.CreateAmazonService();
			}
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			var chk = sender as CheckBox;

			foreach (TreeNode node in this.treeView.Nodes)
			{

				node.Checked = chk.Checked;
				this.CheckAllChildNodes(node, node.Checked, ref this.TotalSize, ref this.numFiles);
			}

			if (!chk.Checked)
			{

			}
		}
	}
}