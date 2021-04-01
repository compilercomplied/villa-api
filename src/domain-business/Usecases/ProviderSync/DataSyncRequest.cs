using domain_business.Usecases.Auth_flow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace domain_business.Usecases.ProviderSync
{

  public class DataSyncRequest
  {

    [JsonProperty("master")]
    public bool Master { get; set; } = false;

    [JsonProperty("from")]
    public DateTime From { get; set; } = DateTime.Today;

  }

}

