using Cv_Core;
using Cv_Core.DataModel;
using Cv_Forms.Controller;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cv_Forms.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        private Description Desc;
        
        public string Email
        {
            get { return Desc?.Email; }
        }

        public string FbLink
        {
            get { return Desc?.FbLink; }
        }
        public string LinkedinLink
        {
            get { return Desc?.LinkedinLink; }
        }

        public ICommand Clicked_Mail { get; private set; }
        public ICommand Clicked_Facebook { get; private set; }
        public ICommand Clicked_Linkedin { get; private set; }


        public ContactViewModel(Description description)
        {
            Title = Constants.APP_NAME;
            Subtitle = Constants.PAGE_CONTACTS;
            if (description != null)
            {
                Desc = description;
            }

            Clicked_Mail = new Command(() =>
            {
                DependencyService.Get<IAppHandler>().LaunchApp("mailto:" + Email);
            });

            Clicked_Linkedin = new Command(() =>
            {
                DependencyService.Get<IAppHandler>().LaunchApp(LinkedinLink);
            });

            Clicked_Facebook = new Command(() =>
            {
                DependencyService.Get<IAppHandler>().LaunchApp(FbLink);
            });

        }
    }
}
