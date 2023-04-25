using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMiniManager : MonoBehaviour
{
    public GameObject gameOverStuff;

    void Start()
    {
        if (PlayerPrefs.GetInt("gameOver") == 1)
        {
            PlayerPrefs.SetInt("gameOver", 0);
            gameOverStuff.SetActive(true);
        }
    }
}
