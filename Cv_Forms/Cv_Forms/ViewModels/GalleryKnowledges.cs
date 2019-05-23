using Cv_Core;
using Cv_Core.DataModel;
using Cv_Forms.Controller;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Cv_Forms.ViewModels
{
    public class GalleryKnowledges
    {
        private List<Knowledge> Knowledges;

        public GalleryKnowledges(ICollection<Knowledge> Knowledges)
        {
            this.Knowledges = Knowledges.ToList();
            
        }

        public Grid FillGrid(Grid view)
        {
            return InitImageGrid(view);
        }



        private Grid InitImageGrid(Grid KnowledgesGrid, int widthcolum = 4)
        {

            int heightColumn = (Knowledges.Count() / widthcolum) + 1;
            for (int col = 0; col < widthcolum; col++)
            {
                KnowledgesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < heightColumn; row++)
            {
                KnowledgesGrid.RowDefinitions.Add(new RowDefinition());
            }

            var knowledgeIndex = 0;
            for (int rowIndex = 0; rowIndex < KnowledgesGrid.RowDefinitions.Count(); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < KnowledgesGrid.ColumnDefinitions.Count(); columnIndex++)
                {
                    if (knowledgeIndex >= Knowledges.Count)
                    {
                        break;
                    }
                    ImageButton image = CreateImageGrid(Knowledges[knowledgeIndex]);
                    knowledgeIndex += 1;
                    KnowledgesGrid.Children.Add(image, columnIndex, rowIndex);
                }

            }
            return KnowledgesGrid;
        }

        private ImageButton CreateImageGrid(Knowledge knowledge)
        {
            return new ImageButton
            {
                Source = ImageManager<string>.GetInstance().GetImageFromPath(knowledge.Image),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(() =>
                {
                    DependencyService.Get<IMessage>().ShortAlert(knowledge.Name);
                })
            };
        }
        
    }
}
