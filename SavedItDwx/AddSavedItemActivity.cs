using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using SavedItDwx.Models;

namespace SavedItDwx
{
    [Activity(Label = "AddSavedItemActivity")]
    public class AddSavedItemActivity : Activity
    {
        private EditText _descriptionText;
        private EditText _priceText;
        private TextView _totalSavedView;
        private Button _saveButton;
        private SavedItemContext _db;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_add_saved_item);
            // Create your application here

            _descriptionText = FindViewById<EditText>(Resource.Id.descriptionText);
            _priceText = FindViewById<EditText>(Resource.Id.priceText);
            _totalSavedView = FindViewById<TextView>(Resource.Id.totalSavedView);
            _saveButton = FindViewById<Button>(Resource.Id.saveButton);

            _saveButton.Click += Save;
            _db = new SavedItemContext();
        }

        private void Save(object sender, EventArgs e)
        {
            if (decimal.TryParse(_priceText.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price))
            {
                var item = new SavedItem
                {
                    Description = _descriptionText.Text,
                    Price = price,
                    Date = DateTime.Now
                };
                _db.SavedItems.Add(item);
                _db.SaveChanges();


                _descriptionText.Text = string.Empty;
                _priceText.Text = string.Empty;
                
                HideKeyboard();
                UpdateTotalLabel();
                //OnBackPressed();
            }
        }

        private void UpdateTotalLabel()
        {
            _totalSavedView.Text = _db.SavedItems.Sum(x => x.Price).ToString("C");
        }

        private void HideKeyboard()
        {
            var imm = (InputMethodManager)
                GetSystemService(InputMethodService);
            if (imm.IsAcceptingText)
            {
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }
        }
    }
}