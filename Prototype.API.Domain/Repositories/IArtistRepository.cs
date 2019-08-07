using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Repositories
{
    public interface IArtistRepository : IDisposable
    {
        Task<IEnumerable<Artist>> GetAllAsync(int offset, int limit);
        Task<Artist> GetByIdAsync(int id);
        Task<Artist> AddAsync(Artist newArtist);
        Task<bool> UpdateAsync(Artist artist);
        Task<bool> DeleteAsync(int id);
    }
}
