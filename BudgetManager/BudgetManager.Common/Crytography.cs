using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace BudgetManager.Common
{
	public static class Crytography
	{
		/// <summary>
		/// Hashes the string
		/// </summary>
		/// <param name="algorithm">The algorithm.</param>
		/// <param name="plaintext">The plain text.</param>
		/// <returns></returns>
		public static SqlString ToHash(SqlString algorithm, [SqlFacet(MaxSize = -1)] SqlString plaintext)
		{
			if (algorithm.IsNull || plaintext.IsNull)
			{
				return SqlString.Null;
			}
			bool HashDefined = true;
			HashAlgorithm hash = null;
			switch (algorithm.Value.ToUpper())
			{
				case "SHA256":
					hash = new SHA256Managed();
					break;
				case "SHA384":
					hash = new SHA384Managed();
					break;
				case "SHA512":
					hash = new SHA512Managed();
					break;
				default:
					HashDefined = false;
					break;
			}
			if (!HashDefined)
			{
				throw new Exception("Unsupported hash algorithm - use SHA256, SHA384 or SHA512");
			}
			var encoding = new System.Text.ASCIIEncoding();
			byte[] hashBytes = hash.ComputeHash(encoding.GetBytes(plaintext.Value));
			// Convert result to a SqlBytes value
			var sb = new StringBuilder();
			foreach (byte hex in hashBytes)
			{
				sb.Append(hex.ToString("x2"));
			}
			return sb.ToString();
		}
	}
}