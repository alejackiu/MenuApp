using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyHubApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        public CategoriaPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var content = (sender as ListView)?.SelectedItem;
            (this.BindingContext as CategoriaViewModel)?.ShowContentCommand.Execute(content);
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as CategoriaViewModel)?.LoadAsync();
            base.OnAppearing();
        }
    }
}
