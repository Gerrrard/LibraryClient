using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ButtonTestActivator : MonoBehaviour
{
    [SerializeField] private SearchLinesHolder searchView;
    [SerializeField] private ResultLinesHolder resultView;
    [SerializeField] private ChangeLinesHolder changeView;
    [SerializeField] private GameObject searchLinePrefab;
    [SerializeField] private GameObject resultLinePrefab;
    [SerializeField] private GameObject changeLinePrefab;
    
    public void ButtonTest()
    {
        string connStr = "server=localhost;port=3306;database=librarydb;user=root;password=rootpassword;";

        DBOperator dbOperator = new DBOperator();
        dbOperator.ConnectToDB(connStr);

        Dictionary<string, string> data = new() { };

        QueryContext queryContext = new QueryContext("librarydb.books", QueryType.search, data);

        var queryResult = dbOperator.SendQuery(queryContext);

        if(queryResult != null && queryResult.Count == 1 && queryResult[0].ContainsKey("rowsAffected")) { Debug.Log(queryResult[0]["rowsAffected"]); }
        else
        {
            MainModelsCreator<Book> mainModelsCreator = new MainModelsCreator<Book>();
            List<Book> queryObjects = (List<Book>)mainModelsCreator.CreateManyLines(queryResult);

            LineModelCreator lineModelCreator = new LineModelCreator();
            LineViewCreator lineViewCreator = new LineViewCreator();

            foreach (IQueryObject queryObject in queryObjects)
            {
                var info = queryObject.GetDictInfo();

                IDictInfoGet testDictLine2 = lineModelCreator.Create(queryObject.GetDictInfo());
                var line1 = lineViewCreator.CreateLineView(searchLinePrefab, testDictLine2);

                ResultInfoLine line2 = (ResultInfoLine)lineViewCreator.CreateLineView(resultLinePrefab, testDictLine2);

                var line3 = lineViewCreator.CreateLineView(changeLinePrefab, testDictLine2);

                searchView.AddToView(line1);
                resultView.AddToView(line2);
                changeView.AddToView(line3);
            }
        }

        dbOperator.Disconnect();
    }

    public void ButtonTest2()
    {
        searchView.ClearLines();
        resultView.ClearLines();
        changeView.ClearLines();
    }
}
