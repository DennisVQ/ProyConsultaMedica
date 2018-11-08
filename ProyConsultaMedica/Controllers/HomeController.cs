﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProyConsultaMedica.Models;
using System.Diagnostics;


namespace ProyConsultaMedica.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection cn = new SqlConnection("server=.;database=BD_CONSULTA;uid=sa;pwd=sql");

        IEnumerable<Especialidad> especialidades()
        {
            List<Especialidad> temporal = new List<Especialidad>();

            SqlCommand cmd = new SqlCommand("Select * from tb_especialidad", cn);

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read() == true)
            {
                Especialidad reg = new Especialidad();
                reg.id_especialidad = dr.GetInt32(0);
                reg.descripcion = dr.GetString(1);

                temporal.Add(reg);
            }

            dr.Close(); cn.Close();

            return temporal;
        }


        IEnumerable<Medico> medicos()
        {
            List<Medico> temporal = new List<Medico>();

            SqlCommand cmd = new SqlCommand("Select * from tb_medico", cn);

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read() == true)
            {
                Medico reg = new Medico();
                reg.id_medico = dr.GetInt32(0);
                reg.nombres = dr.GetString(1);
                reg.apellidos = dr.GetString(2);
                reg.sexo = dr.GetString(3);
                reg.fechaNacimiento = dr.GetDateTime(4);
                reg.email = dr.GetString(5);
                reg.telefono = dr.GetString(6);
                reg.dni = dr.GetString(7);
                reg.cmp = dr.GetInt32(8);
                reg.presentacion = dr.GetString(9);
                reg.centro_estudios_univ = dr.GetString(10);
                reg.lugarLaboral = dr.GetString(11);
                reg.usuario = dr.GetString(12);
                reg.clave = dr.GetString(13);
                reg.estado = dr.GetInt32(14);
                reg.fechaRegistro = dr.GetDateTime(15);
                reg.especialidad = Convert.ToString(dr.GetInt32(16));
                reg.pais = Convert.ToString(dr.GetInt32(17));
                temporal.Add(reg);
            }

            dr.Close(); cn.Close();

            return temporal;
        }



        IEnumerable<Consulta> consultas()
        {
            List<Consulta> temporal = new List<Consulta>();

            SqlCommand cmd = new SqlCommand("Select * from tb_consulta", cn);

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read() == true)
            {
                Consulta reg = new Consulta();
                reg.id_consulta= dr.GetInt32(0);
                reg.codigo_generado = dr.GetString(1);
                reg.correo = dr.GetString(2);
                reg.sexo = dr.GetString(3);
                reg.edad = dr.GetInt32(4);
                reg.tipo_paciente = dr.GetString(5);
                reg.msje_pregunta = dr.GetString(6);
                reg.msje_respuesta = dr.GetString(7);
                reg.fechaPregunta = dr.GetDateTime(8);
                reg.fechaRespuesta = dr.GetDateTime(9);
                reg.calificacion = dr.GetInt32(10);
                reg.estado = dr.GetInt32(11);
                reg.especialidad = Convert.ToString(dr.GetInt32(12));
                reg.medico = Convert.ToString(dr.GetInt32(13));
                temporal.Add(reg);
            }

            dr.Close(); cn.Close();

            return temporal;
        }









        [AllowAnonymous]
        public ActionResult Index()
        {
            Session["consultaLogueada"] = null;

            return View();
        }

        public ActionResult ListarConsultas(int? pagina = 0)
        {
            // Query
            var consultasRespondidas = consultas().Where(x => x.estado == 2).ToList();

            //paginacion
            ViewBag.pagina = pagina;
            int n = consultasRespondidas.Count();
            var numreg = 10;
            ViewBag.botones = n % numreg == 0 ? n / numreg : n / numreg + 1;

            int regini = (int)pagina * numreg;
            int regfin = regini + numreg;

            List<Consulta> temporal = new List<Consulta>();

            // Falta Revisar
            ViewBag.especialidades = especialidades().ToList();
            ViewBag.medicos = medicos().ToList();
            
            for (int i = regini; i < regfin; i++)
            {
                if (i == n) break;

                temporal.Add(consultasRespondidas[i]);
            }

            return View(temporal);

        }


        [AllowAnonymous]
        public ActionResult CrearConsulta()
        {
            ViewBag.especialidades = new SelectList(especialidades(), "id_especialidad", "descripcion");

            return View(new Consulta());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CrearConsulta(Consulta consulta)
        {
            ViewBag.mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarConsulta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_generado", "");
                cmd.Parameters.AddWithValue("@correo", consulta.correo);
                cmd.Parameters.AddWithValue("@sexo", consulta.sexo);
                cmd.Parameters.AddWithValue("@edad", consulta.edad);
                cmd.Parameters.AddWithValue("@tipo_paciente", consulta.tipo_paciente);
                cmd.Parameters.AddWithValue("@msje_pregunta", consulta.msje_pregunta);
                cmd.Parameters.AddWithValue("@fechaPregunta", DateTime.Now);
                cmd.Parameters.AddWithValue("@msje_respuesta", null);
                cmd.Parameters.AddWithValue("@fechaRespuesta", null);
                cmd.Parameters.AddWithValue("@calificacion", 0);
                cmd.Parameters.AddWithValue("@estado", 1);
                cmd.Parameters.AddWithValue("@id_especialidad", consulta.especialidad);
                cmd.Parameters.AddWithValue("@id_medico", Convert.ToInt32("")); // Se escogera al azar

                cmd.ExecuteNonQuery();
                ViewBag.mensaje = "Registro Agregado";
            }
            catch (SqlException ex) { ViewBag.mensaje = ex.Message; }
            finally { cn.Close(); }


            if (ViewBag.mensaje == "Registro Registrado")
            {
                ViewBag.codigoGenerado = ""; // Falta Codigo

                return RedirectToAction("VerCodigoGenerado");
            }

            ViewBag.especialidades = new SelectList(
                especialidades(), "id_especialidad", "descripcion", consulta.especialidad);

            return View(consulta);

        }

        [AllowAnonymous]
        public ActionResult VerCodigoGenerado()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginConsulta()
        {
            Session["consultaLogueada"] = null;

            return View(new Consulta());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginConsulta(Consulta consulta)
        {
            var consultaBuscada = consultas().FirstOrDefault(x => x.codigo_generado == consulta.codigo_generado);

            if (consultaBuscada == null)
            {
                ViewBag.Mensaje = "El Codigo Ingresado es incorrectos";
                return View(consulta);
            }

            Session["consultaLogueada"] = (Consulta) consultaBuscada;

            return RedirectToAction("VerConsulta");
        }

        [AllowAnonymous]
        public ActionResult VerConsulta()
        {
            Consulta consulta = (Consulta) Session["consultaLogueada"];

            if (consulta == null) {
                return RedirectToAction("Index");
            }

            // Se cambio por Session en la Vista
            // ViewBag.estadoConsulta = consulta.estado;

            return View(consulta);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CalificarConsulta (Consulta consulta)
        {
            // Falta Determinar

            // Consulta consulta = (Consulta)Session["consultaLogueada"];


            if (consulta == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("Update tb_consulta " +
                    "set calificacion = @calificacion where id_consulta = @id_consulta", cn);
                
                cmd.Parameters.AddWithValue("@id_consulta", consulta.id_consulta);
                cmd.Parameters.AddWithValue("@calificacion", consulta.calificacion);
                cmd.ExecuteNonQuery();

                ViewBag.mensaje = "Calificacion Realizada";

            }

            catch (SqlException ex) { ViewBag.mensaje = ex.Message; }
            finally { cn.Close(); }


            if (ViewBag.mensaje == "Calificacion Realizada")
            {
                Session["consultaLogueada"] = null;
                return RedirectToAction("Index");

            }

            return VerConsulta();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}