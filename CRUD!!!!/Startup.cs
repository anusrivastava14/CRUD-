using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUD____.Startup))]
namespace CRUD____
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
