using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LaboratoryDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Address {get; set;}
        public string PhoneNumber {get; set;}
    }

    public class LaboratoryWithMedicinesDto {
        public string Name {get; set;}
        public ICollection<MedicineDto> Medicines {get; set;}
    }
}