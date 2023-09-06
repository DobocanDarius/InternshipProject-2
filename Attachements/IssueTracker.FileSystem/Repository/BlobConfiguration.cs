﻿using FileSystem.Repository.Interface;

namespace FileSystem.Repository;
public class BlobConfiguration : IBlobConfigurationFactory
{
    public string Container
    { get; }

    public string ConnectionString
    { get; }

    public string AccountName
    { get; }

    public string AccountKey
    { get; }

    public BlobConfiguration(string container, string connstring, string accountName, string accountKey)
    {
        Container = container;
        ConnectionString = connstring;
        AccountName = accountName;
        AccountKey = accountKey;
    }
    
}