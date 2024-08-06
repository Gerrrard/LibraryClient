using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;



public class WindowModelSelector
{   
    /*
    private WindowSelected _curWindowSelected;

    public void InitSelector(WindowView windowView)
    {
        windowView.SetWindowModelSelector(this);

        ChangeWindow(WindowSelected.books);
    }

    public void ChangeWindow(WindowSelected windowSelected)
    {
        if (_curWindowSelected != WindowSelected.notSelected)

        _curWindowSelected = windowSelected;
    }

    public WindowSelected GetWindowSelected()
    {
        return _curWindowSelected;
    }*/
}




/*public void InitSelector(WindowView windowView,
        IWindowModelInfo booksWindow,
        IWindowModelInfo usersWindow,
        IWindowModelInfo transactionsWindow)
    {
    windowView.SetWindowModelSelector(this);
    OnWindowModelChange += windowView.RenderWindowModel;

    //windowDict[WindowSelected.books] = booksWindow;
    //windowDict[WindowSelected.users] = usersWindow;
    //windowDict[WindowSelected.transactions] = transactionsWindow;

    ChangeWindow(WindowSelected.books);
}

public void ChangeWindow(WindowSelected windowSelected)
{
    if (_curWindowSelected != WindowSelected.notSelected)
        //windowDict[_curWindowSelected].ExitWindow();

        //windowDict[windowSelected].EnterWindow();

        //OnWindowModelChange?.Invoke(windowDict[windowSelected]);

        _curWindowSelected = windowSelected;
}

public WindowSelected GetWindowSelected()
{
    return _curWindowSelected;
}
*/