using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.ApiResources;
using Prototype.API.Domain.Communication;
using Prototype.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public interface ISupervisor
    {
        //=== Albums 
        Task<IEnumerable<Album>> GetAllAlbumAsync(PagingApiModel paging, CancellationToken ct = default);
        Task<Album> GetAlbumByIdAsync(int id, CancellationToken ct = default);

        //Task<IEnumerable<Album>> GetAlbumByArtistIdAsync(int id, CancellationToken ct = default);

        Task<SaveAlbumResponse> AddAlbumAsync(Album newAlbum, CancellationToken ct = default);

        //Task<bool> UpdateAlbumAsync(AlbumResource albumViewModel, CancellationToken ct = default);
        //Task<bool> DeleteAlbumAsync(int id, CancellationToken ct = default);


        //=== Artists 
        Task<IEnumerable<Artist>> GetAllArtistAsync(PagingApiModel paging, CancellationToken ct = default);
        Task<Artist> GetArtistByIdAsync(int id, CancellationToken ct = default);

        Task<SaveArtistResponse> AddArtistAsync(Artist newArtist, CancellationToken ct = default);

        //Task<bool> UpdateArtistAsync(ArtistApiModel artistViewModel,
        //    CancellationToken ct = default);

        //Task<bool> DeleteArtistAsync(int id, CancellationToken ct = default);

    }
}
