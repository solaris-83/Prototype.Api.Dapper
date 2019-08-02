
namespace Prototype.API.Domain.Supervisors
{
    public partial class Supervisor : ISupervisor
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;

        public Supervisor()
        {

        }

        public Supervisor(IAlbumRepository albumRepository, IArtistRepository artistRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
        }
    }
}
