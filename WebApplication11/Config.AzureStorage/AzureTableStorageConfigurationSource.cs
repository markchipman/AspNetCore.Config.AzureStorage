using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication11.Config.AzureStorage
{
    internal class AzureTableStorageConfigurationSource : IConfigurationSource
    {
        public CloudTableClient Client { get; set; }

        public string TableName { get; set; }

        public string PartitionKey { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureTableStorageConfigurationProvider(Client, TableName, PartitionKey);
        }
    }
}
