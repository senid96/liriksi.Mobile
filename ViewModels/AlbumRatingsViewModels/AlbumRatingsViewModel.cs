using liriksi.Model.Requests.rates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.AlbumRatingsViewModels
{
    public class AlbumRatingsViewModel: BaseViewModel
    {
        /* services */
        private readonly APIService _ratingService = new APIService("rating");
        
        /* viewModels & lists */
        public ObservableCollection<RateDetails> AlbumRateList { get; set; } = new ObservableCollection<RateDetails>();

        public AlbumRatingsViewModel()
        {
            Title = "Top 10 albums";
        }


        /* methods */
        public async Task GetTop10Albums()
        {
            var albumRateList = await _ratingService.Get<IEnumerable<RateDetails>>(null, "GetAlbumRates");
            AlbumRateList.Clear();
            foreach (var item in albumRateList)
            {
                AlbumRateList.Add(item);
            }
        }
    }
}
