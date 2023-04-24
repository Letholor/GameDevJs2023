using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolder : MonoBehaviour
{
    public GameObject health, mana, strength, speed, polymorph;
    public bool gotNothin = true;

    public void ArmPotion(PotionType pot)
    {
        switch (pot)
        {
            case PotionType.Health:
                health.SetActive(true);
                break;
            case PotionType.Mana:
                mana.SetActive(true);
                break;
            case PotionType.Strength:
                strength.SetActive(true);
                break;
            case PotionType.Speed:
                speed.SetActive(true);
                break;
            case PotionType.Polymorph:
                polymorph.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid potion type: " + pot);
                break;
        }

        gotNothin = false;
    }

    public void DisarmPotion()
    {
        health.SetActive(false);
        mana.SetActive(false);
        strength.SetActive(false);
        speed.SetActive(false);
        polymorph.SetActive(false);

        gotNothin = true;
    }
}
