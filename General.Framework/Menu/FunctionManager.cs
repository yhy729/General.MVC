using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace General.Framework.Menu
{
    public class FunctionManager
    {
        public static List<FunctionAttribute> GetFunctionLists()
        {
            var asseambly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("General.Mvc"));
            var result = new List<FunctionAttribute>();
            var types = asseambly.GetTypes();
            if (types != null)
            {
                foreach (var type in types)
                {
                    var typeName = type.FullName.ToLower();
                    if (typeName.EndsWith("controller"))
                    {
                        var funAttrList = type.GetCustomAttributes<FunctionAttribute>(false);
                        FunctionAttribute father = null;
                        if (funAttrList != null && funAttrList.Any())
                        {
                            foreach (var fun in funAttrList)
                            {
                                if (string.IsNullOrEmpty(fun.SysResource))
                                {
                                    fun.SysResource = type.FullName;
                                }
                                father = fun;
                                result.Add(fun);
                                break;
                            }
                        }

                        //获取action方法
                        var members = type.FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, Type.FilterName, "*");
                        if (members != null && members.Any())
                        {
                            foreach (var m in members)
                            {
                                var funs = m.GetCustomAttributes<FunctionAttribute>(false);
                                foreach (var fun in funs)
                                {
                                    if (string.IsNullOrEmpty(fun.FatherResource))
                                    {
                                        if (father != null)
                                        {
                                            fun.FatherResource = father.SysResource;
                                        }
                                        object[] routes = m.GetCustomAttributes(typeof(RouteAttribute), false);
                                        if (routes != null && routes.Any())
                                        {
                                            var route = routes.First() as RouteAttribute;
                                            fun.RouteName = route.Name;
                                        }
                                        result.Add(fun);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
