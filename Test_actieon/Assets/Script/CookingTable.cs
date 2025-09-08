using System.Collections;
using UnityEngine;

public class CookingTable : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public void CookFood(FoodData food)
    {
        if (!playerInventory.HasIngredients(food.IngredientsRequired))
        {
            Debug.Log("Not enough ingredients for " + food.Name);
            return;
        }

        if (!playerInventory.UseEnergy(food.EnergyUse))
        {
            Debug.Log("Not enough energy!");
            return;
        }

        playerInventory.ConsumeIngredients(food.IngredientsRequired);
        StartCoroutine(CookingProcess(food));
    }

    private IEnumerator CookingProcess(FoodData food)
    {
        Debug.Log("Cooking started: " + food.Name);
        yield return new WaitForSeconds(food.CookingTime);
        playerInventory.AddFood(food, 1);
        Debug.Log("Cooking finished: " + food.Name);
    }
}
