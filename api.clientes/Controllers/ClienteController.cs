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
    public class ClienteController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ClienteService _clienteService;
        private readonly IValidator<ClienteModel> _clienteValidator;

        public ClienteController(IConfiguration configuration, ClienteService clienteService, IValidator<ClienteModel> clienteValidator)
        {
            _configuration = configuration;
            _clienteService = clienteService;
            _clienteValidator = clienteValidator;
        }

        [HttpPost("AgregarCliente")]
        public async Task<IActionResult> Add(
            [FromQuery] int id,
            [FromQuery] int id_banco,
            [FromQuery] string nombre,
            [FromQuery] string apellido,
            [FromQuery] string documento,
            [FromQuery] string direccion,
            [FromQuery] string mail,
            [FromQuery] string celular,
            [FromQuery] string estado)
        {
            var cliente = new ClienteModel
            {
                id = id,
                id_banco = id_banco,
                nombre = nombre,
                apellido = apellido,
                documento = documento,
                direccion = direccion,
                mail = mail,
                celular = celular,
                estado = estado
            };

            ValidationResult validationResult = await _clienteValidator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                if (await _clienteService.Add(cliente))
                    return Ok("Cliente agregado correctamente");
                else
                    return BadRequest("Error al agregar cliente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ActualizarCliente")]
        public async Task<IActionResult> Update(
            [FromQuery] int id,
            [FromQuery] int id_banco,
            [FromQuery] string nombre,
            [FromQuery] string apellido,
            [FromQuery] string documento,
            [FromQuery] string direccion,
            [FromQuery] string mail,
            [FromQuery] string celular,
            [FromQuery] string estado)
        {
            var cliente = new ClienteModel
            {
                id = id,
                id_banco = id_banco,
                nombre = nombre,
                apellido = apellido,
                documento = documento,
                direccion = direccion,
                mail = mail,
                celular = celular,
                estado = estado
            };

            ValidationResult validationResult = await _clienteValidator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                if (await _clienteService.Update(cliente))
                    return Ok("Cliente actualizado correctamente");
                else
                    return BadRequest("Error al actualizar cliente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EliminarCliente")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if (await _clienteService.Remove(id))
                    return Ok("Cliente eliminado correctamente");
                else
                    return BadRequest("Error al eliminar cliente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConsultarCliente")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cliente = await _clienteService.Get(id);
                if (cliente != null)
                    return Ok(cliente);
                else
                    return NotFound("Cliente no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarClientes")]
        public async Task<IActionResult> List()
        {
            try
            {
                var clientes = await _clienteService.List();
                if (clientes != null)
                    return Ok(clientes);
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