

namespace MGContent;

class BuildPanel : ImGuiWindow
{
	public BuildPanel(ImGuiWindowFlags flags) : base("Build", flags)
	{
	}

	protected override void AddWindowCommands(GameTime time)
	{
		ImGui.Text("Builder");
	}
}
