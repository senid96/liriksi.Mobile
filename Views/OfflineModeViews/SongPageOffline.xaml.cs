using liriksi.Model.Requests;
using lirksi.Mobile.ViewModels.OfflineModeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.OfflineModeViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SongPageOffline : ContentPage
	{
        public SongOfflineViewModel model { get; set; }
        public SongPageOffline ()
		{
            InitializeComponent ();
            BindingContext = model = new SongOfflineViewModel();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.GetAllDownloadedSongs();
        }

        private async void DownloadedSongs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SongGetRequest;
            await Navigation.PushAsync(new SongDetails(item.Id));
        }

        private async void DeleteOfflineSongsBtn_Clicked(object sender, EventArgs e)
        {
            model.DeleteDownloadedSongs();
            await model.GetAllDownloadedSongs();

        }
    }
}