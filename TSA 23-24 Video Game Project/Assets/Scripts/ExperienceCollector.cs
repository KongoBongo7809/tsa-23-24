using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceCollector : MonoBehaviour
{
    private int experience = 0;

    [SerializeField] private Text experienceText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Experience"))
        {
            Destroy(collision.gameObject);
            experience++;
            experienceText.text = "" + experience;
        }
    }
}
