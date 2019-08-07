using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.ApiResources;
using Prototype.API.Domain.Communication;
using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public partial class Supervisor
    {
        public async Task<IEnumerable<Album>> GetAllAlbumAsync(PagingApiModel paging)
        {
            try
            {
                var albums = await _albumRepository.GetAllAsync(paging.Page, paging.PageSize);

                return albums;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Album> GetAlbumByIdAsync(int id)
        {
            return await _albumRepository.GetByIdAsync(id);
        }

        /*
        public async Task<IEnumerable<AlbumResource>> GetAlbumByArtistIdAsync(int id,
            CancellationToken ct = default)
        {
            var albums = await _albumRepository.GetByArtistIdAsync(id, ct);
            return albums.ConvertAll();
        }

        */


        
        public async Task<AlbumResponse> AddAlbumAsync(Album album)
        {
            try
            {
                album = await _albumRepository.AddAsync(album);
                return new AlbumResponse(album);
            }
            catch(Exception ex)
            {
                return new AlbumResponse($"An error occurred when saving the album: {ex.Message}");
            }
        }

        
        public async Task<AlbumResponse> UpdateAlbumAsync(int id, Album album)
        {
            try
            {
                var existingAlbum = await _albumRepository.GetByIdAsync(id);
                if (existingAlbum == null)
                    return new AlbumResponse("Album not found.");

                existingAlbum.Title = album.Title;

                await _albumRepository.UpdateAsync(existingAlbum);
                return new AlbumResponse(existingAlbum);
            }
            catch(Exception ex)
            {
                return new AlbumResponse($"An error occurred when updating the album: {ex.Message}");
            }
        }

        public async Task<AlbumResponse> DeleteAlbumAsync(int id)
        {
            try
            {
                var existingAlbum = await _albumRepository.GetByIdAsync(id);
                if (existingAlbum == null)
                {
                    return new AlbumResponse("Album not found.");
                }

                await _albumRepository.DeleteAsync(id);

                return new AlbumResponse(existingAlbum);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AlbumResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }

    }
}
