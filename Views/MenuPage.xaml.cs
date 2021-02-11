using lirksi.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.MyProfile, Title="MyProfile" },
                new HomeMenuItem {Id = MenuItemType.Songs, Title="Songs" },
                new HomeMenuItem {Id = MenuItemType.Albums, Title="Albums" },
                new HomeMenuItem {Id = MenuItemType.Performers, Title="Performers" },
                new HomeMenuItem {Id = MenuItemType.AlbumRatings, Title="Top 10 albums" },
                new HomeMenuItem {Id = MenuItemType.SongRatings, Title="Top 10 songs" },
                new HomeMenuItem {Id = MenuItemType.Downloaded, Title="Downloaded" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id); //navigacija menu..tu dodajemo meni stavke
            };
        }
    }
}