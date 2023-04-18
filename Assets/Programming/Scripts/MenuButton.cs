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
    [SerializeField] private AudioClip compressedClip, uncompressedClip;
    [SerializeField] private AudioSource source;
    [SerializeField] private bool changeScene;
    [SerializeField] private bool changeToMainMenu;
    [SerializeField] private bool changeToCreditsMenu;
    [SerializeField] private bool changeToOptionsMenu;
    [SerializeField] private bool changeToLoseMenu;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject mainMenuStuff;
    [SerializeField] private GameObject creditsMenuStuff;
    [SerializeField] private GameObject optionsMenuStuff;
    [SerializeField] private GameObject loseMenuStuff;


    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressedSprite;
        source.PlayOneShot(compressedClip);
        if (changeScene)
        {
            SceneManager.LoadScene(sceneName: nextScene);
        }
        if (changeToCreditsMenu)
        {
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(true);
            optionsMenuStuff.SetActive(false);
            loseMenuStuff.SetActive(false);
        }
        if (changeToMainMenu)
        {
            mainMenuStuff.SetActive(true);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(false);
            loseMenuStuff.SetActive(false);
        }
        if (changeToOptionsMenu)
        {
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(true);
            loseMenuStuff.SetActive(false);
        }
        if (changeToLoseMenu) 
        {
            loseMenuStuff.SetActive(true);
            mainMenuStuff.SetActive(false);
            creditsMenuStuff.SetActive(false);
            optionsMenuStuff.SetActive(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = defaultSprite;
        source.PlayOneShot(uncompressedClip);
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked" + this.name);
    }

}
