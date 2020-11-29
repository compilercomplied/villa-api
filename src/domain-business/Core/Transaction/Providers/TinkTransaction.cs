using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_business.Core.Transaction.Providers
{
  // --- Request ---------------------------------------------------------------
  public class TinkTransactionRequest
  {

    [JsonProperty("amount")]
    public Amount Amount { get; set; }

    [JsonProperty("createTime")]
    public DateTimeOffset CreateTime { get; set; }

    [JsonProperty("destinationId")]
    public string DestinationId { get; set; }

    [JsonProperty("destinationType")]
    public string DestinationType { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("sourceId")]
    public string SourceId { get; set; }

    [JsonProperty("sourceType")]
    public string SourceType { get; set; }

  }

  // --- Response --------------------------------------------------------------
  public class TinkTransactionResponse
  {
    [JsonProperty("count")]
    public long Count { get; set; }

    [JsonProperty("metrics")]
    public Metrics Metrics { get; set; }

    [JsonProperty("net")]
    public double Net { get; set; }

    [JsonProperty("periodAmounts")]
    public PeriodAmount[] PeriodAmounts { get; set; }

    [JsonProperty("query")]
    public Query Query { get; set; }

    [JsonProperty("results")]
    public Result[] Results { get; set; }
  }

  public class Metrics
  {
    [JsonProperty("AVG")]
    public long Avg { get; set; }

    [JsonProperty("CATEGORIES")]
    public Categories Categories { get; set; }

    [JsonProperty("COUNT")]
    public long Count { get; set; }

    [JsonProperty("NET")]
    public double Net { get; set; }

    [JsonProperty("SUM")]
    public long Sum { get; set; }
  }

  public class Categories
  {
    [JsonProperty("0e1bade6a7e3459eb794f27b7ba4cea0")]
    public long The0E1Bade6A7E3459Eb794F27B7Ba4Cea0 { get; set; }
  }

  public class PeriodAmount
  {
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("value")]
    public long Value { get; set; }
  }

  public class Query
  {
    [JsonProperty("accounts")]
    public string[] Accounts { get; set; }

    [JsonProperty("categories")]
    public string[] Categories { get; set; }

    [JsonProperty("endDate")]
    public long? EndDate { get; set; }

    [JsonProperty("externalIds")]
    public string[] ExternalIds { get; set; }

    [JsonProperty("includeUpcoming")]
    public bool IncludeUpcoming { get; set; }

    [JsonProperty("limit")]
    public long Limit { get; set; }

    [JsonProperty("maxAmount")]
    public double? MaxAmount { get; set; }

    [JsonProperty("minAmount")]
    public double? MinAmount { get; set; }

    [JsonProperty("offset")]
    public long Offset { get; set; }

    [JsonProperty("order")]
    public string Order { get; set; }

    [JsonProperty("queryString")]
    public string QueryString { get; set; }

    [JsonProperty("sort")]
    public string Sort { get; set; }

    [JsonProperty("startDate")]
    public long? StartDate { get; set; }
  }

  public class Result
  {
    [JsonProperty("transaction")]
    public TinkTransaction Transaction { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
  }

  public class TinkTransaction
  {
    [JsonProperty("accountId")]
    public string AccountId { get; set; }

    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [JsonProperty("categoryType")]
    public string CategoryType { get; set; }

    [JsonProperty("currencyDenominatedAmount")]
    public CurrencyDenominatedAmount CurrencyDenominatedAmount { get; set; }

    [JsonProperty("currencyDenominatedOriginalAmount")]
    public CurrencyDenominatedAmount CurrencyDenominatedOriginalAmount { get; set; }

    [JsonProperty("date")]
    public long Date { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("dispensableAmount")]
    public long DispensableAmount { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("lastModified")]
    public long LastModified { get; set; }

    [JsonProperty("notes")]
    public string Notes { get; set; }

    [JsonProperty("originalAmount")]
    public double OriginalAmount { get; set; }

    [JsonProperty("originalDate")]
    public long OriginalDate { get; set; }

    [JsonProperty("originalDescription")]
    public string OriginalDescription { get; set; }

    [JsonProperty("partnerPayload")]
    public Payload PartnerPayload { get; set; }

    [JsonProperty("parts")]
    public Part[] Parts { get; set; }

    [JsonProperty("payload")]
    public Payload Payload { get; set; }

    [JsonProperty("pending")]
    public bool Pending { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("upcoming")]
    public bool Upcoming { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("userModified")]
    public bool UserModified { get; set; }
  }

  public class CurrencyDenominatedAmount
  {
    [JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonProperty("scale")]
    public long Scale { get; set; }

    [JsonProperty("unscaledValue")]
    public long UnscaledValue { get; set; }
  }

  public class Payload
  {
  }

  public class Part
  {
    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [JsonProperty("counterpartDescription")]
    public string CounterpartDescription { get; set; }

    [JsonProperty("counterpartId")]
    public string CounterpartId { get; set; }

    [JsonProperty("counterpartTransactionAmount")]
    public long CounterpartTransactionAmount { get; set; }

    [JsonProperty("counterpartTransactionId")]
    public string CounterpartTransactionId { get; set; }

    [JsonProperty("date")]
    public long Date { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("lastModified")]
    public long LastModified { get; set; }
  }

  // --- Shared ----------------------------------------------------------------
  public class Amount
  {

    [JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonProperty("scale")]
    public long Scale { get; set; }

    [JsonProperty("unscaledValue")]
    public long UnscaledValue { get; set; }

  }

}
