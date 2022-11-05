using BullDriver.ViewModels;
using BullDriver.Views.Reutilizables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class Paises : BaseViewModel
    {
        private string _iconoUrl;
        private string _pais;
        private string _codigoPais;

        public string IconoUrl
        {
            get { return _iconoUrl; }
            set { SetValue(ref _iconoUrl, value); }
        }
        public string Pais
        {
            get { return _pais; }
            set { SetValue(ref _pais, value); }
        }
        public string CodigoPais
        {
            get { return _codigoPais; }
            set { SetValue(ref _codigoPais, value); }
        }
    }
}
