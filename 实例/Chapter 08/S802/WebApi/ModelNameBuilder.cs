using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi
{
    public static class ModelNameBuilder
    {
        public static string CreatePropertyModelName(string prefix, string propertyName)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return (propertyName ?? string.Empty);
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                return (prefix + "." + propertyName);
            }
            return (prefix ?? string.Empty);
        }

        public static string CreateIndexModelName(string parentName, string index)
        {
            if (parentName.Length != 0)
            {
                return (parentName + "[" + index + "]");
            }
            return ("[" + index + "]");
        }
    }
}