using System;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GridClick.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var displayInformation = DisplayInformation.GetForCurrentView();

            var width = Window.Current.Bounds.Width * (int)displayInformation.RawPixelsPerViewPixel;
            var height = Window.Current.Bounds.Height * (int)displayInformation.RawPixelsPerViewPixel;

            var dpi = DisplayInformation.GetForCurrentView().RawDpiY;

            var screenDiagonal = Math.Sqrt(Math.Pow(width / dpi, 2) +
                        Math.Pow(height / dpi, 2));

            GridClick.App.ScreenSize = new Xamarin.Forms.Size(width, height);
            LoadApplication(new GridClick.App());
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>

    }
}
