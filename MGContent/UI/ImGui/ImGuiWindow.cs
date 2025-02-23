
namespace MGContent;

/// <summary>
/// An ImGui window.
/// </summary>
public abstract class ImGuiWindow
{
	#region rMembers

	protected string mTitle;

	public ImVec2 Size { get; private set; }
	public ImVec2? RequestSize { get; set; }
	public (ImVec2, ImVec2)? SizeMinMax { get; set; }
	public ImVec2 Position { get; set; }


	ImGuiWindowFlags mFlags;

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Create window with title and flags
	/// </summary>
	public ImGuiWindow(string title, ImGuiWindowFlags flags)
	{
		mTitle = title;

		mFlags = flags;
	}

	#endregion rInit





	#region rDraw

	/// <summary>
	/// Draw window by adding ImGui commands
	/// </summary>
	public void AddImGuiCommands(GameTime time)
	{
		if (SizeMinMax.HasValue)
		{
			ImGui.SetNextWindowSizeConstraints(SizeMinMax.Value.Item1, SizeMinMax.Value.Item2);
		}

		if (RequestSize.HasValue)
		{
			ImGui.SetNextWindowSize(RequestSize.Value);
			Size = RequestSize.Value;

			RequestSize = null;
		}
		ImGui.SetNextWindowPos(Position);

		ImGui.Begin(mTitle, mFlags);

		Size = ImGui.GetWindowSize();
		AddWindowCommands(time);

		ImGui.End();
	}



	/// <summary>
	/// Add contents of window
	/// </summary>
	protected abstract void AddWindowCommands(GameTime time);

	#endregion rDraw





	#region rUtil

	/// <summary>
	/// Get window's title
	/// </summary>
	public string GetTitle()
	{
		return mTitle;
	}

	#endregion rUtil
}
