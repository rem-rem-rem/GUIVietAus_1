using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ShrinkAndDisappear : MonoBehaviour
{
    public GameObject objectToShrink;  // Shrink And Disappear
    public GameObject objectToGrow;    // Zoom and show
    public float shrinkDuration = 0.5f;  // Time to shrink
    public float growDuration = 0.5f;    // Time to zoom

    public void ShrinkAndGrowObjects()
    {
        if (objectToGrow.activeInHierarchy == false)
        {
            StartCoroutine(ShrinkAndHide(objectToShrink));  // Start Coroutine to disappear first object
            StartCoroutine(GrowObject(objectToGrow));       // Start Coroutine to Show seacon object
        }

    }

    // Coroutine để thu nhỏ đối tượng
    private IEnumerator ShrinkAndHide(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;  // Save first object scale
        Vector3 targetScale = Vector3.zero;                //  0

        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // Start shrink object with time
            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;

            yield return null;  // wait 1 frame
        }

        // Shrink finish
        obj.transform.localScale = targetScale;

        // disappear object
        
    }

    // Coroutine to zoom another object
    private IEnumerator GrowObject(GameObject obj)
    {
        Vector3 originalScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        float elapsedTime = 0f;

        obj.SetActive(true);

        while (elapsedTime < growDuration)
        {

            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / growDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        obj.transform.localScale = targetScale;
        yield return new WaitForSeconds(1);
        objectToShrink.SetActive(false);
    }





}
