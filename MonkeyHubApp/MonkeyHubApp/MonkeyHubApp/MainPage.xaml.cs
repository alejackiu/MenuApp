﻿using MonkeyHubApp.Services;
using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(new MonkeyHubApiService());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tag = (sender as ListView)?.SelectedItem;
            (BindingContext as MainViewModel)?.ShowCategoriaCommand.Execute(tag);
        }
    }
}
