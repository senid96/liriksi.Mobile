using liriksi.Model.Requests;
using lirksi.Mobile.Services.OfflineModeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.OfflineModeViewModels
{
    public class SongOfflineViewModel : BaseViewModel
    {
        /* VM and lists*/
        public ObservableCollection<SongGetRequest> SongList { get; set; } = new ObservableCollection<SongGetRequest>();

        /* methods */
        public SongOfflineViewModel()
        {
            Title = "Downloaded songs";
        }

        public async Task GetAllDownloadedSongs()
        {
            SongList.Clear();
            List<SongGetRequest> list = await SongServiceOffline.GetAllDownloadedSongs();
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    SongList.Add(item);
                }
            }
        }

        public void DeleteDownloadedSongs()
        {
             SongServiceOffline.DeleteDownloadedSongs();
        }

        
    }
}
