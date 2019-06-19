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
    public class ListExerciseTraining : AppCompatActivity
    {
        private TextView text;
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");
        private List<string> exerciseList;
        private List<int> exerciseId;
        private int trainingId;
        private int userId;
        private string trainingName;
        private int trainingIdCurrent;
        private ListView exerciseListView;
        private Button startTraining;
        private bool flag = false;
        private int modelNumber = 0;
        private Button finishTraining;
        private int i = 0;
        private DatePicker dateTraining;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.listExerciseTraining);
            text = FindViewById<TextView>(Resource.Id.textView1);
            exerciseListView = FindViewById<ListView>(Resource.Id.userTraining);
            startTraining = FindViewById<Button>(Resource.Id.startTraining);
            finishTraining = FindViewById<Button>(Resource.Id.endTraining);
            modelNumber = Intent.GetIntExtra("modelView", 0);

            trainingId = Intent.GetIntExtra("TrainingId", 0);
            userId = Intent.GetIntExtra("UserId", 0);
            trainingName = Intent.GetStringExtra("TrainingName");
            this.Title = "Trenuj";

            switch(modelNumber)
            {
                case 1: startTraining.Text = "Dodaj ćwiczenia";
                    finishTraining.Visibility = ViewStates.Invisible;
                    this.Title = "Edytuj trening"; 
                    break;
            }

            ViewExerciseTraining();

            startTraining.Click += StartTraining_Click;
            finishTraining.Click += delegate
             {
                 Finish();
             };

            exerciseListView.ItemClick += ExerciseListView_ItemClick;
            // Create your application here
        }

        private void StartTraining_Click(object sender, EventArgs e)
        {
            switch(modelNumber)
            {
                case 0:
                    flag = true;

                    LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
                    View view = layoutInflaterAndroid.Inflate(Resource.Layout.trainingDate, null);
                    Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    alertDialogBuilder.SetView(view);
                    dateTraining = view.FindViewById<DatePicker>(Resource.Id.dateTraining);
                    
                    alertDialogBuilder.SetCancelable(false)
                    .SetPositiveButton("Stwórz", delegate
                    {
                        var db = new SQLiteConnection(_dbPath);
                        db.CreateTable<Training>();
                        var maxPk = db.Table<Training>().OrderByDescending(c => c.Id).FirstOrDefault();
                        Training training = new Training()
                        {
                            Id = (maxPk == null ? 1 : maxPk.Id + 1),
                            Name = trainingName,
                            UserId = userId,
                            TrainingDone = true,
                            TrainingDate = getDate()
                        };
                        trainingIdCurrent = training.Id;

                        db.Insert(training);
                        startTraining.Visibility = ViewStates.Invisible;
                        Toast.MakeText(this, trainingName + " rozpoczeto", ToastLength.Short).Show();
                    })
                    .SetNegativeButton("Anuluj", delegate
                    {
                        alertDialogBuilder.Dispose();
                    });

                    Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogBuilder.Create();
                    alertDialogAndroid.Show();

                    break;
                    
                    
                case 1:
                    Intent intent = new Intent(this, typeof(EditTraining));
                    intent.PutExtra("TrainingId", trainingId);
                    StartActivity(intent);
                    Finish();
                    break;
            }
            
            
        }

        private void ExerciseListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (flag)
            {
                Intent intent = new Intent(this, typeof(DoExercise));
                intent.PutExtra("NameExercise", exerciseList[e.Position]);
                intent.PutExtra("ExerciseId", exerciseId[e.Position]);
                intent.PutExtra("TrainingId", trainingIdCurrent);
                StartActivity(intent);

            }
            else 
                if(modelNumber==0)
                Toast.MakeText(this, "Rozpocznij trening.", ToastLength.Short).Show();
        }

        private void ViewExerciseTraining()
        {
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Exercise>();
            exerciseList = new List<string>();
            exerciseId = new List<int>();

            foreach (var item in table)
            {
                if (item.TrainingId == trainingId)
                {
                    exerciseList.Add(item.Name);
                    exerciseId.Add(item.Id);
                }
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, exerciseList);
            exerciseListView.Adapter = adapter;
        }

        private string getDate()
        {
            string strCurrentDate;
            int month = dateTraining.Month+1;
            strCurrentDate = (dateTraining.DayOfMonth + "-" + month + "-" + dateTraining.Year);
            return strCurrentDate.ToString();
        }
    }

}