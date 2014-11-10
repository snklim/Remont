using System;
using System.Collections.Generic;

namespace Remont.WebUI.Infrastructure
{
    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}
