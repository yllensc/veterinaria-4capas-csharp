using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string Name {get; set;}
        public string Address {get; set;}
        public string PhoneNumber {get; set;}
        public ICollection<Medicine> Medicines{ get; set; }
    }
}