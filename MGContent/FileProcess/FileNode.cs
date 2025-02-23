using System.IO;

namespace MGContent;


/// <summary>
/// A node in a file tree. (Dir or File)
/// </summary>
class FileNode : IEquatable<FileNode>
{
	#region rMember

	public bool IsFile { get; private set; }
	public string Path { get; set; }
	public DateTime LastModified { get; set; }
	public List<FileNode> Children { get; set; }

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
		Path = path;
		Children = new List<FileNode>();

		// Determine if the path is a file or directory
		IsFile = File.Exists(path);

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

			LastModified = Directory.GetLastWriteTime(Path);

			var files = Directory.GetFiles(Path);
			var directories = Directory.GetDirectories(Path);

			foreach (var file in files)
			{
				Children.Add(new FileNode(file));
			}

			foreach (var directory in directories)
			{
				Children.Add(new FileNode(directory));
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

		if (other.Path != Path 
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

	#endregion rUtil
}