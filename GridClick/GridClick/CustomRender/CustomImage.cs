
using Xamarin.Forms;

namespace GridClick
{
    public class CustomImage : Image
    {
        public static BindableProperty DontResizeProperty =
            BindableProperty.Create<CustomImage, bool>(p => p.DontResize, true);

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("ImageSource", typeof(string), typeof(CustomImage), default(string), propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (Device.OS != TargetPlatform.Android)
                {
                    var image = (CustomImage)bindable;

                    var baseImage = (Image)bindable;
                    baseImage.Source = image.ImageSource;
                }
            });

        public bool DontResize
        {
            get { return (bool)GetValue(DontResizeProperty); }
            set { SetValue(DontResizeProperty, value); }
        }

        public string ImageSource
        {
            get { return GetValue(ImageSourceProperty) as string; }
            set { SetValue(ImageSourceProperty, value); }
        }

        public CustomImage()
        {
        }
    }
}
