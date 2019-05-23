using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using System.Linq;
using System.Collections.Generic;

namespace Cv_Forms.ViewModels
{
    public class KnowledgesViewModel : BaseViewModel
    {
        private List<Knowledge> _Knowledges;

        private string _SubPage = string.Empty;
        public const string SubPagePropertyName = "SubPage";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string SubPage
        {
            get { return _SubPage; }
            set { SetProperty(ref _SubPage, value); }
        }


        public KnowledgesViewModel()
        {
            Title = Constants.APP_NAME;
            Subtitle = Constants.PAGE_KNOWLEDGE;
            var collection = DataManager.GetInstance().Knowledges();
            if(collection != null)
            {
                _Knowledges = collection.ToList();
            }
            

        }

        public List<Knowledge> KnowledgesTools()
        {
            return _Knowledges?.Where(knowledge => knowledge.Type.Equals("tool")).ToList();
        }
        public List<Knowledge> KnowledgesLanguages()
        {
            return _Knowledges?.Where(knowledge => knowledge.Type.Equals("language")).ToList();
        }

    }
}
