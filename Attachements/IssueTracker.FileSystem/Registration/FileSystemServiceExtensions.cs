using FileSystem.Repository;
using FileSystem.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FileSystem.Registration;

public static class FileSystemServiceExtensions
{
    public static IServiceCollection AddFileSystemServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileProvider, FileProvider>();
        return services;
    }
}
