using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Menu
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class FunctionAttribute : Attribute
    {
        public FunctionAttribute() { }

        public FunctionAttribute(string name, bool isMenu, int sort = 100)
        {
            this.Name = name;
            this.IsMenu = isMenu;
            this.Sort = sort;
        }
        public FunctionAttribute(string name, string cssClass, int sort = 100)
        {
            this.Name = name;
            this.CssClass = cssClass;
            this.Sort = sort;
        }
        public FunctionAttribute(string name, bool isMenu, string cssClass, int sort = 100)
        {
            this.Name = name;
            this.IsMenu = isMenu;
            this.CssClass = cssClass;
            this.Sort = sort;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 菜单
        /// </summary>
        public bool IsMenu { set; get; }

        /// <summary>
        /// 统一资源定位标识符
        /// 格式：namespace,Controller.Action 或 namespace,Controller
        /// </summary>
        public string SysResource { set; get; }

        /// <summary>
        /// 统一资源定位符MD5解析
        /// </summary>
        public string ResourceId
        {
            get
            {
                if (!string.IsNullOrEmpty(SysResource))
                {
                    return Core.Librs.EncryptorHelper.Md5(SysResource);
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 上一级资源定位标识的SysResource值
        /// </summary>
        public string FatherResource { set; get; }

        /// <summary>
        /// 上一级资源定位标识的MD5解析
        /// </summary>
        public string FatherId
        {
            get
            {
                if (!string.IsNullOrEmpty(FatherResource))
                {
                    return Core.Librs.EncryptorHelper.Md5(FatherResource);
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { set; get; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { set; get; }

        /// <summary>
        /// css样式
        /// </summary>
        public string CssClass { set; get; }

        /// <summary>
        /// 排序序号，默认100，正序 小的排在前面
        /// </summary>
        public int Sort { set; get; }
    }
}
