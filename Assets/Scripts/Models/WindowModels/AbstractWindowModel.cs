using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refused to use, too much for the task
public abstract class AbstractWindowModel : IWindowModelInfo
{
    protected LineModelCreator _lineModelCreator;

    protected List<LineModel> _searchLines;
    protected List<LineModel> _resultLines;
    protected List<LineModel> _changeLines;

    protected WindowSelected _currentWindowRole;

    protected abstract void InitSpecifics();

    protected abstract void EnterSpecifics();

    protected abstract void ExitSpecifics();

    public void EnterWindow()
    {


        EnterSpecifics();
    }

    public void ExitWindow()
    {
        

        ExitSpecifics();
    }

    public void InitWindowModel(WindowSelected currentWindowRole)
    {
        _currentWindowRole = currentWindowRole;

        _searchLines = new List<LineModel>();
        _resultLines = new List<LineModel>();
        _changeLines = new List<LineModel>();

        _lineModelCreator = new LineModelCreator();

        InitSpecifics();
    }
}
