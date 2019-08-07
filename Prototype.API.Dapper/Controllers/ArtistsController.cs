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
using Prototype.API.Domain.Resources;
using Prototype.API.Domain.Supervisors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        private readonly ILogger<ArtistsController> _logger;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> Get([FromQuery] PagingApiModel paging, CancellationToken ct = default)
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

            var artists = await _supervisor.GetAllArtistAsync(paging, ct);
            var resource = _mapper.Map<IEnumerable<ArtistResource>>(artists);
            return new ObjectResult(resource);
           
        }

        /// <summary>
        /// Retrieves an artist given an Id.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Selected artist.</returns>
        [HttpGet("{id}")]
        [Produces(typeof(ArtistResource))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistResource>> Get(int id, CancellationToken ct = default)
        {
            try
            {
                var artist = await _supervisor.GetArtistByIdAsync(id, ct);
                if (artist == null)
                {
                    return NotFound();
                }

                var resource = _mapper.Map<ArtistResource>(artist);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Saves a new artist.
        /// </summary>
        /// <param name="input">SaveArtistResource data.</param>
        /// /// <param name="ct"></param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ArtistResource), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistResource>> Post([FromBody] SaveArtistResource input,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var artist = _mapper.Map<Artist>(input);
            var result = await _supervisor.AddArtistAsync(artist, ct);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Artist);
            return StatusCode(201, artistResource);

        }

        /*
        [HttpPut("{id}")]
        [Produces(typeof(ArtistApiModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
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
                _logger.LogError(errors);

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

        [HttpDelete("{id}")]  //TODO Improve status codes. Avoid using 500 for reference key violation errors?
        [Produces(typeof(void))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiModel), StatusCodes.Status500InternalServerError)]
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

    */
    }
}