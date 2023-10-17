using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MedicineMovementDto
    {
        public int Id {get; set;}
        public int Quantity {get; set;}
        public DateTime DateMovement {get; set;}
        public double PriceUnit {get; set;}
        public MedicineDto Medicine {get; set;}
        public int IdMedicine {get; set;}
        public TypeMovementDto TypeMovement {get; set;}
        public int IdTypeMovement {get; set;}
    }
}