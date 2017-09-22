using System;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace RequestForService.Security.Passwords
{
	public static class Crytography
	{
		public enum CrytographyType
		{
			Sha256,
			Sha384,
			Sha512,
		}

		public static string ToPasswordHash(this string input)
		{
			if (!string.IsNullOrWhiteSpace(input))
			{
				var text = "_SALT_" + input + "__R#QU#STFORS#RV!CE__";
				var hash = ToHash(CrytographyType.Sha512, text);
				return hash;
			}
			else
			{
				return null;
			}
		}

		private static string ToHash(CrytographyType type, string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				bool hashDefined = true;
				HashAlgorithm hash = null;
				switch (type)
				{
					case CrytographyType.Sha256:
						hash = new SHA256Managed();
						break;
					case CrytographyType.Sha384:
						hash = new SHA384Managed();
						break;
					case CrytographyType.Sha512:
						hash = new SHA512Managed();
						break;
					default:
						hashDefined = false;
						break;
				}
				if (hashDefined)
				{
					var computeHash = hash.ComputeHash(new ASCIIEncoding().GetBytes(text));
					var sb = new StringBuilder();
					foreach (byte bt in computeHash)
						sb.Append(bt.ToString("x2"));
					return sb.ToString();
				}
				else throw new Exception("Unsupported hash algorithm");
			}
			else return null;
		}
	}
}