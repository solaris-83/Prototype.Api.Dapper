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
        /// <param name="paging">Specify offset and limit for data.</param>
        /// <returns>Albums list.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<AlbumResource>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AlbumResource>>> GetAllAsync([FromQuery] PagingApiModel paging)
        {
            if (paging.Page == 0)
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

            //try
            //{
            var albums = await _supervisor.GetAllAlbumAsync(paging);
            var resource = _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumResource>>(albums);
            return new ObjectResult(resource);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(500, ex);
            //}
        }

        /// <summary>
        /// Retrieves an album given an Id.
        /// </summary>
        /// <returns>Selected album.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumResource>> GetAsync(int id)
        {
            //try
            //{
                var album = await _supervisor.GetAlbumByIdAsync(id);
                if (album == null)
                {
                    return NotFound(); // TODO Handle with specific class to pass in
                }
                
                var resource = _mapper.Map<AlbumResource>(album);
                return Ok(resource);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(500, ex);
            //}
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
        public async Task<ActionResult<List<AlbumResource>>> GetByArtistId(int id)
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
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<AlbumResource>> PostAsync([FromBody] SaveAlbumResource input)  //TODO Evaluate if try/catch is needed
        {
            //try
            //{
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var album = _mapper.Map<SaveAlbumResource, Album>(input);
            var result = await _supervisor.AddAlbumAsync(album);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
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

        
        /// <summary>
        /// Updates an existing album according to an Id.
        /// </summary>
        /// <param name="id">Album identifier.</param>
        /// <param name="input">Updated album data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlbumResource>> PutAsync(int id, [FromBody] SaveAlbumResource input)
        {
            //try
            //{
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                //var album = await _supervisor.GetAlbumByIdAsync(id);
                //if (album == null)
                //{
                //    return NotFound();
                //}

                var album = _mapper.Map<SaveAlbumResource, Album>(input);

                var result = await _supervisor.UpdateAlbumAsync(id, album);
                if(!result.Success)
                {
                    return BadRequest(new ErrorResource(result.Message));
                }

                var albumResource = _mapper.Map<Album, AlbumResource>(result.Album);
                return Ok(albumResource);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(500, ex);
            //}
        }

        
        /// <summary>
        /// Deletes a given album according to an Id.
        /// </summary>
        /// <param name="id">Album identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AlbumResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            //try
            //{
            //if (await _supervisor.GetAlbumByIdAsync(id) == null)
            //{
            //    return NotFound();
            //}

            var result = await _supervisor.DeleteAlbumAsync(id);
            if(!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var albumResource = _mapper.Map<Album, AlbumResource>(result.Album);
            return Ok(albumResource);

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(500, ex);
            //}
        }
    }
}
