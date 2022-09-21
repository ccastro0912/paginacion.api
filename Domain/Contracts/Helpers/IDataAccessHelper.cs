using Newtonsoft.Json.Linq;
using System;

namespace Domain.Contracts.Helpers
{
    public interface IDataAccessHelper : IDisposable
    {
        void EjecutarProcedimiento(string Procedimiento, string ParametroJson = "");
        JToken JsonObject(string Procedimiento, string ParametroJson = "");
        JArray JsonArray(string Procedimiento, string ParametroJson = "");
    }
}
