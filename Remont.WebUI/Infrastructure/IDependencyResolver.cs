using System;

namespace Remont.WebUI.Infrastructure
{
    public interface IDependencyResolver : IDependencyScope, IDisposable
    {
        IDependencyScope BeginScope();
    }
}
