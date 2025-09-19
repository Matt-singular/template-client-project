namespace Business.Core.Interfaces;

using System.Threading.Tasks;
using Business.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The application's database context.
/// </summary>
public interface IAppDbContext
{
  /// <summary>
  /// Gets or sets the ApplicationUsers DbSet.
  /// </summary>
  /// <remarks><inheritdoc cref="ApplicationUser"/></remarks>
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }

  /// <inheritdoc cref="DbContext"/>
  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}