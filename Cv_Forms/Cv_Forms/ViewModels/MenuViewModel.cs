using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Forms.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {

        private string _Name = string.Empty;
        public const string NamePropertyName = "Name";

        /// <summary>
        /// Gets or sets the "Name" property
        /// </summary>
        /// <value>The app name.</value>
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        private string _Email = string.Empty;
        public const string EmailPropertyName = "Email";

        /// <summary>
        /// Gets or sets the "Email" property
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }


        private string _Image = string.Empty;
        public const string ImagePropertyName = "Image";

        /// <summary>
        /// Gets or sets the "Image" property
        /// </summary>
        /// <value>The image.</value>
        public string Image
        {
            get { return _Image; }
            set { SetProperty(ref _Image, value); }
        }



    }
}
