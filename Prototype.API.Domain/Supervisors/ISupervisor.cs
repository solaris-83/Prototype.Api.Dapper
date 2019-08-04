using Prototype.API.Domain.ApiModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public interface ISupervisor
    {

        Task<IEnumerable<AlbumApiModel>> GetAllAlbumAsync(PagingApiModel paging, CancellationToken ct = default);
        Task<AlbumApiModel> GetAlbumByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<AlbumApiModel>> GetAlbumByArtistIdAsync(int id, CancellationToken ct = default);

        Task<AlbumApiModel> AddAlbumAsync(AlbumApiModel newAlbumViewModel,
            CancellationToken ct = default);

        Task<bool> UpdateAlbumAsync(AlbumApiModel albumViewModel, CancellationToken ct = default);
        Task<bool> DeleteAlbumAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<ArtistApiModel>> GetAllArtistAsync(PagingApiModel paging, CancellationToken ct = default);
        Task<ArtistApiModel> GetArtistByIdAsync(int id, CancellationToken ct = default);

        Task<ArtistApiModel> AddArtistAsync(ArtistApiModel newArtistViewModel,
            CancellationToken ct = default);

        Task<bool> UpdateArtistAsync(ArtistApiModel artistViewModel,
            CancellationToken ct = default);

        Task<bool> DeleteArtistAsync(int id, CancellationToken ct = default);

    }
}
