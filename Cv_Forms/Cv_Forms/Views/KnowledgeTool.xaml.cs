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
    public partial class KnowledgeTool : ContentPage
    {
        private List<Knowledge> KnowledgesTools;

        public KnowledgeTool()
        {
            InitializeComponent();

            KnowledgesTools = ((KnowledgesViewModel)BindingContext).KnowledgesTools();
            if (KnowledgesTools != null && KnowledgesTools.Count > 0)
            {
                Tools = new GalleryKnowledges(KnowledgesTools).FillGrid(Tools);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Aucune connaissance d'outil n'est référencé.");
            }

        }

    }
}