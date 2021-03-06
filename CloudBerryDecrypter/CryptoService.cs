﻿using System;
using System.Security.Cryptography;

namespace CloudBerryDecrypter
{
	public enum Algo
	{
		None,
		AES,
		DES,
		TripleDES,
		RC2
	}

	public class CryptoService
	{
		protected string Password { get; private set; }
		protected byte[] Salt { get; private set; }
		protected int Iterations { get; private set; }

		public CryptoService(string password, byte[] salt, int iterations)
		{
			Password = password;
			Salt = salt;
			Iterations = iterations;
		}

		protected virtual DeriveBytes GetDeriveKey()
		{
			throw new NotImplementedException();
		}

		public byte[] Decrypt(Algo algo, int keySize, byte[] iv, byte[] encryptedBytes)
		{
			// var decryptedData = new byte[] { };

			SymmetricAlgorithm provider;
			switch (algo)
			{
				case Algo.AES:
					provider = new AesCryptoServiceProvider();
					break;

				case Algo.DES:
					provider = new DESCryptoServiceProvider();
					break;

				case Algo.TripleDES:
					provider = new TripleDESCryptoServiceProvider();
					break;

				case Algo.RC2:
					provider = new RC2CryptoServiceProvider();
					break;
				
				default:
					return null;
			}

			provider.KeySize = keySize;
			provider.IV = iv;
			provider.Key = GetDeriveKey().GetBytes(keySize / 8);

			using (var decrypter = provider.CreateDecryptor())
			{
				return decrypter.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
			}
		}

		public byte[] Encrypt(Algo algo, int keySize, byte[] iv, byte[] bytes)
		{
			// var encryptedData = new byte[] { };

			SymmetricAlgorithm provider;
			switch (algo)
			{
				case Algo.AES:
					provider = new AesCryptoServiceProvider();
					break;

				case Algo.DES:
					provider = new DESCryptoServiceProvider();
					break;

				case Algo.TripleDES:
					provider = new TripleDESCryptoServiceProvider();
					break;

				case Algo.RC2:
					provider = new RC2CryptoServiceProvider();
					break;

				default:
					return null;
			}

			provider.KeySize = keySize;
			provider.IV = iv;
			provider.Key = GetDeriveKey().GetBytes(keySize / 8);

			using (var decrypter = provider.CreateEncryptor())
			{
				return decrypter.TransformFinalBlock(bytes, 0, bytes.Length);
			}
		}
	}
}