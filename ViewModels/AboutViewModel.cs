using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.facebook.com/ajkunicsenid/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}