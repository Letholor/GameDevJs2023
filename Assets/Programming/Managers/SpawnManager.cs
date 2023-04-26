using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct CharacterBasics
{
    public PotionType[] desiredPotionTypes;
    public Sprite looks;
}

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] characterPool = new GameObject[9];
    public Transform[] spawnPoints;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;

    bool gameStart;

    private List<int> availableSlots = new List<int>() { 0, 1, 2, 3, 4 };

    public bool startSpawning;

    protected override void Awake()
    {
        base.Awake();
        // Assign a default value to characterPool if it hasn't been assigned already
        if (characterPool == null || characterPool.Length == 0)
        {
            characterPool = new GameObject[9];
        }
    }

    public IEnumerator SpawnCharacters()
    {
        gameStart = true;
        // Loop infinitely
        while (true)
        {
            // Wait for a random interval before spawning a character
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            // Only spawn a character if there's an empty slot available
            if (availableSlots.Count > 0)
            {
                // Choose a random character from the pool
                int characterIndex = Random.Range(0, characterPool.Length);

                // Choose a random available slot
                int slotIndex = Random.Range(0, availableSlots.Count);
                int spawnPointIndex = availableSlots[slotIndex];

                // Move the character to the chosen slot if it's not already slotted
                GameObject character;
                Character characterScript;
                lock (characterPool)
                {
                    if (characterIndex < characterPool.Length && characterPool[characterIndex] != null)
                    {
                        character = characterPool[characterIndex];
                        characterScript = character.GetComponent<Character>();
                        characterScript.clock.StartTimer();
                        if (characterScript.slotIndex == 0)
                        {
                            characterPool[characterIndex] = null;
                            availableSlots.RemoveAt(slotIndex);
                            character.transform.position = spawnPoints[spawnPointIndex].position;
                            characterScript.slotIndex = spawnPointIndex;
                            if (characterIndex == characterPool.Length - 1)
                            {
                                // Resize the array if it's not full
                                int newLength = characterPool.Length;
                                for (int i = characterPool.Length - 1; i >= 0; i--)
                                {
                                    if (characterPool[i] == null)
                                    {
                                        newLength--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (newLength < characterPool.Length)
                                {
                                    System.Array.Resize(ref characterPool, newLength);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    private void Update()
    {
        /*if (startSpawning)
        {
            startSpawning = false;
            StartCoroutine(SpawnCharacters());
        }*/

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Character");
        if (objectsWithTag.Length == 0 & gameStart)
        {
            gameStart = false;
            EndGameManager.instance.EndLevel();
        }
    }

    public void RemoveCharacter(int slotIndex)
    {
        // Add the slot index back to the list of available slots
        availableSlots.Add(slotIndex);
    }
}