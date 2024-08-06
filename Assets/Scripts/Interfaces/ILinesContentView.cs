using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lines visualization container
public interface ILinesContentView
{
    public void AddToView(ILineAddable infoDict);
    public void RemoveFromView(ILineAddable addableLine);
    public void AddLines(List<ILineAddable> addableLines);
    public void ClearLines();
}
