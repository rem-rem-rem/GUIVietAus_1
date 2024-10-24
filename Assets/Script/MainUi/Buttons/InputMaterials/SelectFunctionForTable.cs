using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFunctionForTable : MonoBehaviour
{
    [SerializeField] private List<GameObject> funtions;
    [SerializeField] private List<GameObject> MainScreen;

    public void ButtonFunction(string function)
    {
        MainScreen.ForEach(obj => obj.SetActive(false));
        var rem = funtions.FindLastIndex(item  => item.name == function);
        MainScreen[rem].SetActive(true);
    }
}
