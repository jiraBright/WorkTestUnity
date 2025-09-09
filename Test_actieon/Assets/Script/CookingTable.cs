using System.Collections;
using UnityEngine;

public class CookingTable : MonoBehaviour
{
    public void CookFood(FoodData food)
    {
        if (GameManager.Instance.cookingFoods.ContainsKey(food))
        {
            float remainingTime = GameManager.Instance.cookingFoods[food] - Time.realtimeSinceStartup;
            if (remainingTime > 0)
            {
                return;
            }
        }

        if (!GameManager.Instance.UseEnergy(food.EnergyUse))
        {
            return;
        }

        float endTime = Time.realtimeSinceStartup + food.CookingTime;
        GameManager.Instance.cookingFoods[food] = endTime;
        GameManager.Instance.ConsumeIngredients(food.IngredientsRequired);
        StartCoroutine(CookingProcess(food, endTime));
    }

    private IEnumerator CookingProcess(FoodData food, float endTime)
    {
        yield return new WaitForSecondsRealtime(food.CookingTime);

        if (GameManager.Instance.cookingFoods.ContainsKey(food) && Mathf.Approximately(GameManager.Instance.cookingFoods[food], endTime))
        {
            GameManager.Instance.AddFood(food, 1);
            GameManager.Instance.cookingFoods.Remove(food);
            GameManager.Instance.popupManager.ShowPopup($"You receive {food.FoodName} (1)");
        }
    }
    
}
