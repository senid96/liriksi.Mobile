using liriksi.Model;
using liriksi.Model.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.AlbumViewModels
{
    public class AddAlbumViewModel:BaseViewModel
    {
        /* services */
        private readonly APIService _albumService = new APIService("album");
        private readonly APIService _performerService = new APIService("performer");
        private readonly APIService _genreService = new APIService("genre");

        /* ViewModels */
        AlbumInsertRequest _albumReq;
        public AlbumInsertRequest AlbumReq
        {
            get { return _albumReq; }
            set { SetProperty(ref _albumReq, value); }
        }

        Performer _selectedPerformer;
        public Performer SelectedPerformer
        {
            get { return _selectedPerformer; }
            set { SetProperty(ref _selectedPerformer, value); }
        }

        Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { SetProperty(ref _selectedGenre, value); }
        }

      

        public ObservableCollection<Performer> PerformerList { get; set; } = new ObservableCollection<Performer>();
        public ObservableCollection<Genre> GenreList { get; set; } = new ObservableCollection<Genre>();
        public ObservableCollection<int> YearList { get; set; } = new ObservableCollection<int>();

        /* methods */

        public AddAlbumViewModel()
        {
            Title = "Add album";
            AlbumReq = new AlbumInsertRequest();
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

        public async Task GetGenres()
        {
            var genreList = await _genreService.Get<IEnumerable<Genre>>(null, "GetGenres");
            GenreList.Clear();
            foreach (var item in genreList)
            {
                GenreList.Add(item);
            }
        }

        public async Task AddAlbum()
        {
            AlbumReq.PerformerId = SelectedPerformer.Id;
            AlbumReq.GenreId = SelectedGenre.Id;

            await _albumService.Insert<Album>(AlbumReq, "InsertAlbum");
        }

    }
}
