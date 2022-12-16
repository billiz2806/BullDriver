using BullDriver.Conexiones;
using BullDriver.Models;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullDriver.Datos
{
    public class DataUsuarios
    {
        public async Task<bool> InsertUsuario(Usuario parametros)
        {
            await Constantes.firebase
                .Child("Usuarios")
                .PostAsync(new Usuario()
                {
                    IdGoogle = parametros.IdGoogle,
                    Nombre = parametros.Nombre,
                    Apellido = parametros.Apellido,
                    Correo = parametros.Correo,
                    Celular = parametros.Celular,
                    Estado = parametros.Estado,
                    Calificacion = parametros.Calificacion,
                    SimboloMoneda = parametros.SimboloMoneda,
                    Foto = "sinfoto.png"
                });
            return true;
        }

        public async Task<List<Usuario>> ListarUsuarioPorIdGoogle(Usuario parametros)
        {
            return (await Constantes.firebase
                .Child("Usuarios")
                .OnceAsync<Usuario>())
                .Where(a => a.Object.IdGoogle == parametros.IdGoogle)
                .Select(item => new Usuario
                {
                    Id = item.Key,
                    SimboloMoneda = item.Object.SimboloMoneda,
                    Nombre = item.Object.Nombre,
                    Apellido = item.Object.Apellido,
                    Correo = item.Object.Correo,
                    Foto= item.Object.Foto,
                    Celular = item.Object.Celular,
                    Calificacion= item.Object.Calificacion,
                    Estado=item.Object.Estado,
                }).ToList();
        }

        public async Task<string> SubirImagenStorage(Stream imageStream, Usuario parametros)
        {
            string rutaFoto;

            var imagen = await new FirebaseStorage(Constantes.storage)
                .Child("fotoUsuario")
                .Child(parametros.Id + ".jpg")
                .PutAsync(imageStream);

            rutaFoto = imagen;
            return rutaFoto;
        }

        public async Task EditarUsuario(Usuario parametros)
        {
            var data = (await Constantes.firebase
                .Child("Usuarios")
                .OnceAsync<Usuario>())
                .Where(a => a.Key == parametros.Id)
                .FirstOrDefault();

            data.Object.Nombre = parametros.Nombre;
            data.Object.Apellido = parametros.Apellido;
            data.Object.Foto = parametros.Foto;

            await Constantes.firebase
                .Child("Usuarios")
                .Child(data.Key)
                .PutAsync(data.Object);

        }

        public async Task EliminarImagenStorage(string nombreFoto)
        {
            await new FirebaseStorage(Constantes.storage)
                .Child("fotoUsuario")
                .Child(nombreFoto)
                .DeleteAsync();
        }
    }
}
