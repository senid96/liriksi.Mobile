using liriksi.Model;
using liriksi.Model.Requests.rates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.SongRatingsViewModels
{
    public class SongRatingsViewModel: BaseViewModel
    {
        /* services */
        private readonly APIService _ratingService = new APIService("rating");
        private readonly APIService _genreService = new APIService("genre");

        /* viewModels & lists */
        public ObservableCollection<RateDetails> SongRateList { get; set; } = new ObservableCollection<RateDetails>();


        /* methods */
        public SongRatingsViewModel()
        {
            Title = "Top 10 songs";
        }

        public async Task GetTop10Songs()
        {
            var songRateList = await _ratingService.Get<IEnumerable<RateDetails>>(null, "GetSongRates");
            SongRateList.Clear();
            foreach (var item in songRateList)
            {
                SongRateList.Add(item);
            }
        }

    }
}
