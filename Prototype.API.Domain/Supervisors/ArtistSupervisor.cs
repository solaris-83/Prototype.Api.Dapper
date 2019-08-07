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
        public async Task<IEnumerable<Artist>> GetAllArtistAsync(PagingApiModel paging)
        {
            var artists = await _artistRepository.GetAllAsync(paging.Page, paging.PageSize);
            return artists;
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            return artist;
        }

        
        public async Task<ArtistResponse> AddArtistAsync(Artist artist)
        {
            try
            {
                await _artistRepository.AddAsync(artist);
                return new ArtistResponse(artist);
            }
            catch (Exception ex)
            {
                return new ArtistResponse($"An error occurred when saving the artist: {ex.Message}");
            }
        }

        
        public async Task<ArtistResponse> UpdateArtistAsync(int id, Artist artist)
        {
            try
            {
                var existingArtist = await _artistRepository.GetByIdAsync(id);

                if (existingArtist == null)
                    return new ArtistResponse("Artist not found.");

                existingArtist.Name = artist.Name;

                await _artistRepository.UpdateAsync(existingArtist);
                return new ArtistResponse(existingArtist);
            }
            catch (Exception ex)
            {
                return new ArtistResponse($"An error occurred when updating the artist: {ex.Message}");
            }
        }

        public async Task<ArtistResponse> DeleteArtistAsync(int id)
        {
            try
            {
                var existingArtist = await _artistRepository.GetByIdAsync(id);
                if (existingArtist == null)
                {
                    return new ArtistResponse("Artist not found.");
                }

                await _artistRepository.DeleteAsync(id);

                return new ArtistResponse(existingArtist);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ArtistResponse($"An error occurred when deleting the artist: {ex.Message}");
            }
        }  
    }
}
