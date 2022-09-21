using Domain.Contracts.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : BaseController
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            this._productoService = productoService;
        }

        [HttpPost("[action]")]
        public IActionResult GetAll(Paginacion p)
        {
            try
            {
                var Result = Success(_productoService.GetAll(p));
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return Ok(Fail(ex.Message));
            }
        }
    }
}
