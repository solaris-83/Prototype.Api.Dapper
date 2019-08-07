using System.ComponentModel.DataAnnotations;

namespace Prototype.API.Domain.Resources
{
    public class SaveArtistResource
    {
            [Required]
            [MaxLength(120)]
            public string Name { get; set; } 
    }
}
