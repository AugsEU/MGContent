using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace MGContent.ViewModels;

public class MainViewModel : ViewModelBase
{
	public ObservableCollection<TreeItem> ContentTree { get; set; }

	public MainViewModel()
	{
		ContentTree = new ObservableCollection<TreeItem>();

		// Specify the root folder path
		string rootPath = @"C:\Users\Augus\Documents\Programming\MonoGame\GalaxyGame\GalaxyGame\GalaxyGame\@Data";

		if (Directory.Exists(rootPath))
		{
			foreach (var dir in Directory.GetDirectories(rootPath))
			{
				ContentTree.Add(new TreeItem(dir));
			}

			foreach (var file in Directory.GetFiles(rootPath))
			{
				ContentTree.Add(new TreeItem(file));
			}
		}
	}
}
