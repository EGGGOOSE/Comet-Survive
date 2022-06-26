using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    [HideInInspector] public List<GameObject> GameObjects;

   
    [SerializeField] private int numberOfObjects;


    [HideInInspector] private BoxCollider2D col;
    

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    private void Update()
    {

        if (col.bounds.Contains(GameManager.Instance.ball.transform.position))
        {
            if (gameObject.name[gameObject.name.Length-1] == '3')
            {
                GameManager.Instance.UpdateChunksUp();
            }
            if (gameObject.name[gameObject.name.Length - 1] == '1')
            {
                GameManager.Instance.UpdateChunksDown();
            }
            if (gameObject.name[0] == 'L')
            {
                GameManager.Instance.UpdateChunksLeft();
            }
            if (gameObject.name[0] == 'R')
            {
                GameManager.Instance.UpdateChunksRight();
            }

        }

    }

    public void SpawnObjects()
    {

        for (int i = 0; i < numberOfObjects; i++)
        {
            float screenX = Random.Range(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2f, transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2f);
            float screenY = Random.Range(transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f);
            Vector2 pos = new Vector2(screenX, screenY);

            int randomio = Random.RandomRange(1, 100);

            GameObject objForSpawn;

            if (randomio <= 10)
                objForSpawn = GameManager.Instance.ObjectsForSpawnInChunks[1];
            else if(randomio <= 20)
                objForSpawn = GameManager.Instance.ObjectsForSpawnInChunks[2];
            else
                objForSpawn = GameManager.Instance.ObjectsForSpawnInChunks[0];

            GameObjects.Add(Instantiate(objForSpawn, pos, Quaternion.identity));
        }
    }

    public void DeleteObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        { 
            Destroy(GameObjects[i]);
        }
        GameObjects = new List<GameObject>();
    }
}
