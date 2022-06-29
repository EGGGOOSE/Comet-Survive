
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;

    public GameObject Chunk1;
    public GameObject Chunk2;
    public GameObject Chunk3;

    public GameObject LeftChunk1;
    public GameObject LeftChunk2;
    public GameObject LeftChunk3;

    public GameObject RightChunk1;
    public GameObject RightChunk2;
    public GameObject RightChunk3;

    public GameObject floatingPoints;
    public GameObject floatingCombo;

    public List<GameObject> ObjectsForSpawnInChunks;

    void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	public Camera cam;

    public CinemachineVirtualCamera cinemachineVirtualCamera;

    

    public Ball ball;
	public Trajectory trajectory;
	public float pushForce = 4f;
	public float maxPushForce = 9f;
    public float slowMotionEffect;


	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	public Vector2 force;
	float distance;

    private float energy;

    public float Energy
    {
        get
        {
            return energy;
        }
        set
        {
            if(value > 1)
            {
                energy = 1;
            }
            else if (value < 0)
            {
                energy = 0;
            }
            else
            {
                energy = value;
            }

        }
    }

    public void UpdateChunksUp()
    {
        Chunk1.GetComponent<Chunk>().DeleteObjects();

        Chunk1.transform.position = new Vector3(Chunk1.transform.position.x, Chunk1.transform.position.y + Chunk1.GetComponent<SpriteRenderer>().bounds.size.y * 3, Chunk1.transform.position.z);

        var temp = Chunk1;

        Chunk1 = Chunk2;
        Chunk2 = Chunk3;
        Chunk3 = temp;

        Chunk1.gameObject.name = "Chunk1";
        Chunk2.gameObject.name = "Chunk2";
        Chunk3.gameObject.name = "Chunk3";

        Chunk3.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        LeftChunk1.GetComponent<Chunk>().DeleteObjects();

        LeftChunk1.transform.position = new Vector3(LeftChunk1.transform.position.x, LeftChunk1.transform.position.y + LeftChunk1.GetComponent<SpriteRenderer>().bounds.size.y * 3, LeftChunk1.transform.position.z);

        temp = LeftChunk1;

        LeftChunk1 = LeftChunk2;
        LeftChunk2 = LeftChunk3;
        LeftChunk3 = temp;

        LeftChunk1.gameObject.name = "LeftChunk1";
        LeftChunk2.gameObject.name = "LeftChunk2";
        LeftChunk3.gameObject.name = "LeftChunk3";

        LeftChunk3.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        RightChunk1.GetComponent<Chunk>().DeleteObjects();

        RightChunk1.transform.position = new Vector3(RightChunk1.transform.position.x, RightChunk1.transform.position.y + RightChunk1.GetComponent<SpriteRenderer>().bounds.size.y * 3, RightChunk1.transform.position.z);

        temp = RightChunk1;

        RightChunk1 = RightChunk2;
        RightChunk2 = RightChunk3;
        RightChunk3 = temp;

        RightChunk1.gameObject.name = "RightChunk1";
        RightChunk2.gameObject.name = "RightChunk2";
        RightChunk3.gameObject.name = "RightChunk3";

        RightChunk3.GetComponent<Chunk>().SpawnObjects();

    }

    public void UpdateChunksDown()
    {
        Chunk3.GetComponent<Chunk>().DeleteObjects();

        Chunk3.transform.position = new Vector3(Chunk3.transform.position.x, Chunk3.transform.position.y - Chunk3.GetComponent<SpriteRenderer>().bounds.size.y * 3, Chunk3.transform.position.z);

        var temp = Chunk3;

        Chunk3 = Chunk2;
        Chunk2 = Chunk1;
        Chunk1 = temp;

        Chunk1.gameObject.name = "Chunk1";
        Chunk2.gameObject.name = "Chunk2";
        Chunk3.gameObject.name = "Chunk3";

        Chunk1.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        LeftChunk3.GetComponent<Chunk>().DeleteObjects();

        LeftChunk3.transform.position = new Vector3(LeftChunk3.transform.position.x, LeftChunk3.transform.position.y - LeftChunk3.GetComponent<SpriteRenderer>().bounds.size.y * 3, LeftChunk3.transform.position.z);

        temp = LeftChunk3;

        LeftChunk3 = LeftChunk2;
        LeftChunk2 = LeftChunk1;
        LeftChunk1 = temp;

        LeftChunk1.gameObject.name = "LeftChunk1";
        LeftChunk2.gameObject.name = "LeftChunk2";
        LeftChunk3.gameObject.name = "LeftChunk3";

        LeftChunk1.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        RightChunk3.GetComponent<Chunk>().DeleteObjects();

        RightChunk3.transform.position = new Vector3(RightChunk3.transform.position.x, RightChunk3.transform.position.y - RightChunk3.GetComponent<SpriteRenderer>().bounds.size.y * 3, RightChunk3.transform.position.z);

        temp = RightChunk3;

        RightChunk3 = RightChunk2;
        RightChunk2 = RightChunk1;
        RightChunk1 = temp;

        RightChunk1.gameObject.name = "RightChunk1";
        RightChunk2.gameObject.name = "RightChunk2";
        RightChunk3.gameObject.name = "RightChunk3";

        RightChunk1.GetComponent<Chunk>().SpawnObjects();

    }

    public void UpdateChunksLeft()
    {
        RightChunk2.GetComponent<Chunk>().DeleteObjects();

        RightChunk2.transform.position = new Vector3(RightChunk2.transform.position.x - RightChunk2.GetComponent<SpriteRenderer>().bounds.size.x * 3, RightChunk2.transform.position.y, RightChunk2.transform.position.z);

        var temp = RightChunk2;

        RightChunk2 = Chunk2;
        Chunk2 = LeftChunk2;
        LeftChunk2 = temp;

        RightChunk2.gameObject.name = "RightChunk2";
        Chunk2.gameObject.name = "Chunk2";
        LeftChunk2.gameObject.name = "LeftChunk2";

        LeftChunk2.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        RightChunk1.GetComponent<Chunk>().DeleteObjects();

        RightChunk1.transform.position = new Vector3(RightChunk1.transform.position.x - RightChunk1.GetComponent<SpriteRenderer>().bounds.size.x * 3, RightChunk1.transform.position.y, RightChunk1.transform.position.z);

        temp = RightChunk1;

        RightChunk1 = Chunk1;
        Chunk1 = LeftChunk1;
        LeftChunk1 = temp;

        RightChunk1.gameObject.name = "RightChunk1";
        Chunk1.gameObject.name = "Chunk1";
        LeftChunk1.gameObject.name = "LeftChunk1";

        LeftChunk1.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        RightChunk3.GetComponent<Chunk>().DeleteObjects();

        RightChunk3.transform.position = new Vector3(RightChunk3.transform.position.x - RightChunk3.GetComponent<SpriteRenderer>().bounds.size.x * 3, RightChunk3.transform.position.y, RightChunk3.transform.position.z);

        temp = RightChunk3;

        RightChunk3 = Chunk3;
        Chunk3 = LeftChunk3;
        LeftChunk3 = temp;

        RightChunk3.gameObject.name = "RightChunk3";
        Chunk3.gameObject.name = "Chunk3";
        LeftChunk3.gameObject.name = "LeftChunk3";

        LeftChunk3.GetComponent<Chunk>().SpawnObjects();

    }

    public void UpdateChunksRight()
    {
        LeftChunk2.GetComponent<Chunk>().DeleteObjects();

        LeftChunk2.transform.position = new Vector3(LeftChunk2.transform.position.x + LeftChunk2.GetComponent<SpriteRenderer>().bounds.size.x * 3, LeftChunk2.transform.position.y, LeftChunk2.transform.position.z);

        var temp = LeftChunk2;

        LeftChunk2 = Chunk2;
        Chunk2 = RightChunk2;
        RightChunk2 = temp;

        LeftChunk2.gameObject.name = "LeftChunk2";
        Chunk2.gameObject.name = "Chunk2";
        RightChunk2.gameObject.name = "RightChunk2";

        RightChunk2.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        LeftChunk1.GetComponent<Chunk>().DeleteObjects();

        LeftChunk1.transform.position = new Vector3(LeftChunk1.transform.position.x + LeftChunk1.GetComponent<SpriteRenderer>().bounds.size.x * 3, LeftChunk1.transform.position.y, LeftChunk1.transform.position.z);

        temp = LeftChunk1;

        LeftChunk1 = Chunk1;
        Chunk1 = RightChunk1;
        RightChunk1 = temp;

        LeftChunk1.gameObject.name = "RightChunk1";
        Chunk1.gameObject.name = "Chunk1";
        RightChunk1.gameObject.name = "RightChunk1";

        RightChunk1.GetComponent<Chunk>().SpawnObjects();

        //------------------------------------------------------>

        LeftChunk3.GetComponent<Chunk>().DeleteObjects();

        LeftChunk3.transform.position = new Vector3(LeftChunk3.transform.position.x + LeftChunk3.GetComponent<SpriteRenderer>().bounds.size.x * 3, LeftChunk3.transform.position.y, LeftChunk3.transform.position.z);

        temp = LeftChunk3;

        LeftChunk3 = Chunk3;
        Chunk3 = RightChunk3;
        RightChunk3 = temp;

        LeftChunk3.gameObject.name = "LeftChunk3";
        Chunk3.gameObject.name = "Chunk3";
        RightChunk3.gameObject.name = "RightChunk3";

        RightChunk3.GetComponent<Chunk>().SpawnObjects();
    }

   
    void Start ()
	{

        Application.targetFrameRate = 60;
        
        cam = Camera.main;
        
        ball.ActivateRb();
        Energy = 1;
        cam.GetComponent<Transform>();

        Chunk2.transform.position = new Vector3(Chunk1.transform.position.x, Chunk1.transform.position.y + Chunk1.GetComponent<SpriteRenderer>().bounds.size.y, Chunk1.transform.position.z);
        Chunk3.transform.position = new Vector3(Chunk1.transform.position.x, Chunk1.transform.position.y + Chunk1.GetComponent<SpriteRenderer>().bounds.size.y * 2, Chunk1.transform.position.z);
        LeftChunk1.transform.position = new Vector3(Chunk1.transform.position.x - Chunk1.GetComponent<SpriteRenderer>().bounds.size.x, Chunk1.transform.position.y, Chunk1.transform.position.z);
        RightChunk1.transform.position = new Vector3(Chunk1.transform.position.x + Chunk1.GetComponent<SpriteRenderer>().bounds.size.x, Chunk1.transform.position.y, Chunk1.transform.position.z);
    }

    void Update ()
	{

        if (Input.GetMouseButtonDown (0)) {
			isDragging = true;
			OnDragStart ();
		}
		if (Input.GetMouseButtonUp (0)) {
			isDragging = false;
			OnDragEnd ();
		}

		if (isDragging) {
			OnDrag ();
		}

        Energy -= Time.deltaTime * 0.07f / Time.timeScale;
    }

    //-Drag--------------------------------------
    void OnDragStart ()
	{
		//ball.DesactivateRb();
		startPoint = Input.mousePosition;
        cinemachineVirtualCamera.Follow = trajectory.dotsList[12];
		trajectory.Show();
        Time.timeScale = slowMotionEffect;
    }

	void OnDrag ()
	{
		endPoint = Input.mousePosition;
		distance = Vector2.Distance (startPoint, endPoint)/65f;
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

        if (force.magnitude > maxPushForce)
        {
            force.Normalize();
            force *= maxPushForce;
        }

		trajectory.UpdateDots (ball.pos, force);
	}

	void OnDragEnd ()
	{
		//push the ball
        if (Energy > 0.01f)
        {
            ball.DesactivateRb();
            ball.ActivateRb();
            ball.Push(force);
            Energy -= 0.2f;
        }
        trajectory.Hide();
        cinemachineVirtualCamera.Follow = ball.transform;
        Time.timeScale = 1f;
    }

    

}
