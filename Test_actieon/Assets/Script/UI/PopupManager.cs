using UnityEngine;
using TMPro;
using System.Collections;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab; 
    [SerializeField] private Transform popupParent;

    public void ShowPopup(string text, float fadeInDuration = 0.3f, float displayDuration = 1.5f, float fadeOutDuration = 0.3f)
    {
        if (!popupParent)
        {
            popupParent = this.transform;
        }
        GameObject popupObj = Instantiate(popupPrefab, popupParent);
        CanvasGroup canvasGroup = popupObj.GetComponent<CanvasGroup>();
        TMP_Text textComponent = popupObj.GetComponentInChildren<TMP_Text>();

        textComponent.text = text;
        canvasGroup.alpha = 0;

        StartCoroutine(FadePopup(canvasGroup, popupObj, fadeInDuration, displayDuration, fadeOutDuration));
    }

    private IEnumerator FadePopup(CanvasGroup canvasGroup, GameObject popupObj, float fadeInDuration, float displayDuration, float fadeOutDuration)
    {
        float timeInterval = 0f;

        while (timeInterval < fadeInDuration)
        {
            timeInterval += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, timeInterval / fadeInDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(displayDuration);

        timeInterval = 0f;
        while (timeInterval < fadeOutDuration)
        {
            timeInterval += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, timeInterval / fadeOutDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;

        Destroy(popupObj);
    }
}
