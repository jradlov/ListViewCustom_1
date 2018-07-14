using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using Android.Util;

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


// Create a custom adapter: MyAdapter class
// Custom Adapter works the same as SimpleListItem1 from ListView_0
// but now can add code for changing rows for ads/purchase


namespace ListViewCustom_1 
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity 
	{
		List<string> data;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			// create data set for listview
			//var data = new List<string>();
			data = new List<string>();
			for (int i = 0; i < 100; i++)
				data.Add("Item number: " + i);

			// reference listView with layout resource
			var listView = FindViewById<ListView>(Resource.Id.listView1);
			listView.FastScrollEnabled = true;  // enable vertical scrollbar 

			// create custom adapter and apply it to listView
			var adapter = new MyAdapter(this, data);
			listView.Adapter = adapter;

			listView.ItemClick += ListView_ItemClick; // ListView_ItemClick suggested by Intellisense 

		}

		private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
			//Log.Debug("-----DEBUG-----", "Number: " + e.Position);

			var listView = sender as ListView; 
			var item = data[e.Position];
			Log.Debug("-----DEBUG-----", "Number: " + item);
		}
	}
}

