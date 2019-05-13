using Cv_Core;
using System;

namespace Cv_Forms.Controller
{
    public class DirectoryDefaultManager : DirectoryManager
    {
        public override string GetExternalDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}