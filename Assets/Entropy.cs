using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entropy : MonoBehaviour
{
    public float delay = 5f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
