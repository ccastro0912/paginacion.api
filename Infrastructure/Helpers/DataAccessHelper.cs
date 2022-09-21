using Domain.Contracts.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Helpers
{
    public class DataAccessHelper : IDataAccessHelper
    {
        private readonly string ConnectionString = "";
        private readonly IConfiguration _configuration;

        public DataAccessHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("connectionString");
        }

        public void EjecutarProcedimiento(string Procedimiento, string ParametroJson = "")
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sql = new SqlCommand(Procedimiento, cn) { CommandType = CommandType.StoredProcedure})
                {
                    if (!ParametroJson.Equals(String.Empty)) sql.Parameters.Add("@ParametroJSON", SqlDbType.NVarChar).Value = ParametroJson;

                    cn.Open();
                    sql.ExecuteNonQuery();

                    return;
                }
            }
        }

        public JArray JsonArray(string Procedimiento, string ParametroJson = "")
        {
            try
            {
                JArray jArray = new JArray();
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sql = new SqlCommand(Procedimiento, cn) { CommandType = CommandType.StoredProcedure })
                    {
                        if (!ParametroJson.Equals(String.Empty)) sql.Parameters.Add("@ParametroJSON", SqlDbType.NVarChar).Value = ParametroJson;

                        cn.Open();
                        SqlDataReader reader = sql.ExecuteReader();

                        while (reader.Read())
                        {
                            string result = reader.GetValue(0) == DBNull.Value ? "" : (string)reader.GetValue(0);
                            if (result.Length == 0) continue;
                            jArray = JsonConvert.DeserializeObject<JArray>(result.ToString());
                        }
                    }
                }
                return jArray;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JToken JsonObject(string Procedimiento, string ParametroJson = "")
        {
            try
            {
                JToken jToken = new JObject();
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sql = new SqlCommand(Procedimiento, cn) { CommandType = CommandType.StoredProcedure })
                    {
                        if (!ParametroJson.Equals(String.Empty)) sql.Parameters.Add("@ParametroJSON", SqlDbType.NVarChar).Value = ParametroJson;

                        cn.Open();
                        SqlDataReader reader = sql.ExecuteReader();

                        while (reader.Read())
                        {
                            string result = reader.GetValue(0) == DBNull.Value ? "" : (string)reader.GetValue(0);
                            if (result.Length == 0) continue;
                            jToken = JsonConvert.DeserializeObject<JToken>(result.ToString());
                        }
                    }
                }
                return jToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
