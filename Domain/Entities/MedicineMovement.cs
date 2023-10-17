using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicineMovement : BaseEntity
    {
        public int Quantity {get; set;}
        public DateTime DateMovement {get; set;}
        public double PriceUnit {get; set;}
        public Medicine Medicine {get; set;}
        public int IdMedicine {get; set;}
        public TypeMovement TypeMovement {get; set;}
        public int IdTypeMovement {get; set;}
    }
}