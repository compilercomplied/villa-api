using System;
using System.Collections.Generic;
using System.Text;

namespace domain_extensions.Core.Result
{

  public class Error : VillaErrorWrap
  {

    public Error(string error)
      : base(error?.ToString() ?? string.Empty)
    {
    }

  }

}
