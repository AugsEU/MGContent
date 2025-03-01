

namespace MGContent;

class ErrorDialog : ImGuiWindow
{
	public enum State
	{
		Display,
		OK
	}

	State mState;
	string mErrorMsg;

	public ErrorDialog(string title, string errorMsg) : base($"ERROR - {title}", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse)
	{
		mErrorMsg = errorMsg;
		mState = State.Display;
	}

	protected override void AddWindowCommands(GameTime time)
	{
		ImGui.Text(mErrorMsg);

		if (ImGui.Button("OK"))
		{
			mState = State.OK;
		}
	}

	public State GetState()
	{
		return mState;
	}
}
