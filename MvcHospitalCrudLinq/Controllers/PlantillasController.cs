using Microsoft.AspNetCore.Mvc;
using MvcHospitalCrudLinq.Data;
using MvcHospitalCrudLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Controllers
{
    public class PlantillasController : Controller
    {
        PlantillasContext context;
        public PlantillasController() 
        {
            this.context = new PlantillasContext();
        }
        public IActionResult PlantillaHospital(int idhospital)
        {
            ModelPlantillas model = this.context.GetPlantillaHospital(idhospital);
            return View(model);
        }
    }
}
