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
using App16.Model;

namespace App16.Resources.ViewPager
{
    class Back : Android.Support.V4.App.Fragment
    {
        private string[] back = { "Wiosłowanie", "Martwy ciąg", "Ściaganie wyciagu", "Podciaganie", "Narciarz" };
        private ArrayAdapter adapter;
        private ListView exerciseList;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.groupExerciseViewPager, container, false);
           
            exerciseList = (ListView)view.FindViewById(Resource.Id.firstText);
            adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, back);
            exerciseList.Adapter = adapter;
            exerciseList.ItemClick += ExerciseList_ItemClick;

            return view;
        }

        private void ExerciseList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = back[e.Position];
            
            var intent = new Intent(Activity, typeof(ExercisePage));
            intent.PutExtra("Name", "back");
            intent.PutExtra("MyData", name);
            StartActivity(intent);
        }

        public static Back newInstance(string text)
        {
            Back back = new Back();
            Bundle bundle = new Bundle();
            bundle.PutString("msg", text);

            back.Arguments = bundle;

            return back;
        }


    }
}