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

public class Character : MonoBehaviour
{
    public int slotIndex;
    public PotionType desiredPotionType;

    public List<PotionTypeToSprite> potionTypeToSprites = new List<PotionTypeToSprite>();

    public SpriteRenderer thoughtBubbleRenderer;

    void Start()
    {
        UpdateThoughtBubbleSprite();
    }

    void OnMouseDown()
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        spawnManager.RemoveCharacter(slotIndex);
        Destroy(gameObject);
    }

    public void SetDesiredPotionType(PotionType potionType)
    {
        // Set the desired potion type and update the thought bubble sprite
        desiredPotionType = potionType;
        UpdateThoughtBubbleSprite();
    }

    private void UpdateThoughtBubbleSprite()
    {
        // Find the corresponding sprite for the desired potion type
        foreach (PotionTypeToSprite potionTypeToSprite in potionTypeToSprites)
        {
            if (potionTypeToSprite.potionType == desiredPotionType)
            {
                thoughtBubbleRenderer.sprite = potionTypeToSprite.sprite;
                thoughtBubbleRenderer.gameObject.SetActive(true);
                return;
            }
        }

        // If no corresponding sprite is found, hide the thought bubble
        thoughtBubbleRenderer.gameObject.SetActive(false);
    }
}