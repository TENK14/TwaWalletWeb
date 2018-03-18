using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwaWallet.Web.App_Start;

[assembly: OwinStartupAttribute(typeof(TwaWallet.Web.Startup))]
namespace TwaWallet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}