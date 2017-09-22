using System;
using System.Collections.Generic;
using System.IO;

namespace BudgetManager.Common.FoldersAndFiles
{
	/// <summary>To Handle Folder and File Access.</summary>
	public class FolderManager
	{
		#region Constructors

		public FolderManager(string folderRoot, string extension)
		{
			FolderRoot = folderRoot;
			Extension = extension;
			Load(folderRoot, extension);
		}

		#endregion

		#region Properties

		/// <summary>An Error Exception if there is any error Exception thrown.</summary>
		public Exception Error { get; private set; }
		/// <summary>A list of All the Sub Folders.</summary>
		public List<DirectoryInfo> SubFolders { get; set; }
		/// <summary>A list of All the files and sub folders files.</summary>
		public List<FileInfo> Files { get; private set; }
		/// <summary>The path of the root folder.</summary>
		public string FolderRoot { get; private set; }
		/// <summary>The Extension to filter by.</summary>
		public string Extension { get; private set; }

		#endregion

		#region Main Methods

		/// <summary>Loads files and folders of folder specified.</summary>
		/// <param name="folderRoot">The Folder specified.</param>
		/// <param name="extension">The Extension to filter by, "*" if all.</param>
		public void Load(string folderRoot, string extension)
		{
			try
			{
				SubFolders = GetAllSubFolders(folderRoot);
				Files = GetAllFilesAndSubDirectoriesFiles(folderRoot, extension);
			}
			catch (Exception e)
			{
				Error = e;
			}
		}

		/// <summary>Gets All the Sub SubFolders for folder root provided.</summary>
		/// <param name="folderRoot">The folder Root.</param>
		/// <returns>a List of Sub SubFolders.</returns>
		public List<DirectoryInfo> GetAllSubFolders(string folderRoot)
		{
			try
			{
				List<DirectoryInfo> allSubFolders = new List<DirectoryInfo>();
				allSubFolders.AddRange(GetDirectoryInfo(folderRoot).GetDirectories());
				return allSubFolders;
			}
			catch (Exception e)
			{
				Error = e;
				return null;
			}
		}

		/// <summary>Gets All the Files And Sub Directories Files</summary>
		/// <param name="folderRoot">The folder Root.</param>
		/// <param name="extension"> </param>
		/// <returns>a List of Sub SubFolders.</returns>
		public List<FileInfo> GetAllFilesAndSubDirectoriesFiles(string folderRoot, string extension)
		{
			try
			{
				List<FileInfo> allFiles = new List<FileInfo>();
				string searchPattern = !string.IsNullOrWhiteSpace(extension)
					                       ? "*." + extension
					                       : "*.*";
				allFiles.AddRange(GetDirectoryInfo(folderRoot).GetFiles(searchPattern));
				foreach (DirectoryInfo folder in SubFolders)
				{
					allFiles.AddRange(folder.GetFiles(searchPattern));
				}
				return allFiles;
			}
			catch (Exception e)
			{
				Error = e;
				return null;
			}
		}

		#endregion

		#region Private Helpers

		/// <summary>Gets a new GetDirectoryInfo instance of folder specified.</summary>
		/// <param name="folder"></param>
		/// <returns>a new GetDirectoryInfo instance of folder specified</returns>
		private static DirectoryInfo GetDirectoryInfo(string folder)
		{
			return new DirectoryInfo(folder);
		}

		#endregion
	}
}