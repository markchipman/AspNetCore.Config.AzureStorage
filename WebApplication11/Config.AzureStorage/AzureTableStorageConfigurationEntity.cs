using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication11.Config.AzureStorage
{
    public class AzureTableStorageConfigurationEntity : TableEntity
    {
        public string Value { get; set; }
    }
}
