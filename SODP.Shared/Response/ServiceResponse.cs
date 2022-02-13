using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SODP.Shared.Response
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public int StatusCode { get; set; } = StatusCodes.Status204NoContent;
        public IDictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();


        public void SetError(string message)
        {
            SetError(message, StatusCodes.Status500InternalServerError);
        }

        public void SetError(string message, int statusCode)
        {
            Success = false;
            Message += message;
            StatusCode = statusCode;
        }

    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
        public void SetData(T data)
        {
            Data = data;
            StatusCode = StatusCodes.Status200OK;
        }
    }

}
