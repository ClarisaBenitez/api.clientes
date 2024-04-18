using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using System.Diagnostics.Eventing.Reader;

namespace api.clientes.Controllers
{
    //[ApiController]
    //[Route("/Api/v1/")]
    public class ClienteController : Controller
    {
        private ClienteService clienteService;
        private ClienteRepository clienteRepository;

        public ClienteController(IConfiguration configuracion)
        {
            clienteService = new ClienteService(configuracion.GetConnectionString("postgres"));
            clienteRepository = new ClienteRepository(configuracion.GetConnectionString("postgres"));
        }



        // GET: ClienteController/Create
        [HttpPost("AGREGAR")]
        public ActionResult Add(Repository.Data.ClienteModel cliente)
        {
            try
            {
                if (clienteService.add(cliente))
            return Ok("Cliente agregado correctamente");
                else
                    return BadRequest("Error al agregar cliente: Asegúrese de que el nombre, apellido y documento tengan al menos 3 caracteres.");

            //else ojito
              //  return BadRequest("Error al agregar cliente"); ojito
        }
        catch (Exception ex)
            {
                return BadRequest($"Error al actualizar cliente: {ex.Message}");
               // return BadRequest(ex.Message); ojito
    }


    // return Json(new { success = true }); 
}

// POST: ClienteController/Create
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        [HttpPost("Actualizar")]
        public ActionResult update(ClienteModel cliente)
        {
            try
            {
                if (clienteRepository.update(cliente))
                    return Ok("Cliente actualizado correctamente");

                else
                    return BadRequest("Error al actualizar cliente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


         //   return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        [HttpPut]
        [Route("ELIMINAR")]

        public ActionResult remove(string documento)
        {
            try
            {
                if (clienteRepository.remove(documento))
                    return Ok("Cliente eliminado correctamente");

                else
                    return BadRequest("Error al eliminar cliente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //  return View();
        }

        [HttpGet]
        [Route("CONSULTAR")]
        public ActionResult get(string documento)
        {
            try
            {
                var cliente = clienteRepository.get(documento);
                if (cliente != null)
                    return Ok(cliente);
                else
                    return BadRequest("Cliente no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //  return View();
        }

        [HttpGet]
        [Route("LISTA")]
        public ActionResult list()
        {
            try
            {
                var clientes = clienteRepository.list();
                if (clientes != null)
                    return Ok(clientes);
                else
                    return BadRequest("No hay clientes registrados");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
