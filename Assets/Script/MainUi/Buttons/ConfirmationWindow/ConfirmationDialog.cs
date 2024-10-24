using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialogManager : MonoBehaviour
{
    public GameObject confirmationDialog;  // UI Panel chứa hộp thoại xác nhận
    public Button confirmButton;           // Nút "Đồng ý"
    public Button cancelButton;            // Nút "Hủy"
    public Button closeButton;             // Nút "X"

    private GameObject objectToDeactivate; // Đối tượng sẽ bị tắt
    public AudioClip audioClip;
    public AudioSource sfxButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to buttons
        confirmButton.onClick.AddListener(ConfirmDeactivation);
        cancelButton.onClick.AddListener(CancelDeactivation);
        closeButton.onClick.AddListener(CancelDeactivation);

        // Hide the dialog initially
        confirmationDialog.SetActive(false);
    }

    // Call this method to show the confirmation dialog for a specific object
    public void ShowConfirmationDialog(GameObject targetObject)
    {
        sfxButton = gameObject.AddComponent<AudioSource>();
        sfxButton.clip = audioClip;
        sfxButton.Play();
        objectToDeactivate = targetObject;  // Gán đối tượng sẽ bị tắt

        // Hiển thị hộp thoại xác nhận
        confirmationDialog.SetActive(true);
    }

    // Method called when "Confirm" button is clicked
    private void ConfirmDeactivation()
    {
        // Tắt đối tượng đã được gán trước đó
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        else 
        {
            ProjectManagerSetting.Instance.SutdownCallBack();
        }

        // Ẩn hộp thoại xác nhận
        confirmationDialog.SetActive(false);
    }

    // Method called when "Cancel" or "X" button is clicked
    private void CancelDeactivation()
    {
        // Chỉ cần ẩn hộp thoại xác nhận
        confirmationDialog.SetActive(false);
    }
}
