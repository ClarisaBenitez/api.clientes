using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using System.Diagnostics.Eventing.Reader;

namespace api.clientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
    
        private readonly IConfiguration _configuration;
        private readonly ClienteService _clienteService;



        public ClienteController(IConfiguration configuration, ClienteService clienteService)
        {
            _configuration = configuration;
            _clienteService = clienteService;
        }


        // GET: ClienteController/Create
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
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3 ||
                string.IsNullOrWhiteSpace(apellido) || apellido.Length < 3 ||
                string.IsNullOrWhiteSpace(documento) || documento.Length < 3)
            {
                return BadRequest("Error al agregar cliente: Asegurese de que el nombre, apellido y documento tengan al menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(celular) || celular.Length != 10 || !EsNumero(celular))
            {
                return BadRequest("Error al agregar cliente: El celular debe ser un número de 10 dígitos.");
            }

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


        // GET: ClienteController/Edit/5
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
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3 ||
                string.IsNullOrWhiteSpace(apellido) || apellido.Length < 3 ||
                string.IsNullOrWhiteSpace(documento) || documento.Length < 3)
            {
                return BadRequest("Error al actualizar cliente: Asegurese de que el nombre, apellido y documento tengan tener al menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(celular) || celular.Length != 10 || !EsNumero(celular))
            {
                return BadRequest("Error al actualizar cliente: El celular debe ser un número de 10 dígitos.");
            }

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



        // GET: ClienteController/Delete/5
        [HttpDelete]
        [Route("EliminarCliente/{id}")]

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

        [HttpGet]
        [Route("ConsultarCliente")]
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

        [HttpGet]
        [Route("ListarCliente")]
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
        private bool EsNumero(string celular)
        {
            foreach (char c in celular)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }



    }
}
