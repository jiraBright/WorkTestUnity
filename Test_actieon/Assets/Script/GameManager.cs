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
    [SerializeField] private Button addIngredientButton;
    private UIObjectHolder cookUIHolder;

    private Dictionary<string, IngredientData> ingredientDataDict;
    private Dictionary<string, FoodData> foodDataDict;
    
    private DateTime dateTimeLogin;
    private DateTime dateTimeLogout;
    
    public CookingTable cookingTable;

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

        if (IngredientsInventory.Count < ingredientDatas.Count)
        {
            for (int i = 0; i < ingredientDatas.Count; i++)
            {
                if (!string.IsNullOrEmpty(ingredientDatas[i].ID))
                {
                    AddIngredient(ingredientDatas[i], 0);
                }
            }
        }

        openCookButton.onClick.AddListener(OpenCookingMenu);
        addIngredientButton.onClick.AddListener(() => AddAllIngredient(10));
        InitializeDataDictionary();
        InitializeCookingTable();

        StartCoroutine(RefillEnergyRoutine());

        dateTimeLogin = DateTime.Now;
        Debug.Log(dateTimeLogin);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddAllIngredient(1);
        }
    }
    private void OnApplicationQuit()
    {
        dateTimeLogout = DateTime.Now;
    }

    public FoodData GetFoodByID(string id)
    {

        if (foodDataDict.TryGetValue(id, out var food))
        {
            return food;
        }
        return null;
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
        cookUIHolder.Initialize();
    }

    private void AddAllIngredient(int amount)
    {
        AddIngredient(ingredientDatas[1], amount);
        AddIngredient(ingredientDatas[2], amount);
        AddIngredient(ingredientDatas[3], amount);
        AddIngredient(ingredientDatas[4], amount);
    }
    
    private void InitializeCookingTable()
    {
        cookingTable = GetComponent<CookingTable>();
        if (!cookingTable)
        {
            cookingTable = gameObject.AddComponent<CookingTable>();
        }
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

    
    
}
