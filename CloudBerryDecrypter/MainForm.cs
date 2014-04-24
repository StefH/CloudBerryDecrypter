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
		private readonly AppCryptoService _appCryptoService = new AppCryptoService();
		private EditAmazonS3AccountForm _editAmazonS3Account;
		private long _totalSize;
		private int _numFiles;

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

			_editAmazonS3Account = new EditAmazonS3AccountForm(this);
		}

		private void RefreshFiles()
		{
			if (AmazonService == null)
			{
				CreateAmazonService();
			}

			btnRefresh.Text = "Busy...";
			btnRefresh.Enabled = false;
			BeginBlock();

			treeView.Nodes.Clear();
			_totalSize = 0;
			_numFiles = 0;
			lblTotalSizeValue.Text = Utils.Utils.StrFormatByteSize(0);
			lblSelectedFilesValue.Text = "0";
			backgroundWorkerListObjects.RunWorkerAsync(AmazonService);
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(S3Key) || string.IsNullOrWhiteSpace(S3Secret))
			{
				DialogResult result = _editAmazonS3Account.ShowDialog();

				if (result == DialogResult.OK)
				{
					S3DisplayName = _editAmazonS3Account.txtS3DisplayName.Text;
					S3Key = _editAmazonS3Account.txtS3Key.Text;
					S3Secret = _editAmazonS3Account.txtS3Secret.Text;

					CreateAmazonService();
				}
			}
			else
			{
				RefreshFiles();
			}
		}

		private void BeginBlock()
		{
			Cursor = Cursors.WaitCursor;
			treeView.Enabled = false;
			btnRefresh.Enabled = false;
			btnBrowseOutputFolder.Enabled = false;
			chkUnzip.Enabled = false;
			chkCreateFolders.Enabled = false;
			txtPassword.Enabled = false;
			txtOutputFolder.Enabled = false;
		}

		private void EndBlock()
		{
			Cursor = Cursors.Default;
			btnRefresh.Enabled = true;
			btnBrowseOutputFolder.Enabled = true;
			chkUnzip.Enabled = true;
			chkCreateFolders.Enabled = true;
			treeView.Enabled = true;
			txtPassword.Enabled = true;
			txtOutputFolder.Enabled = true;
		}

		private void btnGo_Click(object sender, EventArgs args)
		{
			string password = txtPassword.Text;
			string outputFolder = txtOutputFolder.Text;

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
					BeginBlock();

					backgroundWorker.RunWorkerAsync(new ProcessSettings
					{
						AmazonService = AmazonService,
						CloudBerryPassword = password,
						S3Objects = selectedNodes.Where(n => n.Tag is S3Object).Select(n => n.Tag as S3Object).ToList(),
						CreateFolders = chkCreateFolders.Checked,
						Unzip = chkUnzip.Checked,
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
			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				txtOutputFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		#region Save / Load
		private void MainForm_Load(object sender, EventArgs e)
		{
			txtPassword.Text = _appCryptoService.DecryptText(Settings.Default.Password);
			txtOutputFolder.Text = Settings.Default.OutputFolder;
			chkUnzip.Checked = Settings.Default.UnzipFiles;
			chkCreateFolders.Checked = Settings.Default.CreateFolders;

			S3DisplayName = Settings.Default.S3DisplayName;
			S3Key = _appCryptoService.DecryptText(Settings.Default.S3Key);
			S3Secret = _appCryptoService.DecryptText(Settings.Default.S3Secret);
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.Password = _appCryptoService.EncryptText(txtPassword.Text);
			Settings.Default.S3Key = _appCryptoService.EncryptText(S3Key);
			Settings.Default.S3Secret = _appCryptoService.EncryptText(S3Secret);
			Settings.Default.S3DisplayName = S3DisplayName;
			Settings.Default.OutputFolder = txtOutputFolder.Text;
			Settings.Default.UnzipFiles = chkUnzip.Checked;
			Settings.Default.CreateFolders = chkCreateFolders.Checked;

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
						var fileNode = new TreeNode(nodeText)
						{
							Tag = s3Object
						};
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
				CheckAllChildNodes(childNode, nodeChecked, ref totalSize, ref numFiles);
			}

			lblSelectedFilesValue.Text = numFiles.ToString();
			lblTotalSizeValue.Text = Utils.Utils.StrFormatByteSize(_totalSize);
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			// The code only executes if the user caused the checked state to change.
			if (e.Action != TreeViewAction.Unknown)
			{
				// Calls the CheckAllChildNodes method, passing in the current 
				// Checked value of the TreeNode whose checked state changed.
				CheckAllChildNodes(e.Node, e.Node.Checked, ref _totalSize, ref _numFiles);
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
				RefreshFiles();
			}
		}
		#endregion

		private void CreateAmazonService()
		{
			try
			{
				AmazonService = new AmazonService(S3Key, S3Secret);
				lblStatusValue.Text = string.Format("Connected to Amazon S3 '{0}'", S3DisplayName);
				lblStatusValue.ForeColor = Color.Green;
			}
			catch
			{
				lblStatusValue.Text = string.Format("Error connecting to Amazon S3 '{0}'", S3DisplayName);
				lblStatusValue.ForeColor = Color.Red;
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
			progressBar.Value = e.Cancelled ? 0 : 100;

			btnGo.Enabled = true;
			btnGo.Text = "Decrypt";

			EndBlock();
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
			btnRefresh.Enabled = true;
			btnRefresh.Text = "Refresh";
			EndBlock();
		}
		#endregion BackgroundWorker

		private void amazonS3AccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_editAmazonS3Account.txtS3DisplayName.Text = S3DisplayName;
			_editAmazonS3Account.txtS3Key.Text = S3Key;
			_editAmazonS3Account.txtS3Secret.Text = S3Secret;
			DialogResult result = _editAmazonS3Account.ShowDialog();

			if (result == DialogResult.OK)
			{
				S3DisplayName = _editAmazonS3Account.txtS3DisplayName.Text;
				S3Key = _editAmazonS3Account.txtS3Key.Text;
				S3Secret = _editAmazonS3Account.txtS3Secret.Text;

				CreateAmazonService();
			}
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			var chk = sender as CheckBox;

			foreach (TreeNode node in treeView.Nodes)
			{

				node.Checked = chk.Checked;
				CheckAllChildNodes(node, node.Checked, ref _totalSize, ref _numFiles);
			}

			if (!chk.Checked)
			{

			}
		}
	}
}