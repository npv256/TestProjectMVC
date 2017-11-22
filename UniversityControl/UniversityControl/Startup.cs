using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(UniversityControl.Startup))]
namespace UniversityControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
