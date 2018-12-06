using System;

using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace WebApplication11.Config.AzureStorage
{
    public static class AzureTableStorageConfigurationExtensions
    {
        public static IConfigurationBuilder AddAzureTableStorage(this IConfigurationBuilder configurationBuilder, string connectionString, string tableName, string partitonKey)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            return AddAzureTableStorage(configurationBuilder, CloudStorageAccount.Parse(connectionString), tableName, partitonKey);
        }

        public static IConfigurationBuilder AddAzureTableStorage(this IConfigurationBuilder configurationBuilder, CloudStorageAccount storageAccount, string tableName, string partitonKey)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (storageAccount == null)
            {
                throw new ArgumentNullException(nameof(storageAccount));
            }

            if (tableName == null)
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            if (partitonKey == null)
            {
                throw new ArgumentNullException(nameof(partitonKey));
            }

            configurationBuilder.Add(new AzureTableStorageConfigurationSource
            {
                Client = storageAccount.CreateCloudTableClient(),
                TableName = tableName,
                PartitionKey = partitonKey
            });

            return configurationBuilder;
        }
    }
}
