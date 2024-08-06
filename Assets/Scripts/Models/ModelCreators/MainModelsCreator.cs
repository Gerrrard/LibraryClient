using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModelsCreator<T> : IDictInfoFactory<T> where T : IQueryObject, new()
{
    public MainModelsCreator() { }

    public T Create(IDictionary<string, string> data)
    {
        T model = new T();
        model.SetFromDict(data);

        return model;
    }

    public List<T> CreateManyLines(List<IDictionary<string, string>> data)
    {
        var returnList = new List<T>();
        foreach (var line in data)
        {
            T model = Create(line);
            returnList.Add(model);
        }

        return returnList;
    }
}
