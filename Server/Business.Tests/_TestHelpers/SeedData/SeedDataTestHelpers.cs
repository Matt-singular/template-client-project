namespace Business.Tests._TestHelpers.SeedData;

using System;
using Business.Core.Interfaces.Entities;

/// <summary>
/// Helpers used for setting up seed data for unit tests.
/// </summary>
public static class SeedDataTestHelpers
{
  /// <summary>
  /// Sets the audit fields for an auditable entity.
  /// </summary>
  /// <typeparam name="TAuditableEntity">The type of the entity implementing <see cref="IAuditableEntity"/>.</typeparam>
  /// <param name="entity">The entity to set audit fields for.</param>
  /// <returns>The entity with audit fields set to default values.</returns>
  public static TAuditableEntity SetAuditFields<TAuditableEntity>(this TAuditableEntity entity) where TAuditableEntity : IAuditableEntity
  {
    entity.CreatedBy = 1;
    entity.UpdatedBy = 1;
    entity.CreatedOn = DateTime.UtcNow;
    entity.UpdatedOn = DateTime.UtcNow;

    return entity;
  }
}