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
using App16.Resources.ViewPager;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    class ExercisePage : AppCompatActivity
    {
        
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");

        private Button addExercise;
        private string typeOfExercise;
        private string exerciseName;
        private int trainingId;
        private ImageView image_one, image_two;
        private int viewModel;
        private int viewModel2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            exerciseName = Intent.GetStringExtra("MyData") ?? "Data not available";
            this.Title = exerciseName;
            typeOfExercise = Intent.GetStringExtra("Name") ?? "Data not available";
            viewModel = Intent.GetIntExtra("ViewModel", 0);
            viewModel2 = Intent.GetIntExtra("ViewModel2", 0);
            SetContentView(Resource.Layout.exercisePage);
            addExercise = FindViewById<Button>(Resource.Id.button1);
            image_one = FindViewById<ImageView>(Resource.Id.image_one);
            image_two = FindViewById<ImageView>(Resource.Id.image_two);

            addExercise.Click += AddExercise_Click;
            if (viewModel2 == 2)
                addExercise.Visibility = ViewStates.Invisible;
            switch (typeOfExercise)
            {
                case "back": backSet(); break;
                case "chest": chestSet(); break;
                case "legs": legsSet(); break;
                case "shoulder": shoulderSet(); break;
                case "biceps": bicepsSet(); break;
                case "triceps": tricepsSet(); break;
                default: break; 
            }

        }

        private void tricepsSet()
        {
            switch (exerciseName)
            {
                case "Prostowanie na wyciagu":
                    image_one.SetImageResource(Resource.Drawable.prostowanieWyciag1);
                    image_two.SetImageResource(Resource.Drawable.prostowanieWyciag2);
                    return;
                case "Wyciskanie wasko":
                    image_one.SetImageResource(Resource.Drawable.wasko_1);
                    image_two.SetImageResource(Resource.Drawable.wasko_2);
                    return;
                case "Francuz":
                    image_one.SetImageResource(Resource.Drawable.francuz1);
                    image_two.SetImageResource(Resource.Drawable.francuz2);
                    return;
                case "Prostowanie za glowa":
                    image_one.SetImageResource(Resource.Drawable.prostowanieZaGlowa1);
                    image_two.SetImageResource(Resource.Drawable.prostowanieZaGlowa2);
                    return;
            }
        }

        private void bicepsSet()
        {
            switch (exerciseName)
            {
                case "Modlitewnik":
                    image_one.SetImageResource(Resource.Drawable.modlitewnik1);
                    image_two.SetImageResource(Resource.Drawable.modlitewnik2);
                    return;
                case "Uginanie hantlami":
                    image_one.SetImageResource(Resource.Drawable.uginanie1);
                    image_two.SetImageResource(Resource.Drawable.uginanie2);
                    return;
                case "Uginanie sztangi plaskiej":
                    image_one.SetImageResource(Resource.Drawable.uginanieSztangi1);
                    image_two.SetImageResource(Resource.Drawable.uginanieSztangi2);
                    return;
                case "Uginanie z linka":
                    image_one.SetImageResource(Resource.Drawable.uginanieLinka1);
                    image_two.SetImageResource(Resource.Drawable.uginanieLinka2);
                    return;
                case "Młotki":
                    image_one.SetImageResource(Resource.Drawable.mlotek1);
                    image_two.SetImageResource(Resource.Drawable.mlotek2);
                    return;
            }
        }

        private void shoulderSet()
        {
            switch (exerciseName)
            {
                case "Wyciskanie hantli":
                    image_one.SetImageResource(Resource.Drawable.wyciskanieHantli1);
                    image_two.SetImageResource(Resource.Drawable.wyciskanieHantli2);
                    return;
                case "Wyciskanie żołnierskie":
                    image_one.SetImageResource(Resource.Drawable.ohp1);
                    image_two.SetImageResource(Resource.Drawable.ohp2);
                    return;
                case "Wznosy hantla":
                    image_one.SetImageResource(Resource.Drawable.wznosy1);
                    image_two.SetImageResource(Resource.Drawable.wznosy2);
                    return;
                case "Wznosy linka":
                    image_one.SetImageResource(Resource.Drawable.linka1);
                    image_two.SetImageResource(Resource.Drawable.linka2);
                    return;
            }
        }

        private void legsSet()
        {
            switch (exerciseName)
            {
                case "Przysiad":
                    image_one.SetImageResource(Resource.Drawable.siad1);
                    image_two.SetImageResource(Resource.Drawable.siad2);
                    
                    return;
                case "Suwnica wypychanie":
                    image_one.SetImageResource(Resource.Drawable.suwnica1);
                    image_two.SetImageResource(Resource.Drawable.suwnica2);
                    return;
                case "Prostowanie nóg":
                    image_one.SetImageResource(Resource.Drawable.prostowanie1);
                    image_two.SetImageResource(Resource.Drawable.prostowanie2);
                    return;
                case "Bulgarski przysiad":
                    image_one.SetImageResource(Resource.Drawable.bulgar1);
                    image_two.SetImageResource(Resource.Drawable.bulgar2);
                    return;
                case "Wykroki":
                    image_one.SetImageResource(Resource.Drawable.wykrok1);
                    image_two.SetImageResource(Resource.Drawable.wykrok2);
                    return;
            }
        }

        private void backSet()
        {
            switch (exerciseName)
            {
                case "Wiosłowanie":
                    image_one.SetImageResource(Resource.Drawable.wioslo1);
                    image_two.SetImageResource(Resource.Drawable.wioslo2);
                    return;
                case "Martwy ciąg":
                    image_one.SetImageResource(Resource.Drawable.martwy1);
                    image_two.SetImageResource(Resource.Drawable.martwy2);

                    if (viewModel2 == 2)
                    {
                        var description = FindViewById<TextView>(Resource.Id.descriptionText);
                         description.Text = "Martwy ciąg z ang. deadlift, to ćwiczenie złożone, " +
                        "czyli angażuje do pracy więcej jak jedną grupę mięśniowa. W związku z tym, " +
                        "że największa prace w tym ćwiczeniu wykonuje dolna część grzbietu, to powszechnie uznano, " +
                        "że jest to ćwiczenie na prostowniki grzbietu. Tradycyjny martwy ciąg często mylony jest z martwym ciągiem na prostych nogach, " +
                        "ten drugi nakierowany jest na trening mięśni dwugłowych ud. "; }
                    return;
                case "Ściaganie wyciagu":
                    image_one.SetImageResource(Resource.Drawable.sciaganie1);
                    image_two.SetImageResource(Resource.Drawable.sciaganie2);
                    return;
                case "Podciaganie":
                    image_one.SetImageResource(Resource.Drawable.podciagani1);
                    image_two.SetImageResource(Resource.Drawable.podciaganie2);
                    return;
                case "Narciarz":
                    image_one.SetImageResource(Resource.Drawable.narciarz1);
                    image_two.SetImageResource(Resource.Drawable.narciarz2);
                    return;
            }
        }
        
        private void chestSet()
        {
            switch(exerciseName)
            {
                case "Wyciskanie płaska":
                    image_one.SetImageResource(Resource.Drawable.wyciskanie_1);
                    image_two.SetImageResource(Resource.Drawable.wyciskanie_2);
                    if (viewModel2 == 2)
                    {
                        var description = FindViewById<TextView>(Resource.Id.descriptionText);
                        description.Text = "Ćwiczenie wykonuje się w leżeniu na plecach na płaskiej ławce, " +
                            "stopy oparte są na podłożu, sztangę trzymamy nad sobą, nachwytem, nieco szerzej od szerokości barków. " +
                            "Bierzemy wdech i opuszczamy sztangę do środkowej części klatki piersiowej (nieco powyżej linii sutków), " +
                            "ramiona nie powinny rozchodzić się na boki. W momencie kiedy gryf sztangi dotknie klatki piersiowej, wyciskamy (wypychamy) ją z powrotem do pozycji wyjściowej, " +
                            "robiąc w końcowej fazie ruchu wydech.";
                    }

                    return;
                case "Rozpietki":
                    image_one.SetImageResource(Resource.Drawable.rozpietki1);
                    image_two.SetImageResource(Resource.Drawable.rozpietki2);
                    return;
                case "Spietki":
                    image_one.SetImageResource(Resource.Drawable.spietki1);
                    image_two.SetImageResource(Resource.Drawable.spietki2);
                    return;
                case "Wyciskanie hantle":
                    image_one.SetImageResource(Resource.Drawable.wyciskanieHantli1);
                    image_two.SetImageResource(Resource.Drawable.wyciskanieHantli2);
                    return;
                case "Wyciskanie skos":
                    image_one.SetImageResource(Resource.Drawable.skos1);
                    image_two.SetImageResource(Resource.Drawable.skos2);
                    return;
            }
        }

        private void AddExercise_Click(object sender, EventArgs e)
        {
            var i = 0;
            //var db = new SQLiteConnection(_dbPath);
            //db.CreateTable<Exercise>();
            //var maxPk = db.Table<Exercise>().OrderByDescending(c => c.Id).FirstOrDefault();
            //int id = (maxPk == null ? 1 : maxPk.Id + 1);
            switch (viewModel)
            {
                case 0:
                    //Exercise exercise = new Exercise()
                    //{
                    //    Id = (maxPk == null ? 1 : maxPk.Id + 1),
                    //    Name = exerciseName,
                    //    TrainingId = indexTraining()
                    //};
                    if (i==0)
                        trainingId = indexTraining();
                    if (CheckTableList(exerciseName,0))
                    {
                        //db.Insert(exercise);
                        Intent intent = new Intent(this, typeof(DoExercise));
                        intent.PutExtra("NameExercise", exerciseName);
                        //intent.PutExtra("ExerciseId", id);
                        intent.PutExtra("TrainingId", trainingId);
                        intent.PutExtra("ModelView", 1);
                        StartActivity(intent);
                    }
                    else
                        Toast.MakeText(this, "Ćwiczenie już jest w planie", ToastLength.Short).Show();
                    Finish();
                    break;
                case 1:
                    var id= Intent.GetIntExtra("TrainingId", 0);
                    
                    if (CheckTableList(exerciseName, id))
                    {
                        //db.Insert(exerciseTwo);
                        //Toast.MakeText(this, exerciseName + " dodano", ToastLength.Short).Show();
                        Intent intent = new Intent(this, typeof(DoExercise));
                        intent.PutExtra("NameExercise", exerciseName);
                        //intent.PutExtra("ExerciseId", id);
                        intent.PutExtra("TrainingId", id);
                        intent.PutExtra("ModelView", 2);
                        StartActivity(intent);
                    }
                    else
                        Toast.MakeText(this, "Ćwiczenie już jest w planie", ToastLength.Short).Show();
                    Finish();
                    break;
            }
        }

        private bool CheckTableList(string name, int id)
        {
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Exercise>();
            switch(viewModel)
            {
                case 0:
                    foreach (var item in table)
                    {
                        if (indexTraining() == item.TrainingId && item.Name == name)
                            return false;
                    }
                    return true;
                case 1:
                    foreach (var item in table)
                    {
                        if (id == item.TrainingId && item.Name == name)
                            return false;
                    }
                    return true;
            }
            return true;
        }

        private int indexTraining()
        {
            var db = new SQLiteConnection(_dbPath);
            var max = db.Table<Training>().Max(c=>c.Id);
            return max;
        }
    }
}