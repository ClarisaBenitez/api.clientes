using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace api.clientes.Controllers
{
    [ApiController]
    [Route("[controller]")]



    public class FacturaController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly FacturaService _facturaService;

        public FacturaController(IConfiguration configuration, FacturaService facturaService)
        {
            _configuration = configuration;
            _facturaService = facturaService;
        }


        //---------------------------------------------------------------------
        // GET: FacturaController/Create
        [HttpPost("AgregarFactura")]
        public async Task<IActionResult> Add(
            [FromQuery] int id,
            [FromQuery] int id_cliente,
            [FromQuery] string nro_factura,
            [FromQuery] string fecha_hora,
            [FromQuery] int total,
            [FromQuery] int total_iva5,
            [FromQuery] int total_iva10,
            [FromQuery] int total_iva,
            [FromQuery] string total_letras,
            [FromQuery] string sucursal)
        {
            if (!EsNumeroFacturaValido(nro_factura))
            {
                return BadRequest("El número de factura no es válido. Debe seguir el patrón: 3 números, guion, 3 números, guion, 6 números.");
            }

            if (total <= 0 || total_iva5 < 0 || total_iva10 < 0 || total_iva < 0)
            {
                return BadRequest("Total, Total_iva5, Total_iva10 y Total_iva son obligatorios y deben ser valores numéricos positivos.");
            }

            if (string.IsNullOrWhiteSpace(total_letras) || total_letras.Length < 6)
            {
                return BadRequest("Error al agregar factura: El campo 'Total en letras' es obligatorio y debe tener al menos 6 caracteres.");
            }

            var factura = new FacturaModel
            {
                id = id,
                id_cliente = id_cliente,
                nro_factura = nro_factura,
                fecha_hora = fecha_hora,
                total = total,
                total_iva5 = total_iva5,
                total_iva10 = total_iva10,
                total_iva = total_iva,
                total_letras = total_letras,
                sucursal = sucursal
            };

            try
            {
                if (await _facturaService.Add(factura))
                    return Ok("Factura agregada correctamente");
                else
                    return BadRequest("Error al agregar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: FacturaController/Edit/5
        [HttpPut("ActualizarFactura")]
        public async Task<IActionResult> Update(
            [FromQuery] int id,
            [FromQuery] int id_cliente,
            [FromQuery] string nro_factura,
            [FromQuery] string fecha_hora,
            [FromQuery] int total,
            [FromQuery] int total_iva5,
            [FromQuery] int total_iva10,
            [FromQuery] int total_iva,
            [FromQuery] string total_letras,
            [FromQuery] string sucursal)
        {
            if (!EsNumeroFacturaValido(nro_factura))
            {
                return BadRequest("El número de factura no es válido. Debe seguir el patrón: 3 números, guion, 3 números, guion, 6 números.");
            }

            if (total <= 0 || total_iva5 < 0 || total_iva10 < 0 || total_iva < 0)
            {
                return BadRequest("Total, Total_iva5, Total_iva10 y Total_iva son obligatorios y deben ser valores numéricos positivos.");
            }

            if (string.IsNullOrWhiteSpace(total_letras) || total_letras.Length < 6)
            {
                return BadRequest("Error al actualizar factura: El campo 'Total en letras' es obligatorio y debe tener al menos 6 caracteres.");
            }

            var factura = new FacturaModel
            {
                id = id,
                id_cliente = id_cliente,
                nro_factura = nro_factura,
                fecha_hora = fecha_hora,
                total = total,
                total_iva5 = total_iva5,
                total_iva10 = total_iva10,
                total_iva = total_iva,
                total_letras = total_letras,
                sucursal = sucursal
            };

            try
            {
                if (await _facturaService.Update(factura))
                    return Ok("Factura actualizada correctamente");
                else
                    return BadRequest("Error al actualizar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: FacturaController/Delete/5
        [HttpDelete]
        [Route("EliminarFactura")]

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if (await _facturaService.Remove(id))
                    return Ok("Factura eliminado correctamente");
                else
                    return BadRequest("Error al eliminar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarFactura")]
            public async Task<IActionResult> Get(int id)
            {
                try
                {
                    var factura = await _facturaService.Get(id);
                    if (factura != null)
                        return Ok(factura);
                    else
                        return NotFound("Factura no encontrado");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        [HttpGet]
        [Route("ListarFactura")]
        public async Task<IActionResult> List()
        {
            try
            {
                var facturas = await _facturaService.List();
                if (facturas != null)
                    return Ok(facturas);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool EsNumeroFacturaValido(string nro_factura)
        {
            string patron = @"^\d{3}-\d{3}-\d{6}$";
            return Regex.IsMatch(nro_factura, patron);
        }

        private bool EsNumero(string valor)
        {
            foreach (char c in valor)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}

