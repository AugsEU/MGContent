

namespace MGContent;

class InfoPanel : ImGuiWindow
{
	#region rMembers

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Create info panel.
	/// </summary>
	public InfoPanel(ImGuiWindowFlags flags) : base("Info", flags)
	{
	}

	#endregion rInit





	#region rDraw

	/// <summary>
	/// Draw info panel with ImGui commands
	/// </summary>
	protected override void AddWindowCommands(GameTime time)
	{
		ImGui.Text("Info");
	}

	#endregion rDraw





	#region rUtil

	#endregion rUtil
}
