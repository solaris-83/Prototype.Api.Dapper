using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.DataDapper.StaticQueries
{
    public static class SQLiteQueries
    {
        public static string SELECT_ALL_ALBUMS = "Select * From Albums ORDER BY AlbumId LIMIT {0}, {1}";
        public static string SELECT_ALL_ARTISTS = "Select * From Artists Order by ArtistId LIMIT {0}, {1}";
    }

    public static class SQLServerQueries
    {
        public static string SELECT_ALL_ALBUMS = "Select * From Albums ORDER BY AlbumId OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY";
        public static string SELECT_ALL_ARTISTS = "Select * From Artists Order by ArtistId OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY";
    }
}
