using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public DateOnly DateAppointment {get; set;}
        public TimeSpan Hour {get; set;}
        public string Cause {get; set;}
        public Pet Pet {get; set;}
        public int IdPet {get; set;}
        public Veterinarian Veterinarian {get; set;}
        public int IdVeterinarian {get; set;}
        public ICollection<Treatment> Treatments {get; set;}
    }
}