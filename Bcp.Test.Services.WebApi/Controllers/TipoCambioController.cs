using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bcp.Test.Application.DTO;
using Bcp.Test.Application.Interface;
using System.Threading.Tasks;
using Bcp.Test.Transversal.Common;
using System.Collections.Generic;

namespace Bcp.Test.Services.WebApi.Controllers
{
    /// <summary>
    /// Tipo de cambio
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCambioController : Controller
    {
        private readonly ITipoCambioApplication _tipoCambioApplication;
        /// <summary>
        /// Tipo de cambio
        /// </summary>
        /// <param name="tipoCambioApplication"></param>
        public TipoCambioController(ITipoCambioApplication tipoCambioApplication)
        {
            _tipoCambioApplication = tipoCambioApplication;
        }

        /// <summary>
        /// Listo los Tipos de cambio registrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarTipoCambio")]
        public async Task<IActionResult> ListarTipoCambio()
        {
            var response = await _tipoCambioApplication.ListarTipoCambio();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Actualiza tipo de cambio
        /// </summary>
        /// <param name="tipoCambioDto">Objeto tipo de cambio</param>
        /// <returns></returns>
        [HttpPost("InsertarTipoCambio")]
        public async Task<IActionResult> InsertarTipoCambio([FromBody] TipoCambioFijoDto tipoCambioDto)
        {
            if (tipoCambioDto == null)
                return BadRequest();
            var response = await _tipoCambioApplication.InsertarTipoCambio(tipoCambioDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Actualiza tipo de cambio
        /// </summary>
        /// <param name="tipoCambioDto">Objeto tipo de cambio</param>
        /// <returns></returns>
        [HttpPut("ActualizarTipoCambio")]
        public async Task<IActionResult> ActualizarTipoCambio([FromBody] TipoCambioFijoDto tipoCambioDto)
        {
            if (tipoCambioDto == null)
                return BadRequest();
            var response = await _tipoCambioApplication.ActualizarTipoCambio(tipoCambioDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Calcula el tipo de cambio
        /// </summary>
        /// <param name="monto">Monto a convertir</param>
        /// <param name="monedaOrigen">Moneda origen</param>
        /// <param name="monedaDestino">Moneda destino</param>
        /// <returns></returns>
        [HttpGet("ConvertirTipoCambio/{monto}/{monedaOrigen}/{monedaDestino}")]
        public async Task<ActionResult<Response<TipoCambioResponseDto>>> ConvertirTipoCambio(decimal monto, string monedaOrigen, string monedaDestino)
        {
            var response = await _tipoCambioApplication.ConvertirTipoCambio(monto, monedaOrigen, monedaDestino);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}