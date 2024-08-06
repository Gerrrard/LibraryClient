using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Line visualised
public interface ILineAddable
{
    public void SetLineInfo(IDictInfoGet dictInfoGet);

    public IDictInfoGet GetLineModelInfo();

    public void AddToView(GameObject parent);

    public void RemoveFromView();
}
