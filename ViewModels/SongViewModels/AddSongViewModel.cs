using liriksi.Model;
using liriksi.Model.Requests;
using lirksi.Mobile.ValidationHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels.SongViewModels
{
    public class AddSongViewModel: BaseViewModel
    {
        /* services */
        private readonly APIService _songService = new APIService("song");
        private readonly APIService _performerService = new APIService("performer");
        private readonly APIService _albumService = new APIService("album");

        /*ViewModels */
        SongInsertRequest _songReq;
        public SongInsertRequest SongReq
        {
            get { return _songReq; }
            set { SetProperty(ref _songReq, value); }
        }

        Performer _selectedPerformer;
        public Performer SelectedPerformer
        {
            get { return _selectedPerformer; }
            set { SetProperty(ref _selectedPerformer, value); }
        }

        Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get { return _selectedAlbum; }
            set { SetProperty(ref _selectedAlbum, value); }
        }

        public ObservableCollection<Performer> PerformerList { get; set; } = new ObservableCollection<Performer>();
        public ObservableCollection<Album> AlbumList { get; set; } = new ObservableCollection<Album>();


        /* methods */
        public AddSongViewModel()
        {
            Title = "Add song";
            SongReq = new SongInsertRequest();
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

        public async Task AddSong()
        {
            SongReq.AlbumId = SelectedAlbum.Id;
            await _songService.Insert<SongGetRequest>(_songReq, "AddSong");
        }

    }
}
