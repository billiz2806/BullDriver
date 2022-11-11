using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class Pedido
    {
        public string IdPedido { get; set; }
        public string Destino_lugar { get; set; }
        public string Origen_lugar { get; set; }
        public string Estado { get; set; }
        public string IdChofer { get; set; }
        public string IdUser { get; set; }
        public string Lt_lg_destino { get; set; }
        public string Lt_lg_origen { get; set; }
        public string Tiempo { get; set; }
        public string Tarifa { get; set; }
        public string Distancia { get; set; }


    }
}
