using System;
using System.Collections.Generic;
using System.Text;

namespace Bcp.Test.Domain.Entity
{
    public class TipoCambioFijo
    {
        public DateTime Fecha { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
