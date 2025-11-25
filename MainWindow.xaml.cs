using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KonyvtarAvatkozas
{
	public partial class MainWindow : Window
	{
		const string FILENAME = "olvasok.txt";
		List<Olvaso> olvasok = new List<Olvaso>();

		public MainWindow()
		{
			InitializeComponent();
			BetoltOlvasok();
		}

		private void BetoltOlvasok()
		{
			if (File.Exists(FILENAME))
			{
				foreach (var line in File.ReadAllLines(FILENAME, Encoding.UTF8))
				{
					if (!string.IsNullOrWhiteSpace(line))
					{
						try
						{
							olvasok.Add(Olvaso.Parse(line));
						}
						catch { }
					}
				}
				FrissitListBox();
			}
		}

		private void FrissitListBox()
		{
			olvasokListBox.ItemsSource = null;
			olvasokListBox.ItemsSource = olvasok.ConvertAll(o => o.Nev);
		}

		private void EletkorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !int.TryParse(e.Text, out _);
		}

		private void mentesButton_Click(object sender, RoutedEventArgs e)
		{
			visszajelzesTextBlock.Text = "";

			if (string.IsNullOrWhiteSpace(nevTextBox.Text) || string.IsNullOrWhiteSpace(eletkorTextBox.Text))
			{
				visszajelzesTextBlock.Text = "Kérem, töltse ki a kötelező mezőket!";
				visszajelzesTextBlock.Foreground = System.Windows.Media.Brushes.Red;
				return;
			}

			if (!int.TryParse(eletkorTextBox.Text, out int eletkor))
			{
				visszajelzesTextBlock.Text = "Életkor csak szám lehet!";
				visszajelzesTextBlock.Foreground = System.Windows.Media.Brushes.Red;
				return;
			}

			string mufaj = "";
			if (mufajComboBox.SelectedItem is ComboBoxItem cbi)
				mufaj = cbi.Content.ToString();

			string ertesites = "";
			if (hirlevelCheckBox.IsChecked == true) ertesites += "Hírlevél;";
			if (smsCheckBox.IsChecked == true) ertesites += "SMS;";
			if (ertesites.EndsWith(";")) ertesites = ertesites.Substring(0, ertesites.Length - 1);
			if (string.IsNullOrWhiteSpace(ertesites)) ertesites = "nincs";

			string tagsag = normalRadio.IsChecked == true ? "Normál" :
							diakRadio.IsChecked == true ? "Diák" : "Nyugdíjas";

			var ujOlvaso = new Olvaso
			{
				Nev = nevTextBox.Text,
				Eletkor = eletkor,
				Mufaj = mufaj,
				Ertesitesek = ertesites,
				Tagsag = tagsag
			};

			olvasok.Add(ujOlvaso);

			File.AppendAllText(FILENAME, ujOlvaso + Environment.NewLine, Encoding.UTF8);
			FrissitListBox();

			visszajelzesTextBlock.Text = "Sikeres regisztráció!";
			visszajelzesTextBlock.Foreground = System.Windows.Media.Brushes.Green;
		}
	}
}