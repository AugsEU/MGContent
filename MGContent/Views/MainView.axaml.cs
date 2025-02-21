using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace MGContent.Views;

public partial class MainView : UserControl
{
	public MainView()
	{
		InitializeComponent();
	}

	private void OnCelciusChanged(object? sender, TextChangedEventArgs args)
	{
		if (CelsiusTxb.Text is null || CelsiusTxb.Text.Length == 0) 
			return;

		if (FahrenheitTxb.IsFocused || !CelsiusTxb.IsFocused)
			return;

		if (double.TryParse(CelsiusTxb.Text, out double tempC))
		{
			double tempF = tempC * (9.0 / 5.0) + 32.0;

			FahrenheitTxb.Text = tempF.ToString("0.0");
		}
		else
		{
			CelsiusTxb.Text = "0";
			FahrenheitTxb.Text = "0";
		}
	}

	private void OnFahrenheitChanged(object? sender, TextChangedEventArgs args)
	{
		if (FahrenheitTxb.Text is null || FahrenheitTxb.Text.Length == 0) 
			return;

		if (CelsiusTxb.IsFocused || !FahrenheitTxb.IsFocused)
			return;

		if (double.TryParse(FahrenheitTxb.Text, out double tempF))
		{
			double tempC = (tempF - 32.0) * (5.0 / 9.0);

			CelsiusTxb.Text = tempC.ToString("0.0");
		}
		else
		{
			CelsiusTxb.Text = "0";
			FahrenheitTxb.Text = "0";
		}
	}
}
