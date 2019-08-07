using AutoMapper;
using Prototype.API.Domain.Entities;
using Prototype.API.Domain.ApiModels;
using Prototype.API.Domain.ApiResources;

namespace Prototype.API.Domain.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Album, AlbumResource>();
            CreateMap<Artist, ArtistResource>();
        }
    }
}
