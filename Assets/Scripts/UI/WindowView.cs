using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Search;
using UnityEngine;


public enum WindowSelected
{
    notSelected,
    books,
    users,
    transactions
}

public class WindowView : MonoBehaviour
{
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject updateButton;

    [SerializeField] private SearchLinesHolder searchLinesHolder;
    [SerializeField] private ResultLinesHolder resultLinesHolder;
    [SerializeField] private ChangeLinesHolder changeLinesHolder;

    [SerializeField] private GameObject searchLinePrefab;
    [SerializeField] private GameObject resultLinePrefab;
    [SerializeField] private GameObject changeLinePrefab;

    [SerializeField] private ProgramStateOperator programStateOperator;

    [SerializeField] private TextMeshProUGUI rowsAffectedInfo;

    //private WindowModelSelector _windowModelSelector;
    private WindowSelected _curWindowSelected;
    private string _currentTable;

    private LineModelCreator lineModelCreator = new LineModelCreator();
    private LineViewCreator lineViewCreator = new LineViewCreator();

    public void Start()
    {
        SwitchWindow(1);
    }

    public void AddButtonPressed()
    {
        //changeLinesHolder.ClearLines();

        var inputs = changeLinesHolder.GetAllUserInput();
        Dictionary<string, string> queryUserData = new() { };

        foreach (var line in inputs)
        {
            var dict = line.GetDictInfo();
            queryUserData[dict.Keys.First()] = dict[dict.Keys.First()];
        }

        QueryContext queryContext = new QueryContext(_currentTable, QueryType.insert, queryUserData);

        var queryResult = programStateOperator.SendQuery(queryContext);

        if (queryResult != null && queryResult.Count == 1 && queryResult[0].ContainsKey("rowsAffected"))
            rowsAffectedInfo.text = (queryResult[0]["rowsAffected"]);

        SearchButtonPressed();
    }

    public void SearchButtonPressed()
    {
        resultLinesHolder.ClearLines();

        var inputs = searchLinesHolder.GetAllUserInput();
        Dictionary<string, string> queryUserData = new() { };

        foreach (var line in inputs)
        {
            var dict = line.GetDictInfo();
            queryUserData[dict.Keys.First()] = dict[dict.Keys.First()];
        }

        QueryContext queryContext = new QueryContext(_currentTable, QueryType.search, queryUserData);

        var queryResult = programStateOperator.SendQuery(queryContext);

        if (queryResult != null)
        {
            List<IQueryObject> queryObjects = GetQueryResultLines(queryResult);

            foreach (IQueryObject queryObject in queryObjects)
            {
                var info = queryObject.GetDictInfo();

                IDictInfoGet lineModelInfo = lineModelCreator.Create(info);
                ResultInfoLine resultLine = (ResultInfoLine)lineViewCreator.CreateLineView(resultLinePrefab, lineModelInfo);

                resultLinesHolder.AddToView(resultLine);
                resultLine.BindButtons(this, int.Parse(info["id"]));
            }
        }
    }

    public void ToUpdateButtonPressed(int itemId)
    {
        changeLinesHolder.ClearLines();

        Dictionary<string, string> queryUserData = new() { { "id", itemId.ToString() } };

        QueryContext queryContext = new QueryContext(_currentTable, QueryType.search, queryUserData);

        var queryResult = programStateOperator.SendQuery(queryContext);

        if (queryResult != null)
        {
            List<IQueryObject> queryObjects = GetQueryResultLines(queryResult);

            var lineDictInfo = queryObjects[0].GetDictInfo();

            foreach (var item in lineDictInfo.Keys)
            {
                Dictionary<string, string> lineData = new() { { item, lineDictInfo[item] } };

                IDictInfoGet lineModel = lineModelCreator.Create(lineData);
                ChangeInputLine resultLine = (ChangeInputLine)lineViewCreator.CreateLineView(changeLinePrefab, lineModel);

                changeLinesHolder.AddToView(resultLine);
            }
        }
    }

    public void UpdateButtonPressed()
    {
        var inputs = changeLinesHolder.GetAllUserInput();
        Dictionary<string, string> queryUserData = new() { };

        foreach (var line in inputs)
        {
            var dict = line.GetDictInfo();
            queryUserData[dict.Keys.First()] = dict[dict.Keys.First()];
        }

        QueryContext queryContext = new QueryContext(_currentTable, QueryType.update, queryUserData);

        var queryResult = programStateOperator.SendQuery(queryContext);

        if (queryResult != null && queryResult.Count == 1 && queryResult[0].ContainsKey("rowsAffected"))
            rowsAffectedInfo.text = (queryResult[0]["rowsAffected"]);

        SearchButtonPressed();
    }

