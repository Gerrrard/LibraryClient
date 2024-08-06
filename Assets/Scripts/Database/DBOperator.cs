using MySqlConnector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBOperator
{
    private QueryConnector queryConnector;
    private QueryCreator queryCreator;
    private QuerySender querySender;

    private MySqlConnection connection;

    public DBOperator()
    {
        queryConnector = new QueryConnector();
        queryCreator = new QueryCreator();
        querySender = new QuerySender();
    }

    public void ConnectToDB(string connectionString)
    {
        connection = queryConnector.CreateConnection(connectionString);
    }

    public List<IDictionary<string,string>> SendQuery(QueryContext queryContext)
    {
        return querySender.SendQuery(queryCreator.CreateQuery(connection, queryContext), queryContext.QueryType);
    }

    public void Disconnect()
    {
        queryConnector.CloseConnection(connection);
    }
}
