using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_business.Core.Account.Providers
{

  // --- API Response ----------------------------------------------------------

  public partial class TinkAccountResponse
  { 
    [JsonProperty("accounts")]
    public TinkAccount[] Accounts { get; set; }
  }

  // --- API Models ------------------------------------------------------------
  public partial class TinkAccount
  {
    [JsonProperty("accountExclusion")]
    public string AccountExclusion { get; set; }

    [JsonProperty("accountNumber")]
    public string AccountNumber { get; set; }

    [JsonProperty("balance")]
    public double Balance { get; set; }

    [JsonProperty("closed")]
    public bool Closed { get; set; }

    [JsonProperty("credentialsId")]
    public string CredentialsId { get; set; }

    [JsonProperty("currencyDenominatedBalance")]
    public CurrencyDenominatedBalance CurrencyDenominatedBalance { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }

    [JsonProperty("excluded")]
    public bool Excluded { get; set; }

    [JsonProperty("favored")]
    public bool Favored { get; set; }

    [JsonProperty("financialInstitutionId")]
    public string FinancialInstitutionId { get; set; }

    [JsonProperty("flags")]
    public string Flags { get; set; }

    [JsonProperty("holderName")]
    public string HolderName { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("identifiers")]
    public string Identifiers { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("ownership")]
    public double Ownership { get; set; }

    [JsonProperty("refreshed")]
    public long Refreshed { get; set; }

    [JsonProperty("transferDestinations")]
    public TransferDestination[] TransferDestinations { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
  }

  public partial class CurrencyDenominatedBalance
  {
    [JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonProperty("scale")]
    public long Scale { get; set; }

    [JsonProperty("unscaledValue")]
    public long UnscaledValue { get; set; }
  }

  public partial class Details
  {
    [JsonProperty("interest")]
    public long Interest { get; set; }

    [JsonProperty("nextDayOfTermsChange")]
    public string NextDayOfTermsChange { get; set; }

    [JsonProperty("numMonthsBound")]
    public long? NumMonthsBound { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
  }

  public partial class TransferDestination
  {
    [JsonProperty("balance")]
    public long Balance { get; set; }

    [JsonProperty("displayAccountNumber")]
    public string DisplayAccountNumber { get; set; }

    [JsonProperty("displayBankName")]
    public object DisplayBankName { get; set; }

    [JsonProperty("matchesMultiple")]
    public bool MatchesMultiple { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("uri")]
    public string Uri { get; set; }
  }

}
