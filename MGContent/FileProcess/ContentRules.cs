using System.Text.Json.Serialization;

namespace MGContent;

/// <summary>
/// Defines rules for auto-include.
/// </summary>
class ContentRules
{
	#region rMembers

	[JsonPropertyName("ignore")]
	public List<string> IgnoreList { get; set; }

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Construct default rules.
	/// </summary>
	public ContentRules()
	{
		IgnoreList = new List<string>();
	}



	/// <summary>
	/// Create default rules.
	/// </summary>
	public static ContentRules CreateDefaultRules(string mgcbPath)
	{
		ContentRules result = new();

		result.IgnoreList.Add("obj/*");
		result.IgnoreList.Add("bin/*");
		result.IgnoreList.Add("ContentRules.json");
		result.IgnoreList.Add("Content.mgcb");

		return result;
	}

	#endregion rInit





	#region rUpdate

	#endregion rUpdate





	#region rDraw

	#endregion rDraw





	#region rUtil

	/// <summary>
	/// Should we ignore this path?
	/// </summary>
	public bool ShouldIgnore(string path)
	{
		foreach(string ignorePattern in IgnoreList)
		{
			if (Utils.MatchPathPattern(path, ignorePattern))
			{
				return true;
			}
		}

		return false;
	}

	#endregion rUtil
}
