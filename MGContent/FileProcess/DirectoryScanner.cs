using System.IO;

namespace MGContent;

/// <summary>
/// Scan of a given directory.
/// </summary>
static class DirectoryScanner
{
	#region rMembers

	static FileNode? mContentFolder;
	static FileNode? mContentMGCBFile;

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Init static class.
	/// </summary>
	static DirectoryScanner()
	{
	}

	#endregion rInit




	#region rUtil

	/// <summary>
	/// Open an MGCB file.
	/// </summary>
	public static void OpenMCGB(string path)
	{
		try
		{
			string? mgcbPath = null;
			if (File.Exists(path))
			{
				string? ext = Path.GetExtension(path);
				if (ext is null || ext != ".mgcb")
				{
					throw new Exception("Invalid file.");
				}

				string? mgcbFolder = Path.GetDirectoryName(path);

				if (mgcbFolder is null)
				{
					throw new Exception("Invalid file.");
				}

				mgcbPath = path;
				path = mgcbFolder;
			}
			else if (!Directory.Exists(path))
			{
				throw new Exception("Path doesn't exist");
			}

			mContentFolder = new FileNode(path);

			if (mgcbPath is not null)
			{
				mContentMGCBFile = mContentFolder.GetNodeWithPath(mgcbPath);
			}
			else
			{
				mContentMGCBFile = mContentFolder.GetUniqueFile(".mgcb");
			}
		}
		catch(Exception ex)
		{
			mContentFolder = null;
			mContentMGCBFile = null;

			throw new Exception(ex.Message);
		}
	}


	public static bool IsOpen => mContentFolder is not null;
	public static FileNode? ContentFolder => mContentFolder;
	public static FileNode? MGCBFile => mContentMGCBFile;

	#endregion rUtil
}
