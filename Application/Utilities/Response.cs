using System;
using System.Net;

namespace Application.Utilities
{
  public class Response<T>
  {
    public Response(HttpStatusCode statusCode, string message, bool success, T data = default!,
      Exception exception = null!)
    {
      StatusCode = statusCode;
      Message = message;
      Success = success;
      Data = data;
      ExceptionMessage = exception?.Message;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
    public string? ExceptionMessage { get; set; }
  }
}