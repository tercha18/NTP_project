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
    [Activity(Label = "RegisterPage", Theme = "@style/Theme.AppCompat")]
    public class RegisterPage : AppCompatActivity
    {
        private TextView login, password, confirmPassword;
        private Button registerButton;
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.registerPage);
            
            registerButton = FindViewById<Button>(Resource.Id.loginButton);
            registerButton.Click += RegisterButton_Click;

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            login = FindViewById<TextView>(Resource.Id.login);
            password = FindViewById<TextView>(Resource.Id.password);
            confirmPassword = FindViewById<TextView>(Resource.Id.confirmPassword);

            var db = new SQLiteConnection(dbPath); 
            db.CreateTable<User>();
            var maxPk = db.Table<User>().OrderByDescending(c => c.Id).FirstOrDefault();
            User user = new User()
            {
                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                UserName = login.Text,
                Password = password.Text
            };
            var i = user.CheckInformation(login.Text, password.Text, confirmPassword.Text);
            switch (i)
            {
                case 0:
                    db.Insert(user);
                    Toast.MakeText(this, user.UserName + " dodano", ToastLength.Short).Show();
                    Finish();
                    break;
                case 1: Toast.MakeText(this, "Hasła muszą być takie same", ToastLength.Short).Show(); break;
                case 2: Toast.MakeText(this, "Hasło musi mieć conajmniej 6 znaków", ToastLength.Short).Show(); break;
                case 3: Toast.MakeText(this, "Login musi być krótszy niż 10 znaków", ToastLength.Short).Show(); break;
                case 4: Toast.MakeText(this, "Login musi zaczynać się od litery", ToastLength.Short).Show(); break;
                case 5: Toast.MakeText(this, "Login musi mieć conajmniej 3 litery", ToastLength.Short).Show(); break;
            }
        }
    }
}