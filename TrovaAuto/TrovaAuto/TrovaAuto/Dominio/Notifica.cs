using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    public class Notifica
    {
        public int IdNotifica { get; set; }
        public string Titolo { get { return CostantiDominio.TITOLO_NOTIFICA; } }
        public string Messaggio { get; set; }
        public DateTime DataNotifica { get; set; }

        public Notifica(Posizione _posizioneAssociata)
        {
            IdNotifica = _posizioneAssociata.Id;
            Messaggio = "Accedi all'app e trova la tua auto"; //a " + _posizioneAssociata.NomeCitta + " in " + _posizioneAssociata.Via;
        }
    }
}
