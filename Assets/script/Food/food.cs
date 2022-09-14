using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public BoxCollider2D areaGrid;
    void Start()
    {
        randomFoodPosition();
    }
    private void randomFoodPosition()
    {
        Bounds bounds = this.areaGrid.bounds;

        float x = Random.Range(bounds.min.x,bounds.max.x);
        float y = Random.Range(bounds.min.y,bounds.max.y);

        this.transform.position = new Vector3(x, y, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1")
        {
            randomFoodPosition();
        }
        else if (other.tag == "Player2")
        {
            randomFoodPosition();
        }




    }
}
