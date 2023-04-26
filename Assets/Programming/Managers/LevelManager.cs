using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> allLevels;
    public int currentLevel;
    public int levelMax;
    public GameObject characterTemplate;
    [HideInInspector] public Dictionary<int, Level> levelDict = new Dictionary<int, Level>();
    public Transform spawnPlace;
    [SerializeField] private bool nextLevel;

    private void Start()
    {
        if (SpawnManager.instance == null)
        {
            Debug.LogError("SpawnManager instance is null!");
            return;
        }
        currentLevel = PlayerPrefs.GetInt("currLevel");
        if (currentLevel == 0)
        {
            currentLevel = 1;
        }

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

        SpawnManager.instance.StartCoroutine(SpawnManager.instance.SpawnCharacters());
    }
}
