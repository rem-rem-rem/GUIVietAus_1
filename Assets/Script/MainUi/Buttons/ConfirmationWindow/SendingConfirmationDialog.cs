using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendingConfirmationDialog : MonoBehaviour
{
    public GameObject confirmationDialog;  // The confirmation dialog UI
    public AudioClip audioClip;
    public AudioSource sfxButton;

    // Call this method when you want to deactivate the object
    public void TryDeactivateObject()
    {
        sfxButton = gameObject.AddComponent<AudioSource>();
        sfxButton.clip = audioClip;
        sfxButton.Play();
        // Show the confirmation dialog
        confirmationDialog.SetActive(true);
    }

    // Method called when "Confirm" button is clicked
    public void ConfirmSendingData()
    {
        // Deactivate the target object
        TableManager.Instance.RemoveAllRow();
        // Hide the confirmation dialog
        confirmationDialog.SetActive(false);
    }

    // Method called when "Cancel" or "X" button is clicked
    public void CancelDeactivation()
    {
        // Just hide the confirmation dialog
        confirmationDialog.SetActive(false);
    }
}
