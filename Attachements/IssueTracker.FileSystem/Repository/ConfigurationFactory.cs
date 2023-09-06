using FileSystem.Repository.Interface;
using Microsoft.Extensions.Configuration;

namespace FileSystem.Repository;
internal class ConfigurationFactory : IConfigurationFactory
{
    readonly IConfiguration _Config;
    internal ConfigurationFactory(IConfiguration config)
    {
        _Config = config;
    }
    public T Create<T>() where T : IConfigurationBase
    {
        if (typeof(T) == typeof(IMetaDataConfiguration))
        {
            var connstring = _Config.GetValue<string>("ConnectionStrings:Account");
            var azureTable = _Config.GetValue<string>("ConnectionStrings:AzureTable");
            return (T)(new MetaDataConfiguration(connstring, azureTable) as IMetaDataConfiguration);

        }
        if (typeof(T) == typeof(IBlobConfigurationFactory))
        {
            var connstring = _Config.GetValue<string>("ConnectionStrings:Account");
            var container = _Config.GetValue<string>("ConnectionStrings:Container");
            var accountName = _Config.GetValue<string>("ConnectionStrings:AccountName");
            var accountKey = _Config.GetValue<string>("ConnectionStrings:AccountKey");
            return (T)(new BlobConfiguration(container, connstring, accountName, accountKey) as IBlobConfigurationFactory);
        }
        throw new InvalidOperationException();
    }

}
