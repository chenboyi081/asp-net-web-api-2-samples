using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    public class PreLoadedAssembliesSettings : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public AssemblyElementCollection AssemblyNames
        {
            get { return (AssemblyElementCollection)this[""]; }
        }

        public static PreLoadedAssembliesSettings GetSection()
        {
            return ConfigurationManager.GetSection("preLoadedAssemblies") as PreLoadedAssembliesSettings;
        }
    }

    public class AssemblyElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            AssemblyElement serviceTypeElement = (AssemblyElement)element;
            return serviceTypeElement.AssemblyName;
        }
    }

    public class AssemblyElement : ConfigurationElement
    {
        [ConfigurationProperty("assemblyName", IsRequired = true)]
        public string AssemblyName
        {
            get { return (string)this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }
    }
}
