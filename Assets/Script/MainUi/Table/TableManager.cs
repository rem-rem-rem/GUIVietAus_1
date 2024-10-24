using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform tableContent;
    [SerializeField] private GameObject tableColumn;
    public float rowHeightIncrement;

    // List to keep track of all the instantiated row objects
    public List<GameObject> instantiatedRows = new List<GameObject>();

    private void Start()
    {
        // Set the pivot of the column's RectTransform to be at the center top
        tableColumn.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
    }

    // Method to add a new row to the table
    public void AddRow()
    {
        // Increase the column's height by the row height increment
        tableColumn.transform.localScale += new Vector3(0, rowHeightIncrement, 0);

        // Instantiate a new row and set its parent as the content container
        var newRow = Instantiate(rowPrefab, tableContent.transform);

        // Add the new row to the list of instantiated rows
        instantiatedRows.Add(newRow);
    }

    // Method to remove the last row from the table
    public void RemoveLastRow()
    {
        // Return early if there are no rows to remove
        if (instantiatedRows.Count == 0)
        {
            return;
        }

        // Decrease the column's height by the row height increment
        tableColumn.transform.localScale -= new Vector3(0, rowHeightIncrement, 0);

        // Get the last instantiated row and destroy it
        var lastRow = instantiatedRows[instantiatedRows.Count - 1];
        Destroy(lastRow);

        // Remove the last row from the list
        instantiatedRows.RemoveAt(instantiatedRows.Count - 1);
    }

    /// <summary>
    /// removeAllRow call back from SendingConfirmationDialog
    /// </summary>
    public void RemoveAllRow()
    {
        // Decreate the column's height by the row height increment
        tableColumn.transform.localScale = new Vector3(1, 0, 1);

        // Remove all gameObject in instantiatedRows list
        foreach (var row in instantiatedRows)
        {
            Destroy(row.gameObject);
        }

        instantiatedRows.Clear();
    }
}
