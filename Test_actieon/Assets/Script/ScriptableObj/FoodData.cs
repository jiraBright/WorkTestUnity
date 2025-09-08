using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IngredientRequirement
{
    public IngredientData ingredient;
    public int amount;
}

[CreateAssetMenu(fileName = "FoodData", menuName = "Data/Food", order = 0)]
public class FoodData : ScriptableObject
{
    public string ID;
    public string Name;
    public List<IngredientRequirement> IngredientsRequired;
    public int quality;
    public int EnergyUse = 10;
    public float CookingTime = 5f;

    public Sprite FoodSprite;
}