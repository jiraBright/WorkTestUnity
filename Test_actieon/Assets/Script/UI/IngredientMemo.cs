using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientMemo : MonoBehaviour
{
    public TMP_Text IngredientRequireAmount;
    public Image IngredientImage;
    public void InitializeIngredientMemo(IngredientData ingredient)
    {
        IngredientImage.sprite = ingredient.IngredientSprite;
    }
    public void UpdateIngredientAmount(int currentAmount, int requireAmount)
    {
        if (currentAmount < requireAmount)
        {
            IngredientRequireAmount.text = $"<color=#FF0000>{currentAmount}</color>/{requireAmount}";
        }
        else
        {
            IngredientRequireAmount.text = $"<color=#FFFFFF>{currentAmount}</color>/{requireAmount}";
        }
    }
}
