using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lineValue;

    public void SetText(string text)
    {
        lineValue.text = text;
    }
}
