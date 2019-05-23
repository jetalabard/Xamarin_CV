using Cv_Core;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cv_Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            double mode = Preferences.Get(Constants.REFRESH_MODE_PREFERENCES_DAYS, 1.0);
            labelSlider.Text = Convert.ToString(mode) + " jour(s)";
            slider.Value = mode;

            slider.ValueChanged += (sender, args) =>
            {
                double newStep = Math.Round(slider.Value / 1.0);

                slider.Value = newStep ;
                labelSlider.Text = Convert.ToString(newStep) + " jour(s)";
                Preferences.Set(Constants.REFRESH_MODE_PREFERENCES_DAYS, newStep);
            };
        }
    }
}