using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BullDriver.Conexiones
{
    public class Constantes
    {
        public const string GoogleMapsApiKey = "AIzaSyB6KOw_4i93vV7kFLPzyP18icvr50a0ygk";
        public static FirebaseClient firebase = new FirebaseClient("https://bulldriver-default-rtdb.firebaseio.com/");
    }
}