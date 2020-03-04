using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;


namespace TrovaAuto.Dominio
{
    public interface IPermessiManager
    {
        Task<List<Permission>> ChiediPermessi();
        string GetMessaggioForPermesso(Permission permesso);
        bool ApriImpostazioni();
    }
}
