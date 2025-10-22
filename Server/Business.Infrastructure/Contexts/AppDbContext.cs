namespace Business.Infrastructure.Contexts;

using Business.Core.Interfaces;
using Business.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IAppDbContext"/>
/// <remarks>Implemented using Entity Framework.</remarks>
/// <param name="options">The database context options.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
  // TODO: check how EF wants you to define collations in code-first systems.
  /// <inheritdoc/>
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }

  /// <inheritdoc/>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<ApplicationUser>().HasData(SeedData.GetApplicationSystemUser());
  }
}