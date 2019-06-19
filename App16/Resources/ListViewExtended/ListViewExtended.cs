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

namespace App16.Resources.ListViewExtended
{
    class ListViewExtended : BaseExpandableListAdapter
    {

        private Activity context;
        private List<string> listDataHeader;
        private Dictionary<string, List<string>> listDataItem;

        public ListViewExtended(Activity context, List<string> listDataHeader, Dictionary<String, List<string>> listItemData)
        {
            this.context = context;
            this.listDataHeader = listDataHeader;
            this.listDataItem = listItemData;
        }

        public override int GroupCount
        {
            get
            {
                return listDataHeader.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return listDataItem[listDataHeader[groupPosition]][childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return listDataItem[listDataHeader[groupPosition]].Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            string itemText = (string)GetChild(groupPosition, childPosition);
            if(convertView==null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.exerciseListViewItem, null);
            }
            TextView txtListItem = (TextView)convertView.FindViewById(Resource.Id.listViewItem);
            txtListItem.Text = itemText;
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listDataHeader[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string headTitle = (string)GetGroup(groupPosition);

            convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.exerciseListViewHeader, null);
            var txtListHeader = (TextView)convertView.FindViewById(Resource.Id.listViewHeader);
            txtListHeader.Text = headTitle;
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}