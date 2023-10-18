using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SpecieDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
    }

    public class SpecieWithPetsDto : SpecieDto{
        public ICollection<PetsBasicDto> Pets {get; set;}
    }
    public class SpeciesAllWithPetsDto {
        public string Name {get; set;}
        public ICollection<PetsBasicDto> Pets {get; set;}
    }
}