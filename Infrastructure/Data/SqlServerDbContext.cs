

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
{
}
