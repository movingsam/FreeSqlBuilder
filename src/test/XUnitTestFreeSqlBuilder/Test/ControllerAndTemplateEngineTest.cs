using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace XUnitTestFreeSqlBuilder.Test
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