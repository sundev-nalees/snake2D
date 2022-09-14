using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Gainer;
    [SerializeField] GameObject burner;
    [SerializeField] float nextSpawnDelay;
    BoxCollider2D bgCollider;
    float timerGainer;
    float timerBurner = 5f;


    //Powerup-Spawn
    [SerializeField] GameObject powerups;
    [SerializeField] float delayBtwPowerupSpawns;
    float timerPowerup;
    int currentPowerup;


    private void Start()
    {
        timerGainer = nextSpawnDelay;
        timerPowerup = delayBtwPowerupSpawns;
        bgCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        if (Time.time > timerGainer)
        {
            SpawnFood(Gainer);
            timerGainer = Time.time + nextSpawnDelay;
        }
        if (Time.time > timerBurner)
        {
            SpawnFood(burner);
            timerBurner = Time.time + Random.Range(5, 10);
        }


        if (timerPowerup < Time.time)
        {
            
            SpawnFood(powerups);
            timerPowerup = Time.time + delayBtwPowerupSpawns;
        }

    }
    public void SpawnFood(GameObject spawnItem)
    {
        float x = Random.Range(bgCollider.bounds.min.x, bgCollider.bounds.max.x);
        float y = Random.Range(bgCollider.bounds.min.y, bgCollider.bounds.max.y);
        GameObject spawnPos = Instantiate(spawnItem);
        spawnPos.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }
}
