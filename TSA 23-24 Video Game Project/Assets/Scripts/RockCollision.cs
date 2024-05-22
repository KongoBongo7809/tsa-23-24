using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    public GameObject rock;

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Rock Hit");
            collider.gameObject.GetComponent<PlayerCombat>().TakeDamage(25);
        }

        Destroy(rock);
    }
}
