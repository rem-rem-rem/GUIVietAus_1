using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;
public partial class GetHistoryObjectPooling : MonoBehaviour
{
    [SerializeField] private string url = "https://localhost:7287/api/Materials/Test";
    [SerializeField] private int page;
    private int TotalItems;

    private List<GameObject> history = new List<GameObject>();
    public Transform parent;
    private bool removeNoise;

    public TMP_InputField inputField;
    public TMP_Text dateFrom;
    public TMP_Text dateTo;
    public TMP_Text pageNumber;
    public GameObject prefab;
    public int sizePool = 0;
    public string json;
    private ObjectPool<GameObject> pool;

    // Start is called before the first frame update
    public void Start()
    {
        removeNoise = false;
        pool = new ObjectPool<GameObject>(() => Instantiate(prefab, parent), 50);
        inputField = inputField.GetComponent<TMP_InputField>();

    }

    IEnumerator OnSubmit()
    {
        removeNoise = true;

        Task<string> task = null;
        try
        {
            // Chuẩn bị dữ liệu gửi đi
            MaterialsQueryParameters requestData = new MaterialsQueryParameters(inputField.text, dateFrom.text, dateTo.text, "asc", int.Parse(pageNumber.text));
            string jsonData = JsonConvert.SerializeObject(requestData);

            // Thực hiện API call trong một Task
            task = ApiService.Instance.PostAsync(url, jsonData); // Kết hợp async task và yield
            if (task != null) { removeNoise = false; }
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi gửi request: " + ex.Message);
            removeNoise = false;
            yield break; // Dừng coroutine nếu có lỗi
        }

        yield return new WaitUntil(() => task.IsCompleted);

        if (!string.IsNullOrEmpty(task.Result))
        {
            Debug.Log("Dữ liệu JSON nhận được: " + task.Result);

            vatuList vatTuDatas = JsonConvert.DeserializeObject<vatuList>(task.Result);
            TotalItems = vatTuDatas.TotalItems;

            int batchSize = 5; // Số lượng đối tượng mỗi lần hiển thị
                                //for (int i = 0; i < vatTuDatas.Count; i++)
                                //{
                                //    VatTuData vattu = vatTuDatas[i];
                                //    GameObject newItem = pool.Get(); // Lấy đối tượng từ pool

            for (int i = 0; i < vatTuDatas.data.Count; i++)
            {
                GameObject newItem = pool.Get(); // Lấy đối tượng từ pool
                newItem.SetActive(true);

                VatTuData vattu = vatTuDatas.data[i];

                SetDataForItem(newItem, vattu.tenVatTu, vattu.ngayNhap, vattu.soLuong.ToString(),
                                string.Format("{0:N0}", vattu.thanhTien), vattu.ghiChu);

                history.Add(newItem);

                // Yield sau khi hiển thị batchSize phần tử
                if (i % batchSize == 0)
                {
                    yield return null; // Đợi khung hình tiếp theo để tránh giật lag
                }
            }

            // Thêm các phần tử trống nếu số lượng phần tử nhỏ hơn 50
            if(vatTuDatas.data.Count < 50)
            {
                int itemsToAdd = Math.Max(50 - vatTuDatas.data.Count, 0);
                for (int i = 0; i < itemsToAdd; i++)
                {
                    GameObject newItem = pool.Get(); // Lấy đối tượng từ pool
                    newItem.SetActive(true);

                    SetDataForItem(newItem, "", "", "", "", ""); // Phần tử trống

                    history.Add(newItem);
                }
            }

            yield return new WaitForSeconds(0.1f);
            removeNoise = false;
        }
    }

    private void SetDataForItem(GameObject newItem, string tenVatTu, string ngayNhap, string soLuong, string thanhTien, string ghiChu)
    {
        newItem.transform.Find("TenVatTu").GetComponent<TMP_Text>().text = tenVatTu;
        newItem.transform.Find("NgayNhap").GetComponent<TMP_Text>().text = ngayNhap;
        newItem.transform.Find("SoLuong").GetComponent<TMP_Text>().text = soLuong;
        newItem.transform.Find("ThanhTien").GetComponent<TMP_Text>().text = thanhTien;
        newItem.transform.Find("GhiChu").GetComponent<TMP_Text>().text = ghiChu;
    }

    public void SearchData() //Nút nhấn gọi về hàm này
    {
        if(!removeNoise)
        {
            Debug.Log(inputField.text);
            pageNumber.text = "1"; //trả về số Page bằng 1
            //StartCoroutine(SendDataRequest());
            //await OnSubmit();
            OnSubmitCallback();
        }

    }

    public void GoToAnotherPage(int i)
    {
        int temp = int.Parse(pageNumber.text);
        temp += i;
        if (temp > TotalItems)
        {
            temp = TotalItems;  
        }

        if (temp <= 0)
        {
            temp = 1;
        }
        pageNumber.text = temp.ToString();

        OnSubmitCallback();
    }

    private void OnSubmitCallback()
    {
        // Release các đối tượng cũ về pool
        foreach (var item in history)
        {
            pool.Release(item);
            //Destroy(item.gameObject);
        }
        history.Clear();
        StartCoroutine(OnSubmit());
    }

    //IEnumerator SendDataRequest()
    //{
    //    MaterialsQueryParameters requestData = new MaterialsQueryParameters(inputField.text, 0, 0, "asc", int.Parse(TMP_Text.text));
    //    string jsonData = JsonConvert.SerializeObject(requestData);

    //    UnityWebRequest request = new UnityWebRequest("https://localhost:7287/api/Materials/Search", "POST");
    //    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
    //    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Error: " + request.error);
    //    }
    //    else
    //    {
    //        string jsonTest = request.downloadHandler.text;
    //        Debug.Log("Dữ liệu JSON nhận được: " + jsonTest);

    //        List<VatTuData> vatTuDatas = JsonConvert.DeserializeObject<List<VatTuData>>(jsonTest);

    //        int batchSize = 5; // Số lượng đối tượng mỗi lần hiển thị
    //        for (int i = 0; i < vatTuDatas.Count; i++)
    //        {
    //            VatTuData vattu = vatTuDatas[i];
    //            GameObject newItem = pool.Get(); // Lấy đối tượng từ pool

    //            //GameObject newItem = Instantiate(prefab, parent);
    //            newItem.SetActive(true);
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

}


/// <summary>
/// MaterialsQueryParameters chỉ là parameters cho Datarequest trong OnSubmit
/// </summary>
[System.Serializable]
public struct MaterialsQueryParameters
{
    public string search { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string sorting { get; set; }
    public int page { get; set; }

    public MaterialsQueryParameters(string search, string FromDate, string ToDate, string sorting, int page)
    {
        this.search = search;
        this.FromDate = FromDate;
        this.ToDate = ToDate;
        this.sorting = sorting;
        this.page = page;
    }
}
