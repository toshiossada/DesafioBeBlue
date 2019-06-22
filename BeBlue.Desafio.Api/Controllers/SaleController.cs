using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.Entities.Spotify.Models;
using BeBlue.Desafio.lib.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeBlue.Desafio.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase {
        private readonly ISaleService _saleService;

        public SaleController (ISaleService saleService) {
            _saleService = saleService;
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] Sale sale) {
            _saleService.Create (sale);
        }

        [HttpGet]
        [Route ("date/{begin}/{end}/page/{pageSize}/{pageNumber}")]
        public async Task<ActionResult> GetByDateAsync (DateTime begin, DateTime end, int pageSize=50, int pageNumber=1) {
            try {
                var result = await _saleService.GetByDateAsync (begin, end, pageSize, pageNumber);
                if (result == null) return NotFound ();
                return Ok (result);

            } catch (System.Exception e) {
                return BadRequest (e.Message);
                throw;
            }

        }
        [HttpGet]
        [Route ("{id}")]
        public async Task<ActionResult> GetByIdAsync (int id) {
            try {
                var result = await _saleService.GetAsync (id);
                if (result == null) return NotFound ();
                return Ok (result);

            } catch (System.Exception e) {
                return BadRequest (e.Message);
                throw;
            }

        }        

    }
}