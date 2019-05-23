using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using Cv_Forms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Cv_Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            InitializeComponent();
            Description desc = DataManager.GetInstance().Description();
            BindingContext = new ContactViewModel(desc);
            if (desc == null)
            {
                map.IsVisible = false;
            }
            else
            {
                Position position = new Position(desc.Lat, desc.Long);
                map.Pins.Add(new Pin { Position = position, Label = "Domicile", Type = PinType.Generic,Address = desc.Adress });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.0)));
            }
        }
    }
}