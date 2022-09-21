using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Response<string> Fail(string mensaje, Error error)
        {
            return new Response<string>() { Data = "", Mensaje = mensaje, Success = false, Excepcion = error };
        }
        protected Response<string> Fail(string mensaje)
        {
            return new Response<string>() { Data = "", Mensaje = mensaje, Success = false };
        }
        protected Response<string> Success(string mensaje)
        {
            return Retorno<string>("", mensaje);
        }
        protected Response<T> Success<T>(T data, string mensaje = "", bool success = true)
        {
            return Retorno<T>(data, mensaje);
        }
        protected Response<T> Fail<T>(T data, Error error)
        {
            return Retorno<T>(data, "Error no Controlado", false, error);
        }
        private Response<T> Retorno<T>(T data, string mensaje, bool success = true, Error error = null)
        {
            return new Response<T>() { Data = data, Mensaje = mensaje, Success = success, Excepcion = error };
        }
    }
}
