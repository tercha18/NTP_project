using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Support.V4.View;
using Android.Views;
using Android.Support.V4.App;
using System;
using Java.Lang;
using App16.Resources.ViewPager;

namespace App16
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private int trainingId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Title = "Lista ćwiczeń";
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            trainingId = Intent.GetIntExtra("trainingId", 0);
            ViewPager pager = (ViewPager)FindViewById(Resource.Id.viewpager); 
            pager.Adapter=(new myPagerAdapter(SupportFragmentManager));
        }


    }
}
