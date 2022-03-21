using System;
using System.Collections.Generic;
using System.Text;
using Bcp.Test.Domain.Entity;
using System.Threading.Tasks;

namespace Bcp.Test.Infrastructure.Interface
{
    public interface ITipoCambioRepository
    {
        Task<bool> ActualizarTipoCambio(TipoCambioFijo tipoCambio);
        Task<decimal> ObtenerTipoCambio(DateTime fecha, string monedaOrigen, string monedaDestino);
    }
}
