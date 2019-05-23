using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Core
{
    public interface ICreateImage<T>
    {
        T CreateImage(string imagePath);
    }
}
