using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_business.Usecases.Dashboard
{

  public class TransactionListingRequest
  {

    [JsonProperty(PropertyName = "SkipCount")]
    public int SkipCount { get; set; }

  }

}
