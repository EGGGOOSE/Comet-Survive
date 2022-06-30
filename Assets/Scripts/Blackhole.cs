using UnityEngine;

public class Blackhole : MonoBehaviour{
    
    public float speed;
    public float acceleration;

    public SpriteRenderer spriteRenderer;

    void Update(){
        GameManager gameManager = GameManager.Instance;
        
        if(!gameManager.isDefeat){
            transform.position = new Vector2(gameManager.ball.transform.position.x, transform.position.y + speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, 35 * -Time.deltaTime));

            if(gameManager.ball.transform.position.y - gameManager.ball.GetComponent<SpriteRenderer>().bounds.size.y / 2f < transform.position.y){
                PlayerPrefs.SetInt("Money", Money.money + PlayerPrefs.GetInt("Money", 0));

                if(PlayerPrefs.GetInt("BestHeight") < Score.score){
                    PlayerPrefs.SetInt("BestHeight", Score.score);
                }

                gameManager.isDefeat = true;
                gameManager.musicSource.mute = true;
                gameManager.soundSource.PlayOneShot(gameManager.soundList[GameManager.SoundGameOver]);
                gameManager.ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                gameManager.gui.SetActive(false);
                gameManager.defeatWindow.SetActive(true);
                gameManager.height.text = "Height: " + Score.score;
                gameManager.bestHeight.text = "Best height: " + PlayerPrefs.GetInt("BestHeight", 0);
            }

            float h, s, v;
            Color.RGBToHSV(spriteRenderer.color, out h, out s, out v);
            spriteRenderer.color = Color.HSVToRGB(h + Time.deltaTime * .25f, s, v);

            speed += acceleration * Time.timeScale;
        }
    }
}
