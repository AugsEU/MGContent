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
	/// Get the unique file.
	/// </summary>
	public FileNode GetUniqueFile(string extension)
	{
		FileNode? match = null;

		foreach(FileNode node in Children)
		{
			if (node.IsFile)
			{
				if (node.FullPath.EndsWith(extension))
				{
					if (match is not null)
					{
						throw new Exception("Multiple mgcb files in folder. Please select a specific one.");
					}

					match = node;
				}
			}
		}

		if (match is null)
		{
			throw new Exception("No mgcb file found in folder");
		}

		return match;
	}



	/// <summary>
	/// Get node matching path.
	/// </summary>
	public FileNode GetNodeWithPath(string path)
	{
		FileNode? match = null;

		foreach (FileNode node in Children)
		{
			if (node.IsFile)
			{
				if (node.FullPath == path)
				{
					if (match is not null)
					{
						throw new Exception("Multiple mgcb files in folder with same name? Open an issue on GitHub.");
					}

					match = node;
				}
			}
		}

		if (match is null)
		{
			throw new Exception($"No mgcb file found matching {path}");
		}

		return match;
	}


	/// <summary>
	/// Get the base name of this node.
	/// </summary>
	public string BaseName => mBaseName;

	#endregion rUtil
}