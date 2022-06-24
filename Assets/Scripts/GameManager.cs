
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;
    public GameObject Chunk1;
    public GameObject Chunk2;
    public GameObject Chunk3;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;
	[SerializeField] float maxPushForce = 9f;

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
            if(value <= 1)
            {
                EnergyBar.SetValue(value);
                energy = value;
            }
        }
    }

    public void UpdateChunks()
    {
        Chunk1.transform.position = new Vector3(Chunk1.transform.position.x, Chunk1.transform.position.y + Chunk1.GetComponent<SpriteRenderer>().bounds.size.y * 3, Chunk1.transform.position.z);
        
        var temp = Chunk1;

        Chunk1 = Chunk2;
        Chunk2 = Chunk3;
        Chunk3 = temp;

        Chunk1.gameObject.name = "Chunk 1";
        Chunk2.gameObject.name = "Chunk 2";
        Chunk3.gameObject.name = "Chunk 3";
    }

    //---------------------------------------
    void Start ()
	{
		cam = Camera.main;
        ball.ActivateRb();
        Energy = 1;
        cam.GetComponent<Transform>();
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
	}

	//-Drag--------------------------------------
	void OnDragStart ()
	{
		//ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint (Input.mousePosition);

		trajectory.Show ();
	}

	void OnDrag ()
	{
		endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

        if (force.magnitude > maxPushForce)
        {
            force.Normalize();
            force *= maxPushForce;
        }
            
		//just for debug
		Debug.DrawLine (startPoint, endPoint);


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
    }

    

}
