using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite defaultSprite, pressedSprite;
    [SerializeField] private AudioClip compressedClip, uncompressedClip, moveOverButtonClip;
    [SerializeField] private AudioSource source;
    [SerializeField] private bool changeScene;
    [SerializeField] private bool changeToMainMenu = true;
    [SerializeField] private bool changeToCreditsMenu = false;
    [SerializeField] private bool changeToOptionsMenu = false;
    [SerializeField] private bool changeToLoseMenu = false;
    [SerializeField] private bool changeToLevelSelectMenu = false;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject mainMenuStuff;
    [SerializeField] private GameObject creditsMenuStuff;
    [SerializeField] private GameObject optionsMenuStuff;
    [SerializeField] private GameObject loseMenuStuff;
    [SerializeField] private GameObject levelSelectStuff;


    public void OnPointerDown(PointerEventData eventData)
    {
        //img.sprite = pressedSprite;
        source.PlayOneShot(compressedClip);
        if (changeScene)
        {
            PlayerPrefs.SetInt("currLevel", 1);
            SceneManager.LoadScene(sceneName: nextScene);
        }
        if (changeToCreditsMenu)
        {
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(true);
            optionsMenuStuff.SetActive(false);
            loseMenuStuff.SetActive(false);
            levelSelectStuff.SetActive(false);
        }
        if (changeToMainMenu)
        {
            mainMenuStuff.SetActive(true);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(false);
            loseMenuStuff.SetActive(false);
            levelSelectStuff.SetActive(false);
        }
        if (changeToOptionsMenu)
        {
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(true);
            loseMenuStuff.SetActive(false);
            levelSelectStuff.SetActive(false);
        }
        if (changeToLoseMenu) 
        {
            loseMenuStuff.SetActive(true);
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(false);
            levelSelectStuff.SetActive(false);
        }
        if (changeToLevelSelectMenu) 
        {
            loseMenuStuff.SetActive(false);
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(false);
            levelSelectStuff.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = defaultSprite;
        source.PlayOneShot(uncompressedClip);
    }

    private void OnMouseEnter()
    {
        img.sprite = pressedSprite;
        source.PlayOneShot(moveOverButtonClip);
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked" + this.name);
    }

}
