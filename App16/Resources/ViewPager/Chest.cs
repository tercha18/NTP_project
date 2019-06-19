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
    class Chest : Android.Support.V4.App.Fragment
    {
        private string[] chest = { "Wyciskanie płaska", "Rozpietki", "Spietki", "Wyciskanie hantle", "Wyciskanie skos" };
        private ArrayAdapter adapter;
        private ListView exerciseList;
        //string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3
         
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.groupExerciseViewPager, container, false);

            //var db = new SQLiteConnection(_dbPath);
            exerciseList = (ListView)view.FindViewById(Resource.Id.firstText);
            adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, chest);
            exerciseList.Adapter = adapter;
            
            exerciseList.ItemClick += ExerciseList_ItemClick;
            return view;
        }

        private void ExerciseList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = chest[e.Position];
            var intent = new Intent(Activity, typeof(ExercisePage));
            intent.PutExtra("Name", "chest");
            intent.PutExtra("MyData", name);
            StartActivity(intent);
        }

        public static Chest newInstance(string text)
        {

            Chest chest = new Chest();
            Bundle bundle = new Bundle();
            bundle.PutString("msg", text);

            chest.Arguments = bundle;

            return chest;
        }
    }
}