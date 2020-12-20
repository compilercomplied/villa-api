using System;
using System.Collections.Generic;
using System.Text;

namespace domain_constants.Env
{
  public static class PgEnv
  {
    public static readonly string CONN = "PG_CONN";

  }

  public static class GoogleEnv
  {
    public static readonly string SECRET = "GOOGLE_SECRET";
    public static readonly string ID = "GOOGLE_CLIENTID";
  }
}
