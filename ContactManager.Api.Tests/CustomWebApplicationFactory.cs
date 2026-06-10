using ContactManager.Core.DataLayer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using QuickWebr;

namespace ContactManager.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public IContactRepository GetReader() => Services.GetRequiredService<IContactRepository>();
};
