using System;
using System.Threading;
using System.Threading.Tasks;
using Eto.Forms;
using MGContent;

public static class OpenFileUtil
{
	public static Task<string?> LaunchOpenFileDialog()
	{
		TaskCompletionSource<string?> resultSource = new TaskCompletionSource<string?>();

		Thread dialogThread = new Thread(() =>
		{
			Eto.Forms.OpenFileDialog openFileDialog = new Eto.Forms.OpenFileDialog
			{
				MultiSelect = false,
				Filters = { new FileFilter("MGCB Files", "*.mgcb") }
			};

			string? result = null;
			if (openFileDialog.ShowDialog(null) == DialogResult.Ok && openFileDialog.FileName.Length > 0)
			{
				result = openFileDialog.FileName;
			}

			resultSource.SetResult(result);
		});

		dialogThread.SetApartmentState(ApartmentState.STA);
		dialogThread.Start();

		return resultSource.Task;
	}
}
