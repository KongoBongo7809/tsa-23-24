using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public HealthBar bossBar;
    public BossCombat bossCombat;

    // Start is called before the first frame update
    void Start()
    {
        bossBar.SetMaxHealth(bossCombat.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        bossBar.SetHealth(bossCombat.currentHealth);
    }
}
