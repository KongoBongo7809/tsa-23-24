using System;
using System.Collections;
using UnityEngine;

public class BossStages : MonoBehaviour {

    [Header("Stage One")]
    public GameObject rock;
    public Transform[] rockSpawnPoints;
    public int rockSpeed = 2;
    public int rockDamage = 25;

    [Header("Stage Two")]
    public GameObject spikes;
    public int spikeDamage = 15;
    public int spikeActiveTime = 3;

    [Header("Player")]
    public Transform player;
    public LayerMask playerLayer;

    public void SpawnRocks(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            int rand = UnityEngine.Random.Range(0, rockSpawnPoints.Length);
            Transform rockInstanceSpawn = rockSpawnPoints[rand];
            GameObject rockInstance = Instantiate(rock, rockSpawnPoints[rand].position, Quaternion.identity);
            rockInstance.GetComponent<Rigidbody2D>().velocity = rockInstanceSpawn.up * rockSpeed;
        }
    }

    public void SpawnSpikes()
    {
        spikes.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(SpikeTime());
        spikes.SetActive(false);
        //Check for player collision
        //If so, take damage
    }

    IEnumerator SpikeTime()
    {
        yield return new WaitForSeconds(spikeActiveTime);
    }

      /*
      ADD TO COLLISION MANAGER
      Destroy(collision.gameObject);
      Destroy(gameObject);
      ALSO ADD AUDIO
      */
  
}