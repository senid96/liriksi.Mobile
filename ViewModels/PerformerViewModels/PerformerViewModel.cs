using liriksi.Model;
using liriksi.Model.Requests.performer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.PerformerViewModels
{
    public class PerformerViewModel: BaseViewModel
    {
        /* service */
        private readonly APIService _performerService = new APIService("performer");

        PerformerSearchRequest _performerSearchReq;
        public PerformerSearchRequest PerformerSearchReq
        {
            get { return _performerSearchReq; }
            set { SetProperty(ref _performerSearchReq, value); }
        }

        /* performer list */
        public ObservableCollection<Performer> PerformerList { get; set; } = new ObservableCollection<Performer>();

        public PerformerViewModel()
        {
            Title = "Performer";
            PerformerSearchReq = new PerformerSearchRequest();
        }


        /*---------------------------------------- METHODS ------------------------------------------- */

        public async Task GetPerformers()
        {
            var list = await _performerService.Get<List<Performer>>(null, "GetPerformers");

            PerformerList.Clear();
            foreach (var item in list)
            {
                PerformerList.Add(item);
            }
        }

        public async Task GetPerformersBySearchObj()
        {
            var list = await _performerService.Get<List<Performer>>(PerformerSearchReq, "GetPerformers");

            PerformerList.Clear();
            foreach (var item in list)
            {
                PerformerList.Add(item);
            }
        }
    }
}
