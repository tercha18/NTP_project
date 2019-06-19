using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using App16.Model;
using App16.Resources.ListViewExtended;
using SQLite;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    public class ListExercisesDone : AppCompatActivity
    {
        ExpandableListView expandableListView;
        ListViewExtended listAdapter;
        List<string> listDataHeader;
        private Dictionary<string, List<string>> listDataItem;
        // int previousGroup = -1;
        private string trainingName;
        private int trainingId;
        private List<int> exerciseId;
        private DateTime data;
        
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            data = DateTime.Now;
            trainingName = Intent.GetStringExtra("TrainingName");
            SetContentView(Resource.Layout.exerciseListView);
            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableList);
            trainingId = Intent.GetIntExtra("TrainingId", 0);
            setTitle();

            GetListData();

            listAdapter = new ListViewExtended(this, listDataHeader, listDataItem);
            expandableListView.SetAdapter(listAdapter);
        }

        private void GetListData()
        {
            listDataHeader = new List<string>();
            listDataItem = new Dictionary<string, List<string>>();
            exerciseId = new List<int>();

            var db = new SQLiteConnection(_dbPath);
            var tableExercise = db.Table<Exercise>();
            

            foreach (var item in tableExercise)
            {
                if (item.TrainingId == trainingId)
                {
                    listDataHeader.Add(item.Name);
                    exerciseId.Add(item.Id);
                }
            }

            var listSize = exerciseId.Count;
            if (listSize == 0) return;

            for(int i=0; i<listSize; i++)
            {
                var itemListText = new List<string>();
                var tableExerciseStatistics = db.Table<ExerciseStatistics>();
                foreach (var itemStats in tableExerciseStatistics)
                {
                    if (itemStats.ExerciseId == exerciseId[i])
                    {
                        var itemText = itemStats.Weight + " x " + itemStats.Reps;
                        
                        itemListText.Add(itemText);
                    }
                }
                listDataItem.Add(listDataHeader[i], itemListText);
            }
        }

        private void setTitle()
        {
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Training>();
            foreach(var item in table)
            {
                if (item.Id == trainingId)
                    this.Title = item.Name + " wykonano " + item.TrainingDate;
            }
        }
        
    }
}