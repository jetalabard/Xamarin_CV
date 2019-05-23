using Cv_Core.Downloader;
using Cv_Forms.Controller;
using Xamarin.Forms;
using Image = Xamarin.Forms.Image;
using Label = Xamarin.Forms.Label;

namespace Cv_Forms.ViewModels
{
    public class Button : MR.Gestures.Frame
    {

        public Button()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="title"></param>
        /// <param name="source"></param>
        /// <param name="LinkTitle"></param>
        /// <param name="isUrl"></param>
        public Button(string uri,string title,string source,string LinkTitle, bool isUrl) : this()
        {
            Init(uri, title, source, LinkTitle, isUrl);
        }

        public void Init(string uri, string title, string source, string LinkTitle, bool isUrl)
        {
            CornerRadius = 10;
            Opacity = 30;
            BorderColor = Color.FromHex("#1B3147");
            Padding = 0;

            StackLayout stack = new MR.Gestures.StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Horizontal, Spacing = 10, Padding = new Thickness(10, 10, 10, 10), BackgroundColor = Color.Accent, VerticalOptions = LayoutOptions.FillAndExpand };
            Down += (s, e) => stack.BackgroundColor = Color.FromHex("#1B3147");
            Up += (s, e) => stack.BackgroundColor = Color.Accent;
            Tapped += (s, e) => {

                if (LinkTitle.Contains(".apk"))
                {
                    DependencyService.Get<IDownloaderManager>().Download(uri);
                }
                else if (isUrl)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DependencyService.Get<IAppHandler>().LaunchApp(uri);
                    });
                }
                else
                {
                    DependencyService.Get<IDownloaderManager>().Download(uri);
                }
            };

            var image = new Image
            {
                Source = source,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill

            };
            stack.Children.Add(image);
            stack.Children.Add(new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = title
            });
            Content = stack;
        }
            
    }
}
