using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngularGenerator;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace XUnitTestFsBuilderProject
{
    public class ControllerAndTemplateEngineTest : TestServerFactory
    {
        [Fact]
        public async Task ReturnsView()
        {
            string testProjectDir = Directory.GetCurrentDirectory();
            var factory = new TestServerFactory();
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot(testProjectDir);
                builder.ConfigureTestServices(services => { services.AddMvc().AddRazorRuntimeCompilation(); });
            }).CreateClient();

            var resultIndex = await client.PostAsync($"api/project/Task/Build/1", new StringContent(""));
            var content = await resultIndex.Content.ReadAsStringAsync();
        }
    }
}