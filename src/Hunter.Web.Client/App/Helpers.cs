using System;

namespace Hunter.Web.Client
{
    public static class Helpers
    {
        public static string GetName(this Enums.ResponseType response)
        {
            return Enum.GetName(typeof(Enums.ResponseType), response);
        }
    }
}
