using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name {get; set;}
        public DateTime BirthDate {get; set;}
        public Owner Owner {get; set;}
        public int IdOwner {get; set;}
        public Specie Specie {get;set;}
        public int IdSpecie {get; set;}
        public Race Race {get; set;}
        public int IdRace {get; set;}
        public ICollection<Appointment> Appointments {get; set;}

    }
}