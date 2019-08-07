using AutoMapper;
using Prototype.API.Domain.ApiResources;
using Prototype.API.Domain.Entities;
using Prototype.API.Domain.Resources;

namespace Prototype.API.Domain.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAlbumResource, Album>();
            CreateMap<SaveArtistResource, Artist>();
        }
    }
}
