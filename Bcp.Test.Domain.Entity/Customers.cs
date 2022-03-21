namespace Bcp.Test.Domain.Entity
{
    public class TipoCambioResponse
    {
        public decimal Monto { get; set; }
        public decimal MontoTipoCambio { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
