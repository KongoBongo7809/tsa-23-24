using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped;
    public BossStages bossStages;

    private void Awake()
    {
        LoopStages();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
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
            yield return new WaitForSeconds(5);
            bossStages.SpawnRocks(5);
        }
    }
}