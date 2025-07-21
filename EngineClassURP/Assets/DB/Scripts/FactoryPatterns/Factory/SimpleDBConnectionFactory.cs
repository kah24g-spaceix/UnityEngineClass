using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class SimpleDBConnectionFactory
{
    public IDbConnection CreateConnection(string pType)
    {
        if (pType == "SQLITE")
        {
            string connectionString
                = "URI=file:"
                + Application.streamingAssetsPath
                + "/GameData.db";

            return new SqliteConnection(connectionString);
        }
        else return null;
    }
}