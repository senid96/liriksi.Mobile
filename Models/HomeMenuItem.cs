using System;
using System.Collections.Generic;
using System.Text;

namespace lirksi.Mobile.Models
{
    public enum MenuItemType
    {
        Songs,
        Albums,
        MyProfile,
        Performers,
        About,
        AlbumRatings,
        SongRatings,
        Recommend,
        Logout,
        Downloaded
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
