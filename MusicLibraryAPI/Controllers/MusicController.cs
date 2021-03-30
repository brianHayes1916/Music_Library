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

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<MusicController>
        [HttpGet]
        public IEnumerable<Song> Get()
        {
            List<Song> allSongs = _context.Songs.Select(song => song).ToList();
            return allSongs;
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public Song Get(int id)
        {
            Song ThatOneSong = _context.Songs.Where(song => song.Id == id).FirstOrDefault();
            return ThatOneSong;
        }

        // POST api/<MusicController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Song))]
        public IActionResult Post([FromBody] Song songToAdd)
        {
            _context.Songs.Add(songToAdd);
            _context.SaveChanges();

            return Created("api/MusicController", songToAdd);
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Song))]
        public IActionResult Put(int id, [FromBody] Song UpdatedSong)
        {

            Song songToUpdate = _context.Songs.Where(song => song.Id == id).FirstOrDefault();


            songToUpdate.Album = UpdatedSong.Album;
            songToUpdate.Artist = UpdatedSong.Artist;
            songToUpdate.Title = UpdatedSong.Title;
            songToUpdate.ReleaseDate = UpdatedSong.ReleaseDate;
            _context.Update(songToUpdate);
            _context.SaveChanges();
            
            return Ok(songToUpdate);

        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Song))]
        public IActionResult Delete(int id)
        {
            Song songToDelete = _context.Songs.Where(song => song.Id == id).FirstOrDefault();
            _context.Remove(songToDelete);
            _context.SaveChanges();

            return Ok(songToDelete);
        }
    }
}
