namespace CloudBerryDecrypter
{
	partial class EditAmazonS3AccountForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAmazonS3AccountForm));
			this.label1 = new System.Windows.Forms.Label();
			this.txtS3Secret = new System.Windows.Forms.TextBox();
			this.txtS3DisplayName = new System.Windows.Forms.TextBox();
			this.txtS3Key = new System.Windows.Forms.TextBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(58, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(191, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Specify values for Amazon S3 Account";
			// 
			// txtS3Secret
			// 
			this.txtS3Secret.Location = new System.Drawing.Point(120, 117);
			this.txtS3Secret.Name = "txtS3Secret";
			this.txtS3Secret.PasswordChar = '*';
			this.txtS3Secret.Size = new System.Drawing.Size(217, 20);
			this.txtS3Secret.TabIndex = 3;
			// 
			// txtS3DisplayName
			// 
			this.txtS3DisplayName.Location = new System.Drawing.Point(120, 64);
			this.txtS3DisplayName.Name = "txtS3DisplayName";
			this.txtS3DisplayName.Size = new System.Drawing.Size(217, 20);
			this.txtS3DisplayName.TabIndex = 1;
			// 
			// txtS3Key
			// 
			this.txtS3Key.Location = new System.Drawing.Point(120, 90);
			this.txtS3Key.Name = "txtS3Key";
			this.txtS3Key.Size = new System.Drawing.Size(217, 20);
			this.txtS3Key.TabIndex = 2;
			// 
			// btnTest
			// 
			this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTest.Location = new System.Drawing.Point(245, 153);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(93, 23);
			this.btnTest.TabIndex = 4;
			this.btnTest.Text = "Test Connection";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(174, 208);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(263, 208);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// EditAmazonS3AccountForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.BackgroundImage = global::CloudBerryDecrypter.ImageResources.EditAmazonS3Account;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(350, 247);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.txtS3Key);
			this.Controls.Add(this.txtS3DisplayName);
			this.Controls.Add(this.txtS3Secret);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditAmazonS3AccountForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Edit Amazon S3 Account";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.TextBox txtS3Secret;
		public System.Windows.Forms.TextBox txtS3DisplayName;
		public System.Windows.Forms.TextBox txtS3Key;
	}
}