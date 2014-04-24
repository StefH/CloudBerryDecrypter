using System.IO;
using System.Net;

namespace CloudBerryDecrypter
{
	public class S3File
	{
		public string Key { get; private set; }
		public string SafeFilepath { get; private set; }
		public byte[] Content { get; private set; }
		public WebHeaderCollection Headers { get; private set; }

		public S3File(string key, byte[] content, WebHeaderCollection headers)
		{
			this.Key = key;
			this.Content = content;
			this.Headers = headers;

			this.SafeFilepath = AmazonService.GetSafeFilepath(key);
		}

		public string Filename
		{
			get
			{
				return Path.GetFileName(this.SafeFilepath);
			}
		}

		public string EncryptionInfo
		{
			get
			{
				return this.Headers[CloudBerryCryptoService.EncryptionInfo];
			}
		}
	}
}
