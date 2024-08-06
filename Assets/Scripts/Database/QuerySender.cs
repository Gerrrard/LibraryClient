using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class QuerySender
{
    public List<IDictionary<string, string>> SendQuery(MySqlCommand mySqlCommand, QueryType queryType)
    {
        List<IDictionary<string, string>> ret = new List<IDictionary<string, string>>();

        try
        {
            if (queryType == QueryType.search)
            {
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
                DataTable schema = reader.GetSchemaTable();
                List<string> headers = new List<string>();

                foreach (DataRow rdrColumn in schema.Rows)
                {
                    String columnName = rdrColumn[schema.Columns["ColumnName"]].ToString();
                    headers.Add(columnName);
                }

                while (reader.Read())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    int i = 0;

                    foreach (DataRow head in schema.Rows)
                    {
                        String columnName = head[schema.Columns["ColumnName"]].ToString();
                        dict[columnName] = reader[i].ToString();
                        i++;
                    }

                    ret.Add(dict);
                }

                reader.Close();
            }
            else
            {
                int rowsAffected = mySqlCommand.ExecuteNonQuery();

                ret.Add(new Dictionary<string, string> { { "rowsAffected", rowsAffected.ToString() } });
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        return ret;
    }
}