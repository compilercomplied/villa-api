using System;
using System.Collections.Generic;
using System.Text;

namespace domain_extensions.Core.Result
{
  public abstract class VillaErrorWrap 
  {
    public readonly string ErrorMessage;

    protected VillaErrorWrap(string error)
    {
      ErrorMessage = error;
    }

  }

  public class OperationException : Exception
  {

    public OperationException(VillaErrorWrap error) 
      : base (error.ErrorMessage)
    { 
    }

  }

}
