using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {
        CreateMap<User, UserWithRolDto>()
        .ForMember(dest => dest.RolName, opt => opt.MapFrom(src => src.Roles.FirstOrDefault().Name));
       CreateMap<Appointment,AppointmentDto>().ReverseMap();
        CreateMap<Specie,SpecieDto>().ReverseMap();
        CreateMap<Laboratory,LaboratoryDto>().ReverseMap();
        CreateMap<Pet,PetDto>().ReverseMap();
        CreateMap<Medicine,MedicineDto>().ReverseMap();
        CreateMap<MedicineMovement,MedicineMovementDto>().ReverseMap();
        CreateMap<Owner,OwnerDto>().ReverseMap();
        CreateMap<Provider,ProviderDto>().ReverseMap();
        CreateMap<Race,RaceDto>().ReverseMap();
        CreateMap<TypeMovement,TypeMovementDto>().ReverseMap();
        CreateMap<Treatment,TreatmentDto>().ReverseMap();
        CreateMap<Veterinarian,VeterinarianDto>().ReverseMap();
    }

}
