// Example usage in another script
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    public SendingConfirmationDialog confirmationDialog;

    public void OnClickDeactivateButton()
    {
        confirmationDialog.TryDeactivateObject();
    }
}
