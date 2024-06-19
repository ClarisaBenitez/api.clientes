using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace api.clientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FacturaService _facturaService;
        private readonly IValidator<FacturaModel> _facturaValidator;

        public FacturaController(IConfiguration configuration, FacturaService facturaService, IValidator<FacturaModel> facturaValidator)
        {
            _configuration = configuration;
            _facturaService = facturaService;
            _facturaValidator = facturaValidator;
        }

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

            ValidationResult validationResult = await _facturaValidator.ValidateAsync(factura);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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

            ValidationResult validationResult = await _facturaValidator.ValidateAsync(factura);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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

        [HttpDelete("EliminarFactura")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if (await _facturaService.Remove(id))
                    return Ok("Factura eliminada correctamente");
                else
                    return BadRequest("Error al eliminar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConsultarFactura")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var factura = await _facturaService.Get(id);
                if (factura != null)
                    return Ok(factura);
                else
                    return NotFound("Factura no encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarFactura")]
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
    }
}