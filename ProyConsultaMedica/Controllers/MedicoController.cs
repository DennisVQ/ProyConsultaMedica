using System;
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
    public class MedicoController : Controller
    {

        SqlConnection cn = new SqlConnection("server=.;database=BD_CONSULTA;uid=sa;pwd=sql");

        IEnumerable<Pais> paises()
        {
            List<Pais> temporal = new List<Pais>();

            SqlCommand cmd = new SqlCommand("Select * from tb_pais", cn);

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read() == true)
            {
                Pais reg = new Pais();
                reg.id_pais = dr.GetInt32(0);
                reg.descripcion = dr.GetString(1);

                temporal.Add(reg);
            }

            dr.Close(); cn.Close();

            return temporal;
        }

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

        [AllowAnonymous]
        public ActionResult Login()
        {
            /*
            if (Session["medicoLogueado"] == null)
            {
                Medico medicoLogueado = new Medico();
                Session["medicoLogueado"] = medicoLogueado;
            }
            else {
                return RedirectToAction("Index","Medico");
            }
            

            return View(new Medico());

            */

            if (Session["medicoLogueado"] != null)
            {
                return RedirectToAction("Index", "Medico");
            }

            return View(new Medico());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Medico reg)
        {
            var medicoBuscado = medicos().FirstOrDefault(x => x.usuario == reg.usuario && x.clave == reg.clave);

            if (medicoBuscado == null)
            {
                ViewBag.Mensaje = "Usuario y/o contraseña incorrectos";
                return View(reg);
            }
            
            Session["medicoLogueado"] = (Medico) medicoBuscado;

            return RedirectToAction("Index", "Medico");
        }

        /*
        private static List<SelectListItem> GetListaSexo(string seleccion = "")
        {
            List<SelectListItem> lstSexo = new List<SelectListItem>();
            lstSexo.Add(new SelectListItem() { Text = "Escoge", Value = "" });
            lstSexo.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
            lstSexo.Add(new SelectListItem() { Text = "Femenino", Value = "Femenino" });
            return lstSexo;
        }

        //En el Action
        ViewBag.sexo = GetListaSexo("");

        */
        [AllowAnonymous]
        public ActionResult Nuevo()
        {
            ViewBag.paises = new SelectList(paises(), "id_pais", "descripcion");
            ViewBag.especialidades = new SelectList(especialidades(), "id_especialidad", "descripcion");

            return View(new Medico());
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Nuevo(Medico reg)
        {



            ViewBag.mensaje = "";
            cn.Open();

            try
            {


                SqlCommand cmd = new SqlCommand(
                "Insert tb_medico Values (@nombres, @apellidos, @sexo, @fechaNacimiento, @email, @telefono , @dni, " +
                "@cmp, @presentacion, @centro_estudios_univ, @lugarLaboral, @usuario, @clave, @estado, @fechaRegistro, @especialidad, @pais)", cn
                );

                cmd.Parameters.AddWithValue("@nombres", reg.nombres);
                cmd.Parameters.AddWithValue("@apellidos", reg.apellidos);
                cmd.Parameters.AddWithValue("@sexo", reg.sexo);
                cmd.Parameters.AddWithValue("@fechaNacimiento", reg.fechaNacimiento);
                cmd.Parameters.AddWithValue("@email", reg.email);
                cmd.Parameters.AddWithValue("@telefono", reg.telefono);
                cmd.Parameters.AddWithValue("@dni", reg.dni);
                cmd.Parameters.AddWithValue("@cmp", reg.cmp);
                cmd.Parameters.AddWithValue("@presentacion", reg.presentacion);
                cmd.Parameters.AddWithValue("@centro_estudios_univ", reg.centro_estudios_univ);
                cmd.Parameters.AddWithValue("@lugarLaboral", reg.lugarLaboral);
                cmd.Parameters.AddWithValue("@usuario", reg.usuario);
                cmd.Parameters.AddWithValue("@clave", reg.clave);
                cmd.Parameters.AddWithValue("@estado", 1);
                cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
                cmd.Parameters.AddWithValue("@especialidad", reg.especialidad);
                cmd.Parameters.AddWithValue("@pais", reg.pais);

                cmd.ExecuteNonQuery();
                ViewBag.mensaje = "Medico Registrado";

            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;

            }
            finally
            {
                cn.Close();
            }

            ViewBag.paises = new SelectList(paises(), "id_pais", "descripcion", reg.pais);
            ViewBag.especialidades = new SelectList(especialidades(), "id_especialidad", "descripcion", reg.especialidad);

            if (ViewBag.mensaje == "Medico Registrado")
            {
                return RedirectToAction("Login", "Medico");
            }

            return View(reg);


        }



        // GET: Medico
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("INDEX  ID" + ((Medico) Session["medicoLogueado"]).id_medico);
            System.Diagnostics.Debug.WriteLine("INDEX  USUARIO" + ((Medico)Session["medicoLogueado"]).usuario);

            return View();
        }

        public ActionResult Perfil(int id)
        {
            Debug.WriteLine("PERFIL " + id);

            Medico med = medicos().FirstOrDefault(x => x.id_medico == id) as Medico;

            if (med == null)
            {
                return HttpNotFound();
            }

            var espe = especialidades().Where(x => x.id_especialidad == (Convert.ToInt32(med.especialidad))).FirstOrDefault();
            var pais = paises().Where(x => x.id_pais == (Convert.ToInt32(med.pais))).FirstOrDefault();
            med.especialidad = espe.descripcion;
            med.pais = pais.descripcion;
    
            return View(med);

        }
    }
}