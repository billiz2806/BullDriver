using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string IdGoogle { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public string Calificacion { get; set; }
        public string SimboloMoneda { get; set; }
        public string Foto { get; set; }
    }
}
