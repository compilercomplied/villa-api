using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_business.Usecases.Dashboard
{
  public class DashboardResponse
  {

    [JsonProperty(PropertyName = "transactions")]
    public IEnumerable<QueryListTransaction> Transactions { get; set; }

  }

  public class QueryListTransaction
  { 

    [JsonProperty(PropertyName = "id")]
    public long ID { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "category")]
    public int Category { get; set; }

    [JsonProperty(PropertyName = "date")]
    public DateTime Date { get; set; }

  }

}
