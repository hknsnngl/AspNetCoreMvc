using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Abc.WebUI.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var opt = new StaticFileOptions();
            opt.RequestPath = "/node_modules";
            opt.FileProvider = provider;

            app.UseStaticFiles(opt);
            return app;
        }
    }
}
