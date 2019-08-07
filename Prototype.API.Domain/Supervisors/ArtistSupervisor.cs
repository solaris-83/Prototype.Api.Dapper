using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.Communication;
using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public partial class Supervisor
    {
        public async Task<IEnumerable<Artist>> GetAllArtistAsync(PagingApiModel paging, CancellationToken ct = default)
        {
            var artists = await _artistRepository.GetAllAsync(paging.Page, paging.PageSize, ct);
            return artists;
        }

        public async Task<Artist> GetArtistByIdAsync(int id, CancellationToken ct = default)
        {
            var artist = await _artistRepository.GetByIdAsync(id, ct);
            return artist;
        }

        
        public async Task<SaveArtistResponse> AddArtistAsync(Artist artist, CancellationToken ct = default)
        {
            try
            {
                artist = await _artistRepository.AddAsync(artist, ct);
                return new SaveArtistResponse(artist);
            }
            catch (Exception ex)
            {
                return new SaveArtistResponse($"An error occurred when saving the artist: {ex.Message}");
            }
        }

        
        public async Task<bool> UpdateArtistAsync(Artist artist, CancellationToken ct = default)
        {
            try
            {
                var output = await _artistRepository.GetByIdAsync(artist.ArtistId, ct);

                if (output == null)
                    return false;

                return await _artistRepository.UpdateAsync(artist, ct);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
        public Task<bool> DeleteArtistAsync(int id, CancellationToken ct = default)
            => _artistRepository.DeleteAsync(id, ct);
            */
    }
}
