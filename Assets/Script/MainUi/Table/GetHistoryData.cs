using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class GetHistoryData : MonoBehaviour
{
    [SerializeField] private string url = "https://localhost:7287/api/Materials/Test";

    private List<GameObject> history = new List<GameObject>(); 
    public Transform parent;
    public TMP_InputField inputField;
    public GameObject GameObject;
    public string json;
    // Start is called before the first frame update
    public void Start()
    {
        inputField = inputField.GetComponent<TMP_InputField>();
        StartCoroutine(RequestMaterial());
    }

    public void SearchData()
    {
        foreach(var item in history)
        {
            Destroy(item.gameObject);
        }

        Debug.Log(inputField.text);

        //StartCoroutine(SendDataRequest());
    }

    IEnumerator RequestMaterial()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ConnectionError)
            Debug.LogError("Can't request");

        else
        {
            string jsonTest = request.downloadHandler.text;
            Debug.Log("Dữ liệu JSON nhận được: " + jsonTest);

            List<VatTuData> vatTuDatas = JsonConvert.DeserializeObject<List<VatTuData>>(jsonTest);

            int batchSize = 5; // Số lượng đối tượng mỗi lần hiển thị
            for (int i = 0; i < vatTuDatas.Count; i++)
            {
                VatTuData vattu = vatTuDatas[i];
                GameObject newItem = Instantiate(GameObject, parent);
                newItem.transform.Find("TenVatTu").GetComponent<TMP_Text>().text = vattu.tenVatTu;
                newItem.transform.Find("NgayNhap").GetComponent<TMP_Text>().text = vattu.ngayNhap;
                newItem.transform.Find("SoLuong").GetComponent<TMP_Text>().text = vattu.soLuong.ToString();
                newItem.transform.Find("ThanhTien").GetComponent<TMP_Text>().text = string.Format("{0:N0}", vattu.thanhTien);
                newItem.transform.Find("GhiChu").GetComponent<TMP_Text>().text = vattu.ghiChu;

                history.Add(newItem);

                if (i % batchSize == 0)
                {
                    yield return null; // Đợi khung hình tiếp theo để tránh giật lag
                }
            }
        }
    }

    //IEnumerator SendDataRequest()
    //{
    //    MaterialsQueryParameters requestData = new MaterialsQueryParameters
    //    (inputField.text,
    //        "2024-12-01T00:00:00",
    //        null,
    //        "asc",
    //        1);

    //    // Chuyển đổi đối tượng thành JSON
    //    string jsonData = JsonConvert.SerializeObject(requestData);

    //        // Tạo UnityWebRequest
    //        UnityWebRequest request = new UnityWebRequest("https://localhost:7287/api/Materials/Search", "POST");

    //        // Gán dữ liệu JSON vào yêu cầu
    //        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
    //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        request.downloadHandler = new DownloadHandlerBuffer();

    //        // Đặt tiêu đề HTTP để báo cho server biết đây là JSON
    //        request.SetRequestHeader("Content-Type", "application/json");

    //        // Gửi yêu cầu và đợi phản hồi
    //        yield return request.SendWebRequest();

    //        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            Debug.LogError("Error: " + request.error);
    //        }
    //        else
    //        {
    //        string jsonTest = request.downloadHandler.text;
    //        Debug.Log("Dữ liệu JSON nhận được: " + jsonTest);

    //        List<VatTuData> vatTuDatas = JsonConvert.DeserializeObject<List<VatTuData>>(jsonTest);

    //        int batchSize = 5; // Số lượng đối tượng mỗi lần hiển thị
    //        for (int i = 0; i < vatTuDatas.Count; i++)
    //        {
    //            VatTuData vattu = vatTuDatas[i];
    //            GameObject newItem = Instantiate(GameObject, parent);
    //            newItem.transform.Find("TenVatTu").GetComponent<TMP_Text>().text = vattu.tenVatTu;
    //            newItem.transform.Find("NgayNhap").GetComponent<TMP_Text>().text = vattu.ngayNhap;
    //            newItem.transform.Find("SoLuong").GetComponent<TMP_Text>().text = vattu.soLuong.ToString();
    //            newItem.transform.Find("ThanhTien").GetComponent<TMP_Text>().text = string.Format("{0:N0}", vattu.thanhTien);
    //            newItem.transform.Find("GhiChu").GetComponent<TMP_Text>().text = vattu.ghiChu;

    //            history.Add(newItem);
    //            if (i % batchSize == 0)
    //            {
    //                yield return null; // Đợi khung hình tiếp theo để tránh giật lag
    //            }
    //        }


    //    }
        
    //}

    [System.Serializable]
    public class VatTuData
    {
        public int id;
        public string tenVatTu;
        public string ngayNhap;
        public int soLuong;
        public float thanhTien;
        public string ghiChu;
    }

    [System.Serializable]
    public class vatuList
    {
        public List<VatTuData> data;
    }


}
//[System.Serializable]
//public struct MaterialsQueryParameters
//{
//    public string search { get; set; }
//    public double from { get; set; }
//    public double to { get; set; }
//    public string sorting { get; set; }
//    public int page { get; set; }

//    public MaterialsQueryParameters(string search, double from, double to, string sorting, int page) 
//    {
//        this.search = search;
//        this.from = from;
//        this.to = to;
//        this.sorting = sorting;
//        this.page = page;
//    }
//}
