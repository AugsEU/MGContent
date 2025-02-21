using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MGContent;

public class TreeItem
{
	public string Name { get; set; }
	public string FullPath { get; set; }
	public bool IsDirectory { get; set; }
	public ObservableCollection<TreeItem> Children { get; set; } = new ObservableCollection<TreeItem>();

	/// <summary>
	/// Recursively create tree items
	/// </summary>
	public TreeItem(string path)
	{
		FullPath = path;
		Name = Path.GetFileName(path);
		IsDirectory = Directory.Exists(path);

		if (IsDirectory)
		{
			foreach (var dir in Directory.GetDirectories(path))
			{
				Children.Add(new TreeItem(dir));
			}

			foreach (var file in Directory.GetFiles(path))
			{
				Children.Add(new TreeItem(file));
			}
		}
	}
}