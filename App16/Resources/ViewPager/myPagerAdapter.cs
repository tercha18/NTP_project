using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace App16.Resources.ViewPager
{
    class myPagerAdapter : FragmentPagerAdapter
    {
        public myPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {

        }

        public override int Count { get { return 6; } }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return Chest.newInstance("1");
                case 1: return Back.newInstance("2");
                case 2: return Shoulder.newInstance("3");
                case 3: return Legs.newInstance("4");
                case 4: return Biceps.newInstance("5");
                case 5: return Triceps.newInstance("6");
                default: return Chest.newInstance("Error");
            }
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            switch (position)
            {
                case 0: return new Java.Lang.String("Klatka piersiowa");
                case 1: return new Java.Lang.String("Plecy");
                case 2: return new Java.Lang.String("Barki");
                case 3: return new Java.Lang.String("Nogi");
                case 4: return new Java.Lang.String("Biceps");
                case 5: return new Java.Lang.String("Triceps");
                default: return new Java.Lang.String("Error");
            }
        }

    }
}