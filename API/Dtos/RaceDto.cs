using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RaceDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public SpecieDto Specie {get; set;}
        public int IdSpecie {get; set;}
    }
}