using System;
using System.Collections.Generic;
using System.Text;
using Bcp.Test.Domain.Entity;
using System.Threading.Tasks;

namespace Bcp.Test.Domain.Interface
{
    public interface ITipoCambioDomain
    {
        Task<bool> ActualizarTipoCambio(TipoCambioFijo tipoCambio);
        Task<decimal> ObtenerTipoCambio(DateTime fecha, string monedaOrigen, string monedaDestino);
    }
}
