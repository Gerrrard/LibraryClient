using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Line models creator
public interface IDictInfoFactory<T> where T : IDictInfoGet
{
    public T Create(IDictionary<string, string> data);

    public List<T> CreateManyLines(List<IDictionary<string, string>> data);
}
