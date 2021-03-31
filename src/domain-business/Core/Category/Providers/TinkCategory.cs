using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace domain_business.Core.Category.Providers
{
  public class TinkCategory
  {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("defaultChild")]
    public bool DefaultChild { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("parent")]
    public string Parent { get; set; }

    [JsonProperty("primaryName")]
    public string PrimaryName { get; set; }

    [JsonProperty("searchTerms")]
    public string SearchTerms { get; set; }

    [JsonProperty("secondaryName")]
    public string SecondaryName { get; set; }

    [JsonProperty("sortOrder")]
    public long SortOrder { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("typeName")]
    public string TypeName { get; set; }
  }
}
