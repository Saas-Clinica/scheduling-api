using Microsoft.Extensions.DependencyInjection;
using UseCases;

namespace scheduling_api.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateAppointmentUseCase, CreateAppointmentUseCase>();
        return services;
    }
}
