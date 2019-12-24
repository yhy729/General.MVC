using System;
using System.Collections.Generic;
using System.Text;

namespace General.Core
{
    /// <summary>
    /// 系统引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 构造实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T:class;
    }
}
