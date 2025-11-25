using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
						catch { /* hibás sor kihagyása */ }
					}
				}
				FrissitListBox();
			}
		}
	}
}