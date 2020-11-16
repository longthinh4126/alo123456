using Microsoft.Owin;
using Owin;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartupAttribute(typeof(Do_An.Startup))]
namespace Do_An
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
