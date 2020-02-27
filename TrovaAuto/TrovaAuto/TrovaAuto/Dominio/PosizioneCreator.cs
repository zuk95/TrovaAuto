using System;
using System.Collections.Generic;
using System.Text;
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

                Posizione p = new Posizione();
                p.Latitudine = location.Latitude;
                p.Longitudine = location.Longitude;
                p.DataAcquisizione = DateTime.Now;
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
