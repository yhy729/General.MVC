using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using General.Core.Librs;
using Microsoft.Extensions.DependencyInjection;

namespace General.Core.Extensions
{
    /// <summary>
    /// IServiceCollection容器的扩展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 程序集依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="serviceLifetime"></param>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services) + "为空");
            }
            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new ArgumentNullException(nameof(assemblyName) + "为空");
            }
            var assembly = RuntimeHelper.GetAssemblyByName(assemblyName);
            if (assembly == null)
            {
                throw new ArgumentNullException(assemblyName + "dll不存在");
            }
            var types = assembly.GetTypes();
            var list = types.Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType).ToList();
            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();
                if (interfaceList.Any())
                {
                    var inter = interfaceList.First();
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(inter, type);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }
}
