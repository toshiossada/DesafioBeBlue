using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Spotify.Models;
using BeBlue.Desafio.lib.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeBlue.Desafio.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase {
        private readonly ISpotifyService _spotifyService;

        public SpotifyController (ISpotifyService spotifyService) {
            _spotifyService = spotifyService;
        }
        // GET api/values
        [HttpGet]
        [Route ("categories/{country}")]
        public ActionResult<CategoryList> GetCategories (string country) {
            try {
                var result = _spotifyService.GetCategories (country, "", 50, 0);
                if (result == null) return NotFound ();
                return Ok (result);

            } catch (System.Exception e) {
                return BadRequest (e.Message);
                throw;
            }
        }

        [HttpPut]
        [Route ("seed")]
        public async Task<ActionResult<bool>> Seed () {

            try {
                _ = await _spotifyService.Seed ();
                return NoContent ();
            } catch (System.Exception e) {

                return BadRequest (e.Message);
            }
        }




    }
}