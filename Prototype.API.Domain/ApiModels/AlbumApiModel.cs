using Newtonsoft.Json;
using Prototype.API.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prototype.API.Domain.ApiModels
{
    public class AlbumApiModel : BaseApiModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        public ArtistApiModel Artist { get; set; }
        //public IList<TrackApiModel> Tracks { get; set; }

        public AlbumApiModel()
        { }

        public AlbumApiModel(bool success, string message, int albumId, string title, int artistId, string artistName) : base(success, message)
        {
            this.AlbumId = albumId;
            this.Title = title;
            this.ArtistId = artistId;
            this.ArtistName = artistName;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved album.</param>
        /// <returns>Response.</returns>
        public AlbumApiModel(int albumId, string title, int artistId, string artistName) : this(true, string.Empty, albumId, title, artistId, artistName)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        /// 
        public AlbumApiModel(string message) : this(false, message, albumId: 0, artistName: string.Empty, artistId: 0, title: string.Empty)
        { }

        [JsonIgnore]
        public Album Convert => new Album
        {
            AlbumId = AlbumId,
            ArtistId = ArtistId,
            Title = Title
        };
    }
}
