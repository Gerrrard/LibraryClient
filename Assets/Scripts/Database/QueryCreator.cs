using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
public enum QueryType
{
    insert,
    search, 
    update,
    delete
}

public class QueryCreator
{
    private string insertQuery = "INSERT INTO @Table_name (@Headers_names) VALUES (@Values)";
    private string searchQuery = "SELECT * FROM @Table_name @Conditions";
    private string updateQuery = "UPDATE @Table_name SET @KeyEqValue_line WHERE id = @Item_Id";
    private string deleteQuery = "DELETE FROM @Table_name WHERE id = @Item_Id";

    private Dictionary<QueryType, string> queryTypeString = new Dictionary<QueryType, string>();



    public QueryCreator()
    {
        queryTypeString[QueryType.insert] = insertQuery;
        queryTypeString[QueryType.search] = searchQuery;
        queryTypeString[QueryType.update] = updateQuery;
        queryTypeString[QueryType.delete] = deleteQuery;
    }

    public MySqlCommand CreateQuery(MySqlConnection connection, QueryContext queryData)
    {
        string queryTable = queryData.TableName;
        QueryType queryType = queryData.QueryType;
        IDictionary<string, string> parameterValues = queryData.ParameterKeyValue;

        StringBuilder sb = new StringBuilder();
        MySqlCommand command = null;

        switch (queryType)
        {
            case QueryType.insert:
                string headers = CreateCommaKeyLine(parameterValues.Keys, sb);
                string values = CreateCommaValueLine(parameterValues.Values, sb);

                command = CreateInsertQuery(connection, queryTable, headers, values);
                break;
            case QueryType.search:
                string conditions = CreateSearchLine(parameterValues, sb);
                
                command = CreateSearchQuery(connection, queryTable, conditions);
                break;
            case QueryType.update:
                string itemIdUpd = parameterValues["id"];

                string updates = CreateUpdateLine(parameterValues, sb);

                command = CreateUpdateQuery(connection, queryTable, updates, itemIdUpd);
                break;
            case QueryType.delete:
                string itemIdDel = parameterValues["id"];

                command = CreateDeleteQuery(connection, queryTable, itemIdDel);
                break;
            default:
                Debug.Log("No such QueryType");
                throw new NotImplementedException();
        }

        Debug.Log(command.CommandText);
        return command;
    }



    private MySqlCommand CreateQuery(MySqlConnection connection, QueryType queryType, Dictionary<string, string> parameters)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;

        StringBuilder sb = new StringBuilder();
        sb.Append(queryTypeString[queryType]);

        foreach(var keyString in parameters.Keys)
        {
            sb.Replace(keyString, parameters[keyString]);
        }

        cmd.CommandText = sb.ToString();

        sb.Clear();

        return cmd;
    }

    private MySqlCommand CreateInsertQuery(MySqlConnection connection, string tableName, string headersNames, string values)
    {
        Dictionary<string, string> parameterValues = new Dictionary<string, string>();
        parameterValues["@Table_name"] = tableName;
        parameterValues["@Headers_names"] = headersNames;
        parameterValues["@Values"] = values;

        return CreateQuery(connection, QueryType.insert, parameterValues);
    }

    private MySqlCommand CreateSearchQuery(MySqlConnection connection, string tableName, string conditions)
    {
        Dictionary<string, string> parameterValues = new Dictionary<string, string>();
        parameterValues["@Table_name"] = tableName;
        parameterValues["@Conditions"] = conditions;

        return CreateQuery(connection, QueryType.search, parameterValues);
    }

    private MySqlCommand CreateUpdateQuery(MySqlConnection connection, string tableName, string keyEqValueLine, string itemId)
    {
        Dictionary<string, string> parameterValues = new Dictionary<string, string>();
        parameterValues["@Table_name"] = tableName;
        parameterValues["@KeyEqValue_line"] = keyEqValueLine;
        parameterValues["@Item_Id"] = itemId;

        return CreateQuery(connection, QueryType.update, parameterValues);
    }

    private MySqlCommand CreateDeleteQuery(MySqlConnection connection, string tableName, string itemId)
    {
        Dictionary<string, string> parameterValues = new Dictionary<string, string>();
        parameterValues["@Table_name"] = tableName;
        parameterValues["@Item_Id"] = itemId;

        return CreateQuery(connection, QueryType.delete, parameterValues);
    }

    private string CreateCommaValueLine(ICollection<string> valueCollection, StringBuilder sb)
    {
        var valueArray = valueCollection.ToArray();

        for (int i  = 0; i < valueCollection.Count; i++)
        {
            sb.Append('\"');
            sb.Append(valueArray[i]);
            sb.Append('\"');
            if (i != valueCollection.Count - 1)
                sb.Append(", ");
        }

        string retValue = sb.ToString();
        sb.Clear();

        return retValue;
    }
    
    private string CreateCommaKeyLine(ICollection<string> valueCollection, StringBuilder sb)
    {
        var valueArray = valueCollection.ToArray();

        for (int i = 0; i < valueCollection.Count; i++)
        {
            sb.Append(valueArray[i]);
            if (i != valueCollection.Count - 1)
                sb.Append(", ");
        }

        string retValue = sb.ToString();
        sb.Clear();

        return retValue;
    }

    private string CreateSearchLine(IDictionary<string, string> searchKeyValue, StringBuilder sb)
    {
        List<string> conditionStrings = new List<string>();

        bool isCarringAnyInfo = false;
        foreach (string data in searchKeyValue.Values)
        {
            if (!string.IsNullOrEmpty(data))
            {
                isCarringAnyInfo = true;
                break;
            }
        }
        if (!isCarringAnyInfo) return "";

        sb.Append("WHERE ");

        foreach (var key in searchKeyValue.Keys)
        {
            if (string.IsNullOrEmpty(searchKeyValue[key])) continue;

            conditionStrings.Add(key.ToString() + " RLIKE \"" + searchKeyValue[key] + '\"');
        }

        for (int i = 0; i < conditionStrings.Count; i++)
        {
            sb.Append(conditionStrings[i]);
            if (i != conditionStrings.Count - 1)
                sb.Append(" AND ");
        }

        string retValue = sb.ToString();
        sb.Clear();

        return retValue;
    }

    private string CreateUpdateLine(IDictionary<string, string> updateKeyValue, StringBuilder sb)
    {
        List<string> updateStrings = new List<string>();

        foreach (var key in updateKeyValue.Keys)
        {
            if (string.IsNullOrEmpty(updateKeyValue[key])) continue;

            string conditionString = key.ToString() + " = \"" + updateKeyValue[key] + '\"';
            updateStrings.Add(conditionString);
        }

        for (int i = 0; i < updateStrings.Count; i++)
        {
            sb.Append(updateStrings[i]);
            if (i != updateStrings.Count - 1)
                sb.Append(", ");
        }

        string retValue = sb.ToString();

        sb.Clear();

        return retValue;
    }
}