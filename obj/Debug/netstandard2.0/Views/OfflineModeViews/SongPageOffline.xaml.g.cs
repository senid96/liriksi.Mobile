//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("lirksi.Mobile.Views.OfflineModeViews.SongPageOffline.xaml", "Views/OfflineModeViews/SongPageOffline.xaml", typeof(global::lirksi.Mobile.Views.OfflineModeViews.SongPageOffline))]

namespace lirksi.Mobile.Views.OfflineModeViews {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\OfflineModeViews\\SongPageOffline.xaml")]
    public partial class SongPageOffline : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView DownloadedSongs;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button DeleteOfflineSongsBtn;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(SongPageOffline));
            DownloadedSongs = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "DownloadedSongs");
            DeleteOfflineSongsBtn = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "DeleteOfflineSongsBtn");
        }
    }
}