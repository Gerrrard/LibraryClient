using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SearchInputLine : MonoBehaviour, ILineAddable
{
    [SerializeField] private TextMeshProUGUI lineName;
    [SerializeField] private TextMeshProUGUI placeHolderText;
    [SerializeField] private TMP_InputField userInputText;

    IDictInfoGet _lineModel;

    public void AddToView(GameObject parent)
    {
        this.transform.SetParent(parent.transform, false);
    }

    public void RemoveFromView()
    {
        lineName.text = string.Empty;
        placeHolderText.text = string.Empty;
        userInputText.text = string.Empty;

        this.transform.SetParent(null);

        Destroy(this.gameObject);
    }

    public void SetLineInfo(IDictInfoGet dictInfoGet)
    {
        _lineModel = dictInfoGet;

        var dict = dictInfoGet.GetDictInfo();
        string lineNaming = dict.Keys.First();

        lineName.text = lineNaming;
        placeHolderText.text = dict[lineNaming];
    }

    public void ChangeLineModel()
    {
        try
        {
            var lineModel = (LineModel)_lineModel;
            lineModel.SetInputValue(GetUserInput());
        }
        catch
        {
            throw new System.InvalidCastException();
        }
    }

    public string GetUserInput()
    {
        return userInputText.text;
    }

    public IDictInfoGet GetLineModelInfo() => _lineModel;
}
