using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MvcHospitalCrudLinq.Models;

namespace MvcHospitalCrudLinq.Data
{
    public class PlantillasContext
    {
        private string cadenaconexion;
        private SqlDataAdapter adplantilla;
        private DataTable TablaPlantilla;

        public PlantillasContext() 
        {
            string cadenaconexion = @"Data Source=sqltajamar94.database.windows.net;Initial Catalog=AZURESTEVEN;Persist Security Info=True;User ID=adminsql;Password=Admin123";
            string sql = "select * from plantilla";
            this.adplantilla = new SqlDataAdapter(sql, cadenaconexion);
            this.TablaPlantilla = new DataTable();
            this.adplantilla.Fill(TablaPlantilla);
        }
        public ModelPlantillas GetPlantillaHospital(int idhospital) 
        {
            var consulta = from datos in this.TablaPlantilla.AsEnumerable()
                           where datos.Field<int>("HOSPITAL_COD") == idhospital
                           select datos;
            List<Plantilla> plantillas = new List<Plantilla>();
            foreach (var row in consulta) 
            {
                Plantilla plan = new Plantilla();
                plan.IdPlantilla = row.Field<int>("EMPLEADO_NO");
                plan.Apellido = row.Field<string>("APELLIDO");
                plan.Funcion = row.Field<string>("FUNCION");
                plan.Salario = row.Field<int>("SALARIO");
                plan.Turno = row.Field<string>("T");
                plan.IdHospital = row.Field<int>("HOSPITAL_COD");
                plantillas.Add(plan);
            }
            int personas = plantillas.Count();
            int suma = plantillas.Sum(x => x.Salario);
            double media = plantillas.Average(z => z.Salario);
            ModelPlantillas modelPlantillas = new ModelPlantillas();
            modelPlantillas.Plantillas = plantillas;
            modelPlantillas.SumaSalarial = suma;
            modelPlantillas.MediaSalarial = media;
            modelPlantillas.Personas = personas;
            return modelPlantillas;
        }
    }
}
