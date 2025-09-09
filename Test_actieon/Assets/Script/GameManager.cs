using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : PlayerInventory
{
    public static GameManager Instance { get; private set; }

    [Header("Datas")]
    public List<IngredientData> ingredientDatas;
    public List<FoodData> foodDatas;

    [Header("UI panel")]
    [SerializeField] private GameObject cookingPanel;
    [SerializeField] private Button openCookButton;
    private CookingTable cookingTable;
    private UIObjectHolder cookUIHolder;

    private Dictionary<string, IngredientData> ingredientDataDict;
    private Dictionary<string, FoodData> foodDataDict;
    
    private DateTime dateTimeLogin;
    private DateTime dateTimeLogout;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        openCookButton.onClick.AddListener(OpenCookingMenu);
        InitializeDataDictionary();
        InitializeCookingTable();

        StartCoroutine(RefillEnergyRoutine());
        dateTimeLogin = DateTime.Now;
        Debug.Log(dateTimeLogin);
    }
    
    private void Update()
    {
        
    }

    private void OpenCookingMenu()
    {
        if (cookingPanel.activeInHierarchy)
        {
            return;
        }
        cookingPanel.SetActive(true);
        if (!cookingPanel.TryGetComponent(out cookUIHolder))
        {
            Debug.LogError("Can't find UI Holder component");
            return;
        }
        cookUIHolder.Initialize(this);
    }
    
    private void InitializeCookingTable()
    {
        cookingTable = GetComponent<CookingTable>();
        if (!cookingTable)
        {
            cookingTable = gameObject.AddComponent<CookingTable>();
        }
        cookingTable.playerInventory = this;
    }

    private void InitializeDataDictionary()
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
    
    public FoodData GetFoodByID(string id)
    {
        if (foodDataDict.TryGetValue(id, out var food))
        {
            return food;
        }
        return null;
    }
    
}
