using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineModel : IDictInfoGet
{
    IDictionary<string, string> dataDict;

    public LineModel(IQueryObject queryObject)
    {
        dataDict = queryObject.GetDictInfo();
    }

    public LineModel(IDictionary<string, string> lineData)
    {
        dataDict = lineData;
    }

    public IDictionary<string, string> GetDictInfo()
    {
        return dataDict;
    }

    public void SetInputValue(string value)
    {
        dataDict[dataDict.Keys.First()] = value;
    }
}
