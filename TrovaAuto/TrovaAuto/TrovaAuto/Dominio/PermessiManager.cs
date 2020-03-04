using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace TrovaAuto.Dominio
{
    public class PermessiManager : IPermessiManager
    {
        private Permission[] permessi = new Permission[]
            {
                Permission.Camera,
                Permission.Location,
                Permission.Storage
            };

        /// <summary>
        /// Metodo che chiede i permessi all'utente e ritorna una lista di permessi non consentiti.
        /// In caso i permessi siano tutti consentiti, ritorna una lista vuota
        /// </summary>
        /// <returns></returns>
        public async Task<List<Permission>> ChiediPermessi()
        {
            try
            {
                List<Permission> permessiNonConsentiti = new List<Permission>();
                Dictionary<Permission, PermissionStatus> permessiRisposte = await CrossPermissions.Current.RequestPermissionsAsync(permessi);
                foreach (Permission permesso in permessiRisposte.Keys)
                {
                    if (permessiRisposte[permesso] == PermissionStatus.Denied || permessiRisposte[permesso] == PermissionStatus.Disabled || permessiRisposte[permesso] == PermissionStatus.Unknown)
                    {
                        permessiNonConsentiti.Add(permesso);
                    }
                }
                return permessiNonConsentiti;
            }
            catch (Exception ex)
            {
                throw new Exception($"Problema con gestione dei permessi: {ex.Message}", ex);
            }
        }

        public string GetMessaggioForPermesso(Permission permesso)
        {
            switch (permesso)
            {
                case Permission.Camera: return "La camera è utilizzata dall'app per scattare delle foto in fase di acquisizione della posizione. Vuoi aprire le impostazioni per consentirlo?";
                case Permission.Location: return "La posizione è utilizzata per salvare la posizione a una tua richiesta e visualizzare la mappa di dove si trova la tua auto. Vuoi aprire le impostazioni per consentirlo?";
                case Permission.Storage: return "La memoria del dispositivo è utilizzata per salvare le foto che scatti in fase di acquisizione della posizione e per mandarti delle notifiche allo scadere dei timer che imposti. Vuoi aprire le impostazioni per consentirlo?";
                default: return permesso.ToString() + " non necessario";
            }
        }

        public bool ApriImpostazioni()
        {
            return CrossPermissions.Current.OpenAppSettings();
        }
    }
}
