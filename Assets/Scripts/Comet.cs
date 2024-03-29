﻿ using UnityEngine;
using UnityEngine.UI;

public class Comet : MonoBehaviour{
    
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;
    
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    private GameObject currFloatingCombo;
    public GameObject particles;

    public GameObject ComboBar;
    private Image ComboBarImage;

    private int combo = 0;

    public int Combo{
        get{
            return combo;
        }
        set{
            if(value > 0){
                isCombo = true;
                if(value > 1){
                    ComboBar.SetActive(true);
                    if(currFloatingCombo != null){
                        Destroy(currFloatingCombo);
                    }

                    currFloatingCombo = Instantiate(GameManager.Instance.floatingCombo, new Vector2(transform.position.x + col.radius/4f, transform.position.y + col.radius/4f), Quaternion.identity);

                    TextMesh text = currFloatingCombo.transform.GetChild(0).GetComponent<TextMesh>();
                    text.color = new Color(1f, 1.2f - (value / 10f), 0f);
                    if (text.color.g == 0)
                        text.color = new Color(1f, 0f, value / 20f);

                    text.fontSize = (int) (text.fontSize + value*3f);
                    text.text = "COMBO X" + value;
                }
            } else {
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

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
        ComboBarImage = ComboBar.GetComponent<Image>();
        ComboBar.SetActive(false);

    }

    public void Push(Vector2 force){
        rb.AddForce(force, ForceMode2D.Impulse);
    }

	public void ActivateRb(){
		rb.isKinematic = false;
	}

	public void DesactivateRb(){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}

    public void Update(){
        if(isCombo){
            if(Combo > 1){
                currFloatingCombo.transform.position = new Vector2(transform.position.x + col.radius/4f, transform.position.y + col.radius/4f);
            }

            comboTimeLeft += Time.deltaTime;
            
            ComboBarImage.fillAmount = (comboTimer - comboTimeLeft) / comboTimer;
            if(comboTimeLeft > comboTimer){
                //ComboBarImage.fillAmount = Mathf.Lerp(ComboBarImage.fillAmount, 1, 2 * Time.deltaTime);
                Combo = 0;                
            }

        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isDefeat)
        {
            float rotation = rb.velocity.magnitude * -Time.deltaTime * 45;
            if (rb.velocity.x < 0)
            {
                rotation *= -1;
            }

            rb.MoveRotation(rb.rotation + rotation);
        }
    }
}
