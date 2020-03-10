using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {
        public int id { get; set; }
        [Required]
        [Display (Name ="Nombre")]
        public string Nombres { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Edad")]
        public int Edad { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
        [Required]
        [Display(Name = "Ciudad")]
        public int CodCiudad { get; set; }

        public System.DateTime FechaRegistro { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreCompleto { get { return Nombres + " " + Apellidos; }  }
    }
    [MetadataType(typeof(AlumnoCE))]
    public partial class Alumno
    {
        
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }
        public string NombreCiudad { get; set; }
    }
}