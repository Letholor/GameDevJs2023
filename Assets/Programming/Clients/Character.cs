using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Character : MonoBehaviour
{
    public int slotIndex;
    public PotionType[] desiredPotionTypes;

    SpriteRenderer thoughtRenderer;
    public SpriteRenderer thoughtBubbleRenderer;

    public GameObject _potionGeneric;
    public GameObject shelf;

    List<PotionType> FulfilledPotions = new List<PotionType>();

    void Start()
    {
        UpdateThoughtBubbleSprite();
    }

    void OnMouseDown()
    {
        Dictionary<PotionType, int> fulfilledCounts = new Dictionary<PotionType, int>();
        foreach (PotionType potType in desiredPotionTypes)
        {
            fulfilledCounts[potType] = FulfilledPotions.Count(potionType => potionType == potType);
        }

        List<PotionType> unFulfilledPotions = new List<PotionType>();
        foreach (PotionType potType in desiredPotionTypes)
        {
            int numFulfilled = fulfilledCounts[potType];
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);
            if (numFulfilled < numNeeded)
            {
                unFulfilledPotions.Add(potType);
            }
        }

        foreach (PotionType potType in unFulfilledPotions)
        {
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);
            int numFulfilled = fulfilledCounts[potType];
            int numPotionsToRemove = Math.Min(numNeeded - numFulfilled, DeliverPotion.Instance.readyPotions.Count(potionType => potionType == potType));

            for (int i = 0; i < numPotionsToRemove; i++)
            {
                DeliverPotion.Instance.readyPotions.Remove(potType);
                FulfilledPotions.Add(potType);
                fulfilledCounts[potType]++;
            }
        }

        bool allPotionsFulfilled = true;
        foreach (PotionType potType in desiredPotionTypes)
        {
            int numFulfilled = fulfilledCounts[potType];
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);
            if (numFulfilled < numNeeded)
            {
                allPotionsFulfilled = false;
                break;
            }
        }

        if (allPotionsFulfilled)
        {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            spawnManager.RemoveCharacter(slotIndex);
            Destroy(gameObject);
        }
    }

    public void SetDesiredPotionType(PotionType[] potionType)
    {
        // Set the desired potion type and update the thought bubble sprite
        desiredPotionTypes = potionType;
        UpdateThoughtBubbleSprite();
    }

    private void UpdateThoughtBubbleSprite()
    {
        foreach (PotionType potType in desiredPotionTypes)
        {
            Vector2 vec = thoughtBubbleRenderer.size;
            vec.y += 8;
            thoughtBubbleRenderer.size = vec;

            GameObject potionDesire = Instantiate(_potionGeneric);
            potionDesire.transform.parent = shelf.transform;

            foreach (PotionTypeToSprite potionTypeToSprite in DeliverPotion.Instance.potionTypeToSprites)
            {
                if (potionTypeToSprite.potionType == potType)
                {
                    thoughtRenderer = potionDesire.GetComponent<SpriteRenderer>();
                    thoughtRenderer.sprite = potionTypeToSprite.sprite;
                    thoughtRenderer.gameObject.SetActive(true);
                }
            }
        }
    }
}