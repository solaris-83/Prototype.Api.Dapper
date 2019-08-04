using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public interface IArtistRepository : IDisposable
    {
        Task<IEnumerable<Artist>> GetAllAsync(int offset, int limit, CancellationToken ct = default);
        Task<Artist> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Artist> AddAsync(Artist newArtist, CancellationToken ct = default);
        Task<bool> UpdateAsync(Artist artist, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
