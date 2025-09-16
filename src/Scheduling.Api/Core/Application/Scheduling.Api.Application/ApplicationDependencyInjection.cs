using Microsoft.Extensions.DependencyInjection;
using Scheduling.Api.Application.Common;
using Scheduling.Api.Application.Ports;
using Scheduling.Api.Application.UseCases;

namespace Scheduling.Api.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateAppointmentUseCase, CreateAppointmentUseCase>();
        return services;
    }
}
