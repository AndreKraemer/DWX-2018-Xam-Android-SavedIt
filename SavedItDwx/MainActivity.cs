using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using SavedItDwx.Models;

namespace SavedItDwx
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
		}

	    protected override void OnStart()
	    {
	        base.OnStart();
	        var db = new SavedItemContext();
	        db.Database.EnsureCreated();
	        var listView = FindViewById<ListView>(Resource.Id.savedItemsListView);
	        listView.Adapter = new SavedItemsAdapter(this, db.SavedItems.ToList());
	    }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(AddSavedItemActivity));

            StartActivity(intent);
        }
	}

    public class SavedItemsAdapter : BaseAdapter<SavedItem>
    {
        private readonly MainActivity _context;
        private readonly List<SavedItem> _items;

        public SavedItemsAdapter(MainActivity context, List<SavedItem> items)
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this[position];
            var view = convertView ?? _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Description;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = $"{item.Price:C} ({item.Date:g})";
            return view;
        }

        public override int Count => _items.Count;

        public override SavedItem this[int position] => _items[position];

    }
}

