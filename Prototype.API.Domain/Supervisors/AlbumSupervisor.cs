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
        public async Task<IEnumerable<Album>> GetAllAlbumAsync(PagingApiModel paging, CancellationToken ct = default)
        {
            try
            {
                var albums = await _albumRepository.GetAllAsync(paging.Page, paging.PageSize, ct);

                return albums;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Album> GetAlbumByIdAsync(int id, CancellationToken ct = default)
        {
            return await _albumRepository.GetByIdAsync(id, ct);
        }

        /*
        public async Task<IEnumerable<AlbumResource>> GetAlbumByArtistIdAsync(int id,
            CancellationToken ct = default)
        {
            var albums = await _albumRepository.GetByArtistIdAsync(id, ct);
            return albums.ConvertAll();
        }

        */


        
        public async Task<SaveAlbumResponse> AddAlbumAsync(Album album, CancellationToken ct = default)
        {
            try
            {
                album = await _albumRepository.AddAsync(album, ct);
                return new SaveAlbumResponse(album);
            }
            catch(Exception ex)
            {
                return new SaveAlbumResponse($"An error occurred when saving the album: {ex.Message}");
            }
        }

        
        public async Task<bool> UpdateAlbumAsync(Album album, CancellationToken ct = default)
        {
            try
            {
                var output = await _albumRepository.GetByIdAsync(album.AlbumId, ct);

                if (output == null)
                    return false;

                return await _albumRepository.UpdateAsync(album, ct);
            }
            catch(Exception)
            {
                return false;
            }
        }
        /*
        public Task<bool> DeleteAlbumAsync(int id, CancellationToken ct = default)
            => _albumRepository.DeleteAsync(id, ct);

    */
    }
}
