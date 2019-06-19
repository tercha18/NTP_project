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
using System.Data.SqlClient;
using System.Data;
using SQLite;
using SQLiteNetExtensions.Extensions;


namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat", MainLauncher = true)]
    class LoginPage : AppCompatActivity
    {
        TextView loginName;
        TextView password;
        Button loginButton;
        Button registerButton;
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");
       

        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "Panel logowania";
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.loginPage);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;
            registerButton = FindViewById<Button>(Resource.Id.registerButton);
            registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterPage));
            StartActivity(intent);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            loginName = FindViewById<TextView>(Resource.Id.login);
            password = FindViewById<TextView>(Resource.Id.password);
            
            bool i = false;
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<User>();
            foreach (var item in table)
            {
                if (item.UserName == loginName.Text.ToString() && item.Password == password.Text.ToString())
                {
                    Intent intent = new Intent(this, typeof(PanelPage));
                    intent.PutExtra("UserId", item.Id);
                    StartActivity(intent);
                    i = true;
                }
                
            }
            if(!i)
            Toast.MakeText(this, "Zły login lub hasło", ToastLength.Short).Show();

           /*
            
            TextView displayText = FindViewById<TextView>(Resource.Id.textView3);
            var db = new SQLiteConnection(_dbPath);
            var table = db.Table<Training>();


            var personStored = db.GetWithChildren<Training>(10);
            
            foreach (var item in table)
            {
                
                Training user = new Training(item.Name, item.UserId);
                displayText.Text += user + "\n";
            }*/
        }

       
    }
}