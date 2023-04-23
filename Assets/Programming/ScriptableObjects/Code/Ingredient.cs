using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredients/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite sprite;
    public Color color;
}
