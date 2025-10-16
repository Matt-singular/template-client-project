namespace Business.Infrastructure.Contexts;

using Business.Core.Interfaces.Entities;
using Business.Core.Models.Entities;
using Common.Shared.Constants;

/// <summary>
/// Provides seed data for initializing the application's database context.
/// </summary>
public static class SeedData
{
  /// <summary>
  /// Gets the system application user for seeding.
  /// </summary>
  /// <returns>The system application user.</returns>
  public static ApplicationUser GetApplicationUsers()
  {
    return new ApplicationUser()
    {
      UserId = 1,
      FirstName = ApplicationConstants.SystemUserName,
      Surname = ApplicationConstants.SystemUserName,
      UserName = ApplicationConstants.SystemUserName,
      Email = $"{ApplicationConstants.SystemUserName}@Application.com"
    }.SetAuditFields();
  }

  /// <summary>
  /// Sets the audit fields for an auditable entity.
  /// </summary>
  /// <typeparam name="TAuditableEntity">The type of the entity implementing <see cref="IAuditableEntity"/>.</typeparam>
  /// <param name="entity">The entity to set audit fields for.</param>
  /// <returns>The entity with audit fields set to default values.
  /// </returns>
  public static TAuditableEntity SetAuditFields<TAuditableEntity>(this TAuditableEntity entity) where TAuditableEntity : IAuditableEntity
  {
    entity.CreatedBy = 1;
    entity.UpdatedBy = 1;
    entity.CreatedOn = DateTime.UtcNow;
    entity.UpdatedOn = DateTime.UtcNow;

    return entity;
  }
}