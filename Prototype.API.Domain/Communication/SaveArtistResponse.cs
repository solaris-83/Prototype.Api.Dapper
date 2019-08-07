using Prototype.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.Communication
{
    public class SaveArtistResponse : BaseResponse
    {
        public Artist Artist { get; private set; }

        private SaveArtistResponse(bool success, string message, Artist artist) : base(success, message)
        {
            Artist = artist;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="artist">Saved artist.</param>
        /// <returns>Response.</returns>
        public SaveArtistResponse(Artist artist) : this(true, string.Empty, artist)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveArtistResponse(string message) : this(false, message, null)
        { }
    }
}
