﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Globalization;

namespace Opora
{
    [Activity(Label = "Opora", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string Warning1 = "Неверные исходные данные";
        const string Warning2 = "Требуется выправка или замена опоры контактной сети";

        private Button _button;
        private EditText _editTextH;
        private EditText _editTextX;
        private EditText _editTextH1;
        private EditText _editTextH2;
        private EditText _editTextResult;
        private TextView _labelWarning;
        
        /// <summary>
        /// Высота опоры
        /// </summary>
        private EditText EditTextH
        {
            get { return _editTextH ?? (_editTextH = FindViewById<EditText>(Resource.Id.entry2)); }
        }

        /// <summary>
        /// Конусность опоры
        /// </summary>
        private EditText EditTextX
        {
            get { return _editTextX ?? (_editTextX = FindViewById<EditText>(Resource.Id.entry3)); }
        }

        /// <summary>
        /// Первое изменение
        /// </summary>
        private EditText EditTextH1
        {
            get { return _editTextH1 ?? (_editTextH1 = FindViewById<EditText>(Resource.Id.entry4)); }
        }

        /// <summary>
        /// Второе изменение
        /// </summary>
        private EditText EditTextH2
        {
            get { return _editTextH2 ?? (_editTextH2 = FindViewById<EditText>(Resource.Id.entry5)); }
        }

        private TextView LabelWarning
        {
            get { return _labelWarning ?? (_labelWarning = FindViewById<TextView>(Resource.Id.label8)); }
        }

        /// <summary>
        /// Угол наклона
        /// </summary>
        private EditText EditTextResult
        {
            get { return _editTextResult ?? (_editTextResult = FindViewById<EditText>(Resource.Id.entry6)); }
        }

        private Button Button1
        {
            get { return _button ?? (_button = FindViewById<Button>(Resource.Id.MyButton)); }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button1.Click += ButtonClick;

            //
            EditTextH.Text = EditTextH1.Text = EditTextH2.Text = EditTextX.Text = EditTextResult.Text = (0.0).ToString("F1");
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            double h, h1, h2, x;
            if (!TryParse(EditTextH.Text, out h) || !TryParse(EditTextH1.Text, out h1) || !TryParse(EditTextH2.Text, out h2) ||
                !TryParse(EditTextX.Text, out x))
            {
                LabelWarning.Text = Warning1;
                return;
            }

            double result = x - Math.Abs(h1 - h2) * h;
            EditTextResult.Text = result.ToString();
            LabelWarning.Text = result > 12 ? Warning2 : string.Empty;
        }

        private bool TryParse(string s, out double result)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            s = s.Replace(".", separator).Replace(",", separator);
            return double.TryParse(s, out result);
        }
    }
}