using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prototype.API.Dapper.Extensions;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.ApiResources;
using Prototype.API.Domain.Entities;
using Prototype.API.Domain.Resources;
using Prototype.API.Domain.Supervisors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Dapper.Controllers
{
    [Route("v1/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ArtistsController : Controller
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ArtistsController(ISupervisor supervisor, ILogger<ArtistsController> logger, IMapper mapper)
        {
            _supervisor = supervisor;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all artists.
        /// </summary>
        /// <param name="paging">Specifiy offset and limit for data.</param>
        /// <returns>Artists list.</returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<ArtistResource>))]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllAsync([FromQuery] PagingApiModel paging)
        {
            if (paging.Page == 0)
            {
                var msg = "Offset value must be positive";
                _logger.LogError(msg);
                return BadRequest(new ErrorResource(msg));
            }

            if (paging.PageSize == 0)
            {
                var msg = "Limit value must be positive";
                _logger.LogError(msg);
                return BadRequest(new ErrorResource(msg));
            }

            var artists = await _supervisor.GetAllArtistAsync(paging);
            var resource = _mapper.Map<IEnumerable<ArtistResource>>(artists);
            return new ObjectResult(resource);
           
        }

        /// <summary>
        /// Retrieves an artist given an Id.
        /// </summary>
        /// <returns>Selected artist.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArtistResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistResource>> GetAsync(int id)
        {
            //try
            //{
                var artist = await _supervisor.GetArtistByIdAsync(id);
                if (artist == null)
                {
                    return NotFound(new ErrorResource("Artist not found"));
                }

                var resource = _mapper.Map<ArtistResource>(artist);
                return Ok(resource);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex);
            //}
        }


        /// <summary>
        /// Saves a new artist.
        /// </summary>
        /// <param name="input">SaveArtistResource data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ArtistResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistResource>> PostAsync([FromBody] SaveArtistResource input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var artist = _mapper.Map<SaveArtistResource, Artist>(input);
            var result = await _supervisor.AddArtistAsync(artist);
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Artist);
            return StatusCode(201, artistResource);

        }

        /// <summary>
        /// Update an artist.
        /// </summary>
        /// <param name="id">Artist identifier.</param>
        /// <param name="input">SaveArtistResource data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ArtistResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistResource>> PutAsync(int id, [FromBody] SaveArtistResource input)
        {
            //try
            //{
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var artist = await _supervisor.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound(new ErrorResource("Artist not found"));
            }

            artist = _mapper.Map<SaveArtistResource, Artist>(input);

            var result = await _supervisor.UpdateArtistAsync(id, artist);
            if(!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Artist);
            return Ok(artistResource);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex);
            //}
        }

        /// <summary>
        /// Deletes a given artist according to an Id.
        /// </summary>
        /// <param name="id">Artist identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ArtistResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            //try
            //{
            var artist = await _supervisor.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound(new ErrorResource("Artist not found"));
            }

            var result = await _supervisor.DeleteArtistAsync(id);
            if(!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Artist);
            return Ok(artistResource);

            //}
            //catch (Exception ex)
            //{
            //return StatusCode(500, ex);
            //}
        }
    }
}