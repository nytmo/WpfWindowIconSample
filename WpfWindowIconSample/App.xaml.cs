using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfWindowIconSample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, new RoutedEventHandler((s, e) =>
			{
				if (s is Window win)
					RefreshDpiAwareResourceDictionaries(win);
			}));

			EventManager.RegisterClassHandler(typeof(Window), Window.DpiChangedEvent, new DpiChangedEventHandler((s, e) =>
			{
				if (e.OriginalSource is Window win && e.OldDpi.DpiScaleY != e.NewDpi.DpiScaleY)
					RefreshDpiAwareResourceDictionaries(win);
			}));
		}

		private static void RefreshDpiAwareResourceDictionaries(Window window)
		{
			var dpiScale = System.Windows.Media.VisualTreeHelper.GetDpi(window);

			var dpi = dpiScale.PixelsPerInchY switch
			{
				<= 96 => 96,
				> 96 and <= 120 => 120,
				> 120 and <= 144 => 144,
				> 144 and <= 192 => 192,
				> 192 and <= 288 => 288,
				_ => 384,
			};

			window.Resources.MergedDictionaries.Clear();
			Uri uri = new($"/Icon/Icon-{dpi}dpi.xaml", UriKind.RelativeOrAbsolute);
			window.Resources.MergedDictionaries.Add(new ResourceDictionary
			{
				Source = uri,
			});
			window.Title = $"DPI: {dpi}";
		}
	}

}
