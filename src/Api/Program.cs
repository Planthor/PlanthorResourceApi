using System;
using Application.Members.Commands.Create;
using Application.Members.Commands.Update;
using Application.Members.Queries.Details;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    // Infrastructure
    builder.Host.UseSerilog();

    builder.Services.AddSingleton<IClock>(SystemClock.Instance);
    builder.Services.AddMediatR(cfg =>
    {
        // TODO (Trung) : Use Environment Variable to get LicenseKey Mediatr.
        cfg.LicenseKey = "";
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });

    builder.Services.AddScoped<IRequestHandler<CreateMemberCommand, Guid>, CreateMemberCommandHandler>();

    builder.Services.AddScoped<IValidator<CreateMemberCommand>, CreateMemberCommandValidator>();
    builder.Services.AddScoped<IValidator<UpdateMemberCommand>, UpdateMemberCommandValidator>();
    builder.Services.AddScoped<IValidator<MemberDetailsQuery>, MemberDetailsQueryValidator>();

    builder.Services.AddPlanthorDbContext(
        builder.Configuration.GetConnectionString("PlanthorDbContext")
            ?? throw new InvalidOperationException("PlanthorDbContext is not set in the configuration file."));

    // API Client
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    // OpenAPI + Scalar
    builder.Services.AddOpenApi();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        // Serves the OpenAPI JSON document at /openapi/v1.json
        app.MapOpenApi();

        // Serves the Scalar UI at /scalar/v1
        app.MapScalarApiReference(options =>
        {
            options.Title = "Planthor API";
            options.Theme = ScalarTheme.DeepSpace;
            options.DefaultHttpClient = new(ScalarTarget.Swift, ScalarClient.HttpClient);
        });
    }

    app.MapControllers();

    Log.Information("The app started.");
    await app.RunAsync();
}
catch (InvalidOperationException ex)  // Catch specific exception
{
    // Log detailed exception information for InvalidOperationException
    Log.Error(ex, "An unexpected operation occurred.");
}
catch (AppDomainUnloadedException ex)
{
    Log.Fatal(ex, "The application domain was unloaded unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

/// <summary>
/// Make Program extensible for integration tests
/// </summary>
public partial class Program
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Program"/> class.
    /// </summary>
    protected Program() { }
}
