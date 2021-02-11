using liriksi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class AlbumViewModel:BaseViewModel { 
        /* services */
        private readonly APIService _albumService = new APIService("album");
        private readonly APIService _performerService = new APIService("performer");

       /* ViewModels */
        Performer _selectedPerformer;
        public Performer SelectedPerformer
        {
            get { return _selectedPerformer; }
            set { _selectedPerformer = value; }
            
        }

        /* Album and Performer list */
        public ObservableCollection<Album> AlbumList { get; set; } = new ObservableCollection<Album>();
        public ObservableCollection<Performer> PerformerList { get; set; } = new ObservableCollection<Performer>();

       

        /*---------------------------------------- METHODS ------------------------------------------- */
        public AlbumViewModel()
        {
            Title = "Album";
        }
        public async Task GetPerformers()
        {
            var performerList = await _performerService.Get<IEnumerable<Performer>>(null, "GetPerformers");
            PerformerList.Clear();
            foreach (var item in performerList)
            {
                PerformerList.Add(item);
            }
        }

        public async Task GetAlbums()
        {
            var albumList = await _albumService.Get<IEnumerable<Album>>(SelectedPerformer.Id, "GetAlbumsByPerformerId");
            AlbumList.Clear();
            foreach (var item in albumList)
            {
                AlbumList.Add(item);
            }
        }
    }
}
