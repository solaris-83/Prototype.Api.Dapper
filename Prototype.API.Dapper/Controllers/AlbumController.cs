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
    public class AlbumController : Controller
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ISupervisor supervisor, ILogger<AlbumController> logger)
        {
            _supervisor = supervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces(typeof(List<AlbumApiModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AlbumApiModel>>> Get(CancellationToken ct = default)
        {
            try
            {
                return new ObjectResult(await _supervisor.GetAllAlbumAsync(ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(AlbumApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumApiModel>> Get(int id, CancellationToken ct = default)
        {
            try
            {
                var album = await _supervisor.GetAlbumByIdAsync(id, ct);
                if (album == null)
                {
                    return NotFound();
                }
                
                return Ok(album);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpGet("artist/{id}")]
        [Produces(typeof(List<AlbumApiModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id, CancellationToken ct = default)
        {
            try
            {
                var artist = await _supervisor.GetArtistByIdAsync(id, ct);
                if (artist == null)
                {
                    return NotFound();
                }

                return Ok(await _supervisor.GetAlbumByArtistIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(AlbumApiModel))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                if (input == null)
                {
                    _logger.LogError($"{0}", StatusCodes.Status400BadRequest);
                    return BadRequest();
                }
                    

                return StatusCode(201, await _supervisor.AddAlbumAsync(input, ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(AlbumApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                if (input == null)
                {
                    _logger.LogError($"{0}", StatusCodes.Status400BadRequest);
                    return BadRequest();
                }

                if (await _supervisor.GetAlbumByIdAsync(id, ct) == null)
                {
                    _logger.LogError($"{0}", StatusCodes.Status404NotFound);
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));

                if (await _supervisor.UpdateAlbumAsync(input, ct))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(void))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                if (await _supervisor.GetAlbumByIdAsync(id, ct) == null)
                {
                    _logger.LogError($"{0}", StatusCodes.Status404NotFound);
                    return NotFound();
                }

                if (await _supervisor.DeleteAlbumAsync(id, ct))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }
    }
}
