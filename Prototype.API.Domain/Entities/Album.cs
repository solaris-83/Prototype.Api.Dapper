using Dapper.Contrib.Extensions;

namespace Prototype.API.Domain.Entities
{
    public class Album
    {
        //[Computed]
        [Key]
        public int AlbumId { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        [Write(false)]
        public Artist Artist { get; set; }

    }
}
