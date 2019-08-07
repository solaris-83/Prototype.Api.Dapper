using System.ComponentModel.DataAnnotations;

namespace Prototype.API.Domain.ApiResources
{
    public class SaveAlbumResource
    {
        [Required]
        [MaxLength(160)]
        public string Title { get; set; }

        [Required]
        public int ArtistId { get; set; }
    }
}
