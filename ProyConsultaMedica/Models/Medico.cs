using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace ProyConsultaMedica.Models
{
    public class Medico
    {
        public int id_medico { get; set; }

        [DisplayName("Nombres")]
        [Required(ErrorMessage = "Requerido")]
        public string nombres { get; set; }


        [DisplayName("Apellidos")]
        [Required(ErrorMessage = "Requerido")]
        public string apellidos { get; set; }

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Requerido")]
        public string sexo { get; set; }


        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaNacimiento { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string email { get; set; }

        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Requerido")]
        public string telefono { get; set; }

        [DisplayName("DNI")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Ingrese 8 Digitos")]
        public string dni { get; set; }

        [DisplayName("Nro. CMP")]
        [Required(ErrorMessage = "Requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingresa un numero de CMP valido")]
        public int cmp { get; set; }

        [DisplayName("Presentacion")]
        [Required(ErrorMessage = "Requerido")]
        public string presentacion { get; set; }

        [DisplayName("Centro de Estudios")]
        [Required(ErrorMessage = "Requerido")]
        public string centro_estudios_univ { get; set; }

        [DisplayName("Lugar de Trabajo")]
        [Required(ErrorMessage = "Requerido")]
        public string lugarLaboral { get; set; }

        [DisplayName("Usuario")]
        [Required(ErrorMessage = "Requerido")]
        public string usuario { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Requerido")]
        public string clave { get; set; }

        public int estado { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        //Foreign Keys

        [DisplayName("Especialidad")]
        public string especialidad { get; set; }

        [DisplayName("Pais")]
        public string pais { get; set; }
    }
}