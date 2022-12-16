using BullDriver.Conexiones;
using BullDriver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullDriver.Datos
{
    public class DataPasarelaPago
    {
        public async Task<List<PasarelaPago>> ListarPasarelaPagos()
        {
            return (await Constantes.firebase
                .Child("PasarelaPagos")
                .OnceAsync<PasarelaPago>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new PasarelaPago
                {
                    Id = item.Key,
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                }).ToList();
        }
    }
}
