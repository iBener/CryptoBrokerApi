using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Util.Reflection;

public static class ReflectionUtilities
{
    public static List<Assembly> GetReferencedAssemblies(Assembly? rootAssembly)
    {
        var looked = new HashSet<string>();
        var queue = new Queue<Assembly?>();
        var listResult = new List<Assembly>();

        var root = rootAssembly ?? Assembly.GetEntryAssembly();
        queue.Enqueue(root);

        while (queue.Any())
        {
            var asm = queue.Dequeue();

            if (asm == null)
                break;

            listResult.Add(asm);

            foreach (var reference in asm.GetReferencedAssemblies())
            {
                if (!looked.Contains(reference.FullName))
                {
                    queue.Enqueue(Assembly.Load(reference));
                    looked.Add(reference.FullName);
                }
            }
        }

        return listResult.Distinct().ToList();
    }
}
