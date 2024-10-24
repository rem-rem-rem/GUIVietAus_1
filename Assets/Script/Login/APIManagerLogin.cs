using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
public class APIManagerLogin : MonoBehaviour
{

    public GameObject LoginPrefab;

    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TMP_InputField PassInput;
    // Start is called before the first frame update
    [SerializeField] private string url = "https://localhost:7287/api/Assosiates/login";

    public void OnButtonLoginPress()
    {
        string username = userInput.text;
        string password = PassInput.text;

        StartCoroutine(SendRequestLogin(username, password));
    }
    IEnumerator SendRequestLogin(string username, string password)
    {
        var loginData = new {username_A =  username, password_A = password};    
        string jsonBody = JsonConvert.SerializeObject(loginData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Request Error");
            }

            else
            {
                string jsonResponse = request.downloadHandler.text;
                DataLogin userInfo = JsonConvert.DeserializeObject<DataLogin>(jsonResponse);

                Debug.Log("API Response: " + jsonResponse);

                SceneManager.LoadScene(sceneName: "MainScene");
            }

           
        }

    }
}
