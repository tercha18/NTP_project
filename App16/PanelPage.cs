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
using SQLiteNetExtensions.Attributes;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    class PanelPage : AppCompatActivity
    {
        private ImageButton ImageButtonGuide, ImageButtonTraining, ImageButtonTrainingDone, ImageButtonBase;
        private EditText trainingName;
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");
        private int userId;
        private int done = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Title = "Gym";
            // Create your application here
            SetContentView(Resource.Layout.panelPage);
            ImageButtonGuide = FindViewById<ImageButton>(Resource.Id.imageButtonGuide);
            ImageButtonTraining = FindViewById<ImageButton>(Resource.Id.imageButtonTraining);
            ImageButtonTrainingDone = FindViewById<ImageButton>(Resource.Id.imageButtonTrainingDone);
            ImageButtonBase = FindViewById<ImageButton>(Resource.Id.imageBaseExercise);
            userId = Intent.GetIntExtra("UserId",0);
            ImageButtonGuide.Click += delegate
            {
                createPlan(userId);
            };

            ImageButtonTraining.Click += ImageButtonTraining_Click;
            ImageButtonTrainingDone.Click += ImageButtonTrainingDone_Click;
            ImageButtonBase.Click += delegate
             {
                 Intent intent = new Intent(this, typeof(EditTraining));
                 intent.PutExtra("ViewModel", 2);
                 StartActivity(intent);
             };
        }

        private void ImageButtonTrainingDone_Click(object sender, EventArgs e)
        {
            done = 1;
            Intent intent = new Intent(this, typeof(TrainPage));
            intent.PutExtra("UserId", userId);
            intent.PutExtra("ValueDone", done);
            StartActivity(intent);
        }

        private void ImageButtonTraining_Click(object sender, EventArgs e)
        {
            done = 0;
            Intent intent = new Intent(this, typeof(TrainPage));
            intent.PutExtra("UserId", userId);
            StartActivity(intent);
        }

        public void createPlan(int userId)
        {
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View view = layoutInflaterAndroid.Inflate(Resource.Layout.trainingCreate, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(view);

            trainingName = view.FindViewById<EditText>(Resource.Id.nameTraining);
            alertDialogBuilder.SetCancelable(false)
            .SetPositiveButton("Stwórz", delegate
            {
                Toast.MakeText(this, "Stwórz plan: " + trainingName.Text, ToastLength.Short).Show();
                
                var db = new SQLiteConnection(_dbPath);
                db.CreateTable<Training>();
                var maxPk = db.Table<Training>().OrderByDescending(c => c.Id).FirstOrDefault();
                Training training = new Training()
                {
                    Id = (maxPk == null ? 1 : maxPk.Id + 1),
                    Name = trainingName.Text,
                    UserId = userId,
                    TrainingDone = false
                };
                
                
                db.Insert(training);

                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("trainingId", training.Id);
                StartActivity(intent);
            })
            .SetNegativeButton("Anuluj", delegate
            {
                alertDialogBuilder.Dispose();
            });

            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogBuilder.Create();
            alertDialogAndroid.Show();
        }
    }
}