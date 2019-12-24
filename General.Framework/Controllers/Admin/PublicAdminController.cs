using General.Core;
using General.Framework.Filters;
using General.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Controllers.Admin
{
    //[AdminAuthFilter]
    public class PublicAdminController : AdminAreaController
    {
        private IWorkContext _workContext;
        public PublicAdminController()
        {
            this._workContext = EnginContext.Current.Resolve<IWorkContext>();
        }

        public IWorkContext WorkContext
        {
            get { return _workContext; }
        }
    }
}
