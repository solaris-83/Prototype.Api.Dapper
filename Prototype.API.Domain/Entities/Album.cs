using Newtonsoft.Json;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.Converters;

namespace Prototype.API.Domain.Entities
{
    public sealed class Album : IConvertModel<Album, AlbumApiModel>
    {
        public int AlbumId { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        //public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

        public Artist Artist { get; set; }

        [JsonIgnore]
        public AlbumApiModel Convert => new AlbumApiModel
        {
            AlbumId = AlbumId,
            ArtistId = ArtistId,
            Title = Title
        };
    }
}
