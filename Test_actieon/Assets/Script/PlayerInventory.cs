using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<InventoryData<IngredientData>> IngredientsInventory = new();
    public List<InventoryData<FoodData>> FoodsInventory = new();

    [Header("Energy Settings")]
    public int CurrentEnergy = 100;
    public int maxEnergy = 100;

    private int refillEnergyAmount = 1;
    private float refillEnergyRate = 5f;


    #region Ingredient & Food
    public void AddIngredient(IngredientData ingredient, int amount)
    {
        var item = IngredientsInventory.Find(i => i.Data.ID == ingredient.ID);
        if (item != null)
        {
            item.Amount += amount;
        }
        else
        {
            IngredientsInventory.Add(new InventoryData<IngredientData>(ingredient, amount));
        }
    }

    public void AddFood(FoodData food, int amount)
    {
        var item = FoodsInventory.Find(i => i.Data.ID == food.ID);
        if (item != null)
        {
            item.Amount += amount;
        }
        else
        {
            FoodsInventory.Add(new InventoryData<FoodData>(food, amount));
        }
    }

    public bool HasIngredients(List<IngredientRequirement> requirements)
    {
        foreach (var req in requirements)
        {
            var item = IngredientsInventory.Find(i => i.Data.ID == req.ingredient.ID);
            if (item == null || item.Amount < req.amount)
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
            var item = IngredientsInventory.Find(i => i.Data.ID == req.ingredient.ID);
            if (item != null)
            {
                item.Amount -= req.amount;
                break;
            }
        }
    }

    #endregion

    #region Energy
    private void AddEnergy()
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + refillEnergyAmount, 0, maxEnergy);
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
                AddEnergy();
            }
        }
    }
    
    #endregion
}
