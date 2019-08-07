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
        Task<IEnumerable<Album>> GetAllAlbumAsync(PagingApiModel paging);
        Task<Album> GetAlbumByIdAsync(int id);

        //Task<IEnumerable<Album>> GetAlbumByArtistIdAsync(int id);

        Task<AlbumResponse> AddAlbumAsync(Album newAlbum);
        Task<AlbumResponse> UpdateAlbumAsync(int id, Album album);
        Task<AlbumResponse> DeleteAlbumAsync(int id);


        //=== Artists 
        Task<IEnumerable<Artist>> GetAllArtistAsync(PagingApiModel paging);
        Task<Artist> GetArtistByIdAsync(int id);
        Task<ArtistResponse> AddArtistAsync(Artist newArtist);
        Task<ArtistResponse> UpdateArtistAsync(int id, Artist artist);
        Task<ArtistResponse> DeleteArtistAsync(int id);

    }
}
