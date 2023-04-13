namespace Devantler.DataProduct.Features.Apis.gRPC;

/// <summary>
/// Extensions to register gRPC to the DI container and configure the web application to use it.
/// </summary>
public static class GRpcStartupExtensions
{
    /// <summary>
    /// Registers gRPC to the DI container.
    /// </summary>
    public static IServiceCollection AddGRpc(this IServiceCollection services)
    {
        _ = services.AddGrpc();
        return services;
    }

    /// <summary>
    /// Configures the web application to use gRPC.
    /// </summary>
    public static WebApplication UseGRpc(this WebApplication app) =>
        //_ = app.MapGrpcService<GreeterService>();
        app;
}