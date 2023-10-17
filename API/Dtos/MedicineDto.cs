using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MedicineDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public int QuantityDisp {get; set;}
        public double Price { get; set; }
        public LaboratoryDto Laboratory {get; set;}
        public int IdLaboratory {get; set;}
        public ProviderDto Provider {get; set;}
        public int IdProvider {get; set;}
    }
}