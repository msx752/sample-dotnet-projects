using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SampleProject.Core
{
    public static class Utility
    {
        public static IEnumerable<Assembly> GetLoadedAssemblies(Func<Assembly, bool> fnAssembly = null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                bool yieldReturn = false;
                try
                {
                    assembly.DefinedTypes.FirstOrDefault();
                    if (fnAssembly == null || (fnAssembly != null && fnAssembly.Invoke(assembly)))
                        yieldReturn = true;
                }
                catch
                {
                    continue;
                }

                if (yieldReturn)
                    yield return assembly;
            }
        }
    }
}