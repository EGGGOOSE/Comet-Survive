using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;

    public static int score = 0;

// Update is called once per frame
private void Update()
{
        if (GameManager.Instance.ball.transform.position.y > score)
        {
            score = (int)GameManager.Instance.ball.transform.position.y;
            text.text = " Score: " + score;
        }
    }
}
