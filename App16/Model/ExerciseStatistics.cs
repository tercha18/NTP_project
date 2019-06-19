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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App16.Model
{
    class ExerciseStatistics
    {
        public double Weight { get; set; }
        public int Reps { get; set; }

        [ForeignKey(typeof(Exercise))]
        public int ExerciseId { get; set; }

        [OneToOne]
        public Exercise Exercise { get; set; }


        public ExerciseStatistics() { }

    }
}