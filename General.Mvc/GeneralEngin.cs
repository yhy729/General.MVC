using System;
using System.Collections.Generic;
using System.Text;
using General.Core;
using Microsoft.Extensions.DependencyInjection;

namespace General.Mvc
{
    /// <summary>
    /// Service Locator模式
    /// https://www.cnblogs.com/artech/p/inside-asp-net-core-03-03.html
    /// 文章不错 很有见地 不怎么退件这种方式
    /// </summary>
    public class GeneralEngin : IEngine
    {
        private IServiceProvider _serviceProvider;
        public GeneralEngin(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 通过引擎构建实列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return this._serviceProvider.GetService<T>();
        }
    }
}
