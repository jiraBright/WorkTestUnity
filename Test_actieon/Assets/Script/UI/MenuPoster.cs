using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPoster : MonoBehaviour
{
    [SerializeField] private Sprite[] PosterBGImage;
    public Image PosterImage;
    public Image MenuImage;
    public TMP_Text MenuName;

    public void InitializeMenuPoster(string foodName, int foodQuality, Sprite foodSprite)
    {
        MenuImage.sprite = foodSprite;
        MenuName.text = foodName;
        SetPosterImage(foodQuality);
    }
    private void SetPosterImage(int quality)
    {
        switch (quality)
        {
            case 1:
                {
                    PosterImage.sprite = PosterBGImage[0];
                    break;
                }
            case 2:
                {
                    PosterImage.sprite = PosterBGImage[1];
                    break;
                }
            case 3:
                {
                    PosterImage.sprite = PosterBGImage[2];
                    break;
                }
            default:
                {
                    Debug.LogError("Can't match any quality");
                    break;
                }
        }
    }
}
