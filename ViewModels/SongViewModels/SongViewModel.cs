using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.album;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class SongViewModel : BaseViewModel
    {
        private readonly APIService _songService = new APIService("song");

        /* ViewModels */
        string _songTitle = string.Empty;
        public string SongTitle
        {
            get { return _songTitle; }
            set { SetProperty(ref _songTitle, value); }
        }

        string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        /* Song list */
        public ObservableCollection<SongGetRequest> SongList { get; set; } = new ObservableCollection<SongGetRequest>();

        public SongViewModel()
        {
            Title = "Songs";
        }

        /*---------------------------------------- METHODS ------------------------------------------- */
        public async Task SearchSongs()
        {
            SongSearchRequest req = new SongSearchRequest()
            {
                Title = SongTitle,
                Text = Text
            };

            await GetSongsBySearchParams(req);
        }

        public async Task GetSongsBySearchParams(SongSearchRequest req)
        {
            SongList.Clear();
            List<SongGetRequest> list = await _songService.Get<List<SongGetRequest>>(req, null);
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    SongList.Add(item);
                }
            }
        }


    }
}
