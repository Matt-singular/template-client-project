namespace Common.Shared.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Azure.Identity;
using Common.Shared.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.Extensions.Configuration;
using Serilog;

/// <summary>
/// Configures Always Encrypted for SQL connections.
/// </summary>
[Experimental(ApplicationConstants.Experimental)] // TODO: still need to implement this
[ExcludeFromCodeCoverage(Justification = "Experimental code - not yet in use")]
public static class AlwaysEncryptedExtensions
{
  /// <summary>
  /// Configures Always Encrypted for SQL connections.
  /// </summary>
  /// <param name="configuration">The application's configuration.</param>
  public static async Task ConfigureAlwaysEncrypted(IConfiguration configuration)
  {
    // Enable Always Encrypted for SQL connections
    string connectionString = configuration.TryGetConnectionString(ApplicationConstants.MainDatabaseConnectionStringName);

    RegisterAlwaysEncryptedStoreProviders();

    await EnsureAlwaysEncryptedKeysExistAsync(connectionString).ConfigureAwait(false);
  }

  private static void RegisterAlwaysEncryptedStoreProviders()
  {
    Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new()
    {
      {
        SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, // TODO: config?
        new SqlColumnEncryptionAzureKeyVaultProvider(new DefaultAzureCredential()) // TODO: config?
      }
    };
    try
    {
      SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
      Log.Logger.Information("Successfully registered Always Encrypted key store providers.");
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, "Error registering Always Encrypted key store providers: {Message}", ex.Message);
      throw;
    }
  }

  private static async Task EnsureAlwaysEncryptedKeysExistAsync(string connectionString)
  {
    await using var connection = new SqlConnection(connectionString);
    try
    {
      await connection.OpenAsync().ConfigureAwait(false);
      await CreateAlwaysEncryptedColumnMasterKey(connection).ConfigureAwait(false);
      //await CreateAlwaysEncryptedColumnEncryptionKeys(connection).ConfigureAwait(false);
      Log.Logger.Information("Successfully registered Always Encrypted master & column encryption keys.");
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, "Error creating Column Master Key: {Message}", ex.Message);
      throw;
    }
    finally
    {
      await connection.CloseAsync().ConfigureAwait(false);
    }
  }

  private static async Task CreateAlwaysEncryptedColumnMasterKey(SqlConnection connection)
  {
    // TODO: see if there's a more elegant way to handle this.
    // Ensure that the Column Master Key exists
    string columnMasterKeyName = "MyCMK"; // TODO: config?
    string columnMasterKeyProvider = "AZURE_KEY_VAULT"; // TODO: config?
    string columnMasterKeyPath = "path-to-key"; // TODO: config?

    string createMasterKeySql = $@"
      IF NOT EXISTS (SELECT * FROM sys.column_master_keys WHERE name = '{columnMasterKeyName}')
      BEGIN
        CREATE COLUMN MASTER KEY [{columnMasterKeyName}]
        WITH (
          KEY_STORE_PROVIDER_NAME = N'{columnMasterKeyProvider}',
          KEY_PATH = N'{columnMasterKeyPath}'
        );
      END";

    await using var cmd = new SqlCommand(createMasterKeySql, connection);
    await cmd.ExecuteNonQueryAsync();
  }

  //  private static async Task CreateAlwaysEncryptedColumnEncryptionKeys(SqlConnection connection)
  //  {
  //    string cekName = "CEK_Auto1";
  //    string cekExistsSql = $@"
  //SELECT COUNT(*) FROM sys.column_encryption_keys WHERE name = '{cekName}'";
  //    int cekCount;
  //    await using (var cmd = new SqlCommand(cekExistsSql, connection))
  //    {
  //      cekCount = (int)await cmd.ExecuteScalarAsync();
  //    }

  //    if (cekCount == 0)
  //    {
  //      // Generate CEK encrypted value
  //      var provider = SqlConnection.GetColumnEncryptionKeyStoreProvider(
  //          SqlColumnEncryptionAzureKeyVaultProvider.ProviderName);

  //      // Generate random CEK bytes
  //      byte[] plainCek = SqlColumnEncryptionKeyStoreProvider.GenerateColumnEncryptionKey();
  //      byte[] encryptedCek = provider.EncryptColumnEncryptionKey(
  //          "https://<your-vault>.vault.azure.net/keys/<your-key-name>/<key-version>",
  //          "RSA_OAEP",
  //          plainCek);

  //      string cekSql = $@"
  //CREATE COLUMN ENCRYPTION KEY [{cekName}]
  //WITH VALUES (
  //    COLUMN_MASTER_KEY = [{cmkName}],
  //    ALGORITHM = 'RSA_OAEP',
  //    ENCRYPTED_VALUE = 0x{BitConverter.ToString(encryptedCek).Replace("-", "")}
  //)";

  //      await using var cekCmd = new SqlCommand(cekSql, connection);
  //      await cekCmd.ExecuteNonQueryAsync();
  //    }
  //  }
}