using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped;
    public int secondsBetweenStages = 10;
    public int restPeriod = 15;
    public BossStages bossStages;

    private void Awake()
    {
        LoopSequence();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void LoopStages()
    {
        StartCoroutine(LoopSequence());
    }

    IEnumerator LoopSequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(restPeriod);
            yield return new WaitForSeconds(secondsBetweenStages);
            bossStages.SpawnRocks(4);
            yield return new WaitForSeconds(secondsBetweenStages);
            bossStages.SpawnSpikes();
            yield return new WaitForSeconds(secondsBetweenStages);

        }
    }
}
