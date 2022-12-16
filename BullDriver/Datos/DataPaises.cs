﻿using BullDriver.Models;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BullDriver.Datos
{
    public class DataPaises
    {
        public static List<RegionInfo> ObtenerPisesIso3166()
        {
            var paises = new List<RegionInfo>();
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var info = new RegionInfo(culture.LCID);
                if(paises.All(p=>p.Name != info.Name))
                {
                    paises.Add(info);
                }
            }
            return paises.OrderBy(p=> p.EnglishName).ToList();
        }

        public List<Paises> MostrarPaises()
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var listaPaises = new List<Paises>();
            var isoPaises = ObtenerPisesIso3166();
            listaPaises.AddRange(isoPaises.Select(p => new Paises
            {
                IconoUrl = $"https://hatscripts.github.io/circle-flags/flags/{p.TwoLetterISORegionName.ToLower()}.svg",
                Pais = p.EnglishName,
                SimboloMoneda = p.CurrencySymbol,
                CodigoPais = phoneNumberUtil.GetCountryCodeForRegion
                (p.TwoLetterISORegionName).ToString()
            }));
            return listaPaises;
        }

        public Paises MostrarPaisesPorNombre(string paisParametro)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var isoPaises = ObtenerPisesIso3166();
            var regionInfo = isoPaises.FirstOrDefault(p => p.EnglishName == paisParametro);
            var pais = new Paises();
            if (regionInfo != null)
            {
                pais.CodigoPais = phoneNumberUtil.GetCountryCodeForRegion(regionInfo.TwoLetterISORegionName).ToString();
                pais.Pais = regionInfo.EnglishName;
                pais.SimboloMoneda = regionInfo.CurrencySymbol;
                pais.IconoUrl = $"https://hatscripts.github.io/circle-flags/flags/{regionInfo.TwoLetterISORegionName.ToLower()}.svg";

                return pais;
            }
            else
            {
                pais.Pais = paisParametro;
                return pais;
            }
        }

        public List<Paises> ListaBusquedaPaisesPorNombre(string pais)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var listaPaises = new List<Paises>();
            var isoPaises = ObtenerPisesIso3166();
            var regionInfo = isoPaises.FirstOrDefault(p => p.EnglishName == pais);
            var paises = new Paises();
            if (regionInfo != null)
            {
                paises.CodigoPais = phoneNumberUtil.GetCountryCodeForRegion(regionInfo.TwoLetterISORegionName).ToString();
                paises.Pais = regionInfo.EnglishName;
                paises.SimboloMoneda = regionInfo.CurrencySymbol;
                paises.IconoUrl = $"https://hatscripts.github.io/circle-flags/flags/{regionInfo.TwoLetterISORegionName.ToLower()}.svg";


                listaPaises.Add(paises);
            }
            return listaPaises;
        }
    }
}
