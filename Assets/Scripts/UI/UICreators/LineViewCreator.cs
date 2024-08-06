using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LineViewCreator
{
    public ILineAddable CreateLineView(GameObject prefab, IDictInfoGet lineModel)
    {
        GameObject lineInstance = GameObject.Instantiate(prefab);
        ILineAddable lineView = lineInstance.GetComponent<ILineAddable>();

        lineView.SetLineInfo(lineModel);

        return lineView;
    }
}
