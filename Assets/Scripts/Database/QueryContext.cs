using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;

public class QueryContext
{
    public string TableName { get; private set; }
    public IDictionary<string, string> ParameterKeyValue { get; private set; }
    public QueryType QueryType { get; private set; }

    public QueryContext(string tableName, QueryType queryType, IDictionary<string, string> parameterKeyValue)
    {
        this.TableName = tableName;
        this.ParameterKeyValue = parameterKeyValue;
        this.QueryType = queryType;
    }
}
