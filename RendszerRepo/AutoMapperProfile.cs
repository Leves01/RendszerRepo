using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User maps
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();

            //Part maps
            CreateMap<Part, GetPartDto>();
            CreateMap<AddPartDto, Part>();
            CreateMap<UpdatePartDto, Part>();
            CreateMap<DeletePartDto, Part>();
            CreateMap<UpdatePartPriceDto, Part>();
            CreateMap<PartToProjectDto, Part>();
            CreateMap<PartToProjectDto, Project_properties>();

            //Storage maps
            CreateMap<Storage, GetStoragesDto>();
            CreateMap<AddStorageDto, Storage>();
            CreateMap<UpdateMaxDto, Storage>();
            CreateMap<UpdateStoragesDto, Storage>();
            CreateMap<DeleteStorageDto, Storage>();

            //Project prop
            CreateMap<Project_properties, GetProject_propertiesDto>();
            CreateMap<AddProjectDto, Project_properties>();
            CreateMap<Project, GetProject_propertiesDto>();
            CreateMap<Project_properties, GetProjectDto>();
            
            //Project
            CreateMap<Project, GetPrDto>();
            CreateMap<AddPrDto, Project>();

            //Reserve
            CreateMap<reservedParts, GetReserveDto>();
            CreateMap<AddReserveDto, reservedParts>();
            
            //Dependecies
            CreateMap<Project, GetPartDto>();
            CreateMap<GetPartDto, Project>();
            CreateMap<Storage, GetPartDto>();
            CreateMap<GetPartDto, Storage>();

        }
    }
}