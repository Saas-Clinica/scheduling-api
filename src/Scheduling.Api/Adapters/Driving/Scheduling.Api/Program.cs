using Asp.Versioning;
using FluentValidation;
using Scheduling.Api.Application;
using Scheduling.Api.Application.Common;
using Scheduling.Api.AppServices;
using Scheduling.Api.AppServices.Ports;
using Scheduling.Api.Middleware;
using Scheduling.Api.Validators.v1.Appointment;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentValidator>();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg => cfg.EnableAnnotations());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<INotifier, Notifier>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddScoped<IAppointmentAppService, AppointmentAppService>();
builder.Services.AddApplication();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
