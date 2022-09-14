using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFood : MonoBehaviour
{
    public BoxCollider2D areaGrid;
    

    [SerializeField] float delayBtwPowerupSpawns;
    float timerPowerup;
    int currentPowerup;
    void Start()
    {
        randomFoodPosition();
    }
    private void randomFoodPosition()
    {
        Bounds bounds = this.areaGrid.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(x, y, 0f);
    }
    private void Update()
    {
        if (timerPowerup < Time.time)
        {
            
            randomFoodPosition();
            timerPowerup = Time.time + delayBtwPowerupSpawns;
        }
    }
    




    
}
