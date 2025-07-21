using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;


public class DBConnectionFactoryMethod : DBGameDataGateWay_AbstractFactory
{
    protected override IDbConnection CreateConnection()
    {
        string connectionString
            = "URI=file:"
            + Application.streamingAssetsPath
            + "/GameData.db";

        return new SqliteConnection(connectionString);
    }
}