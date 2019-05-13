using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TalabardJeremyCv.XView.Fragment
{
    public class ExpandableHeightGridView : GridView
    {
        public bool Expanded
        {
            get;set;
        }

        public ExpandableHeightGridView(Context context):base(context)
        {
            
        }
        public ExpandableHeightGridView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {

        }
        public ExpandableHeightGridView(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (Expanded)
            {
                int expandSpec = MeasureSpec.MakeMeasureSpec(10000, MeasureSpecMode.AtMost);
                base.OnMeasure(widthMeasureSpec, expandSpec);

                LayoutParameters.Height = MeasuredHeight;
            }
            else
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            }
        }
    }
}