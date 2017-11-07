using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoASPNETMVC.Models
{
    public class Direccion
    {
        public int CodigoDireccion { get; set; }
        public string Calle { get; set; }
        public int Persona_Id { get; set; }
        public Persona Persona { get; set; }

    }
}