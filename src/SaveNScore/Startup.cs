using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaveNScore.Startup))]
namespace SaveNScore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
