using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.rates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class AlbumDetailsViewModel :BaseViewModel
    {
        /* services */
        private readonly APIService _songService = new APIService("song");
        private readonly APIService _albumService = new APIService("album");
        private readonly APIService _ratingService = new APIService("rating");

        public int _albumId { get; set; }

        /* viewModels */
        private Album _album;
        public Album Album
        {
            get { return _album; }
            set { SetProperty(ref _album, value); }
        }

        private UsersAlbumRate _userRate;
        public UsersAlbumRate UserRate
        {
            get { return _userRate; }
            set { SetProperty(ref _userRate, value); }
        }

        /* song list */
        public ObservableCollection<SongGetRequest> SongList { get; set; } = new ObservableCollection<SongGetRequest>();
        public ObservableCollection<int> DefaultRateList { get; set; } = new ObservableCollection<int>();

        public AlbumDetailsViewModel()
        {
            Title = "Album details";
            for (int i = 1; i <= 5; i++)
            {
                DefaultRateList.Add(i);
            }
        }

        /*---------------------------------------- METHODS ------------------------------------------- */

        public async Task Init()
        {
            await GetAlbumById();
            await GetSongs();
            await GetAlbumRatings();
        }

        public async Task GetAlbumById()
        {
            Album = await _albumService.GetById<Album>(_albumId, "GetAlbumById");
        }
       
        public async Task GetSongs()
        {
                var songList = await _songService.GetById<IEnumerable<SongGetRequest>>(Album.Id, "GetSongsByAlbum");
                SongList.Clear();
                foreach (var item in songList)
                {
                    SongList.Add(item);
                }
        }

        public async Task GetAlbumRatings()
        {
            //search object (userId, albumId)
            HasUserRatedRequest searchObj = new HasUserRatedRequest()
            {
                UserId = APIService._currentUser.Id,
                Id = _albumId
            };

            //set UserRate VM.. so if its already rated, it will display
            UserRate = await _ratingService.Get<UsersAlbumRate>(searchObj, "GetRateByAlbumByUser");

        }

        public async Task RateAlbum()
        {
            UserRate.AlbumId = _albumId;
            UserRate.UserId = APIService._currentUser.Id;
            await _ratingService.Insert<bool>(UserRate, "RateAlbum");
        }

    }
}
