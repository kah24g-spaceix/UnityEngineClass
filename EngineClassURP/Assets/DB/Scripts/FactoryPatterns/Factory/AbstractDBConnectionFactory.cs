using Mono.Data.Sqlite;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;


public interface IDBConnectionFactory
{
    IDbConnection CreateConnection();
}
public class SQLiteDBConnectionFactory : IDBConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        string connectionString
            = "URI=file:"
            + Application.streamingAssetsPath
            + "/GameData.db";

        return new SqliteConnection(connectionString);
    }
}