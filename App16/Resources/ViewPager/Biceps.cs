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
    class Biceps : Android.Support.V4.App.Fragment
    {
        private string[] biceps = { "Modlitewnik", "Uginanie hantlami", "Uginanie sztangi plaskiej", "Uginanie z linka", "Młotki" };
        ArrayAdapter adapter;
        private ListView exerciseList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.groupExerciseViewPager, container, false);

            exerciseList = (ListView)view.FindViewById(Resource.Id.firstText);
            adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, biceps);
            exerciseList.Adapter = adapter;
            exerciseList.ItemClick += ExerciseList_ItemClick;
            return view;
        }

        private void ExerciseList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = biceps[e.Position];

            var intent = new Intent(Activity, typeof(ExercisePage));
            intent.PutExtra("Name", "biceps");
            intent.PutExtra("MyData", name);
            StartActivity(intent);
        }

        public static Biceps newInstance(string text)
        {

            Biceps biceps = new Biceps();
            Bundle bundle = new Bundle();
            bundle.PutString("msg", text);

            biceps.Arguments = bundle;

            return biceps;
        }
    
    }
}