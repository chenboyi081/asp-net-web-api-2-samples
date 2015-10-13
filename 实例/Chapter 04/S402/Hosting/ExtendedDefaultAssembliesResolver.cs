using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace Hosting
{
    public class ExtendedDefaultAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            PreLoadedAssembliesSettings settings = PreLoadedAssembliesSettings.GetSection();
            if (null != settings)
            {
                foreach (AssemblyElement element in settings.AssemblyNames)
                {
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName(element.AssemblyName);
                    if (!AppDomain.CurrentDomain.GetAssemblies() .Any(assembly => AssemblyName.ReferenceMatchesDefinition(assembly.GetName(), assemblyName)))
                    {
                        AppDomain.CurrentDomain.Load(assemblyName);
                    }
                }
            }
            return base.GetAssemblies();
        }
    }
}
