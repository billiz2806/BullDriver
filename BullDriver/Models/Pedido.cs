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
        public string Notificacion { get; set; }
        public string CalificarCliente { get; set; }
        public string CalificarConductor { get; set; }
        public string ComentarioConductor { get; set; }
        public string ComentariosDeseos { get; set; }
        public string IdPasarelaPago { get; set; }


    }
}
