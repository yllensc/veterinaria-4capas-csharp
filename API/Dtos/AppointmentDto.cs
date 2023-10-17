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
        public PetDto Pet {get; set;}
        public int IdPet {get; set;}
        public VeterinarianDto Veterinarian {get; set;}
        public int IdVeterinarian {get; set;}
        public ICollection<TreatmentDto> Treatments {get; set;}
    }
}