using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Models
{
    public class Hospital
    {
        public int HospitalCod { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NumCama { get; set; }
    }
}
