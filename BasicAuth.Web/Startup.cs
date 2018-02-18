using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Web.Security;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BasicAuth.Web.Startup))]

namespace BasicAuth.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
