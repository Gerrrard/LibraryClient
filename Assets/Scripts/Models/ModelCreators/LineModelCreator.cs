using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineModelCreator : IDictInfoFactory<LineModel>
{
    public LineModel Create(IDictionary<string, string> data)
    {
        return new LineModel(data);
    }

    public List<LineModel> CreateManyLines(List<IDictionary<string, string>> data)
    {
        List<LineModel> dictInfoGets = new List<LineModel>();

        foreach (var dict in data)
        {
            dictInfoGets.Add(Create(dict));
        }

        return dictInfoGets;
    }
}
