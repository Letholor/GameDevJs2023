using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EndGameManager : Singleton<EndGameManager>
{
    public GameObject star1, star2, star3, star4, star5;
    public GameObject starGold;
    public GameObject endLevelScreen;
    public RotatePointer clock;
    public AudioSource backgroundMusic, endingMusic;
    public int finalScore;
    bool readyForNextLevel, skip;

    public void EndLevel()
    {
        clock.rotate = false;
        endLevelScreen.SetActive(true);
        StartCoroutine(LevelEndSequence());
    }

    IEnumerator LevelEndSequence()
    {
        backgroundMusic.Stop();
        endingMusic.Play();
        yield return new WaitForSeconds(1f);
        clock.cahsIN = true;
        clock.speedUp = true;
        clock.rotate = true;
        yield return new WaitForSeconds(1f);
        int starWorth = LevelManager.instance.levelDict[LevelManager.instance.currentLevel].maxScore / 5;
        int numbOfStars = 0;

        finalScore = ScoreManager.instance.score;

        for (int i = 1; i <= 5; i++)
        {
            if (finalScore >= i * starWorth)
            {
                numbOfStars++;
                switch (numbOfStars)
                {
                    case 1:
                        Instantiate(starGold, star1.transform);
                        break;
                    case 2:
                        Instantiate(starGold, star2.transform);
                        break;
                    case 3:
                        Instantiate(starGold, star3.transform);
                        break;
                    case 4:
                        Instantiate(starGold, star4.transform);
                        break;
                    case 5:
                        Instantiate(starGold, star5.transform);
                        break;
                }
            }
        }

        //chime noise
        if (!skip)
        {
            yield return new WaitForSeconds(1f);
        }

        readyForNextLevel = true;
    }


    public void NextLevel()
    {
        if (readyForNextLevel)
        {
            int nextLevelNum = LevelManager.instance.currentLevel + 1;
            bool foundNextLevel = false;
            foreach (Level lev in LevelManager.instance.allLevels)
            {
                if (lev.levelNum == nextLevelNum)
                {
                    foundNextLevel = true;
                    break;
                }
            }
            if (foundNextLevel)
            {
                PlayerPrefs.SetInt("currLevel", nextLevelNum);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                PlayerPrefs.SetInt("gameOver", 1);
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            skip = true;
        }
    }

    public void ResetLevel()
    {
        if (readyForNextLevel)
        {
            PlayerPrefs.SetInt("currLevel", LevelManager.instance.currentLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            skip = true;
        }
    }
}
