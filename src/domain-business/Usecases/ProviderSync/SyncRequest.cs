using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace domain_business.Usecases.ProviderSync
{

  public class SyncRequest
  {

    [JsonProperty("init")]
    public bool Init { get; set; } = false;
    [JsonProperty("period")]
    public SyncPeriod Period { get; set; } = SyncPeriod.Yesterday;

  }

  public enum SyncPeriod
  { 

    [Display(Name = "today")]
    Today = 0,
    [Display(Name = "yesterday")]
    Yesterday = 1,

  }

}
