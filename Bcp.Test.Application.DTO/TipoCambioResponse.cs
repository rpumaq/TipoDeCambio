using System;
using System.Collections.Generic;
using System.Text;

namespace Bcp.Test.Application.DTO
{
    public class TipoCambioResponseDto
    {
        public decimal Monto { get; set; }
        public decimal MontoTipoCambio { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
