using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OwnerDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
    }

    public class OwnerWithPetsDto : OwnerDto{
        public ICollection<PetsBasicDto> Pets {get; set;}
    }
}