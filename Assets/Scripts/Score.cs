using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour{
    
    public Text text;
    public static int score;

    private void Start(){
        score = 0;
    }

    private void Update(){
        if(GameManager.Instance.ball.transform.position.y > score){
            score = (int) GameManager.Instance.ball.transform.position.y;
        }
        
        text.text = " Height: " + score;
    }
}
