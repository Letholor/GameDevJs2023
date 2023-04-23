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

    public SpriteRenderer potionLiquid;
    Color startColor;

    private void Start()
    {
        startColor = potionLiquid.color;

        potionHolder1script = potionHolder1.GetComponent<PotionHolder>();
        potionHolder2script = potionHolder2.GetComponent<PotionHolder>();
        potionHolder3script = potionHolder3.GetComponent<PotionHolder>();
    }

    public GameObject ingredientHolder1, ingredientHolder2, ingredientHolder3, ingredientHolder4;
    public GameObject bloodIcon, herbIcon, mushroomIcon, oilIcon;
    bool icon1Ocupied, icon2Ocupied, icon3Ocupied, icon4Ocupied;
    GameObject icon1, icon2, icon3, icon4;

    public void AddIngredient(Ingredient ingredient)
    {
        if (currentIngredients.Count < 4)
        {
            if (!icon1Ocupied)
            {
                if(ingredient.name == "Blood")
                {
                    icon1 = Instantiate(bloodIcon, ingredientHolder1.transform);
                }
                else if(ingredient.name == "Herbs")
                {
                    icon1 = Instantiate(herbIcon, ingredientHolder1.transform);
                }
                else if(ingredient.name == "Mushroom")
                {
                    icon1 = Instantiate(mushroomIcon, ingredientHolder1.transform);
                }
                else if(ingredient.name == "Oils")
                {
                    icon1 = Instantiate(oilIcon, ingredientHolder1.transform);
                }

                icon1Ocupied = true;
            }
            else if (!icon2Ocupied)
            {
                if(ingredient.name == "Blood")
                {
                    icon2 = Instantiate(bloodIcon, ingredientHolder2.transform);
                }
                else if(ingredient.name == "Herbs")
                {
                    icon2 = Instantiate(herbIcon, ingredientHolder2.transform);
                }
                else if(ingredient.name == "Mushroom")
                {
                    icon2 = Instantiate(mushroomIcon, ingredientHolder2.transform);
                }
                else if(ingredient.name == "Oils")
                {
                    icon2 = Instantiate(oilIcon, ingredientHolder2.transform);
                }

                icon2Ocupied = true;
            }
            else if (!icon3Ocupied)
            {
                if(ingredient.name == "Blood")
                {
                    icon3 = Instantiate(bloodIcon, ingredientHolder3.transform);
                }
                else if(ingredient.name == "Herbs")
                {
                    icon3 = Instantiate(herbIcon, ingredientHolder3.transform);
                }
                else if(ingredient.name == "Mushroom")
                {
                    icon3 = Instantiate(mushroomIcon, ingredientHolder3.transform);
                }
                else if(ingredient.name == "Oils")
                {
                    icon3 = Instantiate(oilIcon, ingredientHolder3.transform);
                }

                icon3Ocupied = true;
            }
            else if (!icon4Ocupied)
            {
                if(ingredient.name == "Blood")
                {
                    icon4 = Instantiate(bloodIcon, ingredientHolder4.transform);
                }
                else if(ingredient.name == "Herbs")
                {
                    icon4 = Instantiate(herbIcon, ingredientHolder4.transform);
                }
                else if(ingredient.name == "Mushroom")
                {
                    icon4 = Instantiate(mushroomIcon, ingredientHolder4.transform);
                }
                else if(ingredient.name == "Oils")
                {
                    icon4 = Instantiate(oilIcon, ingredientHolder4.transform);
                }

                icon4Ocupied = true;
            }

            potionLiquid.color = (potionLiquid.color + ingredient.color) /2;

            currentIngredients.Add(ingredient);

            currentPotion = GetPotionFromIngredients(currentIngredients);
        }
    }

    private PotionType GetPotionFromIngredients(List<Ingredient> ingredients)
    {
        foreach (PotionTypeToSprite potion in DeliverPotion.instance.potionTypeToSprites)
        {
            if (potion.recipy.Count == ingredients.Count)
            {
                if (AreListsIdentical(potion.recipy, ingredients))
                {
                    return potion.potionType;
                }
            }
        }

        return PotionType.None;
    }

    public static bool AreListsIdentical(List<Ingredient> list1, List<Ingredient> list2)
    {
        list1.Sort((x, y) => string.Compare(x.ingredientName, y.ingredientName));
        list2.Sort((x, y) => string.Compare(x.ingredientName, y.ingredientName));

        if (list1.Count != list2.Count)
        {
            return false;
        }

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }

        return true;
    }

    public GameObject explosion;
    public GameObject potionHolder1, potionHolder2, potionHolder3;
    [HideInInspector] public PotionHolder potionHolder1script, potionHolder2script, potionHolder3script;

    public void MixPotion()
    {
        if (currentIngredients.Count != 0)
        {
            if (DeliverPotion.instance.readyPotions.Count < 3)
            {
                if (currentPotion != PotionType.None)
                {
                    if (potionHolder1script.gotNothin)
                    {
                        potionHolder1script.ArmPotion(currentPotion);
                    }
                    else if (potionHolder2script.gotNothin)
                    {
                        potionHolder2script.ArmPotion(currentPotion);
                    }
                    else if (potionHolder3script.gotNothin)
                    {
                        potionHolder3script.ArmPotion(currentPotion);
                    }

                    DeliverPotion.instance.readyPotions.Add(currentPotion);
                }
                else
                {
                    explosion.SetActive(true);
                }

                currentIngredients.Clear();

                if (icon1 != null)
                {
                    Destroy(icon1);
                    icon1Ocupied = false;
                }
                if (icon2 != null)
                {
                    Destroy(icon2);
                    icon2Ocupied = false;
                }
                if (icon3 != null)
                {
                    Destroy(icon3);
                    icon3Ocupied = false;
                }
                if (icon4 != null)
                {
                    Destroy(icon4);
                    icon4Ocupied = false;
                }

                potionLiquid.color = startColor;
                currentPotion = PotionType.None;
            }

            //make fail sound
        }
    }

    public void Dump()
    {
        if (icon1 != null)
        {
            Destroy(icon1);
        }
        if (icon2 != null)
        {
            Destroy(icon2);
        }
        if (icon3 != null)
        {
            Destroy(icon3);
        }
        if (icon4 != null)
        {
            Destroy(icon4);
        }

        currentIngredients.Clear();
        currentPotion = PotionType.None;
    }
}
