using UnityEngine;
using UnityEngine.UI;

public class PageBorder : MonoBehaviour
{
    [SerializeField]private Sprite[] pageSprite;
    private int selectPage;

    public void UpdatePageSprite(int page)
    {
        selectPage = page;
        if (transform.childCount == 0)
        {
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (selectPage != i)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = pageSprite[0];
                transform.GetChild(i).GetComponent<Image>().SetNativeSize();
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().sprite = pageSprite[1];
                transform.GetChild(i).GetComponent<Image>().SetNativeSize();
            }
        }
    }
}
