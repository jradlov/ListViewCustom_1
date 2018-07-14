using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


// ListView_1 creates a Custom Adapter for lists of single lines of text.
// If we want more than one line of text we have to create a Custom Row.
// A Custom Row is a layout: we create custom_row.axml in Resources/layout.
// Implement the Custom Row in MyAdapter GetView() method.

// Steps for Custom Row:
// 1) design Custom Row in axml file under Rwsources/layout
// 2) in GetView method in custom adapter, inflate Custom Row you designed
//    view = context.LayoutInflater.Inflate(Resource.Layout.custom_row, null);
// 3) Find views you want to populate with data
// 4) Populate them
//    view.FindViewById<TextView>(Resource.Id.textViewCustomRow).Text = item;


namespace ListViewCustom_1 {
	public class MyAdapter : BaseAdapter<string> {
		List<string> items;  // list of strings to populate the adapter
		Activity context;

		public MyAdapter(Activity context, List<string> items) {
			this.items = items;
			this.context = context;
		}


		// Below: default methods that need to be called when the adapter is fired

		public override long GetItemId(int position) { // return current adapter position
			return position;
		}

		public override string this[int index] {  // return item in question
			get { return items[index]; }
		}

		public override int Count {   // return the length of the list (so android knows how many rows to create)
			get { return items.Count; }  // .Count for List, items.Length if array
		}

		// since it's a custom adapter, we have to create a way to display our data:
		// GetView(): create/recycle row & allow you to populate it with data
		public override View GetView(int position, View convertView, ViewGroup parent) {

			var item = items[position];

			// converView is the recycled row that scrolled off screen that we can reuse
			View view = convertView;

			CustomRowHolder viewHolder;

			if (view == null) {  // we didn't get a recycled cell/row: have to create one
										// view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
										// replace SimpleListItem1 with our own Custom Row:
				view = context.LayoutInflater.Inflate(Resource.Layout.custom_row, null);

				viewHolder = new CustomRowHolder();
				viewHolder.textViewCustomRow = view.FindViewById<TextView>(Resource.Id.textViewCustomRow);
				viewHolder.imgViewCustoRow = view.FindViewById<ImageView>(Resource.Id.imgViewCustoRow);
				viewHolder.btnCustomRow = view.FindViewById<Button>(Resource.Id.btnCustomRow);

				viewHolder.btnCustomRow.Click += OnBtnClick;
				viewHolder.imgViewCustoRow.Click += OnImageClick;

				view.Tag = viewHolder;

				// define brn click event here so it's done only once
				//var btn = view.FindViewById<Button>(Resource.Id.btnCustomRow);
				/*btn.Click += (sender, args) => {
					Log.Debug("-----DEBUG-----", "btn clicked at row = " + position);
				};*/

			}
			else { viewHolder = (CustomRowHolder)view.Tag;  }

			

			// this 'if' is not needed, but good away to add ad/purchase to listview (trick users into buying something :) 
			// can also cut list from 100 items to 10 with last row asking for subscription to get the whole list :) !!!
			if (position == 3)   // replace item 3 with below
				item = "(Available to subscribers only)";

			// inside this row we have some text elements we'd like to populate with data
			// (Text1: default property from SimpleListItem1)
			//view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item;
			//view.FindViewById<TextView>(Resource.Id.textViewCustomRow).Text = item;

			viewHolder.textViewCustomRow.Text = item;

			viewHolder.imgViewCustoRow.Tag = position;
			viewHolder.btnCustomRow.Tag = position;


			//var clock = view.FindViewById<AnalogClock>(Resource.Id.analogClockCustomRow);

			// need to change backround color for every row with else statement.
			// witout else, the red row get recycled from pos 3 to 15,30,50,... whatever
			// because it's set to red and not changed when recycled.
			/*if (position == 3)
				clock.SetBackgroundColor(new Android.Graphics.Color(222, 99, 99));
			else
				clock.SetBackgroundColor(new Android.Graphics.Color(99, 99, 222)); */

			return view;
		}

		private void OnBtnClick(object sender, EventArgs e) {
			var position = (int)(sender as Button).Tag;

			Log.Debug("-----DEBUG-----", "btn clicked at row: " + position);
		}

		private void OnImageClick(object sender, EventArgs e) {
			var position = (int)(sender as ImageView).Tag;

			Log.Debug("-----DEBUG-----", "image clicked at row: " + position);
		}
	}
}