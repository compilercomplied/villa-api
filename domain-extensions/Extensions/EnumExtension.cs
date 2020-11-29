using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace domain_extensions.Extensions
{
  public static class EnumExtension
  {

    public static string GetDisplayName(this Enum enumValue)
    {

      return enumValue
        .GetType()
        .GetMember(enumValue.ToString())
        .FirstOrDefault()
        ?.GetCustomAttribute<DisplayAttribute>()
        ?.GetName()
        ?? string.Empty;

    }

  }

}
