using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public Dictionary<string, int> IngredientsInventory = new();
    public Dictionary<string, int> FoodsInventory = new();
    public Dictionary<FoodData, float> cookingFoods = new();

    

    [Header("Energy Settings")]
    public int CurrentEnergy = 100;
    public int maxEnergy = 100;

    private int refillEnergyAmount = 1;
    private float refillEnergyRate = 5f;


    #region Ingredient & Food
    public void AddIngredient(IngredientData ingredient, int amount)
    {
        if (IngredientsInventory.ContainsKey(ingredient.ID))
        {
            IngredientsInventory[ingredient.ID] += amount;
        }
        else
        {
            IngredientsInventory[ingredient.ID] = amount;
        }
    }

    public void AddFood(FoodData food, int amount)
    {
        if (FoodsInventory.ContainsKey(food.ID))
        {
            FoodsInventory[food.ID] += amount;
        }
        else
        {
            FoodsInventory[food.ID] = amount;
        }
    }

    public bool HasIngredients(List<IngredientRequirement> requirements)
    {
        foreach (var req in requirements)
        {
            if (!IngredientsInventory.TryGetValue(req.ingredient.ID, out int currentAmount) || currentAmount < req.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void ConsumeIngredients(List<IngredientRequirement> requirements)
    {
        foreach (var req in requirements)
        {
            if (IngredientsInventory.ContainsKey(req.ingredient.ID))
            {
                IngredientsInventory[req.ingredient.ID] -= req.amount;
                Debug.Log($"req ingre : {req.ingredient.Name}");
            }
        }
    }

    #endregion

    #region Energy
    private void AddEnergy(int amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0, maxEnergy);
        Debug.Log($"Current energy is: {CurrentEnergy}");
    }

    public bool UseEnergy(int amount)
    {
        if (CurrentEnergy < amount)
        {
            return false;
        }
        CurrentEnergy = Mathf.Clamp(CurrentEnergy - amount, 0, maxEnergy);
        return true;
    }

    public IEnumerator RefillEnergyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(refillEnergyRate);
            if (CurrentEnergy < maxEnergy)
            {
                AddEnergy(refillEnergyAmount);
            }
        }
    }
    
    #endregion
}
