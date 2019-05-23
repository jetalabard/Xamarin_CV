using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using Cv_Forms.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cv_Forms.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private string _Description = string.Empty;
        public const string DescriptionPropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }


        private string _Image = string.Empty;
        public const string ImagePropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Image
        {
            get { return _Image; }
            set { SetProperty(ref _Image, value); }
        }


        public HomeViewModel()
        {
            Description desc = DataManager.GetInstance().Description();
            Title = desc?.Name;
            Subtitle = desc?.Title;
            Image = ImageManager<string>.GetInstance().GetImageFromPath(desc?.Image);
            Description = desc?.Summary;

        }

    }
}