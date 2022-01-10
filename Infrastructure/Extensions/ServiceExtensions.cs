using System;
using System.Linq;
using Domain.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions {

    public static class ServiceExtensions {
        public static IServiceCollection AddDomainServices(this IServiceCollection svc)
        {
            var _services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName?.Contains("Domain", StringComparison.InvariantCulture) ?? false)
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));
 
            foreach (var _service in _services)
            {
                svc.AddTransient(_service);
            }

            return svc;
        }
    }
}
