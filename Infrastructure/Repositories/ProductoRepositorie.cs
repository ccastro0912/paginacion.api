using Domain.Contracts.Helpers;
using Domain.Contracts.Repositories;
using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class ProductoRepositorie : IProductoRepositorie
    {
        private readonly IDataAccessHelper _helper;
        public ProductoRepositorie(IDataAccessHelper helper)
        {
            _helper = helper;
        }
        public ProductoPaginacion GetAll(Paginacion p)
        {
            JToken jToken = this._helper.JsonObject("pListarProducto", JsonConvert.SerializeObject(p));
            if (jToken.HasValues) return JsonConvert.DeserializeObject<ProductoPaginacion>(jToken.ToString());
            else throw new Exception("Error ocurrido al obtener productos paginados.");
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
