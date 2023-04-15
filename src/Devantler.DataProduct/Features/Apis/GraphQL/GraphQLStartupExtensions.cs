using Devantler.DataProduct.Core.Configuration.Options;
using HotChocolate.Types.Pagination;

namespace Devantler.DataProduct.Features.Apis.GraphQL;

/// <summary>
/// Extensions for registering GraphQL to the DI container and configuring the web application to use it.
/// </summary>
public static class GraphQLStartupExtensions
{
    /// <summary>
    /// Registers GraphQL to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddGraphQL(this IServiceCollection services, DataProductOptions options,
        IWebHostEnvironment environment)
    {
        var requestExecutorBuilder = services.AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment())
            .AddQueryType<Query>();
        _ = requestExecutorBuilder.SetPagingOptions(new PagingOptions
        {
            DefaultPageSize = options.Apis.GraphQL.DefaultPageSize,
            MaxPageSize = options.Apis.GraphQL.MaxPageSize
        });
        if (options.Apis.GraphQL.EnableProjections)
            _ = requestExecutorBuilder.AddProjections();
        if (options.Apis.GraphQL.EnableFiltering)
            _ = requestExecutorBuilder.AddFiltering();
        if (options.Apis.GraphQL.EnableSorting)
            _ = requestExecutorBuilder.AddSorting();

        return services;
    }

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication UseGraphQL(this WebApplication app)
    {
        _ = app.MapGraphQL();

        return app;
    }
}
