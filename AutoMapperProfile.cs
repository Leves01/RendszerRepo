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

        }
    }
}