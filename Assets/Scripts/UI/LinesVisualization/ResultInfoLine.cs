using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultInfoLine : MonoBehaviour, ILineAddable
{
    [SerializeField] private GameObject textLayoutGameObject;
    [SerializeField] private GameObject textElementPrefab;

    [SerializeField] private Button removeButton;
    [SerializeField] private Button updateButton;

    private List<TextInfo> textInfoPerLine = new List<TextInfo>();

    public void BindButtons(WindowView windowView, int id)
    {
        removeButton.onClick.AddListener(() => windowView.DeleteButtonPressed(id));
        updateButton.onClick.AddListener(() => windowView.ToUpdateButtonPressed(id));
    }

    public void AddToView(GameObject parent)
    {
        this.transform.SetParent(parent.transform, false);
    }

    public void RemoveFromView()
    {
        ClearTextInfos();

        textInfoPerLine.Clear();
        this.transform.SetParent(null);

        removeButton.onClick.RemoveAllListeners();
        updateButton.onClick.RemoveAllListeners();

        Destroy(this.gameObject);
    }

    public void SetLineInfo(IDictInfoGet dictInfoGet)
    {
        ClearTextInfos();

        var dictValues = dictInfoGet.GetDictInfo().Values;

        foreach (var kvp in dictValues)
        {
            textInfoPerLine.Add(CreateTextInfo(kvp, textLayoutGameObject));
        }
    }

    private TextInfo CreateTextInfo(string textValue, GameObject textValuesParent)
    {
        GameObject textInfoGO = GameObject.Instantiate(textElementPrefab);
        TextInfo textInfo = textInfoGO.GetComponent<TextInfo>();

        textInfo.SetText(textValue);
        textInfoGO.transform.SetParent(textValuesParent.transform, false);

        return textInfo;
    }

    private void ClearTextInfos()
    {
        for (int i = textInfoPerLine.Count - 1; i >= 0; i--)
        {
            RemoveFromResultView(textInfoPerLine[i]);
        }
    }

    private void RemoveFromResultView(TextInfo textInfo)
    {
        textInfoPerLine.Remove(textInfo);
        textInfo.SetText(string.Empty);
        textInfo.transform.SetParent(null, false);

        Destroy(textInfo.gameObject);
    }

    public IDictInfoGet GetLineModelInfo() => null;
}
