using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ApiTester : MonoBehaviour
{
    // Đường dẫn API (thay bằng URL của API của bạn)
    private string apiUrl = "https://localhost:7287/api/Assosiates";

    // Hàm Start để gọi API ngay khi bắt đầu
    void Start()
    {
        StartCoroutine(CheckApiStatus());
    }

    // Coroutine để gửi yêu cầu tới API và in ra kết quả
    IEnumerator CheckApiStatus()
    {
        // Tạo yêu cầu GET (có thể thay đổi thành POST nếu cần)
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Gửi yêu cầu và đợi phản hồi
        yield return request.SendWebRequest();

        // Kiểm tra kết quả phản hồi
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API Error: " + request.error);  // In ra lỗi nếu có
        }
        else
        {
            Debug.Log("API Response: " + request.downloadHandler.text);  // In ra phản hồi nếu thành công
        }
    }
}
