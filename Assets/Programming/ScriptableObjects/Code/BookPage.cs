using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Page", menuName = "Book/Page")]
public class BookPage : ScriptableObject
{
    public string title;
    //public PotionType potion;
    public PotionTypeToSprite deets;
}
