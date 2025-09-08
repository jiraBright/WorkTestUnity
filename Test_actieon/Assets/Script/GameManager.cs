using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PlayerInventory
{
    [SerializeField] private List<IngredientData> ingredientDatas;
    [SerializeField] private List<FoodData> foodDatas;

    [SerializeField] private GameObject cookingPanel;
    private CookingTable cookingTable;
    private Dictionary<string, IngredientData> ingredientDataDict;
    private Dictionary<string, FoodData> foodDataDict;
    
    private DateTime dateTimeLogin;
    private DateTime dateTimeLogout;
    private void Start()
    {
        InitializeDictionary();
        cookingTable = cookingPanel.GetComponent<CookingTable>();
        cookingTable.playerInventory = this;

        StartCoroutine(RefillEnergyRoutine());
        dateTimeLogin = DateTime.Now;
        Debug.Log(dateTimeLogin);
    }
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
            cookingTable.CookFood(GetFoodByID("bun"));
        }
    }
    private void InitializeDictionary()
    {
        AddIngredeintToDict();
        AddFoodToDict();
    }
    private void AddIngredeintToDict()
    {
        ingredientDataDict = new Dictionary<string, IngredientData>();
        foreach (var ingredient in ingredientDatas)
        {
            if (!ingredientDataDict.ContainsKey(ingredient.ID))
            {
                ingredientDataDict.Add(ingredient.ID, ingredient);
            }
        }
    }
    private void AddFoodToDict()
    {
        foodDataDict = new Dictionary<string, FoodData>();
        foreach (var food in foodDatas)
        {
            if (!foodDataDict.ContainsKey(food.ID))
            {
                foodDataDict.Add(food.ID, food);
            }
        }
    }
    private IngredientData GetIngredientByID(string id)
    {
        if (ingredientDataDict.TryGetValue(id, out var data))
        {
            return data;
        }
        return null;
    }
    
    private FoodData GetFoodByID(string id)
    {
        if (foodDataDict.TryGetValue(id, out var food))
        {
            return food;
        }
        return null;
    }
    
}
