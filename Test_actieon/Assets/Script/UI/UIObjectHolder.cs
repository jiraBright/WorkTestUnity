using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIObjectHolder : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    [Header("Cooking side")]
    [SerializeField] private GameObject memoPanel;
    [SerializeField] private GameObject memoPrefab;
    [SerializeField] private SkeletonGraphic spinePot;
    [SerializeField] private TMP_Text cookingTimeText;
    [SerializeField] private Slider energybar;
    [SerializeField] private TMP_Text energyAmountText;
    [SerializeField] private Button startButton;

    [Header("Menu side")]
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private Button filterButton;
    [SerializeField] private GameObject menuBoard;
    [SerializeField] private GameObject posterPrefab;
    [SerializeField] private Button nextMenuButton;
    [SerializeField] private Button previousMenuButton;
    [SerializeField] private PageBorder pageHolder;
    [SerializeField] private GameObject pageprefab;

    private string currentFoodID;
    private FoodData selectedFood => GameManager.Instance.GetFoodByID(currentFoodID);
    private int pageSize = 4;
    private int currentPage;
    private int pageCount;
    private List<string> foodDatasID;
    private List<string> filteredFoodIDs;

    private void Awake()
    {
        currentPage = 0;
        currentFoodID = "";

        closeButton.onClick.AddListener(ClosePanel);
        nextMenuButton.onClick.AddListener(NextPage);
        previousMenuButton.onClick.AddListener(PreviousPage);

        searchInput.onValueChanged.AddListener(OnSearchValueChanged);
        filterButton.onClick.AddListener(OpenFilterOption);

        startButton.onClick.AddListener(() => GameManager.Instance.cookingTable.CookFood(selectedFood));
    }
    void Update()
    {
        if (currentFoodID == "")
        {
            startButton.interactable = false;
        }
        if (!GameManager.Instance.HasIngredients(selectedFood.IngredientsRequired))
        {
            startButton.interactable = false;
        }

        if (GameManager.Instance.cookingFoods.ContainsKey(selectedFood))
        {
            float remainingTime = GameManager.Instance.cookingFoods[selectedFood] - Time.realtimeSinceStartup;
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            if (remainingTime > 0)
            {
                cookingTimeText.SetText(string.Format("{0}:{1:00}", minutes, seconds));
                startButton.interactable = false;
                return;
            }
        }else
        {
            cookingTimeText.SetText("0:00");
        }
        
        startButton.interactable = true;
    }
    
    public void Initialize()
    {
        foodDatasID = new List<string>();
        for (int i = 0; i < GameManager.Instance.foodDatas.Count; i++)
        {
            if (GameManager.Instance.foodDatas[i].ID != "")
            {
                foodDatasID.Add(GameManager.Instance.foodDatas[i].ID);
            }
        }
        pageCount = (int)Math.Ceiling((double)foodDatasID.Count / pageSize);
        SpawnPage();
        filteredFoodIDs = foodDatasID;
        SetupPosterDetail();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    #region MenuPage Handler
    private void SpawnPage()
    {
        if (pageHolder.transform.childCount == pageCount)
        {
            return;
        }
        for (int i = 0; i < pageCount; i++)
        {
            Instantiate(pageprefab, pageHolder.transform);
            pageHolder.UpdatePageSprite(currentPage);
        }
    }
    private void NextPage()
    {
        if (currentPage < pageCount - 1)
        {
            currentPage++;
            pageHolder.UpdatePageSprite(currentPage);
            SetupPosterDetail();
        }
    }
    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            pageHolder.UpdatePageSprite(currentPage);
            SetupPosterDetail();
        }
    }
    private void ClearPage()
    {
        foreach (Transform child in menuBoard.transform)
        {
            Destroy(child.gameObject);
        }
        ClearMemo();
    }

    private void SetupPosterDetail()
    {
        ClearPage();
        if (filteredFoodIDs == null || filteredFoodIDs.Count == 0)
        {
            return;
        }

        var posterCollection = filteredFoodIDs.Skip(currentPage * pageSize).Take(pageSize);
        foreach (var poster in posterCollection)
        {
            SpawnPosterToBoard(GameManager.Instance.GetFoodByID(poster));
        }
    }
    private void SpawnPosterToBoard(FoodData food)
    {
        GameObject foodPoster = Instantiate(posterPrefab, menuBoard.transform);
        foodPoster.GetComponent<MenuPoster>().InitializeMenuPoster(food.FoodName, food.Quality, food.FoodSprite);
        foodPoster.GetComponent<Button>().onClick.AddListener(() => ShowIngredient(food.ID));
    }
    #endregion

    #region Search & Filter Handler
    public void OnSearchValueChanged(string keyword)
    {
        if (keyword.Length < 3)
        {
            filteredFoodIDs = new List<string>(foodDatasID);
        }
        else
        {
            keyword = keyword.ToLower();
            filteredFoodIDs = foodDatasID.Where(id =>
            {
                var food = GameManager.Instance.GetFoodByID(id);
                return food.FoodName.ToLower().Contains(keyword);
            }).ToList();
        }

        ResetPageHolder();
    }

    public void OnFilterByQuality(int quality)
    {
        filteredFoodIDs = foodDatasID.Where(id =>
        {
            var food = GameManager.Instance.GetFoodByID(id);
            return food.Quality == quality;
        }).ToList();

        ResetPageHolder();
    }
    private void OpenFilterOption()
    {
        filterButton.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ResetPageHolder()
    {
        currentPage = 0;
        pageCount = (int)Math.Ceiling((double)filteredFoodIDs.Count / pageSize);
        if (pageHolder.transform.childCount != pageCount)
        {
            foreach (Transform child in pageHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        SpawnPage();

        SetupPosterDetail();
    }
    #endregion

    #region Ingredient Memo

    private void ShowIngredient(string foodID)
    {
        if (currentFoodID == foodID)
        {
            return;
        }
        ClearMemo();
        currentFoodID = foodID;
        List<IngredientRequirement> reqDatas = selectedFood.IngredientsRequired;
        for (int i = 0; i < reqDatas.Count; i++)
        {
            SpawnMemoToBoard(reqDatas[i].amount, reqDatas[i].ingredient);
        }
    }
    private void SpawnMemoToBoard(int reqAmount, IngredientData ingredient)
    {
        GameObject ingredientMemo = Instantiate(memoPrefab, memoPanel.transform);
        IngredientMemo memoDetail = ingredientMemo.GetComponent<IngredientMemo>();
        memoDetail.InitializeIngredientMemo(reqAmount, ingredient);
    }

    private void ClearMemo()
    {
        foreach (Transform child in memoPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion

}
