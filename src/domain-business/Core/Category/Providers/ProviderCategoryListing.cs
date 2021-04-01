using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_business.Core.Category.Providers
{

  public class ProviderCategoryListing
  {
    public IEnumerable<ProviderCategoryGroup> CategoryGroups { get; set; }
      = Enumerable.Empty<ProviderCategoryGroup>();
  }

  public class ProviderCategoryGroup
  {

    public string Name { get; set; } = string.Empty;
    public string ProviderCategoryID { get; set; } = string.Empty;

    public IEnumerable<ProviderCategory> Categories { get; set; }
      = Enumerable.Empty<ProviderCategory>();

  }

}
