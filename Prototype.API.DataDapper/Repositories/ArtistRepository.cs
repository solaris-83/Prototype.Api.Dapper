using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Prototype.API.Domain;
using Prototype.API.Domain.Entities;
using Prototype.API.Domain.Supervisors;
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

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            Connection.Dispose();
        }

        private async Task<bool> ArtistExists(int id, CancellationToken ct = default) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Artists where ArtistId = @id", new { id });

        public async Task<List<Artist>> GetAllAsync(CancellationToken ct = default)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var artists = await Connection.QueryAsync<Artist>("Select * From Artists");
                return artists.ToList();
            }
        }

        public async Task<Artist> GetByIdAsync(int id, CancellationToken ct = default)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return await cn.QueryFirstOrDefaultAsync<Artist>("Select * From Artists WHERE ArtistId = @Id", new { id });
            }
        }

        public async Task<Artist> AddAsync(Artist newArtist, CancellationToken ct = default)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newArtist.ArtistId = await cn.InsertAsync(new Artist { Name = newArtist.Name });
            }

            return newArtist;
        }

        public async Task<bool> UpdateAsync(Artist artist, CancellationToken ct = default)
        {
            if (!await ArtistExists(artist.ArtistId, ct))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.UpdateAsync(artist);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return await cn.DeleteAsync(new Artist { ArtistId = id });
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
