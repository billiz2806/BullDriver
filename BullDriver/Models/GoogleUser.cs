﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Models
{
    public class GoogleUser
    {
        public string NumeroCel { get; set; }
        public string IdGoogle { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
        public string SimboloMoneda { get; set; }

    }

    public interface IGoogleManager
    {
        void Login(Action<GoogleUser,string> OnLoginComplete);
        void Logout();
    }
}
