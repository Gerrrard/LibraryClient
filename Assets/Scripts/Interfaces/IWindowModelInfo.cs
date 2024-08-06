using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refused to use for now
public interface IWindowModelInfo
{
    public void InitWindowModel(WindowSelected windowSelected);

    public void ExitWindow();

    public void EnterWindow();
}