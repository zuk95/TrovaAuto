using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    public class Impostazioni
    {
        [PrimaryKey]
        public int IdImpostazione { get; set; }
        public int NumeroAcquisizioniMassimo { get; set; }
        //public bool AcquisizioneConTimer { get; set; }

    }
}
