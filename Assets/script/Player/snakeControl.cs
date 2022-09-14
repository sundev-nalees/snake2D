using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class snakeControl : MonoBehaviour
{

    [SerializeField] float powerupTime;
    [SerializeField] TextMeshProUGUI powerupText;
    [SerializeField] float nextStepDelay;
    [SerializeField] GameObject body;
    [SerializeField] gameOver gameOver;
    [SerializeField] int initialSize = 4;

    Vector2 direction=Vector2.right;
    
    
    public Transform segmentPrefab;
   

    bool canDie = true;


    private int currentPowerup;
    private Rigidbody2D rb;
    private Score score;
    private float speed;
    private int point;
    private List<Transform> segments;
    private float timeDelay;
    private void Start()
    {
        
        segments = new List<Transform>();
        timeDelay = nextStepDelay;
        segments.Insert(0, transform);
        for (int i = 0; i < initialSize; i++)
        {
            Grow();
        }
        score = GetComponent<Score>();
        point = score.GetScoreForGainer();
    }

    private void Update()
    {

        playerControl();

        DeathCheck();
    }
    private void FixedUpdate()
    {
        
        movement();
        
    }
    public float GetSpeed()
    {
        return nextStepDelay;
    }

    public void SetSpeed(float speed)
    {
        nextStepDelay = speed;
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
        if (Time.time > timeDelay)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].transform.position;
            }
            transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
            timeDelay = Time.time + nextStepDelay;
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
        for (int i = 0; i < 2; i++)
        {
            int listLength = segments.Count;
            GameObject segment = Instantiate(body);
            Vector3 position = segments[listLength - 1].transform.position;
            position.z = 2;
            segment.transform.position = position;
            segments.Insert(segments.Count, segment.transform);
        }
    }
    private void Shrink()
    {
        if (segments.Count<=initialSize)
        {
            gameOver.playerDead();
        }
        else if(segments.Count>initialSize)
        {
            for (int i = 0; i < 2; i++)
            {
                Destroy(segments[segments.Count - 1].gameObject);
                segments.RemoveAt(segments.Count - 1);

            }
        }
    }
    public void SetCanDie(bool value)
    {
        canDie = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Grow();
            Score.instance.IncreaseScore();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "obstacle")
        {
            gameOver.playerDead();
        }
        else if (other.gameObject.tag == "Poision")
        {
            Shrink();
            Score.instance.DecreseScore();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "PowerUp")
        {
            currentPowerup = Random.Range(0, 3);
            if (currentPowerup == 0)
            {
                speed = GetSpeed();
                SetSpeed(speed * .5f);
                Invoke("SetNormalSpeed", powerupTime);
                powerupText.text = gameObject.tag + " Achived " + "SpeedBoost";
                Invoke("SetText", 2f);
                
            }
            else if(currentPowerup==1)
            {
                
                powerupText.text=gameObject.tag+" Achived "+"ScoreBoost";
                Invoke("SetText", 2f);
                score.SetScoreForGainer(point * 2);
                Invoke("SetScore", powerupTime);
            }
            else if (currentPowerup == 2)
            {
                
                powerupText.text = gameObject.tag + " Achived " + "Shield";
                Invoke("SetText", 2f);
                SetCanDie(false);
                Invoke("SetCanDie", powerupTime);
            }
            Destroy(other.gameObject);
            
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

    void SetText()
    {
        powerupText.color = new Color(0f, 0f, 0f, 0f);
    }
    void SetCanDie()
    {
        SetCanDie(true);
    }
    void SetScore()
    {
        score.SetScoreForGainer(point);
    }
    
    void SetNormalSpeed()
    {
        SetSpeed(speed);
    }
}

