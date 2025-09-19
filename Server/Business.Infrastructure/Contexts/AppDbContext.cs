namespace Business.Infrastructure.Contexts;

using Business.Core.Interfaces;
using Business.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IAppDbContext"/>
/// <remarks>Implemented using Entity Framework.</remarks>
/// <param name="options">The database context options.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
  /// <inheritdoc/>
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}