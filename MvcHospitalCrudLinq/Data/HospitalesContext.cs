using MvcHospitalCrudLinq.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS ALMACENADOS
//create procedure modificarhospital
//(@hospitalCod int, @nombre nvarchar(50),@direccion nvarchar(50),@telefono nvarchar(50),@numCama int)
//as
//    update hospital set nombre = @nombre, direccion = @direccion, telefono = @telefono, num_cama = @numCama
//	where hospital_cod = @hospitalCod
//go

//create procedure insertarhospital 
//(@hospitalCod int, @nombre nvarchar(50),@direccion nvarchar(50),@telefono nvarchar(50),@numCama int)
//as
//    insert into hospital values(@hospitalCod, @nombre, @direccion, @telefono, @numCama)
//go
#endregion
namespace MvcHospitalCrudLinq.Data
{
    public class HospitalesContext
    {
        private SqlDataAdapter adhospital;
        private DataTable tablahospital;

        SqlCommand com;
        SqlConnection cn;

        public HospitalesContext() 
        {
            string cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2021";
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.RefreshData();
        }
        private void RefreshData() 
        {
            string cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2021";
            string sql = "select * from hospital";
            this.adhospital = new SqlDataAdapter(sql,cadenaconexion);
            this.tablahospital = new DataTable();
            this.adhospital.Fill(tablahospital);
        }
        //obtener todos los hospitales
        public List<Hospital> GetHospitales() 
        {
            var consulta = from datos in this.tablahospital.AsEnumerable()
                           select datos;
            List<Hospital> hospitales = new List<Hospital>();
            foreach (var row in consulta) 
            {
                Hospital hospital = new Hospital();
                hospital.HospitalCod = row.Field<int>("HOSPITAL_COD");
                hospital.Nombre = row.Field<string>("NOMBRE");
                hospital.Direccion = row.Field<string>("DIRECCION");
                hospital.Telefono = row.Field<string>("TELEFONO");
                hospital.NumCama = row.Field<int>("NUM_CAMA");
                hospitales.Add(hospital);
            }
            return hospitales;
        }
        //metodo para recuperar un solo hospital 
        public Hospital FindHospital(int idhospital) 
        {
            var consulta = from datos in this.tablahospital.AsEnumerable()
                           where datos.Field<int>("HOSPITAL_COD") == idhospital
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else 
            {
                Hospital hospital = new Hospital();
                //metodo para recuperar el primer elemento
                var row = consulta.First();
                hospital.HospitalCod = row.Field<int>("HOSPITAL_COD");
                hospital.Nombre = row.Field<string>("NOMBRE");
                hospital.Direccion = row.Field<string>("DIRECCION");
                hospital.Telefono = row.Field<string>("TELEFONO");
                hospital.NumCama = row.Field<int>("NUM_CAMA");
                return hospital;
            }
        }
        //metodo para modificar Hospital 
        public int ModificarHospital(int hospitalCod, string nombre, string direccion, string telefono, int numCama) 
        {
            string sql = "modificarhospital";
            this.com.Parameters.AddWithValue("@hospitalCod",hospitalCod);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@numCama", numCama);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            int results = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return results;
        }
        //metodo para insertar un nuevo hospital 
        public int InsertarHospital(int hospitalCod, string nombre, string direccion, string telefono, int numCama) 
        {
            string sql = "insertarhospital";
            this.com.Parameters.AddWithValue("@hospitalCod", hospitalCod);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@numCama", numCama);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            int results = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return results;
        }

        public int DeleteHospital(int hospitalCod) 
        {
            string sql = "delete from hospital where hospital_cod= @hospitalCod";
            this.com.Parameters.AddWithValue("@hospitalCod", hospitalCod);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int eliminados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            this.RefreshData();
            return eliminados;
        }
    }
}
