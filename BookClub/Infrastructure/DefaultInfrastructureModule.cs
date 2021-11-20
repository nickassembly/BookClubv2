using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace BookClub.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        // TODO: Resolve Services issue

        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DefaultInfrastructureModule()
        {

        }
    }
}
