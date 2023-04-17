using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite defaultSprite, pressedSprite;
    [SerializeField] private AudioClip compressedClip, uncompressedClip;
    [SerializeField] private AudioSource source;


    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressedSprite;
        source.PlayOneShot(compressedClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = defaultSprite;
        source.PlayOneShot(uncompressedClip);
    }

}
