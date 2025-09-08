namespace Business.Core.Interfaces;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The application's database context.
/// </summary>
public interface IAppDbContext
{
  /// <summary>
  /// Gets or sets the Customers DbSet.
  /// </summary>
  //public DbSet<object> Customers { get; set; }

  /// <inheritdoc cref="DbContext"/>
  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}