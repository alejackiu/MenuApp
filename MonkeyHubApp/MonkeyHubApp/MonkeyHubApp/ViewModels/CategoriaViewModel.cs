using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private readonly IMonkeyHubApiService _monkeyHubApiService;
        private readonly Tag _tag;

        public ObservableCollection<Content> Contents { get; }

        public Command<Content> ShowContentCommand { get; }

        public CategoriaViewModel(IMonkeyHubApiService monkeyHubApiService, Tag tag)
        {
            this._monkeyHubApiService = monkeyHubApiService;
            this._tag = tag;

            this.Contents = new ObservableCollection<Content>();
            this.ShowContentCommand = new Command<Content>(ExecuteShowContentCommand);
        }

        private async void ExecuteShowContentCommand(Content content)
        {
            await PushAsync<ContentWebViewModel>(content);
        }

        public async Task LoadAsync()
        {
            var contents = await _monkeyHubApiService.GetContentsByTagIdAsync(_tag.Id);

            Contents.Clear();
            foreach (var item in contents)
                Contents.Add(item);
        }
    }
}
