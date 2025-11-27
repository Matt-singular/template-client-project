namespace Business.Core.Models.Extensions;

using System.Diagnostics.CodeAnalysis;
using Business.Core.Interfaces.Entities;
using Business.Core.Models.Entities;
using Common.Shared.Constants;

/// <summary>
/// Provides extension methods for querying user-related audit information.
/// </summary>
[Experimental(ApplicationConstants.Experimental)] // TODO: Remove Experimental attribute when this extension is fully implemented and tested.
[ExcludeFromCodeCoverage(Justification = "This method is not implemented or tested, just a basic idea for now")]
public static class UserQueryableExtensions
{
  /// <summary>
  /// Projects the source query to include the CreatedBy and UpdatedBy user names.
  /// </summary>
  /// <typeparam name="TEntity">An entity implementing <see cref="IAuditableEntity"/>.</typeparam>
  /// <typeparam name="TResult">The type of the result returned by the selector.</typeparam>
  /// <param name="query">The source query.</param>
  /// <param name="users">The users queryable (typically a DbSet of <see cref="ApplicationUser"/>).</param>
  /// <param name="selector">A projection function that receives the entity, the CreatedBy user name, and the UpdatedBy user name, and returns a result.</param>
  /// <returns>A query projecting the entity and its CreatedBy/UpdatedBy user names as specified by the selector.</returns>
  public static IQueryable<TResult> WithAuditUserNames<TEntity, TResult>(
      this IQueryable<TEntity> query,
      IQueryable<ApplicationUser> users,
      Func<TEntity, string?, string?, TResult> selector)
      where TEntity : IAuditableEntity
  {
    //  TODO: this is overkill and just a general idea/test case for now, needs proper implementation and testing
    return query
      .Select(entity => new
      {
        Entity = entity,
        CreatedByUser = users.FirstOrDefault(u => u.UserId == entity.CreatedBy),
        UpdatedByUser = users.FirstOrDefault(u => u.UserId == entity.UpdatedBy)
      })
      .Select(entityWithAudit => selector(
        entityWithAudit.Entity,
        entityWithAudit.CreatedByUser != null ? entityWithAudit.CreatedByUser.Username : null,
        entityWithAudit.UpdatedByUser != null ? entityWithAudit.UpdatedByUser.Username : null
      ));
  }
}