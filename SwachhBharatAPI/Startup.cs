using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwachhBharatAPI.Startup))]
namespace SwachhBharatAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
