using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class QueryConnector
{
    public MySqlConnection CreateConnection(string connectionString)
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    public void CloseConnection(MySqlConnection conn)
    {
        if (conn != null)
            conn.Close();
    }
}
