using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractScrollView<T> : MonoBehaviour, ILinesContentView where T : ILineAddable
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private List<T> addedLines;

    public void Awake()
    {
        addedLines = new List<T>();
    }

    public void AddToView(ILineAddable addableLine)
    {
        addedLines.Add((T)addableLine);
        addableLine.AddToView(contentParent);
    }

    public void RemoveFromView(ILineAddable addableLine)
    {
        addedLines.Remove((T)addableLine);
        addableLine.RemoveFromView();
    }

    public void ClearLines()
    {
        for(int i = addedLines.Count - 1; i >= 0; i--)
        {
            RemoveFromView(addedLines[i]);
        }
    }

    public void AddLines(List<ILineAddable> addableLines)
    {
        foreach(ILineAddable line in addableLines)
            AddToView(line);
    }

    public List<IDictInfoGet> GetAllUserInput()
    {
        List<IDictInfoGet> userInputs = new List<IDictInfoGet>();

        foreach (ILineAddable line in addedLines)
            userInputs.Add(line.GetLineModelInfo());

        return userInputs;
    }
}
