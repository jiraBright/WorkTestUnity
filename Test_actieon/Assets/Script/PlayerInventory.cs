using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryData<IngredientData>> IngredientsInventory = new();
    public List<InventoryData<FoodData>> FoodsInventory = new();
    public int Energy = 100;

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
}
