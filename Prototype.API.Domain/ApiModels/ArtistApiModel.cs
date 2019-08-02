using Newtonsoft.Json;
using Prototype.API.Domain.Entities;
using System.Collections.Generic;

namespace Prototype.API.Domain.ApiModels
{
    public class ArtistApiModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public IList<AlbumApiModel> Albums { get; set; }

        [JsonIgnore]
        public Artist Convert => new Artist
        {
            ArtistId = ArtistId,
            Name = Name
        };
    }
}
