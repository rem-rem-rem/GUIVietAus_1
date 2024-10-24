using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class ApiService : MonoBehaviour
{
    private static ApiService _instance;

    public static ApiService Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject apiService = new GameObject("ApiService");
                _instance = apiService.AddComponent<ApiService>();
                DontDestroyOnLoad(apiService);  // Đảm bảo đối tượng này tồn tại qua nhiều scene
            }
            return _instance;
        }
    }

    //public void ShowLoadingIndicator()
    //{
    //    loadingIndicator.SetActive(true);  // Hiển thị biểu tượng loading
    //}

    //public void HideLoadingIndicator()
    //{
    //    loadingIndicator.SetActive(false);  // Ẩn biểu tượng loading
    //}

    // Async Method for GET request
    public async Task<string> GetAsync(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();

        // Wait for the request to finish
        while (!operation.isDone)
            await Task.Yield();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            return null;
        }
        return request.downloadHandler.text;
    }

    // Async Method for POST request
    public async Task<string> PostAsync(string url, string jsonData)
    {
        try
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            // Wait for the request to finish
            while (!operation.isDone)
                await Task.Yield();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}, StatusCode: {request.responseCode}");
                return null;
            }
            return request.downloadHandler.text;
        }
        catch
        {
            Debug.Log("Some thing wrong check wifi");
            return null;
        }

    }
}
