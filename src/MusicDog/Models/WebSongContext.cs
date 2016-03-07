using Microsoft.Data.Entity;
using MusicDog.ViewModels.Song;

namespace MusicDog.Models
{
    public class WebSongContext : DbContext
    {
        private static bool _created = false;

        public WebSongContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        public DbSet<WebSong> WebSong { get; set; }
    }
}
