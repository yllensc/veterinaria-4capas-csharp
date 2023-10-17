using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TreatmentDto
    {
        public int Id {get; set;}
        public string Dosage {get; set;}
        public DateTime DateAdministration {get; set;}
        public string Observations {get; set;}
        public AppointmentDto Appointment {get; set;}
        public int IdAppointment {get; set;}
        public MedicineDto Medicine {get; set;}
        public int IdMedicine {get; set;}
    }
}