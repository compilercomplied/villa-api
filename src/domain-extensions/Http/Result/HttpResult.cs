
using domain_extensions.Core.Result;
using System.Net;

#nullable disable
namespace domain_extensions.Http.Result
{
  public class HttpResult<T, U>
  {

    public readonly T Value;
    public readonly HttpError<U> Error;

    HttpResult(T value, HttpError<U> err)
    {
      Value = value;
      Error = err;
    }

    public bool IsSuccess => Error == default(HttpError<U>);

    public T Unwrap() => IsSuccess ? Value : throw new OperationException(Error);

    // --- Builders ------------------------------------------------------------
    public static HttpResult<T, U> OK(T value) => 
      new HttpResult<T, U>(value, default);

    public static HttpResult<T, U> FAIL(HttpError<U> error) =>
      new HttpResult<T, U>(default, error);

    public static HttpResult<T, U> FAIL(U error, HttpStatusCode code) =>
      new HttpResult<T, U>(default, HttpError<U>.FromRequest(error, code));

  }

}
