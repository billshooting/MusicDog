using Microsoft.Data.Entity;
using System;
using MusicDog.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MusicDog.ViewModels.Song
{

    public sealed class HomeIndexBandList
    {
        public List<WebSong> Band1List { get; set; } = new List<WebSong>();
        public List<WebSong> Band2List { get; set; } = new List<WebSong>();
        public List<WebSong> Band3List { get; set; } = new List<WebSong>();
    }

    public sealed class WebSongList
    {
        public List<WebSong> WebSongs { get; set; }
    }
}
