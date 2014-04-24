using System;
using System.Windows.Forms;

namespace CloudBerryDecrypter
{
	public partial class EditAmazonS3AccountForm : Form
	{
		private MainForm MainForm;

		public EditAmazonS3AccountForm(MainForm mainform)
		{
			this.MainForm = mainform;

			InitializeComponent();
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			var form = new AmazonConnectionStatusForm();

			try
			{
				this.MainForm.AmazonService = new AmazonService(this.txtS3Key.Text, this.txtS3Secret.Text);
			}
			catch
			{
				form.BackgroundImage = global::CloudBerryDecrypter.ImageResources.ConnectionFailed;
			}

			form.ShowDialog();
		}
	}
}
