using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication11.Config.AzureStorage
{
    internal class AzureTableStorageConfigurationProvider : ConfigurationProvider
    {
        public AzureTableStorageConfigurationProvider(CloudTableClient client, string tableName, string partitionKey)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _partitionKey = partitionKey ?? throw new ArgumentNullException(nameof(partitionKey));
        }

        private readonly CloudTableClient _client;
        private readonly string _tableName;
        private readonly string _partitionKey;

        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var table = _client.GetTableReference(_tableName);

            var query = new TableQuery<AzureTableStorageConfigurationEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partitionKey));

            var records = await table.ExecuteQuerySegmentedAsync(query, null).ConfigureAwait(false);

            foreach (var record in records)
            {
                data.Add(record.RowKey, record.Value);
            }

            Data = data;
        }
    }
}
