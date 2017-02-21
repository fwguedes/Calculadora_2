using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Calculadora.Dependency
{
    abstract public class DependencyInjector
    {
        private static readonly object sync = new object();

        private static Container _container;

        public static Container Container
        {
            get {
                if (_container != null)
                { 
                    return _container;
                }
                else
                {
                    lock (sync)
                    {
                        if (_container == null)
                        {
                            _container = new Container();
                        }
                    }

                    return _container;
                }
                

            }
        }

        public static TService Get<TService>() where TService : class
        {
            return Container.GetInstance<TService>();
        }
      
        public static void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(Lifestyle.Singleton);

        }
                
    }
}
