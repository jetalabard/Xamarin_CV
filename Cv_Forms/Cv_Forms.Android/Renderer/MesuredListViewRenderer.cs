using Android.Content;
using Android.Views;
using Cv_Forms.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ListView), typeof(MesuredListViewRenderer))]
namespace Cv_Forms.Droid.Renderer
{
    public class MesuredListViewRenderer : ListViewRenderer
    {
        public MesuredListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var listView = this.Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
    }
}