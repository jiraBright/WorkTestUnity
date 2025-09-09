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
                Debug.Log($"{food.FoodName} is already cooking! Remaining: {remainingTime:F1} sec");
                return;
            }
        }

        if (!GameManager.Instance.UseEnergy(food.EnergyUse))
        {
            Debug.Log("Not enough energy");
            return;
        }

        float endTime = Time.realtimeSinceStartup + food.CookingTime;
        GameManager.Instance.cookingFoods[food] = endTime;
        GameManager.Instance.ConsumeIngredients(food.IngredientsRequired);
        StartCoroutine(CookingProcess(food, endTime));
        Debug.Log("Started cooking: " + food.FoodName);
    }

    private IEnumerator CookingProcess(FoodData food, float endTime)
    {
        yield return new WaitForSecondsRealtime(food.CookingTime);
        
        if (GameManager.Instance.cookingFoods.ContainsKey(food) && Mathf.Approximately(GameManager.Instance.cookingFoods[food], endTime))
        {
            GameManager.Instance.AddFood(food, 1);
            GameManager.Instance.cookingFoods.Remove(food);
            Debug.Log("Finished cooking: " + food.FoodName);
        }
    }
}
