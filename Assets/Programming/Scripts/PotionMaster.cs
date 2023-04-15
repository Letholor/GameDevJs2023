using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMaster : MonoBehaviour
{
    public Rigidbody2D potionRb;
    public BoxCollider2D potionCollider;
    [SerializeField] private Color colorOfPotion;
    [SerializeField] private int potionTier;
    [SerializeField] private bool mixablePotion;

    void Awake()
    {
        potionRb = GetComponent<Rigidbody2D>();
        potionCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (potionTier == 4) { mixablePotion = false; }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(mixablePotion)
        {
            Debug.Log("Combining " + this.name + " and " + collider.name);
        }
    }

}
