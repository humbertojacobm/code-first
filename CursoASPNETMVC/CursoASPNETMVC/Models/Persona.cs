using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoASPNETMVC.Models
{
    public class Persona
    {        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Edad { get; set; }        
        public List<Direccion> Direcciones { get; set; }
    }
}