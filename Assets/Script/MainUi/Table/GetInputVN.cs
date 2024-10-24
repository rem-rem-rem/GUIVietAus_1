using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInputVN : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    string ime;
    string currentString;

    private void Start()
    {
        _inputField.onEndEdit.AddListener(arg0 =>
        {
            if (!string.IsNullOrEmpty(ime))
            {
                _inputField.text = ime;
            }
            ime = "";
        });
    }

    void Update()
    {
        if (Input.compositionString != "")
        {
            ime = _inputField.text;
            ime = ime.Insert(_inputField.caretPosition - Input.compositionString.Length, Input.compositionString);
        }
    }
}
