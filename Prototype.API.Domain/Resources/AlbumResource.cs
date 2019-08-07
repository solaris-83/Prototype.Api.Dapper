using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.ApiModels
{
    public class AlbumResource
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
    }
}
