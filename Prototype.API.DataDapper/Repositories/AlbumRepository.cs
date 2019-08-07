using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Prototype.API.DataDapper.StaticQueries;
using Prototype.API.Domain.DbInfoConnection;
using Prototype.API.Domain.Entities;
using Prototype.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.API.DataDapper.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbInfo _dbInfo;
        private readonly ILogger _logger;

        public AlbumRepository(DbInfo dbInfo, ILogger<AlbumRepository> logger)
        {
            _dbInfo = dbInfo;
            _logger = logger;
        }

        private IDbConnection Connection
        {
            get
            {
                if (_dbInfo.DBConnectionType == DBConnectionEnum.SQLite)
                    return new SqliteConnection(_dbInfo.ConnectionStrings);
                else
                    return new SqlConnection(_dbInfo.ConnectionStrings);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        private async Task<bool> AlbumExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Albums where AlbumId = @id", new { id });

        public async Task<IEnumerable<Album>> GetAllAsync(int offset, int limit)
        {
            try
            {
                using (IDbConnection cn = Connection)
                {
                    
                    cn.Open();
                    var sql = _dbInfo.DBConnectionType == DBConnectionEnum.SQLite? string.Format(SQLiteQueries.SELECT_ALL_ALBUMS, (offset - 1) * limit, limit) : string.Format(SQLServerQueries.SELECT_ALL_ALBUMS, (offset - 1) * limit, limit);
                    var albums = await Connection.QueryAsync<Album>(sql);
                    return albums;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var album = await cn.QueryFirstOrDefaultAsync<Album>("Select * From Albums WHERE AlbumId = @id", new { id });
                return album;
            }
        }

        public async Task<List<Album>> GetByArtistIdAsync(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var albums = await cn.QueryAsync<Album>("Select * From Albums WHERE ArtistId = @id", new { id });
                return albums.ToList();
            }
        }

        public async Task<Album> AddAsync(Album newAlbum)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var albumId = await cn.InsertAsync(new Album { Title = newAlbum.Title, ArtistId = newAlbum.ArtistId }); //TODO handle exceptions

                newAlbum.AlbumId = albumId;
            }

            return newAlbum;
        }

        public async Task<bool> UpdateAsync(Album album)
        {
            if (!await AlbumExists(album.AlbumId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.UpdateAsync(album);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.DeleteAsync(new Album { AlbumId = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
