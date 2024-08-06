using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInputLine : MonoBehaviour, ILineAddable
{
    [SerializeField] private TextMeshProUGUI lineName;
    [SerializeField] private TextMeshProUGUI placeHolderText;
    [SerializeField] private TMP_InputField userInputText;

    private IDictInfoGet _lineModel;

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
        userInputText.text = dict[lineNaming];
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
