using System;
using Bcp.Test.Domain.Entity;
using Bcp.Test.Domain.Interface;
using Bcp.Test.Infrastructure.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bcp.Test.Domain.Core
{
    public class TipoCambioDomain : ITipoCambioDomain
    {
        private readonly ITipoCambioRepository _tipocambioRepository;
        public TipoCambioDomain(ITipoCambioRepository tipocambioRepository)
        {
            _tipocambioRepository = tipocambioRepository;
        }

        public async Task<List<TipoCambioFijo>> ListarTipoCambio()
        {
            return await _tipocambioRepository.ListarTipoCambio();
        }

        public async Task<bool> InsertarTipoCambio(TipoCambioFijo tipoCambio)
        {
            return await _tipocambioRepository.InsertarTipoCambio(tipoCambio);
        }

        public async Task<bool> ActualizarTipoCambio(TipoCambioFijo tipoCambio)
        {
            return await _tipocambioRepository.ActualizarTipoCambio(tipoCambio);
        }

        public async Task<decimal> ObtenerTipoCambio(DateTime fecha, string monedaOrigen, string monedaDestino)
        {
            return await _tipocambioRepository.ObtenerTipoCambio(fecha, monedaOrigen, monedaDestino);
        }
    }
}
