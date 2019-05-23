using Cv_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cv_Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KnowledgePage : TabbedPage
    {
        public KnowledgePage()
        {
            InitializeComponent();
            KnowledgeTool tool = this.FindByName<KnowledgeTool>("Tool");
            tool.Title = Constants.TITLES_KNOWLEDGES[1];
            KnowledgeLanguage lang = this.FindByName<KnowledgeLanguage>("Language");
            lang.Title = Constants.TITLES_KNOWLEDGES[0];
        }


    }
}