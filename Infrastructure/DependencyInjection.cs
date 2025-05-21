

using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("bookingConnection") ?? throw new NullReferenceException("Connectionstring for EventService was not found");
        services.AddDbContext<SqlServerDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}
