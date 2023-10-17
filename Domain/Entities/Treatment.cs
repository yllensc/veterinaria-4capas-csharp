using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Treatment : BaseEntity
    {
        public string Dosage {get; set;}
        public DateTime DateAdministration {get; set;}
        public string Observations {get; set;}
        public Appointment Appointment {get; set;}
        public int IdAppointment {get; set;}
        public Medicine Medicine {get; set;}
        public int IdMedicine {get; set;}

    }
}