using System;
using System.Collections.Generic;
using System.Text;
using Bcp.Test.Application.DTO;
using Bcp.Test.Transversal.Common;
using System.Threading.Tasks;

namespace Bcp.Test.Application.Interface
{
    public interface ITipoCambioApplication
    {
        Task<Response<bool>> ActualizarTipoCambio(TipoCambioFijoDto tipoCambioDto);
        Task<Response<TipoCambioResponseDto>> ConvertirTipoCambio(decimal monto, string monedaOrigen, string monedaDestino);
    }
}
