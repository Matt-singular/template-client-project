namespace Common.Shared.Models.Exceptions;

using System;

/// <summary>
/// Exception thrown when a record is not found in the database.
/// </summary>
/// <param name="message">The error message to be used.</param>
/// <param name="innerException">The inner exception (where relevant)</param>
public class SqlDataNotFoundException(string message, Exception? innerException = null) : Exception(message,innerException);