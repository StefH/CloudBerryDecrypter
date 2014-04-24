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
			Key = key;
			Content = content;
			Headers = headers;

			SafeFilepath = AmazonService.GetSafeFilepath(key);
		}

		public string Filename
		{
			get
			{
				return Path.GetFileName(SafeFilepath);
			}
		}

		public string EncryptionInfo
		{
			get
			{
				return Headers[CloudBerryCryptoService.EncryptionInfo];
			}
		}
	}
}