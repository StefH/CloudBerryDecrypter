﻿using System.Collections.ObjectModel;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace CloudBerryDecrypter
{
	public class AmazonService
	{
		private AmazonS3 s3Client;

		public AmazonService(string awsAccessKey, string awsSecretAccessKey)
		{
			this.s3Client = AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretAccessKey);
			this.s3Client.ListBuckets();
		}

		public ReadOnlyCollection<S3Bucket> GetBuckets()
		{
			return this.s3Client.ListBuckets().Buckets.AsReadOnly();
		}

		public ReadOnlyCollection<S3Object> ListObjects(S3Bucket bucket)
		{
			var request = new ListObjectsRequest().WithBucketName(bucket.BucketName);

			return this.s3Client.ListObjects(request).S3Objects.AsReadOnly();
		}

		public S3File GetS3File(S3Object s3Object)
		{
			var response = this.s3Client.GetObject(new GetObjectRequest()
			{
				BucketName = s3Object.BucketName,
				Key = s3Object.Key
			});

			return new S3File(response.Key, ReadFully(response.ResponseStream), response.Headers);
		}

		private byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];

			using (var ms = new MemoryStream())
			{
				int read = 0;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}

				return ms.ToArray();
			}
		}

		public static string GetSafeFilepath(string key)
		{
			return !string.IsNullOrWhiteSpace(key) ? key.Replace(':', '_') : string.Empty;
		}
	}
}
