using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tms.Domain;
public static class ServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {

        return services;
    }
}
