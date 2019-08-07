using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prototype.API.Dapper.Extensions;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.ApiResources;
using Prototype.API.Domain.Entities;
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
        private readonly IMapper _mapper;

        public AlbumsController(ISupervisor supervisor, ILogger<AlbumsController> logger, IMapper mapper)
        {
            _supervisor = supervisor;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all albums.
        /// </summary>
        /// <param name="paging">Specifiy offset and limit for data.</param>
        /// <returns>Albums list.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<AlbumResource>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AlbumResource>>> Get([FromQuery] PagingApiModel paging, CancellationToken ct = default)
        {
            if (paging.Page == 0) //TODO Move to validation handler when fixed/developed
            {
                var msg = "Offset value must be positive";
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            if (paging.PageSize == 0)
            {
                var msg = "Limit value must be positive";
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            try
            {
                var albums = await _supervisor.GetAllAlbumAsync(paging, ct);
                var resource = _mapper.Map<IEnumerable<AlbumResource>>(albums);
                return new ObjectResult(resource);
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
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumResource>> Get(int id, CancellationToken ct = default)
        {
            try
            {
                var album = await _supervisor.GetAlbumByIdAsync(id, ct);
                if (album == null)
                {
                    return NotFound(); // TODO Handle with specific class to pass in
                }
                
                var resource = _mapper.Map<AlbumResource>(album);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        /*
        /// <summary>
        /// Retrieves all the albums given an Artist Id.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Selected album.</returns>
        [HttpGet("artists/{id}")]
        [ProducesResponseType(typeof(List<AlbumResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AlbumResource>>> GetByArtistId(int id, CancellationToken ct = default)
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

        */


        /// <summary>
        /// Saves a new album.
        /// </summary>
        /// <param name="input">SaveAlbumResource data.</param>
        /// /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<AlbumResource>> Post([FromBody] SaveAlbumResource input, CancellationToken ct = default)  //TODO Evaluate if try/catch is needed
        {
            //try
            //{
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                var album = _mapper.Map<Album>(input);
                var result = await _supervisor.AddAlbumAsync(album, ct);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                var albumResource = _mapper.Map<Album, AlbumResource>(result.Album);
                return StatusCode(201, albumResource);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(500, ex);
            //}
        }

        /*
        /// <summary>
        /// Updates an existing album according to an Id.
        /// </summary>
        /// <param name="id">Album identifier.</param>
        /// <param name="input">Updated album data.</param>
        /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [Produces(typeof(AlbumResource))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumResource>> Put(int id, [FromBody] AlbumResource input,
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

    */
    }
}
