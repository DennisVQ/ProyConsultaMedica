using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyConsultaMedica.Models
{
    public class Consulta
    {
        public int id_consulta { get; set; }

        public string codigo_generado { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string correo { get; set; }

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Requerido")]
        public string sexo { get; set; }

        [DisplayName("Edad")]
        [Required(ErrorMessage = "Requerido")]
        public int edad { get; set; }

        [DisplayName("Tipo de Paciente")]
        [Required(ErrorMessage = "Requerido")]
        public string tipo_paciente { get; set; }

        [DisplayName("Consulta")]
        [Required(ErrorMessage = "Requerido")]
        public string msje_pregunta { get; set; }

        [DisplayName("Respuesta")]
        [Required(ErrorMessage = "Requerido")]
        public string msje_respuesta { get; set; }


        public System.DateTime fechaPregunta { get; set; }

        public System.DateTime fechaRespuesta { get; set; }

        [DisplayName("Calificacion")]
        [Required(ErrorMessage = "Requerido")]
        [Range(0, 6, ErrorMessage = "Ingresa una calificacion valida")]
        public int calificacion { get; set; }

        [DisplayName("Estado de Consulta")]
        public int estado { get; set; }

        [DisplayName("Especialidad")]
        public string especialidad { get; set; }

        [DisplayName("Medico")]
        public string medico { get; set; }


    }
}