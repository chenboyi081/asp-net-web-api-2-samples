using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Http.ValueProviders;

namespace WebApi
{
internal sealed class ElementalValueProvider : IValueProvider
{
    public CultureInfo Culture { get; private set; }
    public string Name { get; private set; }
    public object RawValue { get; private set; }

    public ElementalValueProvider(string name, object rawValue, CultureInfo culture)
    {
        this.Name = name;
        this.RawValue = rawValue;
        this.Culture = culture;
    }
    public bool ContainsPrefix(string prefix)
    {
        return IsPrefixMatch(this.Name, prefix);
    }
    public ValueProviderResult GetValue(string key)
    {
        if (!string.Equals(key, this.Name, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        return new ValueProviderResult(this.RawValue, Convert.ToString(this.RawValue, this.Culture), this.Culture);
    }

    internal static bool IsPrefixMatch(string prefix, string testString)
    {
        if (testString == null)
        {
            return false;
        }
        if (prefix.Length != 0)
        {
            if (prefix.Length > testString.Length)
            {
                return false;
            }
            if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (testString.Length == prefix.Length)
            {
                return true;
            }
            char ch = testString[prefix.Length];
            if ((ch != '.') && (ch != '['))
            {
                return false;
            }
        }
        return true;
    }
}
}
