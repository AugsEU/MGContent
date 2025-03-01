using System.IO;

namespace MGContent;

/// <summary>
/// Information about an MGCB folder that is open.
/// </summary>
class MGCBDirectory
{
	#region rMembers

	FileNode mOpenFolder;
	FileNode mMGCBFile;

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Read the directory.
	/// </summary>
	private MGCBDirectory(FileNode openFolder, FileNode mgcbNode)
	{
		mOpenFolder = openFolder;
		mMGCBFile = mgcbNode;
	}

	#endregion rInit




	#region rUtil

	/// <summary>
	/// Open an MGCB file.
	/// </summary>
	public static MGCBDirectory TryOpenMGCBFolder(string path)
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

			FileNode folderNode = new FileNode(path);
			FileNode? mgcbNode = null;

			if (mgcbPath is not null)
			{
				mgcbNode = folderNode.GetChildWithPath(mgcbPath);
			}
			else
			{
				mgcbNode = folderNode.GetUniqueFileMatching("*.mgcb");
			}

			if (mgcbNode is null)
			{
				throw new Exception($"Could not find MGCB file in folder: {path}");
			}

			return new MGCBDirectory(folderNode, mgcbNode);
		}
		catch(Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public FileNode Root => mOpenFolder;
	public FileNode MGCBFile => mMGCBFile;

	#endregion rUtil
}
