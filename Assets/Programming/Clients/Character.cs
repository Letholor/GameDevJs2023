using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Character : MonoBehaviour
{
    public int slotIndex;
    public int pay;
    public PotionType[] desiredPotionTypes;

    SpriteRenderer thoughtRenderer;
    public SpriteRenderer thoughtBubbleRenderer;

    public GameObject _potionGeneric;
    public GameObject shelf;

    public Timer clock;

    List<PotionType> FulfilledPotions = new List<PotionType>();

    public SpriteRenderer sprite;

    void Start()
    {
        UpdateThoughtBubbleSprite();
    }

    void OnMouseDown()
    {
        // Create a dictionary to keep track of how many potions of each type have been fulfilled so far
        Dictionary<PotionType, int> fulfilledCounts = new Dictionary<PotionType, int>();

        // Loop through each desired potion type and count how many have been fulfilled so far
        foreach (PotionType potType in desiredPotionTypes)
        {
            fulfilledCounts[potType] = FulfilledPotions.Count(potionType => potionType == potType);
        }

        // Create a list to store all the unfulfilled potion types
        List<PotionType> unFulfilledPotions = new List<PotionType>();

        // Loop through each desired potion type and check if it has been fully fulfilled
        foreach (PotionType potType in desiredPotionTypes)
        {
            int numFulfilled = fulfilledCounts[potType];
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);

            // If the number of fulfilled potions of this type is less than the number needed, add it to the list of unfulfilled potions
            if (numFulfilled < numNeeded)
            {
                unFulfilledPotions.Add(potType);
            }
        }

        // Loop through each unfulfilled potion type and remove as many potions as needed from the ready potions list
        foreach (PotionType potType in unFulfilledPotions)
        {
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);
            int numFulfilled = fulfilledCounts[potType];

            // Calculate how many potions of this type need to be removed
            int numPotionsToRemove = Math.Min(numNeeded - numFulfilled, DeliverPotion.Instance.readyPotions.Count(potionType => potionType == potType));

            // Remove the specified number of potions from the ready potions list, add them to the fulfilled potions list, and update the fulfilled counts
            for (int i = 0; i < numPotionsToRemove; i++)
            {
                clock.AddSeconds((clock.timeLimit - clock._time) / 3);

                switch (potType)
                {
                    case PotionType.Health:
                        if (PotionMaster.instance.potionHolder1script.health)
                        {
                            PotionMaster.instance.potionHolder1script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder2script.health)
                        {
                            PotionMaster.instance.potionHolder2script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder3script.health)
                        {
                            PotionMaster.instance.potionHolder3script.DisarmPotion();
                        }
                        break;

                    case PotionType.Mana:
                        if (PotionMaster.instance.potionHolder1script.mana)
                        {
                            PotionMaster.instance.potionHolder1script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder2script.mana)
                        {
                            PotionMaster.instance.potionHolder2script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder3script.mana)
                        {
                            PotionMaster.instance.potionHolder3script.DisarmPotion();
                        }
                        break;

                    case PotionType.Strength:
                        if (PotionMaster.instance.potionHolder1script.strength)
                        {
                            PotionMaster.instance.potionHolder1script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder2script.strength)
                        {
                            PotionMaster.instance.potionHolder2script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder3script.strength)
                        {
                            PotionMaster.instance.potionHolder3script.DisarmPotion();
                        }
                        break;

                    case PotionType.Speed:
                        if (PotionMaster.instance.potionHolder1script.speed)
                        {
                            PotionMaster.instance.potionHolder1script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder2script.speed)
                        {
                            PotionMaster.instance.potionHolder2script.DisarmPotion();
                        }
                        else if (PotionMaster.instance.potionHolder3script.speed)
                        {
                            PotionMaster.instance.potionHolder3script.DisarmPotion();
                        }
                        break;

                    case PotionType.Polymorph:
                        if (PotionMaster.instance.potionHolder1script.polymorph)
                        {
                            PotionMaster.instance.potionHolder1script.DisarmPotion();
                        }
                        if (PotionMaster.instance.potionHolder2script.polymorph)
                        {
                            PotionMaster.instance.potionHolder2script.DisarmPotion();
                        }
                        if (PotionMaster.instance.potionHolder3script.polymorph)
                        {
                            PotionMaster.instance.potionHolder3script.DisarmPotion();
                        }
                        break;
                }

                DeliverPotion.Instance.readyPotions.Remove(potType);
                FulfilledPotions.Add(potType);
                fulfilledCounts[potType]++;
            }
        }

        // Check if all desired potions have been fulfilled
        bool allPotionsFulfilled = true;
        foreach (PotionType potType in desiredPotionTypes)
        {
            int numFulfilled = fulfilledCounts[potType];
            int numNeeded = desiredPotionTypes.Count(potionType => potionType == potType);

            // If the number of fulfilled potions of this type is less than the number needed, set the allPotionsFulfilled flag to false and exit the loop
            if (numFulfilled < numNeeded)
            {
                allPotionsFulfilled = false;
                break;
            }
        }

        // If all desired potions have been fulfilled, add the specified score, remove the character, and destroy the game object
        if (allPotionsFulfilled)
        {
            ScoreManager.instance.AddScore(pay);
            SpawnManager.instance.RemoveCharacter(slotIndex);
            Destroy(gameObject);
        }
    }

    private void UpdateThoughtBubbleSprite()
    {
        foreach (PotionType potType in desiredPotionTypes)
        {
            //change base pay amount
            pay += 6;

            //change thought bubble size
            Vector2 vec = thoughtBubbleRenderer.size;
            vec.y += 8;
            thoughtBubbleRenderer.size = vec;

            GameObject potionDesire = Instantiate(_potionGeneric);
            potionDesire.transform.parent = shelf.transform;
            potionDesire.transform.localScale = new Vector3 (5f, 5f, 5f);

            foreach (PotionTypeToSprite potionTypeToSprite in DeliverPotion.Instance.potionTypeToSprites)
            {
                if (potionTypeToSprite.potionType == potType)
                {
                    thoughtRenderer = potionDesire.GetComponent<SpriteRenderer>();
                    thoughtRenderer.sprite = potionTypeToSprite.sprite;
                    thoughtRenderer.gameObject.SetActive(true);
                }
            }

            potionDesire.transform.position = new Vector3 (0, 0 ,0);
        }
    }
}