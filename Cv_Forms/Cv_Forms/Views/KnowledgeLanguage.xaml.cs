using Cv_Core;
using Cv_Core.DataModel;
using Cv_Forms.Controller;
using Cv_Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageButton = Cv_Forms.ViewModels.ImageButton;

namespace Cv_Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KnowledgeLanguage : ContentPage
    {
        private List<Knowledge> KnowledgesLanguages;
        public KnowledgeLanguage()
        {
            InitializeComponent();

            KnowledgesLanguages = ((KnowledgesViewModel)BindingContext).KnowledgesLanguages();
            if (KnowledgesLanguages != null && KnowledgesLanguages.Count > 0)
            {
                Languages = new GalleryKnowledges(KnowledgesLanguages).FillGrid(Languages);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Aucune connaissance de langage n'est référencé.");
            }
        }
    }
}