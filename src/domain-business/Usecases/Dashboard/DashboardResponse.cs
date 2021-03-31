using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace domain_business.Usecases.Dashboard
{
  public class DashboardResponse
  {
    [JsonProperty(PropertyName = "products")]
    public IEnumerable<DashboardProducts> Products { get; set; }

    [JsonProperty(PropertyName = "transactions")]
    public IEnumerable<QueryListTransaction> Transactions { get; set; }

  }

  public class DashboardProducts
  {

    [JsonProperty(PropertyName = "id")]
    public long ID { get; set; }

    [JsonProperty(PropertyName = "type")]
    public int Type { get; set; }

    [JsonProperty(PropertyName = "balance")]
    public decimal Balance { get; set; }

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
