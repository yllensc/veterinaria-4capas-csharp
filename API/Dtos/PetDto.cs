using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PetDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime BirthDate {get; set;}
        public OwnerDto Owner {get; set;}
        public int IdOwner {get; set;}
        public SpecieDto Specie {get;set;}
        public int IdSpecie {get; set;}
        public RaceDto Race {get; set;}
        public int IdRace {get; set;}
    }

    public class PetsBasicDto{
        public string Name {get; set;}
        public DateTime BirthDate {get; set;}
        public int IdOwner {get; set;}
        public string RaceName {get; set;}
    }
        public class PetsBasicWithOwnerDto{
        public string Name {get; set;}
        public DateTime BirthDate {get; set;}
        public int IdOwner {get; set;}
        public string OwnerName {get; set;}
        public string RaceName {get; set;}
    }
}