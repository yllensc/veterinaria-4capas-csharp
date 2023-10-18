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
        CreateMap<Appointment,AppointmentWithPets>()
        .ForMember(dest => dest.PetName, origen=> origen.MapFrom(origen => origen.Pet.Name))
        .ReverseMap();
        CreateMap<Specie,SpecieDto>().ReverseMap();
        CreateMap<Specie,SpecieWithPetsDto>()
        .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
        .ReverseMap();
        CreateMap<Specie,SpeciesAllWithPetsDto>()
        .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
        .ReverseMap();
        CreateMap<Laboratory,LaboratoryDto>().ReverseMap();
        CreateMap<Laboratory, LaboratoryWithMedicinesDto>()
        .ForMember(dest => dest.Medicines, opt => opt.MapFrom(src => src.Medicines))
        .ReverseMap();
        CreateMap<Pet,PetDto>().ReverseMap();
        CreateMap<Pet,PetsBasicDto>()
        .ForMember(dest => dest.RaceName, origen=> origen.MapFrom(origen => origen.Race.Name))
        .ReverseMap();
        CreateMap<Pet,PetsBasicWithOwnerDto>()
        .ForMember(dest => dest.OwnerName, origen=> origen.MapFrom(origen => origen.Owner.Name))
        .ForMember(dest => dest.RaceName, origen=> origen.MapFrom(origen => origen.Race.Name))
        .ReverseMap();
        CreateMap<Medicine,MedicineDto>().ReverseMap();
        CreateMap<Medicine,MedicineBasicDto>().ReverseMap();
        CreateMap<Medicine,MedicineJustProvidersDto>()
        .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
        .ReverseMap();
        CreateMap<MedicineMovement,MedicineMovementDto>().ReverseMap();
        CreateMap<Owner,OwnerDto>().ReverseMap();
        CreateMap<Owner,OwnerWithPetsDto>()
        .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
        .ReverseMap();
        CreateMap<Provider,ProviderDto>().ReverseMap();
        CreateMap<Race,RaceDto>().ReverseMap();
        CreateMap<TypeMovement,TypeMovementDto>().ReverseMap();
        CreateMap<Treatment,TreatmentDto>().ReverseMap();
        CreateMap<Veterinarian,VeterinarianDto>().ReverseMap();
    }

}
