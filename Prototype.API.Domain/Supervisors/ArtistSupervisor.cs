using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.Domain.Supervisors
{
    public partial class Supervisor
    {
        public async Task<IEnumerable<ArtistApiModel>> GetAllArtistAsync(PagingApiModel paging, CancellationToken ct = default)
        {
            var artists = await _artistRepository.GetAllAsync(paging.Offset,paging.Limit, ct);
            return artists.ConvertAll();
        }

        public async Task<ArtistApiModel> GetArtistByIdAsync(int id, CancellationToken ct = default)
        {
            ArtistApiModel artistApiModel = null;
            var artist = await _artistRepository.GetByIdAsync(id, ct);
            if (artist == null)
                return artistApiModel;

            artistApiModel = (await _artistRepository.GetByIdAsync(id, ct)).Convert;
            artistApiModel.Albums = (await GetAlbumByArtistIdAsync(artistApiModel.ArtistId, ct)).ToList();
            return artistApiModel;
        }

        public async Task<ArtistApiModel> AddArtistAsync(ArtistApiModel newArtistViewModel,
            CancellationToken ct = default)
        {
            /*var artist = new Artist
            {
                Name = newArtistViewModel.Name
            };*/

            var artist = newArtistViewModel.Convert;

            artist = await _artistRepository.AddAsync(artist, ct);
            newArtistViewModel.ArtistId = artist.ArtistId;
            return newArtistViewModel;
        }

        public async Task<bool> UpdateArtistAsync(ArtistApiModel artistViewModel,
            CancellationToken ct = default)
        {
            var artist = await _artistRepository.GetByIdAsync(artistViewModel.ArtistId, ct);

            if (artist == null) return false;
            artist.ArtistId = artistViewModel.ArtistId;
            artist.Name = artistViewModel.Name;

            return await _artistRepository.UpdateAsync(artist, ct);
        }

        public Task<bool> DeleteArtistAsync(int id, CancellationToken ct = default)
            => _artistRepository.DeleteAsync(id, ct);
    }
}