    public void DeleteButtonPressed(int itemId)
    {
        Dictionary<string, string> queryUserData = new() { { "id", itemId.ToString() } };

        QueryContext queryContext = new QueryContext(_currentTable, QueryType.delete, queryUserData);

        var queryResult = programStateOperator.SendQuery(queryContext);

        if (queryResult != null && queryResult.Count == 1 && queryResult[0].ContainsKey("rowsAffected"))
            rowsAffectedInfo.text = (queryResult[0]["rowsAffected"]);

        SearchButtonPressed();
    }

    public void SwitchWindow(int windowSelected)
    {
        _curWindowSelected = (WindowSelected)windowSelected;

        switch ((WindowSelected)windowSelected)
        {
            case WindowSelected.books:
                _currentTable = "librarydb.books";

                InitWindowLines(_curWindowSelected);
                break;
            case WindowSelected.users:
                _currentTable = "librarydb.users";

                InitWindowLines(_curWindowSelected);
                break;
            case WindowSelected.transactions:
                _currentTable = "librarydb.transactions";

                InitWindowLines(_curWindowSelected);
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public void ClearLineViews()
    {
        resultLinesHolder.ClearLines();
        searchLinesHolder.ClearLines();
        changeLinesHolder.ClearLines();

        rowsAffectedInfo.text = string.Empty;
    }

    private void InitWindowLines(WindowSelected windowSelected)
    {
        ClearLineViews();

        LineModelCreator lineModelCreator = new LineModelCreator();
        LineViewCreator lineViewCreator = new LineViewCreator();

        IDictInfoGet fakeObject = null;

        switch (_curWindowSelected)
        {
            case WindowSelected.users:
                fakeObject = new User();
                break;
            case WindowSelected.books:
                fakeObject = new Book();
                break;
            case WindowSelected.transactions:
                fakeObject = new Transaction();
                break;
            default:
                throw new System.NotImplementedException();
        }

        InitSearchLines(fakeObject);
        InitChangeLines(fakeObject);

        SearchButtonPressed();
    }

    private void InitSearchLines(IDictInfoGet fakeObject)
    {
        var info = fakeObject.GetDictInfo();

        foreach (var item in info.Keys)
        {
            Dictionary<string, string> lineData = new() { { item, "" } };

            IDictInfoGet lineModel = lineModelCreator.Create(lineData);
            SearchInputLine resultLine = (SearchInputLine)lineViewCreator.CreateLineView(searchLinePrefab, lineModel);

            searchLinesHolder.AddToView(resultLine);
        }
    }

    private void InitChangeLines(IDictInfoGet fakeObject)
    {
        var info = fakeObject.GetDictInfo();

        foreach (var item in info.Keys)
        {
            Dictionary<string, string> lineData = new() { { item, "" } };

            IDictInfoGet lineModel = lineModelCreator.Create(lineData);
            ChangeInputLine resultLine = (ChangeInputLine)lineViewCreator.CreateLineView(changeLinePrefab, lineModel);

            changeLinesHolder.AddToView(resultLine);
        }
    }

    private List<IQueryObject> GetQueryResultLines(List<IDictionary<string, string>> queryResult)
    {
        List<IQueryObject> queryObjects = new List<IQueryObject>();

        switch (_curWindowSelected)
        {
            case WindowSelected.users:
                MainModelsCreator<User> mainModelsCreator1 = new MainModelsCreator<User>();
                queryObjects = mainModelsCreator1.CreateManyLines(queryResult).Cast<IQueryObject>().ToList();
                break;
            case WindowSelected.books:
                MainModelsCreator<Book> mainModelsCreator2 = new MainModelsCreator<Book>();
                queryObjects = mainModelsCreator2.CreateManyLines(queryResult).Cast<IQueryObject>().ToList();
                break;
            case WindowSelected.transactions:
                MainModelsCreator<Transaction> mainModelsCreator3 = new MainModelsCreator<Transaction>();
                queryObjects = mainModelsCreator3.CreateManyLines(queryResult).Cast<IQueryObject>().ToList();
                break;
        }

        return queryObjects;
    }

}
