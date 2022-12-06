using BullDriver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class OfertaConductor : BaseViewModel
    {
        public string IdOferta { get; set; }
        public string Estado { get; set; }
        public string IdConductor { get; set; }
        public string IdPedido { get; set; }
        public string IdUsuario { get; set; }
        public string TiempoOrigen { get; set; }
        public string Tarifa { get; set; }

        //objetos
        public TimeSpan _timeSpan;
        public double _progress;

        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set { SetValue(ref _timeSpan, value); }
        }

        public double Progress
        {
            get { return _progress; }
            set { SetValue(ref _progress, value); }
        }

    }
}
