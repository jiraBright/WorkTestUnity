using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : PlayerInventory
{
    [SerializeField] private List<IngredientData> ingredientDatas;
    [SerializeField] private List<FoodData> foodDatas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddIngredient(GetIngredientByID("veg"), 1);
            AddIngredient(GetIngredientByID("egg"), 1);
            AddIngredient(GetIngredientByID("car"), 1);
            AddIngredient(GetIngredientByID("rice"), 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddFood(GetFoodByID("1"), 1);
        }
    }
    private IngredientData GetIngredientByID(string id)
    {
        IngredientData pickIngredient = ingredientDatas.First();
        for (int i = 0; i < ingredientDatas.Count; i++)
        {
            if (ingredientDatas[i].ID == id)
            {
                pickIngredient = ingredientDatas[i];
                break;
            }
        }
        if (pickIngredient.ID == "0" || pickIngredient.ID == "")
        {
            Debug.LogError($"Can't find any match ID : {pickIngredient.ID}");
        }
        return pickIngredient;
    }
    
    private FoodData GetFoodByID(string id)
    {
        FoodData pickFood = foodDatas.First();
        for (int i = 0; i < foodDatas.Count; i++)
        {
            if (foodDatas[i].ID == id)
            {
                pickFood = foodDatas[i];
                break;
            }
        }
        if (pickFood.ID == "0" || pickFood.ID == "")
        {
            Debug.LogError($"Can't find any match ID : {pickFood.ID}");
        }
        return pickFood;
    }
}
