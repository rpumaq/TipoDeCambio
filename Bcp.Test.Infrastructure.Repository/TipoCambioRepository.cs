using System;
using Bcp.Test.Domain.Entity;
using Bcp.Test.Infrastructure.Interface;
using Bcp.Test.Transversal.Common;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;

namespace Bcp.Test.Infrastructure.Repository
{
    public class TipoCambioRepository : ITipoCambioRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public readonly IDistributedCache _cache;
        public TipoCambioRepository(IConnectionFactory connectionFactory, IDistributedCache cache)
        {
            _connectionFactory = connectionFactory;
            _cache = cache;
        }

        public async Task<bool> ActualizarTipoCambio(TipoCambioFijo tipoCambio)
        {
            TipoCambioFijo[] tipoCambioResult = await _cache.GetCacheValueAsync<TipoCambioFijo[]>("tipoCambioResult");
            if (tipoCambioResult != null)
            {
                var tipoCambioObj = (from x in tipoCambioResult where x.Fecha == tipoCambio.Fecha && x.MonedaOrigen == tipoCambio.MonedaOrigen && x.MonedaDestino == tipoCambio.MonedaDestino select x).FirstOrDefault();

                if (tipoCambioObj != null)
                {
                    tipoCambioObj.TipoDeCambio = tipoCambio.TipoDeCambio;
                    await _cache.SetCacheValueAsync("tipoCambioResult", tipoCambioResult);
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<decimal> ObtenerTipoCambio(DateTime fecha, string monedaOrigen, string monedaDestino)
        {
            // Check if content exists in cache
            TipoCambioFijo[] tipoCambioResult = await _cache.GetCacheValueAsync<TipoCambioFijo[]>("tipoCambioResult");
            if (tipoCambioResult != null)
            {
                var tipoCambio = (from x in tipoCambioResult where x.Fecha == fecha && x.MonedaOrigen == monedaOrigen && x.MonedaDestino == monedaDestino select x).FirstOrDefault();

                if (tipoCambio != null)
                {
                    return tipoCambio.TipoDeCambio;
                }
            }

            List<TipoCambioFijo> listaTipoCambio = new List<TipoCambioFijo>();
            listaTipoCambio.Add(new TipoCambioFijo
            {
                Fecha = DateTime.Now.Date,
                MonedaOrigen = monedaOrigen,
                MonedaDestino = monedaDestino,
                TipoDeCambio = 3.70m
            });

            tipoCambioResult = listaTipoCambio.ToArray();

            await _cache.SetCacheValueAsync("tipoCambioResult", tipoCambioResult);
            return tipoCambioResult.First().TipoDeCambio;
        }
    }
}
