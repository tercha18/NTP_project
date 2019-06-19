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
    class Exercise
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(typeof(Training))]
        public int TrainingId { get; set; }

        [OneToOne]
        public Training Training { get; set; }

        public Exercise()
        { }
    }
}