using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public Transform player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Death Barrier"))
        {
            player.GetComponent<PlayerCombat>().TakeDamage(200);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
        {
            FindObjectOfType<AudioManager>().Play("Chest");
            FindObjectOfType<SceneManagement>().LoadCutscene();
            FindObjectOfType<SceneManagement>().LoadNextLevel();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            FindObjectOfType<AudioManager>().Play("Rock Hit");
            player.GetComponent<PlayerCombat>().TakeDamage(25);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            FindObjectOfType<AudioManager>().Play("Chest");
            player.GetComponent<PlayerCombat>().TakeDamage(15);
        }
    }
}