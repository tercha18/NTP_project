using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App16.Resources.ViewPager
{
    class Triceps : Android.Support.V4.App.Fragment
    {
        private string[] triceps = { "Prostowanie na wyciagu", "Wyciskanie wasko", "Francuz", "Prostowanie za glowa" };
        ArrayAdapter adapter;
        private ListView exerciseList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.groupExerciseViewPager, container, false);

            exerciseList = (ListView)view.FindViewById(Resource.Id.firstText);
            adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, triceps);
            exerciseList.Adapter = adapter;
            exerciseList.ItemClick += ExerciseList_ItemClick;
            return view;
        }

        private void ExerciseList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = triceps[e.Position];
            var intent = new Intent(Activity, typeof(ExercisePage));
            intent.PutExtra("Name", "triceps");
            intent.PutExtra("MyData", name);
            StartActivity(intent);
        }

        public static Triceps newInstance(string text)
        {

            Triceps triceps = new Triceps();
            Bundle bundle = new Bundle();
            bundle.PutString("msg", text);

            triceps.Arguments = bundle;

            return triceps;
        }
    }
}