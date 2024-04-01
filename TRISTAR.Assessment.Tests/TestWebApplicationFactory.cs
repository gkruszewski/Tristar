using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TRISTAR.Assessment.People;
using TRISTAR.Assessment.Server;

namespace TRISTAR.Assessment
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public void AddTestData(Person[] people)
        {
            var repo = Server.Host.Services.GetService<PersonServerRepository>();
            foreach (var person in people)
            {
                repo.People.AddOrUpdate(person.Id, person, (key, value) => person);
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("Environment", "Development");
            base.ConfigureWebHost(builder);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder().UseStartup<Startup>();
        }
    }
}
