using System;
using System.Data;
using UnityEngine;

public class DBAccess
{
    //void ???? (IDataReader reader)
    public void Read(IDbConnection pConnection, Action<IDataReader> pReader, String pSQL)
    {
        IDbConnection connection = pConnection;
        connection.Open();

        if (connection.State != ConnectionState.Open)
        {
            Debug.LogError("Connection Failed");
            return;
        }

        IDbCommand command = connection.CreateCommand();
        command.CommandText = pSQL;

        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            pReader.Invoke(reader);
        }
        reader.Dispose();
        command.Dispose();
        connection.Close();
    }
}