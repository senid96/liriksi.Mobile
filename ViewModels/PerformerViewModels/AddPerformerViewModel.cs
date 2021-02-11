using liriksi.Model;
using liriksi.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.PerformerViewModels
{
    public class AddPerformerViewModel : BaseViewModel
    {
        /* services */
        private readonly APIService _performerService = new APIService("performer");

        /* View models */
        PerformerInsertRequest _performerReq;
        public PerformerInsertRequest PerformerReq
        {
            get { return _performerReq; }
            set { SetProperty(ref _performerReq, value); }
        }

        /* methods */

        public AddPerformerViewModel()
        {
            Title = "Add performer";
            PerformerReq = new PerformerInsertRequest();
        }


        public async Task SaveNewPerformer()
        {
            await _performerService.Insert<Performer>(PerformerReq, "AddPerformer");
        }
    }
}
