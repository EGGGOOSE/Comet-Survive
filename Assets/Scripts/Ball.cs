
using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

    public GameObject particles;

    public int combo = 0;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
	}

	public void Push(Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void ActivateRb ()
	{
		rb.isKinematic = false;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}

    public void Update()
    {
        float rotation = rb.velocity.magnitude * -Time.deltaTime * 60;
        if (rb.velocity.x < 0)
            rotation *= -1;

        transform.Rotate(new Vector3(0, 0, rotation));

        if (rb.velocity.y < 0)
        {
            combo = 0;
        }

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnergyBall")
        {
            int moneyInscrease = 1;

            combo++;

            GameManager.Instance.Energy += 0.25f;
            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.pushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            Destroy(curParticles, 1f);


            if (combo > 1)
            {
                moneyInscrease *= combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(GameManager.Instance.floatingPoints, collider.transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);
        }

        if (collider.tag == "SuperEnergyBall")
        {
            int moneyInscrease = 2;

            combo++;

            GameManager.Instance.Energy += 0.5f;
            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.maxPushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            Destroy(curParticles, 1f);


            if (combo > 1)
            {
                moneyInscrease *= combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(GameManager.Instance.floatingPoints, collider.transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);
        }

        if (collider.tag == "MoneyBall")
        {
            int moneyInscrease = 10;

            combo++;

            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.pushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            Destroy(curParticles, 1f);

            if (combo > 1)
            {
                moneyInscrease *= combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(GameManager.Instance.floatingPoints, collider.transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);


        }

        if (collider.tag == "NegativeBall")
        {

            GameManager.Instance.Energy -= 0.5f;
            Destroy(collider.gameObject);

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, -GameManager.Instance.maxPushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            Destroy(curParticles, 1f);
        }


    }
    
   
}
