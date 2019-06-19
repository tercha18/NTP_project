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

namespace App16.Model
{
    class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
        
        public override string ToString()
        {
            return UserName + " " + Password;
        }

        public int CheckInformation(string name, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return 1;
            else if (password.Length < 6)
                return 2;
            else if (name.Length > 10)
                return 3;
            else if ((int)name[0] < 65 || ((int)name[0] > 90 && (int)name[0] < 97) && (int)name[0] > 122)
                return 4;
            else if (name.Length < 3)
                return 5;
            return 0;
        }
    }
}