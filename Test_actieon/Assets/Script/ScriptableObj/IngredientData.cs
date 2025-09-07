using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Data/Ingredient", order = 0)]
public class IngredientData : ScriptableObject
{
    public string ID;
    public string Name;
    
    public Sprite IngredientSprite;
}