using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgramStateOperator : MonoBehaviour
{
    [SerializeField] private WindowView windowView;
    //[SerializeField] private WindowModelSelector windowModelSelector;

    private DBOperator _dbOperator;
    private string connStr = "server=localhost;port=3306;database=librarydb;user=root;password=rootpassword;";

    public void Init(DBOperator dbOperator)
    {
        _dbOperator = dbOperator;

        //var booksW = windowModelCreator.CreateBooksWindow();
        //var usersW = windowModelCreator.CreateUsersWindow();
        //var transactionsW = windowModelCreator.CreateTransactionsWindow();

        //windowModelSelector = new WindowModelSelector();
        //windowModelSelector.InitSelector(windowView);
    }

    public List<IDictionary<string, string>> SendQuery(QueryContext queryContext)
    {
        _dbOperator.ConnectToDB(connStr);

        var returnVal = _dbOperator.SendQuery(queryContext);

        _dbOperator.Disconnect();

        return returnVal;
    }

    public void ExitAppButton()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        windowView.ClearLineViews();
        _dbOperator.Disconnect();
    }
}
