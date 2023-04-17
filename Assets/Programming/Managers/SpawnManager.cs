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
    public GameObject[] characterPool;
    public Transform[] spawnPoints;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;

    private List<int> availableSlots = new List<int>() { 0, 1, 2, 3, 4 };

    public bool startSpawning;

    IEnumerator SpawnCharacters()
    {
        // Loop infinitely
        while (true)
        {
            // Wait for a random interval before spawning a character
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            // Only spawn a character if there's an empty slot available
            if (availableSlots.Count > 0 & characterPool.Length > 0)
            {
                // Choose a random character from the pool
                int characterIndex = Random.Range(0, characterPool.Length);

                // Choose a random available slot
                int slotIndex = Random.Range(0, availableSlots.Count);
                int spawnPointIndex = availableSlots[slotIndex];

                // Move the character to the chosen slot if it's not already slotted
                GameObject character = characterPool[characterIndex];
                Character characterScript = character.GetComponent<Character>();
                characterScript.clock.StartTimer();
                if (characterScript.slotIndex == 0)
                {
                    characterPool = characterPool.Where((val, idx) => idx != characterIndex).ToArray();
                    availableSlots.RemoveAt(slotIndex);
                    character.transform.position = spawnPoints[spawnPointIndex].position;
                    characterScript.slotIndex = spawnPointIndex;
                }
            }
        }
    }

    private void Update()
    {
        if (startSpawning)
        {
            startSpawning = false;
            StartCoroutine(SpawnCharacters());
        }
    }

    public void RemoveCharacter(int slotIndex)
    {
        // Add the slot index back to the list of available slots
        availableSlots.Add(slotIndex);
    }
}