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
using App16.Resources.ListViewExtended;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    class EditTraining : AppCompatActivity
    {
        ExpandableListView expandableListView;
        ListViewExtended listAdapter;
        List<string> listDataHeader;
        private Dictionary<string, List<string>> listDataItem;
        private int trainingId;
        private int modelView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Title = "Lista ćwiczeń";
            SetContentView(Resource.Layout.exerciseListView);
            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableList);
            trainingId = Intent.GetIntExtra("TrainingId", 0);
            modelView = Intent.GetIntExtra("ViewModel", 0);

            GetListData();
            expandableListView.ChildClick += ExpandableListView_ChildClick;

            listAdapter = new ListViewExtended(this, listDataHeader, listDataItem);
            expandableListView.SetAdapter(listAdapter);
        }

        private void ExpandableListView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            string[] title = new string[] { "chest", "shoulder", "back", "triceps", "biceps", "legs" };
            
            Intent intent = new Intent(this, typeof(ExercisePage));
            intent.PutExtra("MyData", (string)listAdapter.GetChild(e.GroupPosition, e.ChildPosition));
            intent.PutExtra("Name",title[(int)listAdapter.GetGroupId(e.GroupPosition)]);
            intent.PutExtra("TrainingId", trainingId);
            intent.PutExtra("ViewModel", 1);
            if(modelView==2)
            intent.PutExtra("ViewModel2", 2);
            StartActivity(intent);
        }

        private void GetListData()
        {
            listDataHeader = new List<string>();
            listDataItem = new Dictionary<string, List<string>>();

            string[] title = new string[] { "Klatka piersiowa", "Barki", "Plecy", "Triceps", "Biceps", "Nogi" };
            
            foreach (var item in title)
            {
                listDataHeader.Add(item);
            }

            List<List<string>> temp = new List<List<string>>();
            List<string> exerciseChest = new List<string>();

            exerciseChest.Add("Wyciskanie płaska");
            exerciseChest.Add("Rozpietki");
            exerciseChest.Add("Spietki");
            exerciseChest.Add("Wyciskanie hantle");
            exerciseChest.Add("Wyciskanie skos");
            temp.Add(exerciseChest);

            List<string> exerciseShoulder = new List<string>();

            exerciseShoulder.Add("Wyciskanie hantli");
            exerciseShoulder.Add("Wyciskanie żołnierskie");
            exerciseShoulder.Add("Wznosy hantla");
            exerciseShoulder.Add("Wznosy linka");
            temp.Add(exerciseShoulder);

            List<string> exerciseBack = new List<string>();

            exerciseBack.Add("Wiosłowanie");
            exerciseBack.Add("Martwy ciąg");
            exerciseBack.Add("Ściaganie wyciagu");
            exerciseBack.Add("Podciaganie");
            exerciseBack.Add("Narciarz");
            temp.Add(exerciseBack);

            List<string> exerciseTriceps = new List<string>();

            exerciseTriceps.Add("Prostowanie na wyciagu");
            exerciseTriceps.Add("Wyciskanie wasko");
            exerciseTriceps.Add("Francuz");
            exerciseTriceps.Add("Prostowanie za glowa");
            temp.Add(exerciseTriceps);

            List<string> exerciseBiceps = new List<string>();

            exerciseBiceps.Add("Uginanie hantlami");
            exerciseBiceps.Add("Modlitewnik");
            exerciseBiceps.Add("Uginanie sztangi plaskiej");
            exerciseBiceps.Add("Uginanie z linka");
            exerciseBiceps.Add("Młotki");
            temp.Add(exerciseBiceps);

            List<string> exerciseLegs = new List<string>();

            exerciseLegs.Add("Przysiad");
            exerciseLegs.Add("Suwnica wypychanie");
            exerciseLegs.Add("Prostowanie nóg");
            exerciseLegs.Add("Bulgarski przysiad");
            exerciseLegs.Add("Wykroki");
            temp.Add(exerciseLegs);
            
            for (int i = 0; i < 6; i++)
            {
                listDataItem.Add(listDataHeader[i], temp[i]);
            }
        }
    }
}