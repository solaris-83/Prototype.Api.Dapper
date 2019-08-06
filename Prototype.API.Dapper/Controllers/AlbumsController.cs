using Microsoft.AspNetCore.Authorization;
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
    [Produces("application/json")]
    [ApiController]
    public class AlbumsController : Controller
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AlbumsController> _logger;

        public AlbumsController(ISupervisor supervisor, ILogger<AlbumsController> logger)
        {
            _supervisor = supervisor;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all albums.
        /// </summary>
        /// <param name="paging">Specifiy offset and limit for data.</param>
        /// <returns>Albums lists.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AlbumApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<AlbumApiModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AlbumApiModel>>> Get([FromQuery] PagingApiModel paging, CancellationToken ct = default)
        {
            if (paging.Offset == 0) //TODO Move to validation handler when fixed/developed
            {
                var msg = "Offset value must be positive";
                _logger.LogError(msg);
                return BadRequest(new ErrorApiModel(msg));
            }

            if (paging.Limit == 0)
            {
                var msg = "Limit value must be positive";
                _logger.LogError(msg);
                return BadRequest(new ErrorApiModel(msg));
            }

            try
            {
                return new ObjectResult(await _supervisor.GetAllAlbumAsync(paging, ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Retrieves an album given an Id.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Selected album.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlbumApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Retrieves all the albums given an Artist Id.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Selected album.</returns>
        [HttpGet("artists/{id}")]
        [ProducesResponseType(typeof(List<AlbumApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Saves a new album.
        /// </summary>
        /// <param name="input">AlbumApiModel data.</param>
        /// /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AlbumApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                if (input == null)
                {
                    var message = "Data input is null";
                    _logger.LogError($"{0} {1}", StatusCodes.Status400BadRequest, message);
                    return BadRequest(new ErrorApiModel(message));
                }

                var result = await _supervisor.AddAlbumAsync(input, ct);
                if (!result.Success)
                {
                    return BadRequest(new ErrorApiModel(result.Message));
                }

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Updates an existing album according to an Id.
        /// </summary>
        /// <param name="id">Album identifier.</param>
        /// <param name="input">Updated album data.</param>
        /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [Produces(typeof(AlbumApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input,
            CancellationToken ct = default)
        {
            try
            {
                //if (input == null)
                //{
                //    _logger.LogError($"{0}", StatusCodes.Status400BadRequest);
                //    return BadRequest();
                //}

                if (input == null)
                {
                    var message = "Data input is null";
                    _logger.LogError($"{0} {1}", StatusCodes.Status400BadRequest, message);
                    return BadRequest(new ErrorApiModel(message));
                }

                if (await _supervisor.GetAlbumByIdAsync(id, ct) == null)
                {
                    _logger.LogError($"{0}", StatusCodes.Status404NotFound);
                    return NotFound();
                }

                var errors = JsonConvert.SerializeObject(ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));
                _logger.LogError(errors);

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

        /// <summary>
        /// Deletes a given album according to an Id.
        /// </summary>
        /// <param name="id">Album identifier.</param>
        /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                if (await _supervisor.GetAlbumByIdAsync(id, ct) == null)
                {
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
