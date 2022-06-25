using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Blackhole : MonoBehaviour
{
    public float speed;
    [HideInInspector] public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(GameManager.Instance.ball.transform.position.x, transform.position.y + speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 25) * - Time.deltaTime);

        if (GameManager.Instance.ball.transform.position.y - GameManager.Instance.ball.GetComponent<SpriteRenderer>().bounds.size.y / 2f < transform.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
