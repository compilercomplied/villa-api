using domain_extensions.Core.Result;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace domain_extensions.Http.Result
{

  public class HttpError<T> : VillaErrorWrap
  {

    public readonly HttpStatusCode StatusCode;

    protected HttpError(T error, HttpStatusCode statusCode)
      : base(error?.ToString() ?? string.Empty)
    {
      StatusCode = statusCode;
    }


    // --- Builders ------------------------------------------------------------
    public static HttpError<T> FromRequest(T error, HttpStatusCode statusCode) =>
      new HttpError<T>(error, statusCode);

  }

}
