using System.Linq;
using System.ComponentModel;

using Xamarin.Forms;
using Cv_Forms.ViewModels;
using Cv_Core.DataModel;
using Cv_Core;
using System.Collections.Generic;
using Button = Cv_Forms.ViewModels.Button;

using System;

namespace Cv_Forms.Views
{
    [DesignTimeVisible(true)]
    public partial class ActivitiesPage : ContentPage
    {
        public List<Activity> Activities;

        private int MaxColumn = 3;
        
        public ActivitiesPage(ICollection<Activity> list, string subtitle)
        {
            InitializeComponent();
            if(list != null && list.Count > 0)
            {
                Activities = list.ToList();
                Activities.ForEach(act => act.Image = ImageManager<string>.GetInstance().GetImageFromPath(act.Image));

                BindingContext = new ActivityViewModel(Activities, subtitle);

                foreach (Activity act in list)
                {
                    Content.Children.Add(ShowActivity(act));
                }
            }
           
        }


        private StackLayout ShowActivity(Activity act)
        {
            var stack = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            var TitleLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
            var ageLabel = new Label { FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };

            var image = new MR.Gestures.Image { Source = act.Image, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            image.Tapped += ImageTapped;
            var summary = new ExtendedLabel { JustifyText = true, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };


            TitleLabel.Text = act.Title;
            ageLabel.Text = act.SubTitleWithDate;
            summary.Text = act.Summary;


            var grid = new Grid { HorizontalOptions=LayoutOptions.CenterAndExpand };
            if(act.Links.Count() <= MaxColumn)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            else
            {
                for (int row = 0; row < (act.Links.Count() / MaxColumn) +1; row++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
            }
            for(int col=0;col< act.Links.Count(); col++)
            {
                if(col >= MaxColumn)
                {
                    break;
                }
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var productIndex = 0;
            for (int rowIndex = 0; rowIndex < grid.RowDefinitions.Count(); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < grid.ColumnDefinitions.Count(); columnIndex++)
                {
                    if (productIndex >= act.Links.Count)
                    {
                        break;
                    }
                    var product = act.Links[productIndex];
                    productIndex += 1;
                    grid.Children.Add(new Button(product.Url, product.Title, product.IsUrl ? "externalLink.png" : "download.png",product.LinkTitle,product.IsUrl), columnIndex, rowIndex);
                }
            }

            stack.Children.Add(TitleLabel);
            stack.Children.Add(ageLabel);
            stack.Children.Add(image);
            stack.Children.Add(summary);

            stack.Children.Add(grid);
            stack.Children.Add(new BoxView() { Color = Color.FromHex("#1B3147"), WidthRequest = 100, HeightRequest = 2 });

            return stack;

        }


        private void ImageTapped(object sender, MR.Gestures.TapEventArgs e)
        {
            Image theImage = (Image)sender;
            popupImageView.IsVisible = true;
            imgPopup.Source = theImage.Source;
        }


        private void ButtonPopup_Clicked(object sender, EventArgs e)
        {
            popupImageView.IsVisible = false;
        }
    }
}