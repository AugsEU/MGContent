using System.IO;

namespace MGContent;


/// <summary>
/// A node in a file tree. (Dir or File)
/// </summary>
class FileNode : IEquatable<FileNode>
{
	#region rMember

	public bool IsFile { get; private set; }
	public string FullPath { get; set; }
	public DateTime LastModified { get; set; }
	public List<FileNode> Children { get; set; }

	string mBaseName = "";

	#endregion rMember





	#region rInit

	/// <summary>
	/// Initializes a new instance of the <see cref="FileNode"/> class.
	/// </summary>
	/// <param name="path">The path to the file or directory.</param>
	/// <exception cref="UnauthorizedAccessException">
	/// Thrown when access to the directory is denied.
	/// </exception>
	/// <exception cref="IOException">
	/// Thrown when an I/O error occurs.
	/// </exception>
	public FileNode(string path)
	{
		path = Utils.NormalisedPath(path);
		FullPath = path;
		Children = new List<FileNode>();

		// Determine if the path is a file or directory
		IsFile = File.Exists(path);

		mBaseName = Path.GetFileName(path);

		if (IsFile)
		{
			LastModified = File.GetLastWriteTime(path);
		}
		else
		{
			if (!Directory.Exists(path))
			{
				throw new IOException($"Invalid file structure: {path}");
			}

			LastModified = Directory.GetLastWriteTime(FullPath);

			var files = Directory.GetFiles(FullPath);
			var directories = Directory.GetDirectories(FullPath);

			foreach (var directory in directories)
			{
				Children.Add(new FileNode(directory));
			}

			foreach (var file in files)
			{
				Children.Add(new FileNode(file));
			}
		}

		
	}

	#endregion rInit




	#region rUtil

	/// <summary>
	/// Is this equal to the other file node?
	/// </summary>
	public bool Equals(FileNode? other)
	{
		if (other is null) 
			return false;

		if (other.FullPath != FullPath 
			|| other.IsFile != IsFile
			|| other.Children.Count != Children.Count)
		{
			return false;
		}

		foreach (FileNode otherNode in other.Children)
		{
			bool anyEquals = false;
			foreach (FileNode node in Children)
			{
				if(otherNode.Equals(node))
				{
					anyEquals = true;
					break;
				}
			}

			if (!anyEquals)
			{
				return false;
			}
		}

		return true;
	}



	/// <summary>
	/// Get the unique file in this directory.
	/// </summary>
	public FileNode? GetUniqueFileMatching(string pattern)
	{
		FileNode? match = null;

		foreach(FileNode node in Children)
		{
			if (node.IsFile)
			{
				if (Utils.MatchPathPattern(node.FullPath, pattern))
				{
					if (match is not null)
					{
						throw new Exception($"Multiple |{pattern}| files in folder. Please select a specific one.");
					}

					match = node;
				}
			}
		}

		return match;
	}



	/// <summary>
	/// Get node matching path.
	/// </summary>
	public FileNode? GetChildWithPath(string path)
	{
		path = Utils.NormalisedPath(path);
		return RecursiveFileNodeSearch(path);
	}



	/// <summary>
	/// Search children for a match to a file path. Done in a way to avoid allocations.
	/// </summary>
	private FileNode? RecursiveFileNodeSearch(string path)
	{
		if (path == FullPath)
		{
			return this;
		}

		if (!path.StartsWith(FullPath))
		{
			return null;
		}

		foreach (FileNode child in Children)
		{
			FileNode? childSearch = child.RecursiveFileNodeSearch(path);

			if(childSearch is not null)
			{
				return childSearch;
			}
		}

		return null;
	}



	/// <summary>
	/// Does this match the pattern?
	/// </summary>
	public bool MatchesPattern(string pattern)
	{
		return Utils.MatchPathPattern(FullPath, pattern);
	}



	/// <summary>
	/// Get the base name of this node.
	/// </summary>
	public string BaseName => mBaseName;

	#endregion rUtil
}