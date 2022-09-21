using Domain.Contracts.Helpers;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Models;
using Domain.Services;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using Xunit;

namespace Test
{
    public class ProductTest
    {

        [Fact]
        public void TestMethod ()
        {
            string procedimiento = "pListarProducto";
            Paginacion p = new Paginacion { NumeroPagina = 1, RegistrosPagina = 10, NumeroPaginas = 0, Filter = 0 };

            Mock<IDataAccessHelper> helper = new Mock<IDataAccessHelper>();
            helper.Setup(a => a.JsonObject(procedimiento, JsonConvert.SerializeObject(p)));

            Mock<IProductoRepositorie> repositorie = new Mock<IProductoRepositorie>();
            repositorie.Setup(a => a.GetAll(p)).Returns(new ProductoPaginacion
            {
                Paginacion = new Paginacion
                {
                    NumeroPagina = p.NumeroPagina,
                    RegistrosPagina = p.RegistrosPagina,
                    NumeroPaginas = p.NumeroPaginas,
                    Filter = p.Filter
                },
                Producto = new List<Producto>()
            });

            ProductoService service = new ProductoService(repositorie.Object);
            ProductoPaginacion pp = service.GetAll(p);

            Assert.Equal(p.NumeroPaginas, pp.Paginacion.NumeroPaginas);
        }
    }
}
