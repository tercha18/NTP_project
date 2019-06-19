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
    class Training
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        [OneToOne]
        public User User { get; set; }

        List<string> items = new List<string>();

        public bool TrainingDone { get; set; }

        public string TrainingDate { get; set; }
        
        public Training()
        {

        }

        public Training(string name)
        {
            this.Name = name;
        }

        public Training(string name, int userId)
        {
            this.Name = name;
            this.UserId = userId;
        }
    }
        
}