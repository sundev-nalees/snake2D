using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class snakeControl : MonoBehaviour
{
    Vector2 direction=Vector2.right;
    Rigidbody2D rb;
    public gameOver gameOver;
    private List<Transform> segments;

    public Transform segmentPrefab;

    public int initialSize = 4;
    bool canDie = true;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        initialSegment();
    }

    private void Update()
    {

        playerControl();


    }
    private void FixedUpdate()
    {
        segmentOrder();
        movement();
        DeathCheck();
    }
    void playerControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction!=Vector2.down)
            {
                direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector2.up)
            {
                direction = Vector2.down;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector2.right)
            {
                direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Vector2.left)
            {
                direction = Vector2.right;
            }
        }

    }
    void movement()
    {
        transform.position = new Vector3(Mathf.Round( transform.position.x) + direction.x,Mathf.Round( transform.position.y )+ direction.y, 0f);
        transform.eulerAngles = new Vector3(0, 0, GetAngelFromVector(direction) - 90);
    }
    void segmentOrder()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
            
        }
    }
    void initialSegment()
    {
        for(int i = 1; i < this.initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }
    }
    private float GetAngelFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        Vector3 segmentPos = segments[segments.Count - 1].position;
        segmentPos.z = -1;
        segment.position = segmentPos;
        segments.Add(segment);

        //segments.Add(segment);
        //segments.Add(segment);
        //segments.Add(segment);
    }
    private void Shrink()
    {
        if (segments.Count<=initialSize)
        {
            gameOver.playerDead();
        }
        else if(segments.Count>initialSize)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            Vector3 segmentPos = segments[segments.Count - 1].position;
            segmentPos.z = -1;
            segment.position = segmentPos;
            segments.Remove(segment);
            

        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            Score.scoreAmount += 1;
        }
        else if (other.tag == "obstacle")
        {
            gameOver.playerDead();
        }
        else if (other.tag == "Poision")
        {
            Shrink();
            Score.scoreAmount -= 1;
        }
        
       
    }
    void DeathCheck()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Vector3 position = transform.position;
            if (position.x == segments[i].position.x && position.y == segments[i].position.y && position.z == segments[i].position.z)
            {
                if (canDie)
                {
                    gameOver.playerDead();
                }
            }
        }
    }
}
