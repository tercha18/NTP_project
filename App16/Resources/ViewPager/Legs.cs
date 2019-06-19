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
    class Legs : Android.Support.V4.App.Fragment
    {
        private string[] legs = { "Przysiad", "Suwnica wypychanie", "Prostowanie nóg", "Bulgarski przysiad", "Wykroki" };
        ArrayAdapter adapter;
        private ListView exerciseList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.groupExerciseViewPager, container, false);

            exerciseList = (ListView)view.FindViewById(Resource.Id.firstText);
            adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, legs);
            exerciseList.Adapter = adapter;
            exerciseList.ItemClick += ExerciseList_ItemClick;
            return view;
        }

        private void ExerciseList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = legs[e.Position];
            var intent = new Intent(Activity, typeof(ExercisePage));
            intent.PutExtra("Name", "legs");
            intent.PutExtra("MyData", name);
            StartActivity(intent);
        }

        public static Legs newInstance(string text)
        {

            Legs legs = new Legs();
            Bundle bundle = new Bundle();
            bundle.PutString("msg", text);

            legs.Arguments = bundle;

            return legs;
        }
    }
}