﻿
namespace infra_configuration.Clients
{


  // --- Settings --------------------------------------------------------------
  public class TinkSettings
  {
    public TinkPathSettings APIPath { get; set; } = new TinkPathSettings { };
  }

  public class TinkPathSettings
  { 
    public string? OAuthToken { get; set; }
    public string? ListAccounts { get; set; }
    public string? ListCategories { get; set; }
    public string? SearchTransactions { get; set; }
    public string? GetUserProfile { get; set; }
  }

}
