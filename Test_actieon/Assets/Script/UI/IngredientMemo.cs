using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientMemo : MonoBehaviour
{
    public TMP_Text IngredientRequireAmount;
    public Image IngredientImage;

    private string ingredientID;
    private int reqAmount;
    private int oldAmount;
    private int currentAmount;

    private void Update()
    {
        currentAmount = GameManager.Instance.IngredientsInventory[ingredientID];
        if (oldAmount != currentAmount)
        {
            UpdateIngredientAmount(currentAmount, reqAmount);
            oldAmount = currentAmount;
        }
    }
    public void InitializeIngredientMemo(int foodReqAmount, IngredientData ingredient)
    {
        IngredientImage.sprite = ingredient.IngredientSprite;
        ingredientID = ingredient.ID;
        reqAmount = foodReqAmount;
        currentAmount = GameManager.Instance.IngredientsInventory[ingredientID];
        oldAmount = currentAmount;
        UpdateIngredientAmount(currentAmount, reqAmount);
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
