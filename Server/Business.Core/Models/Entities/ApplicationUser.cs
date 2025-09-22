namespace Business.Core.Models.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using Business.Core.Interfaces.Entities;

/// <summary>
/// The Application User Entity.
/// </summary>
/// <remarks>
/// This entity is used for auditing purposes and links to the users in the identity system.
/// </remarks>
public class ApplicationUser : IAuditableEntity
{
  /// <summary>
  /// Gets or sets the unique identifier for the user.
  /// </summary>
  /// <value>The primary key for the user entity.</value>
  [Key]
  public int UserId { get; set; }

  /// <summary>
  /// Gets or sets the first name of the user.
  /// </summary>
  /// <remarks>Cannot exceed 32 characters.</remarks>
  [Required]
  [MaxLength(32, ErrorMessage = $"{nameof(FirstName)} cannot exceed 32 characters.")]
  public required string FirstName { get; set; }

  /// <summary>
  /// Gets or sets the surname of the user.
  /// </summary>
  /// <remarks>Cannot exceed 64 characters.</remarks>
  [Required]
  [MaxLength(64, ErrorMessage = $"{nameof(Surname)} cannot exceed 64 characters.")]
  public required string Surname { get; set; }

  /// <summary>
  /// Gets or sets the username for the user.
  /// </summary>
  /// <remarks>Cannot exceed 32 characters.</remarks>
  [Required]
  [MaxLength(32, ErrorMessage = $"{nameof(UserName)} cannot exceed 32 characters.")]
  public required string UserName { get; set; }

  /// <summary>
  /// Gets or sets the email address for the user.
  /// </summary>
  /// <remarks>Cannot exceed 256 characters.</remarks>
  [Required]
  [EmailAddress(ErrorMessage = "Invalid email address format.")]
  [MaxLength(256, ErrorMessage = $"{nameof(UserName)} cannot exceed 256 characters.")]
  public required string Email { get; set; }

  /// <inheritdoc/>
  public int CreatedBy { get; set; }

  /// <inheritdoc/>
  public int UpdatedBy { get; set; }

  /// <inheritdoc/>
  public DateTime CreatedOn { get; set; }

  /// <inheritdoc/>
  public DateTime UpdatedOn { get; set; }
}