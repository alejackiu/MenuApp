using Android.App;
using Android.Widget;
using Android.OS;
using MaratonaXamarin.Shared;
using System.Linq;

namespace MaratonaXamarin.AndroidApp
{
    [Activity(Label = "MaratonaXamarin.AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var btnCarregar = this.FindViewById<Button>(Resource.Id.btnCarregar);
            var list = this.FindViewById<ListView>(Resource.Id.lvwItens);

            btnCarregar.Click += async (sender, e) => {
                var api = new UserApi();
                var users = await api.ListAsync(
                    new Developer {
                        Id = System.Guid.NewGuid().ToString() ,
                        Name = "Alexandre",
                        Email = "a.jackiu@gmail.com",
                        State = "PR",
                        City = "Curitiba" });

                list.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemSingleChoice, 
                    users.OrderBy(p=> p.Name).Select(p=> $"{p.Id} {p.Name}").ToArray());
            };
        }
    }
}

