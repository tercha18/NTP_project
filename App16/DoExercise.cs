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
    public class DoExercise : AppCompatActivity
    {
        private TextView seriesNumber;
        private TextView nameExercise;
        private TextView weightText;
        private Button plusWeight;
        private Button minusWeight;
        private TextView numberReps;
        private Button plusReps;
        private Button minusReps;
        private Button nextSeries;
        private Button finishExercise;
        private int numberSeries = 1;
        private int currentNumberTable = 0;
        private List<double> weight;
        private List<int> reps;
        private List<double> setWeight;
        private List<int> setReps;
        private int trainingId;
        private int modelView;
        private int exerciseId;
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doExercise);

            seriesNumber = FindViewById<TextView>(Resource.Id.seriesNumber);
            nameExercise = FindViewById<TextView>(Resource.Id.nameExercise);
            weightText = FindViewById<TextView>(Resource.Id.weightText);
            plusWeight = FindViewById<Button>(Resource.Id.plusWeight);
            minusWeight = FindViewById<Button>(Resource.Id.minusWeight);
            numberReps = FindViewById<TextView>(Resource.Id.numberReps);
            plusReps = FindViewById<Button>(Resource.Id.plusReps);
            minusReps = FindViewById<Button>(Resource.Id.minusReps);
            nextSeries = FindViewById<Button>(Resource.Id.nextSeries);
            finishExercise = FindViewById<Button>(Resource.Id.finishExercise);
            nameExercise.Text = Intent.GetStringExtra("NameExercise");
            trainingId = Intent.GetIntExtra("TrainingId", 0);
            modelView = Intent.GetIntExtra("ModelView", 0);
            exerciseId = Intent.GetIntExtra("ExerciseId", 0);
            weightText.Text = "40";
            numberReps.Text = "8";
            switch (modelView)
            {
                case 0:
                    getDataExerciseStatistics();
                    setCurrentStatistics(currentNumberTable); break;

                case 1: nextSeries.Text = "Dodaj serie";
                    
                    break;
                case 2: nextSeries.Text = "Dodaj serie"; break;
            }
            
            
            weight = new List<double>();
            reps = new List<int>();

            plusWeight.Click += PlusWeight_Click;
            minusWeight.Click += MinusWeight_Click;
            plusReps.Click += PlusReps_Click;
            minusReps.Click += MinusReps_Click;
            nextSeries.Click += NextSeries_Click;
            finishExercise.Click += FinishExercise_Click;

        }

        private void FinishExercise_Click(object sender, EventArgs e)
        {
            var i = reps.Count;
            if (i > 0)
            { 
                var db = new SQLiteConnection(_dbPath);
                db.CreateTable<Exercise>();
                var maxPk = db.Table<Exercise>().OrderByDescending(c => c.Id).FirstOrDefault();
                Exercise exercise = new Exercise()
                {
                    Id = (maxPk == null ? 1 : maxPk.Id + 1),
                    Name = nameExercise.Text,
                    TrainingId = trainingId
                };
                db.Insert(exercise);


                for (int j = 0; j < i; j++)
                {
                    db.CreateTable<ExerciseStatistics>();
                    ExerciseStatistics exerciseStatistics = new ExerciseStatistics()
                    {
                        Reps = reps[j],
                        Weight = weight[j],
                        ExerciseId = exercise.Id
                    };

                    db.Insert(exerciseStatistics);
                }
                for (int j = 0; j < i; j++)
                {
                    reps.Remove(j);
                    weight.Remove(j);
                }
                Toast.MakeText(this, nameExercise.Text + " dodano", ToastLength.Short).Show();
                this.Finish();

            }
            else
                Toast.MakeText(this, "Dodaj serie.", ToastLength.Short).Show();
        }

        private void NextSeries_Click(object sender, EventArgs e)
        {
            var weightCurrent = weightText.Text;
            var weightCurrentDouble = 0.0;
            var repsCurrent = numberReps.Text;
            var repsCurrentInt = 0;

            if(Double.TryParse(weightCurrent, out weightCurrentDouble) &&
                Int32.TryParse(repsCurrent, out repsCurrentInt))
            {
                switch (modelView)
                {
                    case 0:
                        Toast.MakeText(this, "Seria " + numberSeries + " zakończona.", ToastLength.Short).Show();
                        break;
                    case 1:
                        Toast.MakeText(this, "Seria " + numberSeries + " dodano.", ToastLength.Short).Show();
                        break;
                }
                reps.Add(repsCurrentInt);
                weight.Add(weightCurrentDouble);
                numberSeries++;
                if(modelView == 0)
                {
                    currentNumberTable++;
                    var size = setReps.Count;
                    if (currentNumberTable < size)
                        setCurrentStatistics(currentNumberTable);
                }
                seriesNumber.Text = numberSeries.ToString() + ": ";
                
            }
            else Toast.MakeText(this, "Niedozwolone znaki", ToastLength.Short).Show();
        }

        private void MinusReps_Click(object sender, EventArgs e)
        {
            var number = numberReps.Text;
            var numberInt = 0;
            Int32.TryParse(number, out numberInt);
            if (numberInt > 0)
                numberInt -= 1;
            numberReps.Text = numberInt.ToString();
        }

        private void PlusReps_Click(object sender, EventArgs e)
        {
            var number = numberReps.Text;
            var numberInt = 0;
            Int32.TryParse(number, out numberInt);
            if (numberInt > 0)
                numberInt += 1;
            numberReps.Text = numberInt.ToString();
        }

        private void MinusWeight_Click(object sender, EventArgs e)
        {
            var weight = weightText.Text;
            var weightDouble = 0.0;
            Double.TryParse(weight, out weightDouble);
            if (weightDouble > 0)
                weightDouble -= 2.5;
            weightText.Text = weightDouble.ToString();
        }

        private void PlusWeight_Click(object sender, EventArgs e)
        {
            var weight = weightText.Text;
            var weightDouble = 0.0;
            Double.TryParse(weight, out weightDouble);
            if (weightDouble > 0)
                weightDouble += 2.5;
            weightText.Text = weightDouble.ToString();

        }

        private void getDataExerciseStatistics()
        {
            
            var db = new SQLiteConnection(_dbPath);
            var tableExercise = db.Table<ExerciseStatistics>();
            setReps = new List<int>();
            setWeight = new List<double>();
            foreach(var item in tableExercise)
            {
                if(item.ExerciseId== exerciseId)
                {
                    setReps.Add(item.Reps);
                    setWeight.Add(item.Weight);
                }
            }
        }

        private void setCurrentStatistics(int numberSeries)
        {
            weightText.Text = setWeight[numberSeries].ToString();
            numberReps.Text = setReps[numberSeries].ToString();
        }
    }
}