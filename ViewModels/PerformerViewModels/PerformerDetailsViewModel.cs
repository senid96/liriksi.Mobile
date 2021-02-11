using liriksi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.PerformerViewModels
{
    public class PerformerDetailsViewModel: BaseViewModel
    {
        /* service */
        private readonly APIService _performerService = new APIService("performer");

        public int _performerId;

        private Performer _performerDetails;
        public Performer PerformerDetails
        {
            get { return _performerDetails; }
            set { SetProperty(ref _performerDetails, value); }
        }


        public PerformerDetailsViewModel()
        {
            Title = "Performer details";
        }

        /*---------------------------------------- METHODS ------------------------------------------- */

        public async Task GetPerformer()
        {
            PerformerDetails = await _performerService.GetById<Performer>(_performerId, "GetPerformerById");
        }

    }
}
