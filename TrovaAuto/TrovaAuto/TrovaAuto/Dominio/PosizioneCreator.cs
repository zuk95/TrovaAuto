using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TrovaAuto.Dominio
{
    public static class PosizioneCreator
    {
        public async static Task<Posizione> AcquisisciPosizione()
        {
            try
            {
                GeolocationRequest geoRequestOptions = new GeolocationRequest(GeolocationAccuracy.Best,TimeSpan.FromSeconds(10));
                Location location = await Geolocation.GetLocationAsync(geoRequestOptions);

                Posizione p = new Posizione
                {
                    Latitudine = location.Latitude,
                    Longitudine = location.Longitude,
                    DataAcquisizione = DateTime.Now
                };
                var placemarks = await Geocoding.GetPlacemarksAsync(p.Latitudine, p.Longitudine);
                var placemark = placemarks?.FirstOrDefault();
                if(placemark != null)
                {
                    p.NomeCitta = placemark.Locality;
                    p.Via = placemark.Thoroughfare + " " + placemark.SubThoroughfare;
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
