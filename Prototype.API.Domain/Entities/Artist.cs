using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.Converters;
using System.Collections.Generic;

namespace Prototype.API.Domain.Entities
{
    public class Artist : IConvertModel<Artist, ArtistApiModel>
    {
        [Key]
        [Computed]
        public int ArtistId { get; set; }

        public string Name { get; set; }

        [Computed]
        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();

        [JsonIgnore]
        [Computed]
        public ArtistApiModel Convert => new ArtistApiModel
        {
            ArtistId = ArtistId,
            Name = Name
        };
    }
}
