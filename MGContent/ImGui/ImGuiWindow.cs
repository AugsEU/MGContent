
namespace MGContent;

/// <summary>
/// An ImGui window.
/// </summary>
public abstract class ImGuiWindow
{
	#region rMembers

	protected string mTitle;

	ImVec2 mWindowSize;
	ImVec2? mReqSetWindowSize;

	ImVec2 mWindowPos;
	ImVec2? mReqSetWindowPos;

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
		ImGui.Begin(mTitle, mFlags);

		// Pos
		if (mReqSetWindowPos.HasValue)
		{
			ImGui.SetWindowPos(mReqSetWindowPos.Value);
			mReqSetWindowPos = null;
		}

		mWindowPos = ImGui.GetWindowPos();

		// Size
		if (mReqSetWindowSize.HasValue)
		{
			ImGui.SetWindowSize(mReqSetWindowSize.Value);
			mReqSetWindowSize = null;
		}

		mWindowSize = ImGui.GetWindowSize();

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



	/// <summary>
	/// Get last size of window.
	/// </summary>
	public ImVec2 GetWindowSize()
	{
		return mWindowSize;
	}



	/// <summary>
	/// Request window size for next frame.
	/// </summary>
	public void RequestWindowSize(ImVec2 size)
	{
		mReqSetWindowSize = size;
	}



	/// <summary>
	/// Request window size for next frame.
	/// </summary>
	public void RequestWindowSize(float x, float y)
	{
		RequestWindowSize(new ImVec2(x, y));
	}



	/// <summary>
	/// Get the window's last position.
	/// </summary>
	public ImVec2 GetWindowPos()
	{
		return mWindowPos;
	}



	/// <summary>
	/// Request window position for next frame.
	/// </summary>
	public void RequestWindowPos(float x, float y)
	{
		mReqSetWindowPos = new ImVec2(x, y);
	}

	#endregion rUtil
}
