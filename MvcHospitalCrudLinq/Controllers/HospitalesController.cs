using Microsoft.AspNetCore.Mvc;
using MvcHospitalCrudLinq.Data;
using MvcHospitalCrudLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHospitalCrudLinq.Controllers
{
    public class HospitalesController : Controller
    {
        HospitalesContext context;
        //constructor
        public HospitalesController() 
        {
            this.context = new HospitalesContext();
        }
        public IActionResult Index()
        {
            List<Hospital> hospitales = this.context.GetHospitales();
            return View(hospitales);
        }
        public IActionResult Details(int idhospital) 
        {

            Hospital hospital = this.context.FindHospital(idhospital);
            return View(hospital);
        }

        //metodo get 
        public IActionResult Editar(int idhospital) 
        {
            Hospital hospital = this.context.FindHospital(idhospital);
            ViewBag.Hospital = hospital;
            return View();    
        }
        //metodo post
        [HttpPost]
        public IActionResult Editar(int hospitalCod,string nombre,string direccion,string telefono,int numCama)
        {
            int results = this.context.ModificarHospital(hospitalCod, nombre, direccion, telefono, numCama);
            ViewData["MENSAJE"] = "Modificado : " + results + "hospital.";

            Hospital hospital = this.context.FindHospital(hospitalCod);
            ViewBag.Hospital = hospital;
            //return View();
            return RedirectToAction("Index");
        }

        //metodo para insertar GET
        public IActionResult Insertar() 
        {
            return View();
        }

        //metodo para insertar POST
        [HttpPost]
        public IActionResult Insertar(int hospitalCod, string nombre, string direccion, string telefono, int numCama) 
        { 
            int results = this.context.InsertarHospital(hospitalCod, nombre, direccion, telefono, numCama);
            ViewData["MENSAJE"] = "Modificado : " + results + "hospital.";
            //return View();
            return RedirectToAction("Index");
        }
        //metodo para eliminar el hospital 
        public IActionResult Delete(int hospitalCod) 
        {
            this.context.DeleteHospital(hospitalCod);
            return RedirectToAction("Index");
        }
    }
}
