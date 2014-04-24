using System.Collections.Generic;
using Amazon.S3.Model;

namespace CloudBerryDecrypter
{
	public class ProcessSettings
	{
		public AmazonService AmazonService { get; set; }

		public string CloudBerryPassword { get; set; }

		public List<S3Object> S3Objects { get; set; }

		public string OutputFolder { get; set; }

		public bool CreateFolders { get; set; }

		public bool Unzip { get; set; }
	}
}
