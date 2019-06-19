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
using Android.Views;
using Android.Widget;
using App16.Model;
using SQLite;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    class TrainPage: AppCompatActivity
    {
        private ListView userTraining;

        private List<string> trainingList;
        private int userId;
        private List<int> listOfId;
        private int modelView;
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.trainPage);
            userId = Intent.GetIntExtra("UserId", 5);
            modelView = Intent.GetIntExtra("ValueDone", 0);
            userTraining = FindViewById<ListView>(Resource.Id.userTraining);
            switch (modelView)
            {
                case 0: ViewUserTraining(); this.Title = "Moje treningi"; break;
                case 1: ViewUserTrainingDone(); this.Title = "Skończone treningi"; break;
            } 
            switch(modelView)
            {
                case 0: userTraining.ItemClick += UserTraining_ItemClick; break;
                case 1: userTraining.ItemClick += UserTraining_ItemClickTrainingDone; break;
            }
            
        }

        private void UserTraining_ItemClickTrainingDone(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intentTwo = new Intent(this, typeof(ListExercisesDone));
            intentTwo.PutExtra("UserId", userId);
            intentTwo.PutExtra("TrainingId", listOfId[e.Position]);
            intentTwo.PutExtra("TrainingName", trainingList[e.Position]);
            StartActivity(intentTwo);
        }

        private void ViewUserTrainingDone()
        {
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Training>();
            trainingList = new List<string>();
            listOfId = new List<int>();

            foreach (var item in table)
            {
                if (item.UserId == userId && item.TrainingDone)
                {
                    trainingList.Add(item.Name);
                    listOfId.Add(item.Id);
                }
            }
            var text = FindViewById<TextView>(Resource.Id.textView1);
            text.Text = "Wykonane treningi: ";

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, trainingList);
            userTraining.Adapter = adapter;
        }

        private void UserTraining_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View view = layoutInflaterAndroid.Inflate(Resource.Layout.actionWithTrainingLayout, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(view);
            Button editTraining = (Button)view.FindViewById(Resource.Id.editTraining);
            Button doTraining = (Button)view.FindViewById(Resource.Id.doTraining);
            alertDialogBuilder.SetCancelable(false)
            

            .SetNegativeButton("Anuluj", delegate
            {
                alertDialogBuilder.Dispose();
            });

            editTraining.Click += delegate
             {
                 Intent intent = new Intent(this, typeof(ListExerciseTraining));
                 intent.PutExtra("modelView", 1);
                 intent.PutExtra("UserId", userId);
                 intent.PutExtra("TrainingId", listOfId[e.Position]);
                 intent.PutExtra("TrainingName", trainingList[e.Position]);
                 StartActivity(intent);
             };

            doTraining.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ListExerciseTraining));
                intent.PutExtra("UserId", userId);
                intent.PutExtra("TrainingId", listOfId[e.Position]);
                intent.PutExtra("TrainingName", trainingList[e.Position]);
                StartActivity(intent);
                Finish();
            };

            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogBuilder.Create();
            alertDialogAndroid.Show();
        }

        private void ViewUserTraining()
        {
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Training>();
            trainingList = new List<string>();
            listOfId = new List<int>();

            foreach (var item in table)
            {
                if(item.UserId==userId && !item.TrainingDone)
                {
                    trainingList.Add(item.Name);
                    listOfId.Add(item.Id);
                } 
            }
       
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, trainingList);
            userTraining.Adapter = adapter;
        }
    }
}