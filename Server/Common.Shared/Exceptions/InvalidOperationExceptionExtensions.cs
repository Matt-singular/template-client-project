namespace Common.Shared.Exceptions;

using System.Diagnostics.CodeAnalysis;

/// <inheritdoc cref="System.InvalidOperationException"/>
/// <remarks>Provides extension methods for throwing <see cref="System.InvalidOperationException"/> with custom conditions.</remarks>
public static class InvalidOperationException
{
  /// <summary>
  /// Throws an <see cref="System.InvalidOperationException"/> if the specified <paramref name="value"/> is <see langword="null"/>.
  /// </summary>
  /// <typeparam name="TValue">The type of the value to be checked.</typeparam>
  /// <param name="value">The value to be checked.</param>
  /// <param name="message">The custom message to use if <paramref name="value"/> is null.</param>
  /// <exception cref="System.InvalidOperationException">Thrown if the <paramref name="value"/> is <see langword="null"/>.</exception>
  public static bool ThrowIfNull<TValue>([NotNull] TValue? value, string? message = null) where TValue : class
  {
    if (value is null)
    {
      throw new System.InvalidOperationException(message ?? "The value cannot be null.");
    }

    return true;
  }
}