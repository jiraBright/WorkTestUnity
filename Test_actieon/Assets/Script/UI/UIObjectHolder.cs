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
    public GameObject MemoPanel;
    public GameObject MemoPrefab;
    public SkeletonGraphic SpinePot;
    public TMP_Text CookingTimeText;
    public Slider Energybar;
    public TMP_Text EnergyAmountText;
    public Button StartButton;

    [Header("Menu side")]
    public TMP_InputField SearchInput;
    public Button FilterButton;
    public GameObject MenuBoard;
    public GameObject PosterPrefab;
    public Button NextMenuButton;
    public Button PreviousMenuButton;
    public PageBorder PageHolder;
    [SerializeField] private GameObject Pageprefab;

    private string currentFoodID;
    private int pageSize = 4;
    private int currentPage;
    private int pageCount;
    private List<string> foodDatasID;

    private void Awake()
    {
        currentPage = 0;
        closeButton.onClick.AddListener(ClosePanel);
        NextMenuButton.onClick.AddListener(NextPage);
        PreviousMenuButton.onClick.AddListener(PreviousPage);
    }
    public void Initialize(GameManager manager)
    {
        foodDatasID = new List<string>();
        for (int i = 0; i < manager.foodDatas.Count; i++)
        {
            if (manager.foodDatas[i].ID != "0")
            {
                foodDatasID.Add(manager.foodDatas[i].ID);
            }
        }
        pageCount = (int)Math.Ceiling((double)foodDatasID.Count / pageSize);
        SpawnPage();
        SetupPosterDetail();
    }
    
    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    private void SpawnPage()
    {
        if (PageHolder.transform.childCount == pageCount)
        {
            return;
        }
        for (int i = 0; i < pageCount; i++)
        {
            Instantiate(Pageprefab, PageHolder.transform);
            PageHolder.UpdatePageSprite(currentPage);
        }
    }
    private void NextPage()
    {
        if (currentPage < pageCount - 1)
        {
            currentPage++;
            PageHolder.UpdatePageSprite(currentPage);
            SetupPosterDetail();
        }  
    }
    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            PageHolder.UpdatePageSprite(currentPage);
            SetupPosterDetail();
        }
    }
    private void ClearPage()
    {
        for (int i = 0; i < MenuBoard.transform.childCount; i++)
        {
            Destroy(MenuBoard.transform.GetChild(i).gameObject);
        }
    }
    private void SetupPosterDetail()
    {
        if (foodDatasID.Count == 0)
        {
            return;
        }
        ClearPage();
        var posterCollection = foodDatasID.Skip(currentPage * pageSize).Take(pageSize);
        foreach (var poster in posterCollection)
        {
            SpawnPosterToBoard(GameManager.Instance.GetFoodByID(poster));
        }
    }
    private void SpawnPosterToBoard(FoodData food)
    {
        Instantiate(PosterPrefab, MenuBoard.transform)
            .GetComponent<MenuPoster>()
                .InitializeMenuPoster(food.FoodName, food.Quality, food.FoodSprite);
    }
}
