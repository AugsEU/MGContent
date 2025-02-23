namespace MGContent;

/// <summary>
/// Scan of a given directory.
/// </summary>
static class DirectoryScanner
{
	#region rMembers

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
		mContentMGCBFile = new FileNode(path);
	}

	#endregion rUtil
}
