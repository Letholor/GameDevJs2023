using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> allLevels;
    public int currentLevel;
    public int levelMax;
    public GameObject characterTemplate;
    private Dictionary<int, Level> levelDict = new Dictionary<int, Level>();
    public Transform spawnPlace;
    [SerializeField] private bool nextLevel;

    private void Awake()
    {
        if (currentLevel == null)
        {
            currentLevel = 0;
        }
    }

    private void Start()
    {
        foreach (Level level in allLevels)
        {
            levelDict.Add(level.levelNum, level);
        }

        if (levelDict.TryGetValue(currentLevel, out Level currentLevelObj))
        {
            int i = 0;

            foreach (CharacterBasics chars in currentLevelObj.levelClientele)
            {
                var character = Instantiate(characterTemplate, spawnPlace.position, spawnPlace.rotation, spawnPlace);
                character.transform.localScale = new Vector3(10,10,10);
                var charScript = character.GetComponent<Character>();
                var charImg = charScript.sprite;
                charImg.sprite = chars.looks;

                PotionType[] characterDesires = new PotionType[chars.desiredPotionTypes.Length];
                Array.Copy(chars.desiredPotionTypes, characterDesires, chars.desiredPotionTypes.Length);
                charScript.desiredPotionTypes = characterDesires;

                SpawnManager.instance.characterPool[i] = character;
                i++;
            }
        }

        SpawnManager.instance.startSpawning = true;
    }

    private void Update()
    {
        if (nextLevel = true)
        {
            currentLevel++;
            nextLevel = false;
        }
    }
}
