using System.Collections;
using UnityEngine;

public class CookingTable : MonoBehaviour
{
    [HideInInspector]
    public PlayerInventory playerInventory;

    public void CookFood(FoodData food)
    {
        if (playerInventory.cookingFoods.ContainsKey(food))
        {
            float remainingTime = playerInventory.cookingFoods[food] - Time.realtimeSinceStartup;
            if (remainingTime > 0)
            {
                Debug.Log($"{food.Name} is already cooking! Remaining: {remainingTime:F1} sec");
                return;
            }
        }

        if (!playerInventory.HasIngredients(food.IngredientsRequired))
        {
            Debug.Log("Not enough ingredients for " + food.Name);
            return;
        }

        if (!playerInventory.UseEnergy(food.EnergyUse))
        {
            Debug.Log("Not enough energy");
            return;
        }
        float endTime = Time.realtimeSinceStartup + food.CookingTime;
        playerInventory.cookingFoods[food] = endTime;

        playerInventory.ConsumeIngredients(food.IngredientsRequired);
        StartCoroutine(CookingProcess(food, endTime));
        Debug.Log("Started cooking: " + food.Name);
    }

    private IEnumerator CookingProcess(FoodData food, float endTime)
    {
        yield return new WaitForSecondsRealtime(food.CookingTime);

        if (playerInventory.cookingFoods.ContainsKey(food) && Mathf.Approximately(playerInventory.cookingFoods[food], endTime))
        {
            playerInventory.AddFood(food, 1);
            playerInventory.cookingFoods.Remove(food);
            Debug.Log("Finished cooking: " + food.Name);
        }
    }
}
