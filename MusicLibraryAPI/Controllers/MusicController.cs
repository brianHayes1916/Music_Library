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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MusicController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
