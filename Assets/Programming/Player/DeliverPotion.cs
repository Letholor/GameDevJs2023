using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PotionType
{
    None,
    Health,
    Mana,
    Strength,
    Speed
}

[System.Serializable]
public struct PotionTypeToSprite
{
    public PotionType potionType;
    public Sprite sprite;
}

public class DeliverPotion : Singleton<DeliverPotion>
{
    public List<PotionTypeToSprite> potionTypeToSprites = new List<PotionTypeToSprite>();
    public List<PotionType> readyPotions;
}
