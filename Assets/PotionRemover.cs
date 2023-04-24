using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRemover : MonoBehaviour
{
    public GameObject x;
    public PotionHolder pottyHol;

    private void OnMouseEnter()
    {
        if (!pottyHol.gotNothin)
        {
            x.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        x.SetActive(false);
    }

    private void OnMouseDown()
    {
        x.SetActive(false);
        pottyHol.DisarmPotion();

        if (pottyHol.health)
        {
            DeliverPotion.instance.readyPotions.Remove(PotionType.Health);
        }
        else if (pottyHol.mana)
        {
            DeliverPotion.instance.readyPotions.Remove(PotionType.Mana);
        }
        else if (pottyHol.strength)
        {
            DeliverPotion.instance.readyPotions.Remove(PotionType.Strength);
        }
        else if (pottyHol.speed)
        {
            DeliverPotion.instance.readyPotions.Remove(PotionType.Speed);
        }
        else if (pottyHol.polymorph)
        {
            DeliverPotion.instance.readyPotions.Remove(PotionType.Polymorph);
        }
    }
}
