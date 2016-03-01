using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GridClick
{
    public class GridClickPage : ContentPage
    {
        public GridClickPage()
        {
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            BackgroundColor = Color.Green;

            CreateUI();
        }

        void CreateUI()
        {
            var numbers = new List<string>
            {
                "ace", "2", "3","4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king"
            };

            var hands = new List<string>
            {
                "spades", "hearts", "clubs", "diamonds"
            };

            var cards = new List<string>();
            foreach (var h in hands)
            {
                foreach (var n in numbers)
                    cards.Add(string.Format("{0}_of_{1}.png", n, h));
            }

            var gridStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                WidthRequest = App.ScreenSize.Width,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, -8, 0, 0)
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(6, 0, 6, 4),
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = App.ScreenSize.Width * .3 },
                    new ColumnDefinition { Width = App.ScreenSize.Width * .3 },
                    new ColumnDefinition { Width = App.ScreenSize.Width * .3 },
                }
            };

            int left = 0, top = 0;

            foreach (var pl in cards)
            {
                var imgProduct = new CustomImage
                {
                    Source = Device.OS == TargetPlatform.Windows ? "Assets/" + pl : pl,
                    VerticalOptions = LayoutOptions.Start,
                    WidthRequest = App.ScreenSize.Width * .27
                };

                imgProduct.SizeChanged += (object sender, EventArgs e) =>
                {
                    if (imgProduct.Height > 152)
                        imgProduct.HeightRequest = Device.OS == TargetPlatform.iOS ? 130 : imgProduct.Height;
                    if (imgProduct.Height < 130)
                        imgProduct.HeightRequest = Device.OS == TargetPlatform.iOS ? 130 : 152;
                };
                var lblText = new Label
                {
                    Text = pl.Split('.')[0],
                    TextColor = Color.Blue,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14
                };

                var stack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    WidthRequest = App.ScreenSize.Width * .33,
                    HeightRequest = App.ScreenSize.Height * .3,
                    BackgroundColor = Color.White,
                    Spacing = 0.4,
                    StyleId = lblText.Text,
                    Children =
                    {
                        new StackLayout
                        {
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                            { imgProduct,
                                new StackLayout
                                {
                                    Padding = new Thickness(0, -4, 0, 0),
                                    Children = { lblText }
                                },
                            }
                        }

                    }
                };

                var stackTap = new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(async (t) =>
                    {
                        await DisplayAlert("Clicked", "You've just clicked the " + stack.StyleId.Split('.')[0].Replace('_', ' '), "OK");
                    })
                };
                stack.GestureRecognizers.Add(stackTap);
                grid.Children.Add(stack, left, top);
                left++;
                if (left == 3)
                {
                    left = 0;
                    top++;
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
            }

            gridStack.Children.Add(new ScrollView { VerticalOptions = LayoutOptions.FillAndExpand, Content = grid });

           Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    gridStack
                }
            };
        }
    }
}
