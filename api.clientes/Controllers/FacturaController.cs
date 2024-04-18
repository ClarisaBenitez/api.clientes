using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using System.Diagnostics.Eventing.Reader;

namespace api.clientes.Controllers
{
    //[ApiController]
    //[Route("/Api/v1/")]


    public class FacturaController : Controller
    {

        private FacturaService facturaService;
        private FacturaRepository facturaRepository;

        public FacturaController(IConfiguration configuracion)
        {
            facturaService = new FacturaService(configuracion.GetConnectionString("postgres"));
            facturaRepository = new FacturaRepository(configuracion.GetConnectionString("postgres"));
        }

        //---------------------------------------------------------------------
        // GET: FacturaController/Create
        [HttpPost("Add")]
        public ActionResult Add(Repository.Data.FacturaModel factura)
        {
            try
            {
                if (facturaService.add(factura))
                    return Ok("Factura agregado correctamente");

                else
                    return BadRequest("Error al agregar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            // return Json(new { success = true }); 
        }

        // GET: FacturaController/Edit/5
        [HttpPost("Update")]
        public ActionResult Update(FacturaModel factura)
        {
            try
            {
                if (facturaRepository.update(factura))
                    return Ok("Factura actualizado correctamente");

                else
                    return BadRequest("Error al actualizar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //   return View();
        }



        // GET: FacturaController/Delete/5
        [HttpPut]
        [Route("remove")]

        public ActionResult remove(string nro_factura)
        {
            try
            {
                if (facturaRepository.remove(nro_factura))
                    return Ok("Factura eliminado correctamente");

                else
                    return BadRequest("Error al eliminar factura");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //  return View();
        }

        [HttpGet]
        [Route("get")]
        public ActionResult get(string nro_factura)
        {
            try
            {
                var factura = facturaRepository.get(nro_factura);
                if (factura != null)
                    return Ok(factura);
                else
                    return BadRequest("Factura no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //  return View();
        }


        [HttpGet]
        [Route("list")]
        public ActionResult list()
        {
            try
            {
                var facturas = facturaRepository.list();
                if (facturas != null)
                    return Ok(facturas);
                else
                    return BadRequest("No hay facturas registradas");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: FacturaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FacturaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FacturaController/Create


        public ActionResult Add()
        {
            return View();
        }

        // POST: FacturaController/Create
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

        // GET: FacturaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FacturaController/Edit/5
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

        // GET: FacturaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FacturaController/Delete/5
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
