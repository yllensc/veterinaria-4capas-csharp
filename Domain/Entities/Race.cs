using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Race : BaseEntity
    {
        public string Name {get; set;}
        public Specie Specie {get; set;}
        public int IdSpecie {get; set;}
        public ICollection<Pet> Pets {get; set;}
    }
}