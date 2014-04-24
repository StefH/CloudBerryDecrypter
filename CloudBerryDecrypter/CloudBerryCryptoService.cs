using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace CloudBerryDecrypter
{
	/*
	 * x-amz-meta-cb-encryptioninfo
	 * 
	 * <version>;<sourcesize>;<algo>;<keysize_in_bits>;<Base64encodedIV>;;<Compression>;
	 * Use semicolon character (';') is used as a separator between fields (all fields a positioned for version 1)
	 *
	 * <version> - Currently only version 1 is supported.
	 * <sourcesize> - is the size of unencrypted/uncompressed file
	 * <algo> - one of these values: AES, TripleDES, DES or RC2
	 * <keysize_in_bits> - the size of the encryption key in bits. For example, for AES 128 (in tools->options), the size is 128.
	 * <Base64encodedIV> - the Base64 encoded IV. It's different for each file. So there's no way to decrypt several files at once with specifying only one IV.
	 * It must be specified for each file. If you specify wrong IV, the beginning of the file will be garbled, though you won't receive any errors.
	 * <Compression> - either empty value or GZip
	 */
	public class CloudBerryCryptoService : CryptoService
	{
		public const string EncryptionInfo = "x-amz-meta-cb-encryptioninfo";

		private static byte[] CBSalt = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
		private static int CBIterations = 1000;

		public CloudBerryCryptoService(string password)
			: base(password, CBSalt, CBIterations)
		{
		}

		protected override DeriveBytes GetDeriveKey()
		{
			return new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(base.Password), base.Salt, base.Iterations);
		}

		public void ProcessFile(byte[] content, string filePath, string encryptionInfo, string outputFolder, bool createFolders, bool unzip)
		{
			bool isCloudBerryEncrypted = false;

			Algo Algo = Algo.None;
			int keySize = 0;
			byte[] iv = null;
			string compression = null;

			try
			{
				var fields = encryptionInfo.Split(';');
				int version = Convert.ToInt32(fields[0]);
				long SourceSize = Convert.ToInt64(fields[1]);
				Enum.TryParse(fields[2], out Algo);
				keySize = Convert.ToInt32(fields[3]);
				iv = Convert.FromBase64String(fields[4]);
				compression = fields[6];

				isCloudBerryEncrypted = true;
			}
			catch
			{
			}

			byte[] decryptedBytes = isCloudBerryEncrypted ? base.Decrypt(Algo, keySize, iv, content) : content;

			string folderName = null;

			if (createFolders)
			{
				folderName = Path.Combine(outputFolder, Path.GetDirectoryName(filePath));
				if (!Directory.Exists(folderName))
				{
					Directory.CreateDirectory(folderName);
				}
			}
			else
			{
				folderName = outputFolder;
			}

			string outputFilename = Path.Combine(folderName, Path.GetFileName(filePath));

			// (decryptedBytes[0] == 0x1f && decryptedBytes[1] == 0x8b

			// Check if file is GZIP compressed
			if (decryptedBytes.Length > 0 && compression == "GZip")
			{
				if (unzip)
				{
					// Unzip the file
					using (var fileStream = File.OpenWrite(outputFilename))
					{
						using (var stream = new MemoryStream(decryptedBytes))
						{
							using (var gzipStream = new GZipStream(stream, CompressionMode.Decompress))
							{
								gzipStream.CopyTo(fileStream);
							}
						}
					}
				}
				else
				{
					// Add .gz extension
					File.WriteAllBytes(outputFilename + ".gz", decryptedBytes);
				}
			}
			else
			{
				File.WriteAllBytes(outputFilename, decryptedBytes);
			}
		}

		/*
		public void ProcessFiles(string[] files, string outputFolder, bool unzip)
		{
			foreach (string filePath in files)
			{
				string filename = Path.GetFileName(filePath);
				byte[] encryptedBytes = File.ReadAllBytes(filePath);
				byte[] decryptedBytes = base.Decrypt(this.Algo, encryptedBytes);

				string outputFilename = Path.Combine(outputFolder, filename);

				// Check if file is GZIP compressed
				if (decryptedBytes.Length > 0 &&
					((decryptedBytes[0] == 0x1f && decryptedBytes[1] == 0x8b) || this.Compression == "GZip"))
				{
					if (unzip)
					{
						// Unzip the file
						using (var fileStream = File.OpenWrite(outputFilename))
						{
							using (var stream = new MemoryStream(decryptedBytes))
							{
								using (var gzipStream = new GZipStream(stream, CompressionMode.Decompress))
								{
									gzipStream.CopyTo(fileStream);
								}
							}
						}
					}
					else
					{
						// Add .gz extension
						File.WriteAllBytes(outputFilename + ".gz", decryptedBytes);
					}
				}
				else
				{
					File.WriteAllBytes(outputFilename, decryptedBytes);
				}
			}
		}*/
	}
}
