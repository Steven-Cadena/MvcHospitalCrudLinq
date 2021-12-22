using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Models
{
    public class ModelPlantillas
    {
        public List<Plantilla> Plantillas { get; set; }
        public int Personas { get; set; }
        public int SumaSalarial { get; set; }
        public double  MediaSalarial {get;set;}

    }
}
