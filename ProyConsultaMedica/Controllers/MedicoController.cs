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


        IEnumerable<Consulta> consultas()
        {
            List<Consulta> temporal = new List<Consulta>();

            SqlCommand cmd = new SqlCommand("Select * from tb_consulta", cn);

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read() == true)
            {
                Consulta reg = new Consulta();
                reg.id_consulta = dr.GetInt32(0);
                reg.codigo_generado = dr.GetString(1);
                reg.correo = dr.GetString(2);
                reg.sexo = dr.GetString(3);
                reg.edad= dr.GetInt32(4);
                reg.tipo_paciente = dr.GetString(5);
                reg.msje_pregunta = dr.GetString(6);
                reg.msje_respuesta = dr.GetString(7);
                reg.fechaPregunta = dr.GetDateTime(8);
                reg.fechaRespuesta = dr.GetDateTime(9);
                reg.calificacion = dr.GetInt32(10);
                reg.estado = dr.GetInt32(11);
                reg.especialidad = dr.GetString(12);
                reg.medico = dr.GetString(13);
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
        public ActionResult Index(int? pagina = 0)
        {
            Medico medicoLog = (Medico) Session["medicoLogueado"];

            // Query
            var consultasPorMedico = consultas().Where(
                x => x.medico == Convert.ToString(medicoLog.id_medico)).ToList();

            //paginacion
            ViewBag.pagina = pagina;
            int n = consultasPorMedico.Count();
            var numreg = 10;
            ViewBag.botones = n % numreg == 0 ? n / numreg : n / numreg + 1;

            int regini = (int) pagina * numreg;
            int regfin = regini + numreg;

            List<Consulta> temporal = new List<Consulta>();
            for (int i = regini; i < regfin; i++)
            {
                if (i == n) break;

                temporal.Add(consultasPorMedico[i]);
            }

            return View(temporal);

        }

        public ActionResult DetalleConsulta (int id) {

            Consulta cons = consultas().FirstOrDefault(x => x.id_consulta== id) as Consulta;


            if (cons == null)
            {
                return HttpNotFound();
            }

            var espe = especialidades().Where(x => x.id_especialidad == (Convert.ToInt32(cons.especialidad))).FirstOrDefault();
            var med = medicos().Where(x => x.id_medico== (Convert.ToInt32(cons.medico))).FirstOrDefault();
            cons.especialidad = espe.descripcion;
            cons.medico = "Dr. " + med.apellidos + ", " + med.nombres;
            
            return View(cons);
        }

        public ActionResult ResponderConsulta(int id)
        {
            
            if (id == null)
            {
                // https://stackoverrun.com/es/q/8612373

                // return RedirectToAction("Index");

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Consulta cons = consultas().FirstOrDefault(x => x.id_consulta == id) as Consulta;

            if (cons == null)
            {
                /* https://stackoverrun.com/es/q/8612373 */

                // return RedirectToAction("Index");

                return HttpNotFound();
            }


            var espe = especialidades().Where(x => x.id_especialidad == (Convert.ToInt32(cons.especialidad))).FirstOrDefault();
            cons.especialidad = espe.descripcion;
            
            return View(cons);

        }


        [HttpPost]
        public ActionResult ResponderConsulta(Consulta consulta) {
            ViewBag.mensaje = "";

            cn.Open();


            //recupero el registro original para actualizarlo
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarConsulta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_consulta", consulta.id_consulta);
                cmd.Parameters.AddWithValue("@msje_respuesta", consulta.msje_respuesta);
                cmd.Parameters.AddWithValue("@fechaRespuesta", DateTime.Now);
                cmd.Parameters.AddWithValue("@calificacion", consulta.calificacion); // No Cambia
                cmd.Parameters.AddWithValue("@estado", 2);

                cmd.ExecuteNonQuery();
                ViewBag.mensaje = "Consulta Respondida";
                
            }
            catch (SqlException ex) { ViewBag.mensaje = ex.Message; }
            finally { cn.Close(); }


            if (ViewBag.mensaje == "Consulta Respondida")
            {
                return RedirectToAction("Index");
            }

            return View(consulta);

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