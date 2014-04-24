using System;
using System.Windows.Forms;

namespace CloudBerryDecrypter
{
	public partial class EditAmazonS3AccountForm : Form
	{
		private readonly MainForm _mainForm;

		public EditAmazonS3AccountForm(MainForm mainform)
		{
			_mainForm = mainform;

			InitializeComponent();
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			var form = new AmazonConnectionStatusForm();

			try
			{
				_mainForm.AmazonService = new AmazonService(txtS3Key.Text, txtS3Secret.Text);
			}
			catch
			{
				form.BackgroundImage = ImageResources.ConnectionFailed;
			}

			form.ShowDialog();
		}
	}
}