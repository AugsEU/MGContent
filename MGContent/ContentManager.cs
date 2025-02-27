namespace MGContent;

/// <summary>
/// Module that does the content managing.
/// </summary>
static class ContentManager
{
	#region rMembers

	static MGCBDirectory? mMgcbDir;
	static ContentRules mRules;

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Init static class
	/// </summary>
	static ContentManager()
	{
		mMgcbDir = null;
		mRules = new ContentRules();
	}

	#endregion rInit





	#region rUpdate

	#endregion rUpdate





	#region rDraw

	#endregion rDraw





	#region rUtil

	/// <summary>
	/// Load an mgcb. Returns optinal error message.
	/// </summary>
	public static string? TryOpenMGCB(string path)
	{
		try
		{
			mMgcbDir = MGCBDirectory.TryOpenMGCBFolder(path);

			// @Todo: Load from file.
			mRules = ContentRules.CreateDefaultRules(mMgcbDir.Root.FullPath);
		}
		catch (Exception ex)
		{
			mMgcbDir = null;
			return ex.Message;
		}

		return null;
	}



	/// <summary>
	/// Is a folder open?
	/// </summary>
	public static bool IsOpen => mMgcbDir is not null;

	public static FileNode? OpenFolder => mMgcbDir?.Root;
	public static FileNode? OpenMGCB => mMgcbDir?.MGCBFile;

	public static ContentRules Rules => mRules;

	#endregion rUtil
}
