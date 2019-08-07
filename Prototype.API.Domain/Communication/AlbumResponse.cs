using Prototype.API.Domain.Entities;

namespace Prototype.API.Domain.Communication
{
    public class AlbumResponse : BaseResponse
    {
        public Album Album { get; private set; }

        private AlbumResponse(bool success, string message, Album album) : base(success, message)
        {
            Album = album;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="album">Saved album.</param>
        /// <returns>Response.</returns>
        public AlbumResponse(Album album) : this(true, string.Empty, album)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AlbumResponse(string message) : this(false, message, null)
        { }
    }
}
