
using System;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

    private GameObject currFloatingCombo;
    public GameObject particles;

    public GameObject ComboBar;
    private Image ComboBarImage;

    private int combo = 0;

    public int Combo
    {
        get
        {
            return combo;
        }
        set
        {
            if (value > 0)
            {
                isCombo = true;
                if (value > 1)
                {
                    ComboBar.SetActive(true);
                    if (currFloatingCombo != null)
                        Destroy(currFloatingCombo);

                    currFloatingCombo = Instantiate(GameManager.Instance.floatingCombo, new Vector2(transform.position.x + col.radius/4f, transform.position.y + col.radius/4f), Quaternion.identity);

                    TextMesh text = currFloatingCombo.transform.GetChild(0).GetComponent<TextMesh>();
                    text.color = new Color(1f, 1.2f - (value / 10f), 0f);
                    if (text.color.g == 0)
                        text.color = new Color(1f, 0f, value / 20f);

                    text.fontSize = (int) (text.fontSize + value*3f);
                    text.text = "COMBO X" + value;
                }
            }
            else
            {
                //проиграть анимацию исчезновения нада
                ComboBar.SetActive(false);
                Destroy(currFloatingCombo);
                isCombo = false;
            }

            comboTimeLeft = 0f;
            combo = value;
        }
    }

    public float comboTimer;
    public float comboTimeLeft = 0f;
    private bool isCombo = false;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
        ComboBarImage = ComboBar.GetComponent<Image>();
        ComboBar.SetActive(false);

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

        if (isCombo)
        {
            if (Combo > 1)
                currFloatingCombo.transform.position = new Vector2(transform.position.x + col.radius/4f, transform.position.y + col.radius/4f);

            comboTimeLeft += Time.deltaTime;
            
            ComboBarImage.fillAmount = (comboTimer - comboTimeLeft) / comboTimer;
            if (comboTimeLeft > comboTimer)
            {
                //ComboBarImage.fillAmount = Mathf.Lerp(ComboBarImage.fillAmount, 1, 2 * Time.deltaTime);
                Combo = 0;                
            }


        }

    }

    private void FixedUpdate()
    {
        float rotation = rb.velocity.magnitude * -Time.deltaTime * 60;
        if (rb.velocity.x < 0)
            rotation *= -1;

        rb.MoveRotation(rb.rotation + rotation);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnergyBall")
        {
            int moneyInscrease = (int) (1 * (1 + Score.score/100f));

            Combo++;

            GameManager.Instance.Energy += 0.25f;
            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.pushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            Destroy(curParticles, 1f);


            if (Combo > 1)
            {
                moneyInscrease *= Combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(GameManager.Instance.floatingPoints, collider.transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);
        }

        if (collider.tag == "SuperEnergyBall")
        {
            int moneyInscrease = (int)(2 * (1 + Score.score / 100f));

            Combo++;

            GameManager.Instance.Energy += 0.5f;
            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.maxPushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            curParticles.GetComponent<ParticleSystem>().startColor = new Color(1f, 207f/255f, 91f/255f);
            Destroy(curParticles, 1f);


            if (Combo > 1)
            {
                moneyInscrease *= Combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(GameManager.Instance.floatingPoints, collider.transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);
        }

        if (collider.tag == "MoneyBall")
        {
            int moneyInscrease = (int)(10 * (1 + Score.score / 100f));

            Combo++;

            Destroy(collider.gameObject);
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            Push(new Vector2(0, GameManager.Instance.pushForce));

            GameObject curParticles = Instantiate(particles, collider.transform.position, Quaternion.identity);
            curParticles.GetComponent<ParticleSystem>().startColor = new Color(1f, 198f/255f, 66f/255f);
            Destroy(curParticles, 1f);

            if (Combo > 1)
            {
                moneyInscrease *= Combo;
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
            curParticles.GetComponent<ParticleSystem>().startColor = new Color(1f, 51f/255f, 68f/255f);
            Destroy(curParticles, 1f);
        }


    }
    
   
}
