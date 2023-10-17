using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medicine : BaseEntity
    {
        public string Name {get; set;}
        public int QuantityDisp {get; set;}
        public double Price { get; set; }
        public Laboratory Laboratory {get; set;}
        public int IdLaboratory {get; set;}
        public Provider Provider {get; set;}
        public int IdProvider {get; set;}
        public ICollection<Treatment> Treatments {get; set;}
        public ICollection<MedicineMovement> MedicineMovements {get; set;}

    }
}