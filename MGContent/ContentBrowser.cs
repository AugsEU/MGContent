
namespace MGContent;

/// <summary>
/// Content browser window is where you find all your files.
/// </summary>
class ContentBrowser : ImGuiWindow
{
	#region rMembers

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Create content browser window.
	/// </summary>
	public ContentBrowser(ImGuiWindowFlags flags) : base("Browser", flags)
	{
	}

	#endregion rInit





	#region rUpdate

	#endregion rUpdate





	#region rDraw

	/// <summary>
	/// Draw content browser.
	/// </summary>
	protected override void AddWindowCommands(GameTime time)
	{
		ImGui.Text("Browser");
	}

	#endregion rDraw





	#region rUtil

	#endregion rUtil
}
