using System.IO;
using System.Text.RegularExpressions;

namespace MGContent;

static partial class Utils
{
	static Dictionary<(string, string), bool> sPathMatchCache = new();
	static Dictionary<string, string> sNormalisedPathsCache = new();

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
		string forwardSlashPath = Utils.NormalisedPath(path);
		string regexPattern = string.Format("{0}", Regex.Escape(pattern).Replace("/", @"").Replace(@"\*", ".*").Replace(@"\?", "."));

		bool result = Regex.IsMatch(forwardSlashPath, regexPattern, RegexOptions.IgnoreCase);
		sPathMatchCache.Add((path, pattern), result);

		return result;
	}



	/// <summary>
	/// Create a normalised version of a path.
	/// </summary>
	public static string NormalisedPath(string path)
	{
		if (sNormalisedPathsCache.TryGetValue(path, out string? value))
		{
			return value;
		}
		
		string normalPath = path.Replace("\\", "/");
		sNormalisedPathsCache.Add(path, normalPath);

		return normalPath;
	}



	/// <summary>
	/// Is this a path delimeter?
	/// </summary>
	public static bool IsPathDelimeter(char c)
	{
		return c == '/' || c == '\\';
	}
}
