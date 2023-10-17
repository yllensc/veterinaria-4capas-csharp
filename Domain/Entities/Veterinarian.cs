using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Veterinarian : BaseEntity
    {
        public string Name {get; set;}
        public string PhoneNumber {get; set;}
        public string Specialty {get; set;}
        public User User { get; set; }
        public int IdUser { get; set; } 
        public ICollection<Appointment> Appointments {get; set;}
        
    }
}