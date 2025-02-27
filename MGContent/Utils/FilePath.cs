using System.IO;
using System.Text.RegularExpressions;

namespace MGContent;

static partial class Utils
{
	static Dictionary<(string, string), bool> sPathMatchCache = new();

	/// <summary>
	/// Check if path matches pattern
	/// </summary>
	public static bool MatchPathPattern(string path, string pattern)
	{
		if(sPathMatchCache.TryGetValue((path, pattern), out bool cachedResult))
		{
			return cachedResult;
		}


		//@Todo: Reduce allocations.
		string forwardSlashPath = path.Replace("\\", "/");
		string regexPattern = string.Format("{0}", Regex.Escape(pattern).Replace("/", @"").Replace(@"\*", ".*").Replace(@"\?", "."));

		bool result = Regex.IsMatch(forwardSlashPath, regexPattern, RegexOptions.IgnoreCase);
		sPathMatchCache.Add((path, pattern), result);

		return result;
	}
}
