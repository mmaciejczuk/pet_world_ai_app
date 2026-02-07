using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetWorld.Application.Abstractions.AI;
using PetWorld.Domain.Repositories;
using PetWorld.Infrastructure.AI;
using PetWorld.Infrastructure.Persistence;
using PetWorld.Infrastructure.Repositories;

namespace PetWorld.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("PetWorldDb");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException("Missing connection string: PetWorldDb");

        // Minimal fix: don't AutoDetect (it connects immediately).
        var serverVersion = new MySqlServerVersion(new Version(8, 4, 8));

        services.AddDbContext<PetWorldDbContext>(opt =>
        {
            opt.UseMySql(cs, serverVersion);
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IChatHistoryRepository, ChatHistoryRepository>();

        services.AddOptions<AiOptions>()
            .Bind(config.GetSection("AI"));

        services.AddSingleton<IWriterAgent, OpenAiWriterAgent>();
        services.AddSingleton<ICriticAgent, OpenAiCriticAgent>();

        return services;
    }
}
