using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using U4.API.Middlewares;
using U4.Application.Extesions;
using U4.Domain.Entities;
using U4.Infrastructure.Authorization;
using U4.Infrastructure.Extensions;
using U4.Infrastructure.Persistence;
using U4.Infrastructure.Seeders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{


    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            [] 
        }
    });

});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
    .AddEntityFrameworkStores<RestaurantDbContext>();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(PoloicyNames.HasNationality, policy => policy.RequireClaim(AppClaimTypes.Nationality));
builder.Services.AddApplication();
builder.Services.AddAuthentication();
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.MapGroup("api/identity").MapIdentityApi<User>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
