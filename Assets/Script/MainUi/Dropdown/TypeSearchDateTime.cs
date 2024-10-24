using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeSearchDateTime : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown type;
    [SerializeField] private List<GameObject> typeDropdown;
    // Start is called before the first frame update
    void Start()
    {
        type.onValueChanged.AddListener(delegate { OnTypeChange(type); });
    }

    private void OnTypeChange(TMP_Dropdown change)
    {
        HideAllObject();
        
        if(change.value < typeDropdown.Count)
        {
            typeDropdown[change.value].SetActive(true);
        }
    }
    // Update is called once per frame
    public void HideAllObject()
    {
        foreach (GameObject go in typeDropdown)
        {
            go.SetActive(false);
        }
    }
}
