using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {       
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearchCommand { get; }

        public Command AboutCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }


        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            this.SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            this.AboutCommand = new Command(ExecuteAboutCommand);
            this.ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
            this.Resultados = new ObservableCollection<Tag>();
        }

        async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_monkeyHubApiService, tag);
        }

        async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {                
                var tags = await _monkeyHubApiService.GetTagsAsync();
                Resultados.Clear();

                if (tags != null)
                {
                    foreach (var item in tags)
                        Resultados.Add(item);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Desculpe, me confundi.", "Ok");

        }

        bool CanExecuteSearchCommand()
        {
            return !string.IsNullOrEmpty(SearchTerm);
        }
    }
}
