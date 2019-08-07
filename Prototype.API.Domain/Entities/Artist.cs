using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Prototype.API.Domain.Entities
{
    public class Artist
    {
        [Key]
        //[Computed]
        public int ArtistId { get; set; }

        public string Name { get; set; }

        [Write(false)]
        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();

    }
}
