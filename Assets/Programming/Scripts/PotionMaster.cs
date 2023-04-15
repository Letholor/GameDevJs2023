using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionMaster : Singleton<PotionMaster>
{
    public Button[] ingredientButtons;
    public Image resultImage;
    public Text resultText;

    private List<Ingredient> currentIngredients = new List<Ingredient>();
    private PotionType currentPotion;

    public void AddIngredient(Ingredient ingredient)
    {
        currentIngredients.Add(ingredient);

        currentPotion = GetPotionFromIngredients(currentIngredients);
    }

    private PotionType GetPotionFromIngredients(List<Ingredient> ingredients)
    {
        foreach (PotionTypeToSprite potion in DeliverPotion.instance.potionTypeToSprites)
        {
            if (potion.recipy.Count == ingredients.Count)
            {
                bool ingredientsMatch = true;
                foreach (Ingredient ingredient in ingredients)
                {
                    if (!potion.recipy.Contains(ingredient))
                    {
                        ingredientsMatch = false;
                        break;
                    }
                }
                if (ingredientsMatch)
                {
                    return potion.potionType;
                }
            }
        }
        return PotionType.None;
    }

    public void MixPotion()
    {
        if (currentIngredients.Count != 0)
        {
            if (currentPotion != PotionType.None)
            {
                DeliverPotion.instance.readyPotions.Add(currentPotion);
                currentIngredients.Clear();
            }
            else
            {
                //explode
                currentIngredients.Clear();
            }
            currentPotion = PotionType.None;
        }
    }

    public void Dump()
    {
        currentIngredients.Clear();
        currentPotion = PotionType.None;
    }
}
