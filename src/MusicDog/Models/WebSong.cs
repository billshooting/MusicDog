using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicDog.Models
{
    public class WebSong
    {
        public int Id { get; set; }

        [Display(Name ="歌名")]
        public string Title { get; set; }

        [Display(Name ="歌手")]
        public string Artist { get; set; }

        [Display(Name ="时长")]
        public TimeSpan Duration { get; set; }

        [Display(Name ="专辑")]
        public string Album { get; internal set; }

        [Display(Name ="封面")]
        public string Albumpic_small { get; set; }

        public string Lyric { get; set; }
        public string LyricText { get; set; }
        public int Albumid { get; set; }
        public string Albummid { get; set; }
        public string Albumpic_big { get; set; }
        public string DownUrl { get; set; }
        public int Singerid { get; set; }
        public int Songid { get; set; }
        public string Url { get; set; }
    }
}
