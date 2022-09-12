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
    public GameObject body;

    public int initialSize = 4;
    bool canDie = true;
    Score score;

    private void Start()
    {
        segments = new List<Transform>();
        
        segments.Insert(0, transform);
        for (int i = 0; i < initialSize; i++)
        {
            Grow();
        }
    }

    private void Update()
    {

        playerControl();

        DeathCheck();
    }
    private void FixedUpdate()
    {
        segmentOrder();
        movement();
        
    }
    void playerControl()
    {
        if (gameObject.tag == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (direction != Vector2.down)
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
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (direction != Vector2.down)
                {
                    direction = Vector2.up;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (direction != Vector2.up)
                {
                    direction = Vector2.down;
                }

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (direction != Vector2.right)
                {
                    direction = Vector2.left;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (direction != Vector2.left)
                {
                    direction = Vector2.right;
                }
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
    
    private float GetAngelFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    }
    private void Grow()
    {
      
        int listLength = segments.Count;
        GameObject segment = Instantiate(body);
        Vector3 position = segments[listLength - 1].transform.position;
        position.z = 2;
        segment.transform.position = position;
        segments.Insert(segments.Count, segment.transform);
        
    }
    private void Shrink()
    {
        if (segments.Count<=initialSize)
        {
            gameOver.playerDead();
        }
        else if(segments.Count>initialSize)
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.RemoveAt(segments.Count - 1);
            

        }
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Grow();

            Score.instance.IncreaseScore();
        }
        else if (other.gameObject.tag == "obstacle")
        {
            gameOver.playerDead();
        }
        else if (other.gameObject.tag == "Poision")
        {
            Shrink();
            Score.instance.DecreseScore();
        }
        else if (other.gameObject.tag == "P2_Body" && this.gameObject.tag == "Player1")
        {
            gameOver.playerDead();
        }
        else if (other.gameObject.tag == "P1_Body" && this.gameObject.tag == "Player2")
        {
            gameOver.playerDead();
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
