namespace FileSystem.Repository.Interface;
internal interface IConfigurationFactory
{
    T Create<T>() where T : IConfigurationBase;
}
