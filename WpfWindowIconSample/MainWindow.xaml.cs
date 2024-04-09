using System.ComponentModel;
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

namespace WpfWindowIconSample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public int Index { get; private set; } = 0;

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Index = (Index + 1) % 2;

			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary
			{
				Source = new($"/Icon/Index-{Index}.xaml", UriKind.RelativeOrAbsolute),
			});

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Index)));
		}
	}
}