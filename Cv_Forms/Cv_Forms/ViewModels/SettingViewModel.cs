using Cv_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Forms.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
            Title = Constants.APP_NAME;
            Subtitle = Constants.PAGE_SETTING;
        }
    }
}
