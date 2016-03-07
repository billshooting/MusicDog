using MusicDog.Models;
using MusicDog.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicDog.Handler
{
    public class SongFileManager
    {
        private const string uriMatchKey = @"\w{1,}\.jpg";

        public static void SetSongListByBand(List<WebSong> songs, List<WebRequestSong> httpReqSongs)
        {
            //
            int id = 1;
            foreach (WebRequestSong websong in httpReqSongs)
            {
                if (string.IsNullOrEmpty(websong.songname) || string.IsNullOrEmpty(websong.albumpic_small))
                    continue;
                if (!Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                {
                    websong.albumpic_small = "~/images/Default/Default.jpg";
                    websong.albumpic_big = "~/images/Default/Default.jpg";
                }

                WebSong displaySong = new WebSong()
                {
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                    Songid = websong.songid,
                    Singerid = websong.singerid,
                    Albumid = websong.albumid,
                    Albummid = websong.albummid,
                    Albumpic_big = websong.albumpic_big,
                    Albumpic_small = websong.albumpic_small,
                    DownUrl = websong.downUrl,
                    Url = websong.url
                };
                id++;
                songs.Add(displaySong);
            }
        }

        public static void SetSongListByName(List<WebSong> songs, List<Contentlist> httpReqSongs)
        {
            //
            int id = 1;
            foreach (var websong in httpReqSongs)
            {
                if (string.IsNullOrEmpty(websong.songname) || string.IsNullOrEmpty(websong.albumpic_small))
                    continue;
                if (!Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                {
                    websong.albumpic_small = "~/images/Default/Default.jpg";
                    websong.albumpic_big = "~/images/Default/Default.jpg";
                }


                WebSong displaySong = new WebSong()
                {
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Album = websong.albumname,
                    //Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                    Songid = websong.songid,
                    Singerid = websong.singerid,
                    Albumid = websong.albumid,
                    Albummid = websong.albummid,
                    Albumpic_big = websong.albumpic_big,
                    Albumpic_small = websong.albumpic_small,
                    DownUrl = websong.downUrl,
                    //Url = websong.url
                };
                id++;
                songs.Add(displaySong);
            }
        }

        public static void SetSongById(WebSong song, SongIdRes httpReqSong)
        {
            song.Lyric = httpReqSong.lyric;
            song.LyricText = httpReqSong.lyric_txt;
        }
    }

}
