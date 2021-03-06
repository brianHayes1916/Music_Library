using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryAPI.Controllers
{
    [Route("api/MusicController")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public List<SongDTO>  songDTOs;

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
            songDTOs = SongsConverter(_context.Songs.ToList());
        }
        // GET: api/<MusicController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongDTO))]
        public IActionResult Get()
        {
            //List<Song> allSongs = _context.Songs.Select(song => song).ToList();

            return Ok(songDTOs);
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongDTO))]
        public IActionResult Get(int id)
        {
            //Song ThatOneSong = _context.Songs.Where(song => song.Id == id).FirstOrDefault();
            SongDTO ThatOneSong = songDTOs.Where(song => song.Id == id).FirstOrDefault();
            return Ok(ThatOneSong);
        }

        // POST api/<MusicController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SongDTO))]
        public IActionResult Post([FromBody] SongDTO songDTOToAdd)
        {
            Song songToAdd = DTOToSong(songDTOToAdd);
            _context.Songs.Add(songToAdd);
            _context.SaveChanges();
            songDTOs = SongsConverter(_context.Songs.ToList());

            return Created("api/MusicController", songDTOToAdd);
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongDTO))]
        public IActionResult Put(int id, [FromBody] SongDTO UpdatedSongDTO)
        {

            SongDTO songDTOToUpdate = songDTOs.Where(song => song.Id == id).FirstOrDefault();
            try
            {
                songDTOToUpdate.Album = UpdatedSongDTO.Album;
                songDTOToUpdate.Artist = UpdatedSongDTO.Artist;
                songDTOToUpdate.Title = UpdatedSongDTO.Title;
                songDTOToUpdate.ReleaseDate = UpdatedSongDTO.ReleaseDate;

                Song songToUpdate = DTOToSong(songDTOToUpdate);
                if (UpdatedSongDTO.LikesSong)
                {
                    songDTOToUpdate.LikesSong = false;
                    songToUpdate.Likes++;
                }
                songDTOToUpdate.DisplayLikes = songToUpdate.Likes;
                _context.Update(songToUpdate);
                _context.SaveChanges();
                songDTOs = SongsConverter(_context.Songs.ToList());

                return Ok(songDTOToUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                return StatusCode(400, "Bad Request, Object Not found");
            }
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongDTO))]
        public IActionResult Delete(int id)
        {
            SongDTO songDTOToDelete = songDTOs.Where(song => song.Id == id).FirstOrDefault();
            Song songToDelete = DTOToSong(songDTOToDelete);

            _context.Remove(songToDelete);
            _context.SaveChanges();
            songDTOs = SongsConverter(_context.Songs.ToList());

            return Ok(songDTOToDelete);
        }

        public List<SongDTO> SongsConverter(List<Song> songs)
        {
            List<SongDTO> Dtos = new List<SongDTO>();
            foreach (Song song in songs)
            {
                Dtos.Add(SongToDTO(song));
            }
            return Dtos;
        }
        public SongDTO SongToDTO(Song song)
        {
            SongDTO songDTO = new SongDTO();
            songDTO.Album = song.Album;
            songDTO.Artist = song.Artist;
            songDTO.Id = song.Id;
            songDTO.ReleaseDate = song.ReleaseDate;
            songDTO.Title = song.Title;
            songDTO.DisplayLikes = song.Likes;
            return songDTO;
        }
        public Song DTOToSong(SongDTO songDTO)
        {
            Song song = new Song();
            if (songDTO.Id > 0)
            {
                song = _context.Songs.Where(sg => sg.Id == songDTO.Id).FirstOrDefault();
            }
            song.Album = songDTO.Album;
            song.Artist = songDTO.Artist;
            song.Id = songDTO.Id;
            song.ReleaseDate = songDTO.ReleaseDate;
            song.Title = songDTO.Title;
            return song;
        
        }
        public List<Song> SongDTOsConverter(List<SongDTO> songDTOs)
        {
            List<Song> songs = new List<Song>();
            foreach (SongDTO songDTO in songDTOs)
            {
                songs.Add(DTOToSong(songDTO));
            }
            return songs;
        }
    }
}
