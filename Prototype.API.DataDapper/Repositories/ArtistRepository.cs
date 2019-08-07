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
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.API.DataDapper.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DbInfo _dbInfo;
        private readonly ILogger _logger;

        public ArtistRepository(DbInfo dbInfo, ILogger<ArtistRepository> logger)
        {
            _dbInfo = dbInfo;
            _logger = logger;
        }

        private IDbConnection Connection => new SqliteConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            Connection.Dispose();
        }

        private async Task<bool> ArtistExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Artists where ArtistId = @id", new { id });

        public async Task<IEnumerable<Artist>> GetAllAsync(int offset, int limit)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var sql = _dbInfo.DBConnectionType == DBConnectionEnum.SQLite ? string.Format(SQLiteQueries.SELECT_ALL_ARTISTS, (offset - 1) * limit, limit) : string.Format(SQLServerQueries.SELECT_ALL_ARTISTS, (offset - 1) * limit, limit);
                var artists = await Connection.QueryAsync<Artist>(sql);
                return artists;
            }
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return await cn.QueryFirstOrDefaultAsync<Artist>("Select * From Artists WHERE ArtistId = @id", new { id });
            }
        }

        public async Task<Artist> AddAsync(Artist newArtist)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newArtist.ArtistId = await cn.InsertAsync(new Artist { Name = newArtist.Name });
            }

            return newArtist;
        }

        public async Task<bool> UpdateAsync(Artist artist)
        {
            if (!await ArtistExists(artist.ArtistId))
                return false;

            //try
            //{
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.UpdateAsync(artist);
                }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return false;
            //}
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //try
            //{
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.DeleteAsync(new Artist { ArtistId = id });
                }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return false;
            //}
        }
    }
}
