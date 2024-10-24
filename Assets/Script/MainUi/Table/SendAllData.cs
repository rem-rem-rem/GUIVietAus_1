using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendAllData : MonoBehaviour
{
    public string url = "https://localhost:7287/api/Materials";
    List<MaterialsData> materialsDatas = new List<MaterialsData>();
    
    public void SendAllDataToAPI()
    {
        List<MaterialsData> materialsDatasList = new List<MaterialsData>();

        foreach (var row in TableManager.Instance.instantiatedRows)
        {
            try
            {
                TMP_InputField[] inputFields = row.GetComponentsInChildren<TMP_InputField>(); // Lấy tất cả các InputField trong hàng
                MaterialsData material = new MaterialsData
                {
                    TenVatTu = inputFields[0].text,
                    NgayNhap = (DateTime.Now).ToString("yyyy-MM-ddTHH:mm:ss.fff"),             
                    SoLuong = int.Parse(inputFields[2].text.Replace(",", "")),
                    PhongBan = "Rem",
                    BoPhan = "Rem",
                    DonGia = decimal.Parse("000"),
                    ThanhTien = decimal.Parse(inputFields[3].text.Replace(",", "")),
                    GhiChu = inputFields[4].text
                };
                materialsDatasList.Add(material);
            }

            catch
            {
                continue;
            }

        }
        Debug.Log("Rem");
        materialsDatas = materialsDatasList;
        //string jsonData = JsonConvert.SerializeObject(materialsDatasList);

        //APIServices.Instance.PostData(url, jsonData);

        StartCoroutine(SendDataRequest(materialsDatasList)); // Gửi tất cả dữ liệu lên API
    }

    IEnumerator SendDataRequest(List<MaterialsData> allMaterials)
    {
        string jsonData = JsonConvert.SerializeObject(allMaterials);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("All data sent successfully!");
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
