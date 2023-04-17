using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheZAway : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
