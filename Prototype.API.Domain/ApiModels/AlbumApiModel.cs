﻿using Newtonsoft.Json;
using Prototype.API.Domain.Entities;

namespace Prototype.API.Domain.ApiModels
{
    public class AlbumApiModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        public ArtistApiModel Artist { get; set; }
        //public IList<TrackApiModel> Tracks { get; set; }

        [JsonIgnore]
        public Album Convert => new Album
        {
            AlbumId = AlbumId,
            ArtistId = ArtistId,
            Title = Title
        };
    }
}
