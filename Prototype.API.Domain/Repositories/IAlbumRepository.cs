using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Repositories
{
    public interface IAlbumRepository : IDisposable
    {
        Task<IEnumerable<Album>> GetAllAsync(int offset, int limit);
        Task<Album> GetByIdAsync(int id);
        Task<List<Album>> GetByArtistIdAsync(int id);
        Task<Album> AddAsync(Album newAlbum);
        Task<bool> UpdateAsync(Album album);
        Task<bool> DeleteAsync(int id);
    }
}
