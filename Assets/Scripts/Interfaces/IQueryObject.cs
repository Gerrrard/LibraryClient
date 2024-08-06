using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueryObject : IDictInfoGet
{
    public void SetFromDict(IDictionary<string, string> dict);
}
