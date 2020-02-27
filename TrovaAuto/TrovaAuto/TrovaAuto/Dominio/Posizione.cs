using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    public class Posizione
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public DateTime DataAcquisizione { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
        public byte[] byteImmagine { get; set; }

    }
}
