using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AppointmentDto
    {
        
        public int Id {get; set;}
        public DateOnly DateAppointment {get; set;}
        public TimeSpan Hour {get; set;}
        public string Cause {get; set;}
        public PetsBasicDto Pet {get; set;}
        public int IdPet {get; set;}
        public VeterinarianDto Veterinarian {get; set;}
        public int IdVeterinarian {get; set;}
    }

    public class AppointmentWithPets{
        public DateOnly DateAppointment {get; set;}
        public string PetName {get; set;}
        public int IdPet {get; set;}
    }
}