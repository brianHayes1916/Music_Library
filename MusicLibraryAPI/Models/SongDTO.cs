using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string ReleaseDate { get; set; }
        public bool LikesSong { get;  set; }
        public int DisplayLikes { get; set; }
    }
}
