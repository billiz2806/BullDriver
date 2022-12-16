using BullDriver.Datos;
using BullDriver.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BullDriver.ViewModels
{
    public class PerfilUsuarioViewModel : BaseViewModel
    {
        #region VARIABLES
        string _fotoUsuario;
        string _txtNombre;
        string _txtApellido;
        MediaFile file;
        public Usuario modelUsuario { get; set; }
        #endregion

        #region CONSTRUCTOR
        public PerfilUsuarioViewModel(INavigation navigation, Usuario parametrosUsuario)
        {
            Navigation = navigation;
            modelUsuario = parametrosUsuario;
            FotoUsuario = modelUsuario.Foto;
            TxtApellido = modelUsuario.Apellido;
            TxtNombre = modelUsuario.Nombre;
        }
        #endregion

        #region OBJETOS
        public string TxtNombre
        {
            get { return _txtNombre; }
            set { SetValue(ref _txtNombre, value); }
        }
        public string TxtApellido
        {
            get { return _txtApellido; }
            set { SetValue(ref _txtApellido, value); }
        }
        public string FotoUsuario
        {
            get { return _fotoUsuario; }
            set { SetValue(ref _fotoUsuario, value); }
        }
        #endregion

        #region PROCESOS
        private async void SeleccionarFoto()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync( new PickMediaOptions());
                if (file != null)
                {
                    FotoUsuario = file.Path;
                    Foto = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "ok");
            }
        }
        private async Task SubirFoto()
        {
            if (file != null)
            {
                await EliminarFoto();
                var funcion = new DataUsuarios();
                var parametros = new Usuario();
                parametros.Id = modelUsuario.Id;
                FotoUsuario = await funcion.SubirImagenStorage(file.GetStream() ,parametros);
            }
        }
        private async Task EliminarFoto()
        {
            if(modelUsuario.Foto != "sinfoto.png")
            {
                var funcion = new DataUsuarios();
                var nombreFoto = modelUsuario.Id + ".jpg";
                await funcion.EliminarImagenStorage(nombreFoto);
            }
        }
        private async void EditarUsuario()
        {
            if (!string.IsNullOrWhiteSpace(TxtNombre))
            {
                if (!string.IsNullOrWhiteSpace(TxtApellido))
                {
                    SubirFoto();
                    var funcion = new DataUsuarios();
                    var parametros = new Usuario();
                    parametros.Id = modelUsuario.Id;
                    parametros.Nombre = TxtNombre;
                    parametros.Apellido = TxtApellido;
                    parametros.Foto = FotoUsuario;

                    await funcion.EditarUsuario(parametros);
                    AdondeVamosViewModel.estadoActualizar = true;

                }
                else
                {
                    await DisplayAlert("Alerta", "Debe ingresar apellidos", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Debe ingresar nombres", "Ok");
            }

        }
        private async void Volver()
        {
            await Navigation.PopAsync();
        }
        #endregion
        #region COMANDOS
        public ICommand VolverCommand => new Command(Volver);
        public ICommand EditarUsuarioCommand => new Command(EditarUsuario);
        public ICommand SeleccionarFotoCommand => new Command(SeleccionarFoto);
        #endregion
    }
}
