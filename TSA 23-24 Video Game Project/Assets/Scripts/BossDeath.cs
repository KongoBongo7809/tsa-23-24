using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public EnemyCombat bossCombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossCombat.currentHealth <= 0)
        {
            FindObjectOfType<SceneManagement>().LoadNextLevel();
        }
    }
}
