using System;
using AutoMapper;
using Bcp.Test.Application.DTO;
using Bcp.Test.Application.Interface;
using Bcp.Test.Domain.Entity;
using Bcp.Test.Domain.Interface;
using Bcp.Test.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bcp.Test.Application.Main
{
    public class TipoCambioApplication : ITipoCambioApplication
    {
        private readonly ITipoCambioDomain _tipocambioDomain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<TipoCambioApplication> _logger;
        public TipoCambioApplication(ITipoCambioDomain tipocambioDomain, IMapper mapper, IAppLogger<TipoCambioApplication> logger)
        {
            _tipocambioDomain = tipocambioDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<List<TipoCambioFijoDto>>> ListarTipoCambio()
        {
            var response = new Response<List<TipoCambioFijoDto>>();
            try
            {
                List<TipoCambioFijo> ltc  = await _tipocambioDomain.ListarTipoCambio();
                List<TipoCambioFijoDto> ltcr = new List<TipoCambioFijoDto>();
                foreach (var item in ltc)
                {
                    TipoCambioFijoDto tc = new TipoCambioFijoDto()
                    {
                        Fecha = item.Fecha,
                        MonedaDestino = item.MonedaDestino,
                        MonedaOrigen = item.MonedaOrigen,
                        TipoDeCambio =item.TipoDeCambio
                    };
                    ltcr.Add(tc);
                }
                response.IsSuccess = true;
                response.Data = ltcr;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }


        public async Task<Response<bool>> InsertarTipoCambio(TipoCambioFijoDto tipoCambioDto)
        {
            var response = new Response<bool>();
            try
            {
                var tipoCambio = _mapper.Map<TipoCambioFijo>(tipoCambioDto);
                response.Data = await _tipocambioDomain.InsertarTipoCambio(tipoCambio);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Tipo de cambio ya se encuentra registrado";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarTipoCambio(TipoCambioFijoDto tipoCambioDto)
        {
            var response = new Response<bool>();
            try
            {
                var tipoCambio = _mapper.Map<TipoCambioFijo>(tipoCambioDto);
                response.Data = await _tipocambioDomain.ActualizarTipoCambio(tipoCambio);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<TipoCambioResponseDto>> ConvertirTipoCambio(decimal monto, string monedaOrigen, string monedaDestino)
        {
            var response = new Response<TipoCambioResponseDto>();
            try
            {
                var validacion = ValidarRequest(monto, monedaOrigen, monedaDestino);
                if (validacion.IsSuccess)
                {
                    TipoCambioResponseDto tipoCambioData = new TipoCambioResponseDto() { MonedaOrigen = monedaOrigen, MonedaDestino = monedaDestino, Monto = monto };

                    tipoCambioData.TipoDeCambio = await _tipocambioDomain.ObtenerTipoCambio(DateTime.Now.Date, monedaOrigen, monedaDestino);

                    if (tipoCambioData.TipoDeCambio >= 0)
                    {
                        if (monedaOrigen == Constantes.Moneda_Soles)
                        {
                            tipoCambioData.MontoTipoCambio = decimal.Round(monto / tipoCambioData.TipoDeCambio, 2);
                        }
                        else//Dolares (USD)
                        {
                            tipoCambioData.MontoTipoCambio = decimal.Round(monto * tipoCambioData.TipoDeCambio, 2);
                        }

                        response.Data = tipoCambioData;
                        response.IsSuccess = true;
                        response.Message = "Consulta Exitosa";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Tipo de cambio no resgistrado";
                    }

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = validacion.Message;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        private Validacion ValidarRequest(decimal monto, string monedaOrigen, string monedaDestino)
        {
            List<string> monedaValida = new List<string>();
            monedaValida.Add(Constantes.Moneda_Soles);
            monedaValida.Add(Constantes.Moneda_Dolares);

            Validacion respuesta = new Validacion();

            if (monto < 0)
            {
                respuesta.IsSuccess = false;
                respuesta.Message = "El monto debe ser mayor a cero";
            }
            if (monedaOrigen == monedaDestino)
            {
                respuesta.IsSuccess = false;
                respuesta.Message = "La moneda origen y destino no pueden ser las mismas";
            }
            if (!monedaValida.Contains(monedaOrigen))
            {
                respuesta.IsSuccess = false;
                respuesta.Message = "La moneda origen es invalida";
            }

            if (!monedaValida.Contains(monedaDestino))
            {
                respuesta.IsSuccess = false;
                respuesta.Message = "La moneda destino es invalida";
            }

            return respuesta;
        }
    }
}
