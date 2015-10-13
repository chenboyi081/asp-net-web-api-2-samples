using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            configuration.Configure(container);
            A a = (A)container.Resolve<IA>();
            Console.WriteLine("a.B == null ? {0}", a.B == null ? "Yes" : "No");
            Console.WriteLine("a.C == null ? {0}", a.C == null ? "Yes" : "No");
            Console.WriteLine("a.D == null ? {0}", a.D == null ? "Yes" : "No");
        }
    }

    public interface IA { }
    public interface IB { }
    public interface IC { }
    public interface ID { }

    public class A : IA
    {
        public IB B { get; set; }
        [Dependency]
        public IC C { get; set; }
        public ID D { get; set; }

        public A(IB b)
        {
            this.B = b;
        }
        [InjectionMethod]
        public void Initialize(ID d)
        {
            this.D = d;
        }
    }
    public class B : IB { }
    public class C : IC { }
    public class D : ID { }
}
