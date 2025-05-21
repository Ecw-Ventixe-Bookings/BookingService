

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
{
    internal DbSet<BookingEntity> Bookings { get; set; }
    internal DbSet<BookingOwnerEntity> BookingOwners { get; set; }
    internal DbSet<BookingOwnerAddressEntity> BookingOwnersAddresses { get; set; }
}
