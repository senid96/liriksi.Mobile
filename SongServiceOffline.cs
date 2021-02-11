using liriksi.Model.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace lirksi.Mobile.Services.OfflineModeServices
{
    public static class SongServiceOffline
    {
        public static async Task<List<SongGetRequest>> GetAllDownloadedSongs()
        {
            var songList = new List<SongGetRequest>();
            string storedSongIds = await SecureStorage.GetAsync("SongIds");
            if (string.IsNullOrWhiteSpace(storedSongIds))
                return songList;

            string[]  storedIds = storedSongIds.Split(',');
            for (int i = 0; i < storedIds.Length; i++)
            {
                if (int.TryParse(storedIds[i], out int songId))
                {
                    var song = GetDownloadedSongById(songId);
                    if (song != null)
                    {
                        songList.Add(song);
                    }
                }
            }

            return songList;
        }

        public static SongGetRequest GetDownloadedSongById(int songId)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Song_" + songId + ".dat");
            if (File.Exists(fileName))
            {
                var jsonContent = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<SongGetRequest>(jsonContent);
            }
            return null;
        }
        public async static Task DownloadSong(SongGetRequest song)
        {
            string storedSongIds = await SecureStorage.GetAsync("SongIds");
            if (string.IsNullOrWhiteSpace(storedSongIds))
            {
                storedSongIds = song.Id.ToString();
            }
            else
            {
                storedSongIds += "," + song.Id.ToString();
            }

            await SecureStorage.SetAsync("SongIds", storedSongIds);

            string serialized_str = JsonConvert.SerializeObject(song);

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Song_" + song.Id + ".dat");
            File.WriteAllText(fileName, serialized_str);
        }

        public async static void DeleteDownloadedSongs()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //C:\\Users\\senid.ajkunic\\AppData\\Local\\Packages\\23bcd613-ef51-4b04-8564-e5ad6fb64a3a_av2fnhpmnrf78\\LocalState\\ProgramData
            string storedIds = await SecureStorage.GetAsync("SongIds");
            if (!string.IsNullOrWhiteSpace(storedIds))
            {
                string[] arrStoredIds = storedIds.Split(',');
                for (int i = 0; i < arrStoredIds.Length; i++)
                {
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Song_" + arrStoredIds[i] + ".dat");
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }

            SecureStorage.Remove("SongIds");
        }
    }
}
