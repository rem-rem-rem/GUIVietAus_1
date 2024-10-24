using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonCustom : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    //private Image buttonImage;
    //public Color hoverColor = Color.white;  // Màu khi di chuột vào
    //public Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0f);  // Màu bình thường
    //public float transitionDuration = 0.2f; // Thời gian chuyển màu

    //public Texture2D cursorTexture;  // Hình ảnh con trỏ mới
    //public Vector2 hotspot = Vector2.zero;  // Vị trí "hotspot" (điểm được coi là trung tâm của con trỏ)
    //private CursorMode cursorMode = CursorMode.Auto;

    //private Coroutine colorChangeCoroutine;

    //void Start()
    //{
    //    buttonImage = GetComponent<Image>();
    //    buttonImage.color = normalColor;  // Đặt màu mặc định cho nút
    //}

    //// Khi di chuột vào nút
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Cursor.SetCursor(cursorTexture, hotspot, cursorMode);  // Đổi con trỏ chuột
    //    // Nếu có Coroutine đang chạy thì dừng nó
    //    if (colorChangeCoroutine != null)
    //    {
    //        StopCoroutine(colorChangeCoroutine);
    //    }

    //    // Bắt đầu Coroutine để chuyển màu mượt mà
    //    colorChangeCoroutine = StartCoroutine(ChangeColor(buttonImage.color, hoverColor));
    //}

    //// Khi chuột rời khỏi nút
    //public void OnPointerExit(PointerEventData eventData)
    //{

    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);  // Trở về con trỏ mặc định
    //    // Nếu có Coroutine đang chạy thì dừng nó
    //    if (colorChangeCoroutine != null)
    //    {
    //        StopCoroutine(colorChangeCoroutine);
    //    }

    //    // Bắt đầu Coroutine để chuyển màu về bình thường
    //    colorChangeCoroutine = StartCoroutine(ChangeColor(buttonImage.color, normalColor));
    //}

    //// Coroutine để chuyển màu từ màu hiện tại sang màu mới
    //private IEnumerator ChangeColor(Color fromColor, Color toColor)
    //{
    //    float elapsedTime = 0f;

    //    // Lặp trong khoảng thời gian chuyển đổi
    //    while (elapsedTime < transitionDuration)
    //    {
    //        // Lerp màu theo thời gian
    //        buttonImage.color = Color.Lerp(fromColor, toColor, elapsedTime / transitionDuration);
    //        elapsedTime += Time.deltaTime;

    //        // Đợi 1 frame
    //        yield return null;
    //    }

    //    // Đảm bảo rằng màu kết thúc chính xác là màu đích
    //    buttonImage.color = toColor;
    //}
}
