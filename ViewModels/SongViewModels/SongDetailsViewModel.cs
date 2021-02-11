using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.rates;
using lirksi.Mobile.Services.OfflineModeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class SongDetailsViewModel:BaseViewModel
    {
        /* Services */
        private readonly APIService _songService = new APIService("song");
        private readonly APIService _ratingService = new APIService("rating");

        public int _songId { get; set; }

        /* ViewModels */
        private SongGetRequest _songDetail;
        public SongGetRequest SongDetail
        {
            get { return _songDetail; }
            set { SetProperty(ref _songDetail, value); }
        }

        private UsersSongRate _userRate;
        public UsersSongRate UserRate
        {
            get { return _userRate; }
            set { SetProperty(ref _userRate, value); }
        }

        public ObservableCollection<SongGetRequest> RecommendedList { get; set; } = new ObservableCollection<SongGetRequest>();
        public ObservableCollection<int> DefaultRateList { get; set; } = new ObservableCollection<int>();


        /*---------------------------------------- METHODS ------------------------------------------- */
        public SongDetailsViewModel()
        {
            Title = "Song details";
            
        }

        public async Task Init()
        {
            for (int i = 1; i <= 5; i++)
            {
                DefaultRateList.Add(i);
            }
            await GetSongDetailsBySongId();
            await GetSongRatings();
        }
        
        public async Task GetSongDetailsBySongId()
        {
            SongDetail = await _songService.GetById<SongGetRequest>(_songId, "GetSongById");
        }

        public async Task GetSongDetailsBySongIdOffline()
        {
            SongDetail = SongServiceOffline.GetDownloadedSongById(_songId);
        }

        public async Task GetSongRatings()
        {
            //search object (userId, songId)
            HasUserRatedRequest searchObj = new HasUserRatedRequest()
            {
                UserId = APIService._currentUser.Id,
                Id = _songId
            };

            //set UserRate VM.. so if its already rated, it will display
            UserRate = await _ratingService.Get<UsersSongRate>(searchObj, "GetRateBySongByUser");
            
        }

        public async Task RateSong()
        {
            UserRate.SongId = _songId;
            UserRate.UserId = APIService._currentUser.Id;
            await _ratingService.Insert<bool>(UserRate, "RateSong");
        }

        public async Task GetRecommendedSongs()
        {
            string id = _songId.ToString();
            RecommendedList.Clear();
            List<SongGetRequest> list = await _songService.GetById<List<SongGetRequest>>(id, "GetRecommendedSongs");
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    RecommendedList.Add(item);
                }
            }
           
        }

        public async Task DownloadSongOffline()
        {
            await SongServiceOffline.DownloadSong(SongDetail);
        }

        public SongGetRequest GetSongByIdOffline(int songId)
        {
            return SongServiceOffline.GetDownloadedSongById(songId);
        }
    }
}
