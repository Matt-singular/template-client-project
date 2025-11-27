namespace Business.Core.Interfaces.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Common.Shared.Constants;

/// <summary>
/// Marks an entity as auditable.
/// </summary>
public interface IAuditableEntity
{
  /// <summary>
  /// Gets or sets the created by user identifier.
  /// </summary>
  public int CreatedBy { get; set; }

  /// <summary>
  /// Gets or sets the updated by user identifier.
  /// </summary>
  public int UpdatedBy { get; set; }

  /// <summary>
  /// Gets or sets the created on timestamp.
  /// </summary>
  [Column(TypeName = DatabaseConstants.DateTimeColumn)] // TODO: will need to move the above to a base class
  public DateTime CreatedOn { get; set; }

  /// <summary>
  /// Gets or sets the updated on timestamp.
  /// </summary>
  [Column(TypeName = DatabaseConstants.DateTimeColumn)] // TODO: will need to move the above to a base class
  public DateTime UpdatedOn { get; set; }
}