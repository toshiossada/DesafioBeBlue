using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeBlue.Desafio.Api.Controllers {
    [Route ("api/[controller]")]
    public class TrackController : ControllerBase {
        private readonly ITrackService _trackService;

        public TrackController (ITrackService trackService) {
            _trackService = trackService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get () {
            try {
                var result = await _trackService.GetAsync ();
                if(result == null) return NotFound();
                return Ok (result);
            } catch (System.Exception e) {

                return BadRequest (e.Message);
            }
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id) {
            try {
                var result = await _trackService.GetByIdAsync (id);
                if(result == null) return NotFound();
                return Ok (result);
            } catch (System.Exception e) {

                return BadRequest (e.Message);
            }
        }
        // GET api/values/5
        [HttpGet]
        [Route ("{id}/dayOfWeek/{dayOfWeek}")]
        public async Task<IActionResult> GetByDayOfWeek (int id, int? dayOfWeek) {
            try {
                var result = await _trackService.GetByIdAndDayOfWeekAsync (id, dayOfWeek);
                if(result == null) return NotFound();
                return Ok (result);
            } catch (System.Exception e) {

                return BadRequest (e.Message);
            }
        }

        [HttpGet]
        [Route ("genre/{name}/{pageSize}/{pageNumber}")]
        public async Task<IActionResult> GetByGenreAsync (string name, int pageSize = 50, int pageNumber = 1) {
            try {
                var tracks = await _trackService.GetByGenreAsync (name, pageSize, pageNumber);
                if(tracks == null) return NotFound();
                return Ok (tracks);
            } catch (System.Exception e) {

                return BadRequest (e.Message);
            }
        }
    }
}