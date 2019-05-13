using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Forms.Models
{
    public enum MenuItemType
    {
        Home,
        Project,
        Contact
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
