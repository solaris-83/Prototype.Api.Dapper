using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.Supervisors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Dapper.Controllers
{
    [Route("v1/api/[controller]")]
    public class ArtistController : Controller
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(ISupervisor supervisor, ILogger<ArtistController> logger)
        {
            _supervisor = supervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces(typeof(List<ArtistApiModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ArtistApiModel>>> Get(CancellationToken ct = default)
        {
            try
            {
                return new ObjectResult(await _supervisor.GetAllArtistAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ArtistApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistApiModel>> Get(int id, CancellationToken ct = default)
        {
            try
            {
                var artist = await _supervisor.GetArtistByIdAsync(id, ct);
                if (artist == null)
                {
                    return NotFound();
                }

                return Ok(artist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(ArtistApiModel))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistApiModel>> Post([FromBody] ArtistApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _supervisor.AddArtistAsync(input, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(ArtistApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistApiModel>> Put(int id, [FromBody] ArtistApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _supervisor.GetArtistByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));

                if (await _supervisor.UpdateArtistAsync(input, ct))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]  //TODO: Improve status codes. Avoid using 500 for reference key violation errors?
        [Produces(typeof(void))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id, CancellationToken ct = default)
        {
            try
            {
                if (await _supervisor.GetArtistByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }

                if (await _supervisor.DeleteArtistAsync(id, ct))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}