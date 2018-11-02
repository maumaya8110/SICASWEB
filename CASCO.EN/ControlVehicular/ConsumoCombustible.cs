using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.ControlVehicular
{
    public class ConsumoCombustible:Item
    {
        public int NoVehiculo { get; set; }
        public string InformacionAdicional { get; set; }
        public string Conductor { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public double Litros { get; set; }
        public double Pesos { get; set; }
        public int KmInicial { get; set; }
        public int KmFinal { get; set; }
        public int KmRecorridos { get; set; }
        public double Rendimiento { get; set; }
        public int Servicios { get; set; }
        public double Ingresos { get; set; }
        public string Modelo { get; set; }
        public int ModeloUnidad_ID { get; set; }
    }
}
