using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectHolder : MonoBehaviour
{
    [Header("Cooking side")]
    public GameObject MemoPanel;
    public SkeletonGraphic SpinePot;
    public TMP_Text CookingTimeText;
    public Slider Energybar;
    public TMP_Text EnergyAmountText;
    public Button StartButton;

    [Header("Menu side")]
    public TMP_InputField SearchInput;
    public Button FilterButton;
    public GameObject MenuBoard;
    public Button NextMenuButton;
    public Button PreviousMenuButton;
    public GameObject PageHolder;
}
