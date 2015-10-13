using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DomainAttribute : ValidationAttribute
    {
        public IEnumerable<string> Values { get; private set; }

        public DomainAttribute(string value)
        {
            this.Values = new string[] { value };
        }

        public DomainAttribute(params string[] values)
        {
            this.Values = values;
        }

        public override bool IsValid(object value)
        {
            return this.Values.Any(item => value.ToString() == item);
        }

        public override string FormatErrorMessage(string name)
        {
            string[] values = this.Values.Select(value => string.Format("'{0}'", value)).ToArray();
            return string.Format(base.ErrorMessageString, name,string.Join(",", values));
        }
    }
}