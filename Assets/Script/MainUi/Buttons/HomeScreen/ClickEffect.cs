using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEffect : MonoBehaviour
{
    public bool isButtonClick;

    private Vector3 originalScale;
    private int originalSiblingIndex;
    private GridLayoutGroup gridLayoutGroup;

    public float hoverScaleMultiplier = 1.1f;  // Factor to scale the object when hovered.
    public float transitionSpeed = 5f;         // Speed at which the scaling transition occurs.
    private RectTransform rectTransform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        { 
            if(isButtonClick)
            {
                Debug.Log("Rem");
                isButtonClick = false;
            }

            else if (!isButtonClick)
            {
                Debug.Log("Rem");
                isButtonClick = true;
            }
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = transform.localScale;  // Save the initial scale of the object.

        // Retrieve the GridLayoutGroup from the parent object.
        gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
    }

    public void ButtonClick()
    {
        if (isButtonClick)
        {
            HaveClick();
            isButtonClick = false;
        }

        else if (!isButtonClick)
        {
            CancelClick();
            isButtonClick = true;
        }
    }

    public void HaveClick()
    {
        
        StopAllCoroutines();
        // Save the original position in the hierarchy.
        //originalSiblingIndex = transform.GetSiblingIndex();

        // Adjust the pivot based on the screen position (left or right).
        if (gridLayoutGroup != null) AdjustPivotBasedOnPosition();

        // Scale up when the mouse pointer enters.
        StartCoroutine(ScaleTo(originalScale * hoverScaleMultiplier, true));
    }

    public void CancelClick()
    {
        StopAllCoroutines();

        // Enable the GridLayoutGroup when the mouse pointer exits.
        if (gridLayoutGroup != null)
        {
            gridLayoutGroup.enabled = true;
        }

        // Restore the object to its original position in the hierarchy.
        //transform.SetSiblingIndex(originalSiblingIndex);

        // Scale back to the original size.
        StartCoroutine(ScaleTo(originalScale, false));
    }


    private void AdjustPivotBasedOnPosition()
    {
        // Check if the button is on the left or right side of the screen based on its position.
        if (Input.mousePosition.x < 600)
        {
            // If on the left side of the screen, expand from the left.
            rectTransform.pivot = new Vector2(0, 1);  // Pivot on the left.
        }
        else if (Input.mousePosition.x > 1500)
        {
            // If on the right side of the screen, expand from the right.
            rectTransform.pivot = new Vector2(1, 1);  // Pivot on the right.
        }
        else
        {
            rectTransform.pivot = new Vector2(0.5f, 1);
        }
    }

    private IEnumerator ScaleTo(Vector3 targetScale, bool enter)
    {

        if (enter)
        {
            // Disable the GridLayoutGroup when the mouse pointer enters.
            if (gridLayoutGroup != null)
            {
                gridLayoutGroup.enabled = false;
            }

            // Move the object to the last position in the hierarchy.
            //transform.SetAsLastSibling();
        }

        // Smoothly scale the object until the target scale is nearly reached.
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, transitionSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localScale = targetScale;  // Ensure precise scaling after the animation ends.
    }
}
