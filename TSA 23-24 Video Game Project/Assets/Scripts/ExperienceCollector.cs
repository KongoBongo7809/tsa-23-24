using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceCollector : MonoBehaviour
{
    public int experience = 0;

    [SerializeField] public Text experienceText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Experience"))
        {
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().PlayOneShot("Experience");
            experience++;
            experienceText.text = "" + experience;
        }
    }
}
