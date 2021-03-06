﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace CloudBerryDecrypter
{
	public class AppCryptoService : CryptoService
	{
		private static readonly byte[] AppSalt = { 0, 1, 2, 3, 4, 5, 6, 7 };
		private const int AppIterations = 1000;
		private const int AppKeySize = 256;
		private readonly byte[] AppIV = Encoding.ASCII.GetBytes("6c73a696b1eb438d");

		public AppCryptoService()
			: base("522c23291488c94177a82ae81219bed1", AppSalt, AppIterations)
		{
		}

		protected override DeriveBytes GetDeriveKey()
		{
			return new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(Password), Salt, Iterations);
		}

		public string EncryptText(string text)
		{
			byte[] textBytes = Encoding.UTF8.GetBytes(text);
			byte[] encodedBytes = Encrypt(Algo.AES, AppKeySize, AppIV, textBytes);

			return Convert.ToBase64String(encodedBytes);
		}

		public string DecryptText(string base64Encoded)
		{
			try
			{
				byte[] encodedBytes = Convert.FromBase64String(base64Encoded);
				byte[] decodedBytes = Decrypt(Algo.AES, AppKeySize, AppIV, encodedBytes);

				return Encoding.UTF8.GetString(decodedBytes);
			}
			catch
			{
				return null;
			}
		}
	}
}