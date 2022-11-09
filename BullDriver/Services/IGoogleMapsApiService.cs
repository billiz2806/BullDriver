using BullDriver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BullDriver.Services
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> ApiPlaces(string text);
        Task<GooglePlace> ApiPlacesDetails(string placeId);
    }
}
