using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InputFieldFormatter : MonoBehaviour
{
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(FormatTextOnEndEdit);
        inputField.onDeselect.AddListener(delegate { FormatText(inputField.text); });
    }

    private void FormatTextOnEndEdit(string input)
    {
        FormatText(input);
    }

    private void FormatText(string input)
    {
        string formattedText = FormatNumber(input);
        inputField.text = formattedText; ;
    }

    private string FormatNumber(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if(decimal.TryParse(input, out decimal number)) 
        {
            return string.Format("{0:N0}", number);
        }

        return input;

    }
}
