using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreateRow : MonoBehaviour
{
    public GameObject rowPrefab;  // Hàng mẫu (Prefab) chứa các ô
    public Transform content;     // Vùng Content của Scroll View nơi chứa các hàng
    public int inputField; // InputField để người dùng nhập số hàng muốn tạo

    private void Start()
    {
        GenerateRows();
    }
    // Hàm để sinh ra các hàng tự động
    public void GenerateRows()
    {
        // Xóa các hàng hiện có trong Content (nếu có)

        // Lấy số lượng hàng từ InputField
        int numberOfRows = inputField;

        // Tạo số lượng hàng theo yêu cầu
        StartCoroutine(SpawnObjectsOverTime(inputField));
    }

    IEnumerator SpawnObjectsOverTime(int numberOfObjects)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 1; i < numberOfObjects + 1; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, -39f * i, 0);
            // Sinh ra hàng mới từ Prefab
            Instantiate(rowPrefab, spawnPosition, Quaternion.identity, content);

            yield return null;  // Chờ một frame trước khi tạo đối tượng tiếp theo
        }
    }
}
