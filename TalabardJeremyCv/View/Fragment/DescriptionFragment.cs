using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using System;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Cv_Core.DataModel;
using Cv_Core.DataManagement;
using Cv_Core;

namespace TalabardJeremyCv.XView.XFragment
{
    public class DescriptionFragment : Android.App.Fragment
    {
        private Description _description;
        private List<Header> _headers;

        public static DescriptionFragment NewInstance()
        {
            return new DescriptionFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _description = DataManager.GetInstance().Description();
            _headers = DataManager.GetInstance().Headers().ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.description, container, false);

            ImageView image = view.FindViewById<ImageView>(Resource.Id.ProfilImage);
            TextView descriptionLabel = view.FindViewById<TextView>(Resource.Id.labelDescription);

            if (_description != null)
            {
                descriptionLabel.SetText(_description.Summary, TextView.BufferType.Normal);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                {
                    ((AppCompatActivity)Activity).SupportActionBar.Subtitle = _description.Title;
                }
                else
                {
                    TextView TitleLabel = view.FindViewById<TextView>(Resource.Id.labelTitle);
                    TitleLabel.SetText(_description.Title, TextView.BufferType.Normal);
                    TitleLabel.Visibility = ViewStates.Invisible;
                }

                image.SetImageBitmap(ImageManager.GetBitmapFromPath(_description.Image));
            }
            InitCardView(view, Resource.Id.schoolLink, Resource.Id.TitleSchool, Resource.Id.DescriptionSchool, "Formations", OnClickSchoolCardView);
            InitCardView(view, Resource.Id.projectLink, Resource.Id.TitleProject, Resource.Id.DescriptionProject, "Projets et Stages", OnClickProjectCardView);
            InitCardView(view, Resource.Id.personalProjectLink, Resource.Id.TitlePersonal, Resource.Id.DescriptionPersonal, "Projets Personnels", OnClickPersonalProjectCardView);
            InitCardView(view, Resource.Id.hobiesLink, Resource.Id.TitleHobies, Resource.Id.DescriptionHobies, "Loisirs", OnClickHobieCardView);
            InitCardView(view, Resource.Id.jobLink, Resource.Id.TitleJob, Resource.Id.DescriptionJob, "Emplois", OnClickJobCardView);
            InitCardView(view, Resource.Id.knowledgesLink, Resource.Id.TitleKnowledge, Resource.Id.DescriptionKnowledge, "Mes compétences informatiques", OnClickKnowledgeCardView);

            return view;
        }

        private void InitCardView(View view, int resourceLink, int resourceTitle, int resourceDescription, string titleHeader, EventHandler methodOnClick)
        {
            CardView card = view.FindViewById<CardView>(resourceLink);
            TextView title = view.FindViewById<TextView>(resourceTitle);
            TextView description = view.FindViewById<TextView>(resourceDescription);
            Header header = _headers.FirstOrDefault(act => act.Title.Equals(titleHeader));
            title.Text = header.Title;
            description.Text = header.Summary;
            card.Click += methodOnClick;
        }

        private void OnClickProjectCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new ListActivitiesFragment<Project>());
        }

        private void OnClickPersonalProjectCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new ListActivitiesFragment<PersonalProject>());
        }

        private void OnClickHobieCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new ListActivitiesFragment<Hobie>());
        }

        private void OnClickJobCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new ListActivitiesFragment<Job>());
        }

        private void OnClickKnowledgeCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new KnowledgeFragment());
        }

        private void OnClickSchoolCardView(object sender, EventArgs e)
        {
            var parentActivity = (HomeActivity)Activity;
            parentActivity.ChangeFragment(new ListActivitiesFragment<Training>());
        }
    }
}