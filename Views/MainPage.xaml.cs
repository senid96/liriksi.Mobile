using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lirksi.Mobile.Models;
using Xamarin.Essentials;
using lirksi.Mobile.Views.OfflineModeViews;

namespace lirksi.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Songs:
                        MenuPages.Add(id, new NavigationPage(new SongPage()));
                        break;
                    case (int)MenuItemType.Albums:
                        MenuPages.Add(id, new NavigationPage(new AlbumPage()));
                        break;
                    case (int)MenuItemType.MyProfile:
                        MenuPages.Add(id, new NavigationPage(new MyProfile()));
                        break;
                    case (int)MenuItemType.Performers:
                        MenuPages.Add(id, new NavigationPage(new PerformerViews.PerformerPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.AlbumRatings:
                        MenuPages.Add(id, new NavigationPage(new AlbumRatingsViews.AlbumRatings()));
                        break;
                    case (int)MenuItemType.SongRatings:
                        MenuPages.Add(id, new NavigationPage(new SongRatingsViews.SongRatings()));
                        break;
                    case (int)MenuItemType.Downloaded:
                        MenuPages.Add(id, new NavigationPage(new SongPageOffline()));
                        break;
                    case (int)MenuItemType.Logout:
                        SecureStorage.Remove("username");
                        SecureStorage.Remove("password");
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}