using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace FileSystem.Repository.Interface;
internal interface IBlobConfigurationFactory : IConfigurationBase
{
    string Container { get; }
    string AccountName { get; }
    string AccountKey { get; }
}
internal interface IConfigurationBase
{
    string ConnectionString { get; }
}
internal interface IMetaDataConfiguration : IConfigurationBase
{
    string AzureTable { get; }
}
