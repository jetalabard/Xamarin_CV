using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Forms.Controller
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
