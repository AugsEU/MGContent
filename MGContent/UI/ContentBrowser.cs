
namespace MGContent;



/// <summary>
/// Content browser window is where you find all your files.
/// </summary>
class ContentBrowser : ImGuiWindow
{
	#region rConst

	const int INDENT_SIZE = 3;

	#endregion rConst





	#region rMembers

	FileNode? mSelectedFileNode;
	Dictionary<FileNode, bool> mOpenDirectories = new();
	Dictionary<FileNode, string> mStringCache = new();

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Create content browser window.
	/// </summary>
	public ContentBrowser(ImGuiWindowFlags flags) : base("Browser", flags)
	{
		mSelectedFileNode = null;

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
		FileNode? mgcbFolder = DirectoryScanner.ContentFolder;
		if (mgcbFolder is null)
		{
			ImGui.Text("No MGCB file loaded...");
			return;
		}

		foreach (FileNode node in mgcbFolder.Children)
		{
			DoFileNode(0, node);
		}
	}



	/// <summary>
	/// Draw a file node and it's children
	/// </summary>
	private void DoFileNode(int indent, FileNode node)
	{
		if (node.IsFile)
		{
			string lineNum = GetStringForFileNode(indent, node);
			if (ImGui.Selectable(lineNum, false, ImGuiSelectableFlags.SpanAllColumns))
			{
			}
		}
		else // Needs to be a separate case?
		{
			string lineNum = GetStringForFileNode(indent, node);

			mOpenDirectories.TryGetValue(node, out bool dirOpen);

			if (ImGui.Selectable(lineNum, false, ImGuiSelectableFlags.SpanAllColumns))
			{
				dirOpen = !dirOpen;
				mOpenDirectories[node] = dirOpen;
			}

			if(dirOpen)
			{
				foreach (FileNode child in node.Children)
				{
					DoFileNode(indent + 1, child);
				}
			}
		}
	}

	#endregion rDraw





	#region rUtil

	string GetStringForFileNode(int indent, FileNode node)
	{
		if (mStringCache.TryGetValue(node, out string value))
		{
			return value;
		}

		string result = "";

		if (node.IsFile)
		{
			string spacing = new string(' ', indent * INDENT_SIZE);
			result = $" {spacing}| {node.BaseName}";
		}
		else
		{
			string spacing = new string(' ', indent * INDENT_SIZE);
			result = $" {spacing}¬ {node.BaseName}";
		}

			mStringCache.Add(node, result);
		return result;
	}

	#endregion rUtil
}
