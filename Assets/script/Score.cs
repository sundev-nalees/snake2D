using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int scoreAmount;
    private TextMesh scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMesh>();
        scoreAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + scoreAmount;
    }
}
