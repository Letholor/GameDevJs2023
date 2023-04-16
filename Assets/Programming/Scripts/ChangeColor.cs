using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Image image;

    public void ChangeToRed()
    {
        image.color = Color.red;
    }
    public void ChangeTGreen()
    {
        image.color = Color.green;
    }
}
